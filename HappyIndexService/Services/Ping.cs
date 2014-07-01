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
		public object Get( HttpRequest request ) {
			return "pong";
		}
		public object Post( HttpRequest request ) {
			return "post";
		}
	}
}