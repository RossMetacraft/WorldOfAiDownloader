namespace Metacraft.FlightSimulation.WoaiDownloader
{
	internal class PackageGroup
	{
		public string Anchor { get; private set; }
		public string Name { get; private set; }

		public PackageGroup(string anchor, string name)
		{
			Anchor = anchor;
			Name = name;
		}
	}
}
