using System.Web;

namespace HappyIndexService.Services {
	public class Ping : IService {
		public string ServiceName {
			get {
				return "ping";
			}
		}
		public string Description {
			get {
				return "A service for testing connectivity";
			}
		}
		public object GetData( HttpRequest request ) {
			return "pong";
		}
	}
}