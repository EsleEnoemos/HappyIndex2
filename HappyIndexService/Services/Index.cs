using System;
using System.IO;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using HappyIndex2.Common;
using HappyIndexService.Data;

namespace HappyIndexService.Services {
	public class Index : IService {
		#region public string ServiceName
		/// <summary>
		/// Gets the ServiceName of the Index
		/// </summary>
		/// <value></value>
		public string ServiceName {
			get {
				return "index";
			}
		}
		#endregion
		#region public string Description
		/// <summary>
		/// Gets the Description of the Index
		/// </summary>
		/// <value></value>
		public string Description {
			get {
				return "Returns a Happy Index for the specified date";
			}
		}
		#endregion
		public object Get( HttpRequest request ) {
			if( string.Equals( request.HttpMethod, "POST" ) ) {
				
			}
			WindowsIdentity identity = (WindowsIdentity)request.RequestContext.HttpContext.User.Identity;
			if( identity == null || identity.User == null ) {
				return null;
			}
			DateTime date;
			if( request.QueryString[ "date" ] == null ) {
				date = DateTime.Now;
			} else {
				if( !DateTime.TryParse( request.QueryString[ "date" ], out date ) ) {
					date = DateTime.Now;
				}
			}
			if( date == DateTime.MinValue ) {
				date = DateTime.Now;
			}
			return DataFactory.GetHappyIndex( identity.User.AccountDomainSid.Value, date );
		}
		public object Post( HttpRequest request ) {
			WindowsIdentity identity = (WindowsIdentity)request.RequestContext.HttpContext.User.Identity;
			if( identity == null || identity.User == null ) {
				return null;
			}
			using( StreamReader s = new StreamReader( request.GetBufferlessInputStream( true ) ) ) {
				string str = s.ReadToEnd();
				JavaScriptSerializer js = new JavaScriptSerializer();
				HappyIndex happyIndex = js.Deserialize<HappyIndex>( str );
				DataFactory.UpdateHappyIndex( identity.User.AccountDomainSid.Value, happyIndex );
				return happyIndex;
			}
		}
	}
}