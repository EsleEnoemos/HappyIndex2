using System.Collections.Generic;
using System.DirectoryServices;
using System.Security.Principal;
using System.Web;
using HappyIndex2.Common;

namespace HappyIndexService.Services {
	public class Me : IService {
		public string ServiceName {
			get {
				return "me";
			}
		}
		public string Description {
			get {
				return "Information about you";
			}
		}
		public object GetData( HttpRequest request ) {
			WindowsIdentity identity = request.RequestContext.HttpContext.User.Identity as WindowsIdentity;

			User user = new User { Name = identity.Name};
			if( identity.Groups != null ) {
				user.Groups = new List<string>();
				foreach( IdentityReference g in identity.Groups ) {
					user.Groups.Add( g.Translate( typeof( NTAccount ) ).Value );
				}
			}

			return user;
		}
		//private string uEmail( string uid ) {
		//	DirectorySearcher dirSearcher = new DirectorySearcher();
		//	DirectoryEntry entry = new DirectoryEntry( dirSearcher.SearchRoot.Path );
		//	dirSearcher.Filter = "(&(objectClass=user)(objectcategory=person)(objectSid=" + uid + "*))";

		//	SearchResult srEmail = dirSearcher.FindOne();

		//	string propName = "mail";
		//	ResultPropertyValueCollection valColl = srEmail.Properties[ propName ];
		//	try {
		//		return valColl[ 0 ].ToString();
		//	} catch {
		//		return "";
		//	}

		//}
	}
}