using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Metacraft.FlightSimulation.WoaiDownloader
{
	[Serializable]
	public class Config
	{
		public string AvsimUsername { get; set; }
		public string AvsimPassword { get; set; }
		public bool SavePassword { get; set; }
		public string Simulator { get; set; }
		public string DownloadFolder { get; set; }

		public Config()
		{
			AvsimUsername = string.Empty;
			AvsimPassword = string.Empty;
			SavePassword = false;
			Simulator = "FSX";
			DownloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		}

		public void Save()
		{
			try {
				string path = GetConfigPath();
				FileInfo fi = new FileInfo(path);
				if (!fi.Directory.Exists) {
					fi.Directory.Create();
				}
				XmlSerializer serializer = new XmlSerializer(typeof(Config));
				using (StreamWriter sw = new StreamWriter(path)) {
					serializer.Serialize(sw, this);
				}
			}
			catch { }
		}

		public static Config Load()
		{
			string path = GetConfigPath();
			if (!File.Exists(path)) {
				return new Config();
			}

			try {
				XmlSerializer serializer = new XmlSerializer(typeof(Config));
				using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
					XmlTextReader reader = new XmlTextReader(fs);
					reader.Normalization = false;
					return (Config)serializer.Deserialize(reader);
				}
			}
			catch {
				return new Config();
			}
		}

		private static string GetConfigPath()
		{
			return Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
				"WoaiDownloader",
				"Config.xml"
			);
		}
	}
}
