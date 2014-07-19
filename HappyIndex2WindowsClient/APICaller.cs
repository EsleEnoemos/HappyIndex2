using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace HappyIndex2WindowsClient {
	public static class APICaller {
		#region private static string Remote
		/// <summary>
		/// Gets the Remote of the APICaller
		/// </summary>
		/// <value></value>
		private static string Remote {
			get {
				if( _remote == null ) {
					_remote = ConfigurationManager.AppSettings[ "Remote" ];
					if( _remote != null && !_remote.EndsWith( "/" ) ) {
						_remote += "/";
					}
					_remote += "API/";
				}
				return _remote;
			}
		}
		private static string _remote;
		#endregion

		public static T GetData<T>( string service, NameValueCollection parameters = null ) where T : new() {
			string url = string.Format( "{0}{1}", Remote, service );
			if( parameters != null ) {
				List<string> parms = new List<string>();
				foreach( string key in parameters.AllKeys ) {
					string[] values = parameters.GetValues( key ) ?? new string[0];
					foreach( string value in values ) {
						parms.Add( string.Format( "{0}={1}", key, value ) );
					}
				}
				if( parms.Count > 0 ) {
					url = string.Format( "{0}?{1}", url, string.Join( "&", parms ) );
				}
			}
			using( WebClient wc = new WebClient() ) {
				try {
					wc.UseDefaultCredentials = true;
					wc.Encoding = Encoding.UTF8;
					string res = wc.DownloadString( url );
					JavaScriptSerializer js = new JavaScriptSerializer();
					T t = js.Deserialize<T>( res );
					return t;
				} catch( WebException ex ) {
					if( ex.Response != null ) {
						ex.Response.Close();
						ex.Response.Dispose();
					}
					//throw;
				}
			}
			return new T();
		}
		public static T PostData<T,T2>( string service, T2 postData, NameValueCollection parameters = null ) {
			string url = string.Format( "{0}{1}", Remote, service );
			if( parameters != null ) {
				List<string> parms = new List<string>();
				foreach( string key in parameters.AllKeys ) {
					string[] values = parameters.GetValues( key ) ?? new string[ 0 ];
					foreach( string value in values ) {
						parms.Add( string.Format( "{0}={1}", key, value ) );
					}
				}
				if( parms.Count > 0 ) {
					url = string.Format( "{0}?{1}", url, string.Join( "&", parms ) );
				}
			}
			using( WebClient wc = new WebClient() ) {
				try {
					wc.UseDefaultCredentials = true;
					wc.Encoding = Encoding.UTF8;
					JavaScriptSerializer js = new JavaScriptSerializer();
					string res = wc.UploadString( url, js.Serialize( postData ) );
					T t = js.Deserialize<T>( res );
					return t;
				} catch( WebException ex ) {
					if( ex.Response != null ) {
						ex.Response.Close();
						ex.Response.Dispose();
					}
					throw;
				}
			}
		}
	}
}
 