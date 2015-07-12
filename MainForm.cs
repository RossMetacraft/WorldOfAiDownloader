using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace Metacraft.FlightSimulation.WoaiDownloader
{
	public partial class MainForm : Form
	{
		private const string LOCAL_PACKAGE_LIST_FILE = "packages.html";
		private const string WORLD_OF_AI_PACKAGE_LIST_URL = "http://www.world-of-ai.com/allpackages.php";
		private const string AVSIM_LOGIN_URL = "http://library.avsim.net/dologin.php";
		private const string AVSIM_DOWNLOAD_URL_FORMAT = "http://library.avsim.net/sendfile.php?Location=AVSIM&Proto=ftp&DLID={0}";

		private List<PackageGroup> mPackageGroups = new List<PackageGroup>() {
			new PackageGroup("airlines", "Passenger Airlines"),
			new PackageGroup("cargo", "Cargo Airlines"),
			new PackageGroup("ga", "General Aviation"),
			new PackageGroup("military", "Military"),
			new PackageGroup("ceased", "Airlines No Longer Operating")
		};
		private Dictionary<string, Dictionary<string, List<PackageInfo>>> mPackages;
		private WebClient mPackageListClient;
		private CookieAwareWebClient mPackageDownloadClient;
		private bool mDownloadInProgress;
		private List<PackageInfo> mSelectedPackages;
		private int mCurrentPackageIndex;

		public MainForm()
		{
			InitializeComponent();
			mPackageListClient = new WebClient();
			mPackageListClient.DownloadProgressChanged += mPackageListClient_DownloadProgressChanged;
			mPackageListClient.DownloadStringCompleted += mPackageListClient_DownloadStringCompleted;
			mPackageDownloadClient = new CookieAwareWebClient();
			mPackageDownloadClient.UploadValuesCompleted += mPackageDownloadClient_UploadValuesCompleted;
			mPackageDownloadClient.DownloadStringCompleted += mPackageDownloadClient_DownloadStringCompleted;
			mPackageDownloadClient.DownloadFileCompleted += mPackageDownloadClient_DownloadFileCompleted;
			ddlSim.Items.Add("FS9");
			ddlSim.Items.Add("FSX");
			Config cfg = Config.Load();
			txtAvsimUsername.Text = cfg.AvsimUsername;
			txtAvsimPassword.Text = cfg.AvsimPassword;
			chkSavePassword.Checked = cfg.SavePassword;
			ddlSim.SelectedItem = cfg.Simulator;
			txtDownloadFolder.Text = cfg.DownloadFolder;
			SetControlStates();
		}

		private void SetControlStates()
		{
			if (mDownloadInProgress) {
				progCurrentFile.Style = ProgressBarStyle.Marquee;
				btnDownloadPackages.Text = "Cancel Download";
				btnDownloadPackages.Enabled = true;
				grpConfiguration.Enabled = false;
				treePackages.Enabled = false;
				btnRefreshPackageList.Enabled = false;
			} else {
				progCurrentFile.Style = ProgressBarStyle.Continuous;
				btnDownloadPackages.Text = "Download Selected Packages";
				btnDownloadPackages.Enabled =
					!string.IsNullOrEmpty(txtAvsimUsername.Text)
					&& !string.IsNullOrEmpty(txtAvsimPassword.Text)
					&& (GetCheckedNodes(treePackages.Nodes).Count > 0);
				grpConfiguration.Enabled = true;
				treePackages.Enabled = true;
				btnRefreshPackageList.Enabled = true;
			}
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			Application.DoEvents();
			FetchPackageList();
		}

		private void btnRefreshPackageList_Click(object sender, EventArgs e)
		{
			FetchPackageList();
		}

		private void FetchPackageList()
		{
			AddMessage("Fetching package list ...");
			if (File.Exists(LOCAL_PACKAGE_LIST_FILE)) {
				try {
					string html = File.ReadAllText(LOCAL_PACKAGE_LIST_FILE);
					AddMessage(" done." + Environment.NewLine);
					ParsePackageList(html);
					if (mPackages != null) PopulateTree();
				}
				catch (Exception ex) {
					MessageBox.Show(this, string.Format("Error loading local package file: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			} else {
				try {
					btnRefreshPackageList.Hide();
					progFetchPackageList.Value = 0;
					progFetchPackageList.Show();
					mPackageListClient.DownloadStringAsync(new Uri(WORLD_OF_AI_PACKAGE_LIST_URL));
				}
				catch (Exception ex) {
					MessageBox.Show(this, string.Format("Error loading package list from WoAI web site: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}
		}

		void mPackageListClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			progFetchPackageList.Value = e.ProgressPercentage;
		}

		void mPackageListClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
		{
			if (e.Cancelled) return;
			if (e.Error != null) {
				MessageBox.Show(this, string.Format("Error loading package list from WoAI web site: {0}{1}{2}{3}", e.Error.Message, Environment.NewLine, Environment.NewLine, e.Error.InnerException != null ? e.Error.InnerException.Message : ""), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				AddErrorMessage(" error." + Environment.NewLine);
				return;
			}
			AddMessage(" done." + Environment.NewLine);
			progFetchPackageList.Hide();
			btnRefreshPackageList.Show();
			ParsePackageList(e.Result);
			if (mPackages != null) PopulateTree();
		}

		private void ParsePackageList(string html)
		{
			AddMessage("Parsing package links ...");
			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			mPackages = new Dictionary<string, Dictionary<string, List<PackageInfo>>>();
			doc.LoadHtml(html);
			foreach (PackageGroup packageGroup in mPackageGroups) {
				mPackages.Add(packageGroup.Name, new Dictionary<string, List<PackageInfo>>());
				string query = string.Format("//a[@name='{0}']/following::table[1]", packageGroup.Anchor);
				HtmlNode table = doc.DocumentNode.SelectSingleNode(query);
				if (table == null) continue;
				foreach (HtmlNode row in table.SelectNodes("tr")) {
					HtmlNodeCollection cells = row.SelectNodes("td");
					if (cells.Count != 6) continue;
					PackageInfo pi = new PackageInfo() {
						Name = cells[1].InnerText.Trim(),
						Country = cells[2].InnerText.Trim()
					};
					if (string.IsNullOrEmpty(pi.Country)) pi.Country = "N/A";
					HtmlNodeCollection links = cells[5].SelectNodes("a");
					if (links.Count == 2) {
						pi.AvsimUrlFs9 = links[0].Attributes["href"].Value;
						pi.AvsimUrlFsx = pi.AvsimUrlFs9;
					} else {
						pi.AvsimUrlFs9 = links[0].Attributes["href"].Value;
						pi.AvsimUrlFsx = links[2].Attributes["href"].Value;
					}
					if (!mPackages[packageGroup.Name].ContainsKey(pi.Country)) mPackages[packageGroup.Name].Add(pi.Country, new List<PackageInfo>());
					mPackages[packageGroup.Name][pi.Country].Add(pi);
				}
			}
			AddMessage(" done." + Environment.NewLine);
		}

		private void PopulateTree()
		{
			treePackages.Nodes.Clear();
			foreach (PackageGroup packageGroup in mPackageGroups) {
				TreeNode groupNode = treePackages.Nodes.Add(packageGroup.Name);
				foreach (string country in mPackages[packageGroup.Name].Keys.OrderBy(x => x)) {
					TreeNode countryNode = groupNode.Nodes.Add(country);
					foreach (PackageInfo pi in mPackages[packageGroup.Name][country].OrderBy(x => x.Name)) {
						TreeNode packageNode = countryNode.Nodes.Add(pi.Name);
						packageNode.Tag = pi;
					}
				}
			}
		}

		private void treePackages_AfterExpand(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Parent != null) e.Node.Parent.Expand();
		}

		private void treePackages_AfterCheck(object sender, TreeViewEventArgs e)
		{
			treePackages.AfterCheck -= new TreeViewEventHandler(treePackages_AfterCheck);
			CheckAllNodes(e.Node, e.Node.Checked);
			CheckParentsWhereAllChildrenChecked(treePackages.Nodes);
			treePackages.AfterCheck += new TreeViewEventHandler(treePackages_AfterCheck);
			SetControlStates();
		}

		private List<TreeNode> GetCheckedNodes(TreeNodeCollection nodes)
		{
			List<TreeNode> checkedNodes = new List<TreeNode>();
			foreach (TreeNode node in nodes) {
				if (node.Checked) checkedNodes.Add(node);
				checkedNodes.AddRange(GetCheckedNodes(node.Nodes));
			}
			return checkedNodes;
		}

		private void CheckAllNodes(TreeNode node, bool isChecked)
		{
			foreach (TreeNode childNode in node.Nodes) {
				childNode.Checked = isChecked;
				CheckAllNodes(childNode, isChecked);
			}
		}

		private void CheckParentsWhereAllChildrenChecked(TreeNodeCollection nodes)
		{
			bool allChecked = true;
			foreach (TreeNode node in nodes) {
				CheckParentsWhereAllChildrenChecked(node.Nodes);
				if (!node.Checked) allChecked = false;
			}
			if ((nodes.Count > 0) && (nodes[0].Parent != null)) nodes[0].Parent.Checked = allChecked;
		}

		private void btnBrowseDownloadFolder_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.Description = "Specify the folder where the package files will be saved:";
			dlg.ShowNewFolderButton = true;
			dlg.SelectedPath = txtDownloadFolder.Text;
			DialogResult result = dlg.ShowDialog(this);
			if (result == DialogResult.OK) txtDownloadFolder.Text = dlg.SelectedPath;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveConfig();
			if (mPackageListClient != null) mPackageListClient.Dispose();
			if (mPackageDownloadClient != null) mPackageDownloadClient.Dispose();
		}

		private void Credentials_TextChanged(object sender, EventArgs e)
		{
			SetControlStates();
		}

		private void btnDownloadPackages_Click(object sender, EventArgs e)
		{
			if (mDownloadInProgress) {
				StopDownload();
			} else {
				StartDownload();
			}
		}

		private bool hasWriteAccessToFolder(string folderPath)
		{
			try {
				// Attempt to get a list of security permissions from the folder. 
				// This will raise an exception if the path is read only or do not have access to view the permissions. 
				System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(folderPath);
				return true;
			}
			catch (UnauthorizedAccessException) {
				return false;
			}
		}

		private void StartDownload()
		{
			if (!hasWriteAccessToFolder(txtDownloadFolder.Text)) {
				MessageBox.Show(this, "You do not appear to have write access to the specified download folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
				return;
			}
			mDownloadInProgress = true;
			SetControlStates();
			mSelectedPackages = GetCheckedNodes(treePackages.Nodes).Where(x => x.Tag != null).Select(x => x.Tag as PackageInfo).ToList();
			mCurrentPackageIndex = -1;
			progOverall.Value = 0;
			progOverall.Maximum = mSelectedPackages.Count;
			DoLogin();
		}

		private void StopDownload()
		{
			if (mPackageDownloadClient.IsBusy) mPackageDownloadClient.CancelAsync();
			mDownloadInProgress = false;
			SetControlStates();
			progOverall.Value = 0;
			AddErrorMessage("Download aborted." + Environment.NewLine);
		}

		private void DoLogin()
		{
			AddMessage("Logging into AVSIM ...");
			NameValueCollection form = new NameValueCollection();
			form.Add("UserLogin", txtAvsimUsername.Text);
			form.Add("Password", txtAvsimPassword.Text);
			mPackageDownloadClient.UploadValuesAsync(new Uri(AVSIM_LOGIN_URL), "POST", form);
		}

		void mPackageDownloadClient_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e)
		{
			if (e.Cancelled) return;
			if (e.Error != null) {
				AddErrorMessage(" error." + Environment.NewLine);
				StopDownload();
				MessageBox.Show(this, string.Format("Error logging into AVSIM: {0}{1}{2}{3}", e.Error.Message, Environment.NewLine, Environment.NewLine, e.Error.InnerException != null ? e.Error.InnerException.Message : ""), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			CookieCollection cookies = mPackageDownloadClient.CookieContainer.GetCookies(new Uri(AVSIM_LOGIN_URL));
			bool gotAuthCookie = false;
			foreach (Cookie cookie in cookies) {
				cookie.Path = "/";
				if (cookie.Name == "LibraryLogin") gotAuthCookie = true;
			}
			mPackageDownloadClient.CookieContainer.Add(cookies);
			if (!gotAuthCookie) {
				AddErrorMessage(" error." + Environment.NewLine);
				StopDownload();
				MessageBox.Show(this, "AVSIM login failed. Please check your username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			AddMessage(" done." + Environment.NewLine);
			DownloadNextPackage();
		}

		private void DownloadNextPackage()
		{
			mCurrentPackageIndex++;
			if (mCurrentPackageIndex >= mSelectedPackages.Count) {
				mDownloadInProgress = false;
				SetControlStates();
				MessageBox.Show(this, "Package download complete!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			AddMessage(string.Format("Fetching download link for {0} ...", mSelectedPackages[mCurrentPackageIndex].Name));
			string downloadUri = (ddlSim.SelectedText == "FSX") ? mSelectedPackages[mCurrentPackageIndex].AvsimUrlFsx : mSelectedPackages[mCurrentPackageIndex].AvsimUrlFs9;
			mPackageDownloadClient.DownloadStringAsync(new Uri(downloadUri));
		}

		void mPackageDownloadClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
		{
			if (e.Cancelled) return;
			if (e.Error != null) {
				AddErrorMessage(" error." + Environment.NewLine);
				StopDownload();
				MessageBox.Show(this, string.Format("Error fetching download link or FTP URL: {0}{1}{2}{3}", e.Error.Message, Environment.NewLine, Environment.NewLine, e.Error.InnerException != null ? e.Error.InnerException.Message : ""), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			string redirectUrl = mPackageDownloadClient.ResponseHeaders[HttpResponseHeader.Location];
			AddMessage(" done." + Environment.NewLine);
			if (string.IsNullOrEmpty(redirectUrl)) {
				HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
				doc.LoadHtml(e.Result);
				HtmlNode link = doc.DocumentNode.SelectSingleNode("//a[contains(@href,'download.php')]");
				if (link == null) {
					AddErrorMessage("Could not find download link in HTML returned from AVSIM." + Environment.NewLine);
					DownloadNextPackage();
					return;
				}
				Match match = Regex.Match(link.Attributes["href"].Value, @"DLID=(\d+)", RegexOptions.IgnoreCase);
				if (!match.Success) {
					AddErrorMessage("Could not find download link in HTML returned from AVSIM." + Environment.NewLine);
					DownloadNextPackage();
					return;
				}
				string downloadUrl = string.Format(AVSIM_DOWNLOAD_URL_FORMAT, match.Groups[1].Value);
				AddMessage("Fetching FTP URL ...");
				mPackageDownloadClient.DownloadStringAsync(new Uri(downloadUrl));
			} else {
				AddMessage("Downloading file ...");
				Uri ftpUri = new Uri(redirectUrl);
				string filename = ftpUri.Segments.Last();
                if (!File.Exists(Path.Combine(txtDownloadFolder.Text, filename)))
                {
                    mPackageDownloadClient.DownloadFileAsync(ftpUri, Path.Combine(txtDownloadFolder.Text, filename));
                }
                else
                {
                    //Skip over files that already exist.
                    AddMessage(" skipped. File already exists." + Environment.NewLine);
                    progOverall.Value = mCurrentPackageIndex + 1;
                    DownloadNextPackage();
                }
			}
		}

		void mPackageDownloadClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			if (e.Cancelled) return;
            if (e.Error != null)
            {
                AddErrorMessage(" error." + Environment.NewLine);
                //StopDownload(); //Don't stop the download, just ignore this file
                AddErrorMessage(
                    string.Format(
                    "Error downloading package: {0}{1}{2}{3}",
                    e.Error.Message,
                    Environment.NewLine,
                    e.Error.InnerException != null ? e.Error.InnerException.Message : "",
                    Environment.NewLine));
            }
            else
            {
                AddMessage(" done." + Environment.NewLine);
            }
			progOverall.Value = mCurrentPackageIndex + 1;
			DownloadNextPackage();
		}

		private void AddMessage(string message)
		{
			AddMessage(message, Color.Black);
		}

		private void AddErrorMessage(string error)
		{
			AddMessage(error, Color.Red);
		}

		private void AddMessage(string message, Color color)
		{
			if (rtfMessages.IsDisposed || rtfMessages.Disposing) return;
			if (string.IsNullOrEmpty(message)) return;
			bool scrolledToBottom = rtfMessages.IsAtMaxScroll();
			rtfMessages.SelectionStart = rtfMessages.TextLength;
			rtfMessages.SelectionColor = color;
			rtfMessages.SelectedText = message;
			rtfMessages.SelectionStart = rtfMessages.TextLength;
            //if (scrolledToBottom) rtfMessages.ScrollToCaret();
            rtfMessages.ScrollToCaret(); //Bit more clunky but line above wasn't working. scrolledToBottom was always false
		}

		private void SaveConfig()
		{
			Config cfg = new Config() {
				AvsimUsername = txtAvsimUsername.Text,
				AvsimPassword = chkSavePassword.Checked ? txtAvsimPassword.Text : string.Empty,
				SavePassword = chkSavePassword.Checked,
				Simulator = ddlSim.SelectedItem.ToString(),
				DownloadFolder = txtDownloadFolder.Text
			};
			cfg.Save();
		}
	}
}
