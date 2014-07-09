using System.Web;

namespace HappyIndexService {
	public abstract class BaseHandler : IHttpHandler {
		public bool IsReusable {
			get {
				return false;
			}
		}
		public abstract void ProcessRequest( HttpContext context );
	}
}