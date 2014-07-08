using System.Web;
using HappyIndexService.Data;

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
		public object Get( HttpRequest request ) {
			//WindowsIdentity identity = (WindowsIdentity)request.RequestContext.HttpContext.User.Identity;
			//if( identity == null || identity.User == null ) {
			//	return null;
			//}
			//string[] a = identity.Name.Split( '\\' );
			//System.DirectoryServices.DirectoryEntry ADEntry = new System.DirectoryServices.DirectoryEntry( "WinNT://" + a[ 0 ] + "/" + a[ 1 ] );
			//string Name = ADEntry.Properties[ "FullName" ].Value.ToString();
			//User user = new User { Name = identity.Name };
			//SecurityIdentifier sid = identity.User.AccountDomainSid;
			//sid.Value;
			//foreach( string pn in ADEntry.Properties.PropertyNames ) {
			//	PropertyValueCollection pvs = ADEntry.Properties[ pn ];
			//	user.Groups.Add( string.Format( "{0} = {1}", pn, pvs != null ? pvs.Value : "null" ) );
			//}

			//return user;
			return DataFactory.GetUser();
		}
		public object Post( HttpRequest request ) {
			return null;
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