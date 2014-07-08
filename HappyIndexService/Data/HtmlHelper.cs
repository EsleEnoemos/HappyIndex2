using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.UI.HtmlControls;
using HappyIndex2.Common;

namespace HappyIndexService.Data {
	/// <summary>
	/// </summary>
	/// <remarks></remarks>
	/// <example></example>
	public class HtmlHelper {
		#region public static HtmlHelper Instance
		/// <summary>
		/// Returns the singleton instance of the <see cref="HtmlHelper"/> class.
		/// </summary>
		/// <value></value>
		public static HtmlHelper Instance {
			get {
				if( _instance == null ) {
					lock( _instanceLock ) {
						if( _instance == null ) {
							_instance = new HtmlHelper();
						}
					}
				}
				return _instance;
			}
		}
		private volatile static HtmlHelper _instance;
		private static object _instanceLock = new object();
		#endregion
		#region private HtmlHelper()
		/// <summary>
		/// Initializes a new instance of the <b>HtmlHelper</b> class.
		/// </summary>
		private HtmlHelper() {
		}
		#endregion

		#region public IDisposable BeginForm( string action = null, string method = "post", string enctype = null, string id = null, object attributes = null, HtmlForm serverForm = null, string cssClass = null )
		/// <summary>
		/// 
		/// </summary>
		/// <param name="action">The action of the form. Default value is the RawUrl of the current request</param>
		/// <param name="method">The method of the form. Default value is post</param>
		/// <param name="enctype">The enctype of the form. Default value is null</param>
		/// <param name="id">The id of the form. Default value is null</param>
		/// <param name="attributes"></param>
		/// <param name="serverForm"></param>
		/// <param name="cssClass"></param>
		/// <returns></returns>
		public IDisposable BeginForm( string action = null, string method = "post", string enctype = null, string id = null, object attributes = null, HtmlForm serverForm = null, string cssClass = null ) {
			return new FormTag( serverForm, action, method, enctype, id, attributes, cssClass );
		}
		#endregion

		private class FormTag : IDisposable {
			private string action;
			private string method;
			private string encType;
			private string id;
			private object attributes;
			private List<string> attributeList;
			private HtmlForm serverForm;
			private string cssClass;
			#region private HttpContext CTX
			/// <summary>
			/// Gets the CTX of the FormTag
			/// </summary>
			/// <value></value>
			private HttpContext CTX {
				get {
					return _cTX ?? (_cTX = HttpContext.Current);
				}
			}
			private HttpContext _cTX;
			#endregion

			#region public FormTag( HtmlForm serverForm, string action, string method, string encType, string id, object attributes, string cssClass )
			/// <summary>
			/// Initializes a new instance of the <b>FormTag</b> class.
			/// </summary>
			/// <param name="serverForm"></param>
			/// <param name="action"></param>
			/// <param name="method"></param>
			/// <param name="encType"></param>
			/// <param name="id"></param>
			/// <param name="attributes"></param>
			/// <param name="cssClass"></param>
			public FormTag( HtmlForm serverForm, string action, string method, string encType, string id, object attributes, string cssClass ) {
				this.serverForm = serverForm;
				this.action = action;
				this.method = method;
				this.encType = encType;
				this.id = id;
				this.attributes = attributes;
				this.cssClass = cssClass;
				Init();
			}
			#endregion
			#region private void Init()
			/// <summary>
			/// 
			/// </summary>
			private void Init() {
				if( string.IsNullOrEmpty( method ) ) {
					method = "post";
				}
				if( string.IsNullOrEmpty( action ) && CTX != null ) {
					action = CTX.Request.RawUrl;
				}
				Hashtable attHash = new Hashtable();
				attributeList = new List<string>();
				if( attributes != null ) {
					try {
						Type t = attributes.GetType();
						PropertyInfo[] properties = t.GetProperties();
						foreach( PropertyInfo pi in properties ) {
							try {
								string name = pi.Name;
								string value = pi.GetValue( attributes, null ) as string;
								attributeList.Add( string.Format( "{0}=\"{1}\"", name, value ?? name ) );
								attHash[ name.ToLower() ] = null;
							} catch{}
						}
					} catch{}
				}
				if( serverForm != null ) {
					foreach(string key in serverForm.Attributes.Keys) {
						if( string.IsNullOrEmpty( key ) ) {
							continue;
						}
						string k = key.ToLower();
						if( "id".Equals( k ) || "action".Equals( k ) || "method".Equals( k ) ) {
							continue;
						}
						if( attHash.ContainsKey( k ) ) {
							continue;
						}
						attributeList.Add( string.Format( "{0}=\"{1}\"", key, serverForm.Attributes[key] ?? key ) );
						attHash[ k ] = null;
					}
				}
				RenderStart();
			}
			#endregion
			#region private void RenderStart()
			/// <summary>
			/// 
			/// </summary>
			private void RenderStart() {
				if( CTX != null ) {
					try {
						CTX.Response.Write( string.Format( "<form action=\"{0}\" method=\"{1}\"{2}{3}{4}{5}>",
							action,
							method,
							string.IsNullOrEmpty( encType ) ? "" : " enctype=\"{0}\"".FillBlanks( encType ),
							string.IsNullOrEmpty( id ) ? "" : " id=\"{0}\"".FillBlanks( id ),
							string.IsNullOrEmpty( cssClass ) ? "" : " class=\"{0}\"".FillBlanks( cssClass ),
							attributeList != null && attributeList.Count > 0 ? " {0}".FillBlanks( attributeList.ToString( " " ) ) : "") );
					} catch {
					}
				}
			}
			#endregion
			#region public void Dispose()
			/// <summary>
			/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
			/// </summary>
			/// <filterpriority>2</filterpriority>
			public void Dispose() {
				if( CTX != null ) {
					try {
						CTX.Response.Write( "</form>" );
					} catch {
					}
				}
			}
			#endregion
		}
	}
}