using System;
using System.Net;

namespace RossCarlson.FlightSimulation.WoaiDownloader
{
	public class CookieAwareWebClient : WebClient
	{
		public CookieContainer CookieContainer { get; }

		public CookieAwareWebClient()
		{
			CookieContainer = new CookieContainer();
		}

		protected override WebRequest GetWebRequest(Uri address)
		{
			var request = base.GetWebRequest(address);
			if (request is HttpWebRequest) {
				(request as HttpWebRequest).CookieContainer = CookieContainer;
				(request as HttpWebRequest).AllowAutoRedirect = true;
			}

			return request;
		}
	}
}
