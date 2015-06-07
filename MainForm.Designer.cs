namespace Metacraft.FlightSimulation.WoaiDownloader
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.btnRefreshPackageList = new System.Windows.Forms.Button();
			this.treePackages = new System.Windows.Forms.TreeView();
			this.txtAvsimUsername = new System.Windows.Forms.TextBox();
			this.grpConfiguration = new System.Windows.Forms.GroupBox();
			this.btnBrowseDownloadFolder = new System.Windows.Forms.Button();
			this.txtDownloadFolder = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtAvsimPassword = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.progFetchPackageList = new System.Windows.Forms.ProgressBar();
			this.label4 = new System.Windows.Forms.Label();
			this.btnDownloadPackages = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.progOverall = new System.Windows.Forms.ProgressBar();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.progCurrentFile = new System.Windows.Forms.ProgressBar();
			this.rtfMessages = new Metacraft.FlightSimulation.WoaiDownloader.EnhancedRichTextBox();
			this.ddlSim = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.grpConfiguration.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnRefreshPackageList
			// 
			this.btnRefreshPackageList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnRefreshPackageList.Location = new System.Drawing.Point(12, 556);
			this.btnRefreshPackageList.Name = "btnRefreshPackageList";
			this.btnRefreshPackageList.Size = new System.Drawing.Size(261, 23);
			this.btnRefreshPackageList.TabIndex = 3;
			this.btnRefreshPackageList.Text = "Refresh Package List";
			this.btnRefreshPackageList.UseVisualStyleBackColor = true;
			this.btnRefreshPackageList.Click += new System.EventHandler(this.btnRefreshPackageList_Click);
			// 
			// treePackages
			// 
			this.treePackages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.treePackages.BackColor = System.Drawing.Color.White;
			this.treePackages.CheckBoxes = true;
			this.treePackages.Location = new System.Drawing.Point(12, 101);
			this.treePackages.Name = "treePackages";
			this.treePackages.Size = new System.Drawing.Size(262, 449);
			this.treePackages.TabIndex = 2;
			this.treePackages.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treePackages_AfterCheck);
			this.treePackages.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treePackages_AfterExpand);
			// 
			// txtAvsimUsername
			// 
			this.txtAvsimUsername.Location = new System.Drawing.Point(9, 32);
			this.txtAvsimUsername.Name = "txtAvsimUsername";
			this.txtAvsimUsername.Size = new System.Drawing.Size(102, 20);
			this.txtAvsimUsername.TabIndex = 1;
			this.txtAvsimUsername.TextChanged += new System.EventHandler(this.Credentials_TextChanged);
			// 
			// grpConfiguration
			// 
			this.grpConfiguration.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpConfiguration.Controls.Add(this.label8);
			this.grpConfiguration.Controls.Add(this.ddlSim);
			this.grpConfiguration.Controls.Add(this.btnBrowseDownloadFolder);
			this.grpConfiguration.Controls.Add(this.txtDownloadFolder);
			this.grpConfiguration.Controls.Add(this.label3);
			this.grpConfiguration.Controls.Add(this.txtAvsimPassword);
			this.grpConfiguration.Controls.Add(this.label2);
			this.grpConfiguration.Controls.Add(this.label1);
			this.grpConfiguration.Controls.Add(this.txtAvsimUsername);
			this.grpConfiguration.Location = new System.Drawing.Point(12, 12);
			this.grpConfiguration.Name = "grpConfiguration";
			this.grpConfiguration.Size = new System.Drawing.Size(859, 63);
			this.grpConfiguration.TabIndex = 0;
			this.grpConfiguration.TabStop = false;
			this.grpConfiguration.Text = "Configuration";
			// 
			// btnBrowseDownloadFolder
			// 
			this.btnBrowseDownloadFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseDownloadFolder.Location = new System.Drawing.Point(793, 32);
			this.btnBrowseDownloadFolder.Name = "btnBrowseDownloadFolder";
			this.btnBrowseDownloadFolder.Size = new System.Drawing.Size(55, 20);
			this.btnBrowseDownloadFolder.TabIndex = 8;
			this.btnBrowseDownloadFolder.Text = "Browse";
			this.btnBrowseDownloadFolder.UseVisualStyleBackColor = true;
			this.btnBrowseDownloadFolder.Click += new System.EventHandler(this.btnBrowseDownloadFolder_Click);
			// 
			// txtDownloadFolder
			// 
			this.txtDownloadFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDownloadFolder.Location = new System.Drawing.Point(279, 32);
			this.txtDownloadFolder.Name = "txtDownloadFolder";
			this.txtDownloadFolder.ReadOnly = true;
			this.txtDownloadFolder.Size = new System.Drawing.Size(508, 20);
			this.txtDownloadFolder.TabIndex = 7;
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(279, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(508, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Download Folder:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtAvsimPassword
			// 
			this.txtAvsimPassword.Location = new System.Drawing.Point(117, 32);
			this.txtAvsimPassword.Name = "txtAvsimPassword";
			this.txtAvsimPassword.PasswordChar = '*';
			this.txtAvsimPassword.Size = new System.Drawing.Size(102, 20);
			this.txtAvsimPassword.TabIndex = 3;
			this.txtAvsimPassword.TextChanged += new System.EventHandler(this.Credentials_TextChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(117, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(102, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "AVSIM Password:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(9, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(102, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "AVSIM Username:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// progFetchPackageList
			// 
			this.progFetchPackageList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.progFetchPackageList.Location = new System.Drawing.Point(12, 556);
			this.progFetchPackageList.MarqueeAnimationSpeed = 10;
			this.progFetchPackageList.Name = "progFetchPackageList";
			this.progFetchPackageList.Size = new System.Drawing.Size(261, 23);
			this.progFetchPackageList.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progFetchPackageList.TabIndex = 4;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 80);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(261, 18);
			this.label4.TabIndex = 1;
			this.label4.Text = "Available Packages:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnDownloadPackages
			// 
			this.btnDownloadPackages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDownloadPackages.Location = new System.Drawing.Point(279, 556);
			this.btnDownloadPackages.Name = "btnDownloadPackages";
			this.btnDownloadPackages.Size = new System.Drawing.Size(592, 23);
			this.btnDownloadPackages.TabIndex = 10;
			this.btnDownloadPackages.Text = "Download Selected Packages";
			this.btnDownloadPackages.UseVisualStyleBackColor = true;
			this.btnDownloadPackages.Click += new System.EventHandler(this.btnDownloadPackages_Click);
			// 
			// label5
			// 
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label5.Location = new System.Drawing.Point(279, 80);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(591, 18);
			this.label5.TabIndex = 4;
			this.label5.Text = "Messages:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// progOverall
			// 
			this.progOverall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progOverall.Location = new System.Drawing.Point(351, 527);
			this.progOverall.Name = "progOverall";
			this.progOverall.Size = new System.Drawing.Size(519, 23);
			this.progOverall.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progOverall.TabIndex = 9;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label6.Location = new System.Drawing.Point(280, 527);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(65, 23);
			this.label6.TabIndex = 8;
			this.label6.Text = "Overall:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label7.Location = new System.Drawing.Point(280, 498);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(65, 23);
			this.label7.TabIndex = 6;
			this.label7.Text = "Current File:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// progCurrentFile
			// 
			this.progCurrentFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progCurrentFile.Location = new System.Drawing.Point(351, 498);
			this.progCurrentFile.MarqueeAnimationSpeed = 10;
			this.progCurrentFile.Name = "progCurrentFile";
			this.progCurrentFile.Size = new System.Drawing.Size(519, 23);
			this.progCurrentFile.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progCurrentFile.TabIndex = 7;
			// 
			// rtfMessages
			// 
			this.rtfMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtfMessages.Location = new System.Drawing.Point(279, 101);
			this.rtfMessages.Name = "rtfMessages";
			this.rtfMessages.Size = new System.Drawing.Size(591, 391);
			this.rtfMessages.TabIndex = 5;
			this.rtfMessages.Text = "";
			// 
			// ddlSim
			// 
			this.ddlSim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlSim.FormattingEnabled = true;
			this.ddlSim.Location = new System.Drawing.Point(225, 31);
			this.ddlSim.Name = "ddlSim";
			this.ddlSim.Size = new System.Drawing.Size(48, 21);
			this.ddlSim.TabIndex = 5;
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(225, 16);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(48, 13);
			this.label8.TabIndex = 4;
			this.label8.Text = "Sim:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(884, 591);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.progCurrentFile);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.progOverall);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.btnDownloadPackages);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.grpConfiguration);
			this.Controls.Add(this.treePackages);
			this.Controls.Add(this.btnRefreshPackageList);
			this.Controls.Add(this.progFetchPackageList);
			this.Controls.Add(this.rtfMessages);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(900, 630);
			this.Name = "MainForm";
			this.Text = "World of AI Package Downloader";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.Shown += new System.EventHandler(this.MainForm_Shown);
			this.grpConfiguration.ResumeLayout(false);
			this.grpConfiguration.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnRefreshPackageList;
		private System.Windows.Forms.TreeView treePackages;
		private System.Windows.Forms.TextBox txtAvsimUsername;
		private System.Windows.Forms.GroupBox grpConfiguration;
		private System.Windows.Forms.TextBox txtAvsimPassword;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnBrowseDownloadFolder;
		private System.Windows.Forms.TextBox txtDownloadFolder;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ProgressBar progFetchPackageList;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnDownloadPackages;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ProgressBar progOverall;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ProgressBar progCurrentFile;
		private EnhancedRichTextBox rtfMessages;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox ddlSim;
	}
}

