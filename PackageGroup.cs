namespace Metacraft.FlightSimulation.WoaiDownloader
{
	internal class PackageGroup
	{
		public string Anchor { get; }
		public string Name { get; }

		public PackageGroup(string anchor, string name)
		{
			Anchor = anchor;
			Name = name;
		}
	}
}
