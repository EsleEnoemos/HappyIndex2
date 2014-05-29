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
			WindowsIdentity identity = (WindowsIdentity)request.RequestContext.HttpContext.User.Identity;
			//string[] a = identity.Name.Split( '\\' );
			//System.DirectoryServices.DirectoryEntry ADEntry = new System.DirectoryServices.DirectoryEntry( "WinNT://" + a[ 0 ] + "/" + a[ 1 ] );
			//string Name = ADEntry.Properties[ "FullName" ].Value.ToString();
			User user = new User { Name = identity.Name };
			//foreach( string pn in ADEntry.Properties.PropertyNames ) {
			//	PropertyValueCollection pvs = ADEntry.Properties[ pn ];
			//	user.Groups.Add( string.Format( "{0} = {1}", pn, pvs != null ? pvs.Value : "null" ) );
			//}
			if( identity.Groups != null ) {
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