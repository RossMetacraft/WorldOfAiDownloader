namespace RossCarlson.FlightSimulation.WoaiDownloader
{
	internal class PackageInfo
	{
		public string Name { get; set; }
		public string Country { get; set; }
		public string AvsimUrlFs9 { get; set; }
		public string AvsimUrlFsx { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
