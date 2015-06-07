namespace Metacraft.FlightSimulation.WoaiDownloader
{
	internal class PackageInfo
	{
		public string Name { get; set; }
		public string Country { get; set; }
		public string AvsimUrl { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
