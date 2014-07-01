using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace HappyIndexService {
	public class APIHandler : IHttpHandler {
		#region public bool IsReusable
		/// <summary>
		/// Gets the IsReusable of the APIHandler
		/// </summary>
		/// <value></value>
		public bool IsReusable {
			get {
				return false;
			}
		}
		#endregion
		#region public static List<IService> Methods
		/// <summary>
		/// Gets the Methods of the APIHandler
		/// </summary>
		/// <value></value>
		public static List<IService> Methods {
			get {
				if( _methods == null ) {
					lock( methodLock ) {
						if( _methods == null ) {
							List<IService> tmpMethods = new List<IService>();
							Hashtable hash = new Hashtable();
							FileInfo currentFile = new FileInfo( typeof( APIHandler ).Assembly.Location );
							if( currentFile.Directory != null ) {
								FileInfo[] files = currentFile.Directory.GetFiles( "*.dll", SearchOption.AllDirectories );
								foreach( FileInfo dll in files ) {
									try {
										Assembly ass = Assembly.LoadFrom( dll.FullName );
										Type[] types = ass.GetTypes();
										foreach( Type type in types ) {
											try {
												if( !type.IsAbstract ) {
													if( typeof( IService ).IsAssignableFrom( type ) ) {
														IService mt = ass.CreateInstance( type.FullName ) as IService;
														if( mt != null ) {
															if( !hash.ContainsKey( mt.ServiceName ) ) {
																tmpMethods.Add( mt );
																hash[ mt.ServiceName ] = null;
															}
														}
													}
												}
											} catch( Exception ) {
											}
										}
									} catch( ReflectionTypeLoadException rex ) {
										foreach( Type type in rex.Types ) {
											try {
												if( !type.IsAbstract ) {
													if( typeof( IService ).IsAssignableFrom( type ) ) {
														IService mt = type.Assembly.CreateInstance( type.FullName ) as IService;
														if( mt != null ) {
															if( !hash.ContainsKey( mt.ServiceName ) ) {
																tmpMethods.Add( mt );
																hash[ mt.ServiceName ] = null;
															}
														}
													}
												}
											} catch( Exception ) {
											}
										}
									} catch {
									}
								}
							}
							if( tmpMethods.Count > 1 ) {
								tmpMethods.Sort( delegate( IService x, IService y ) {
									return string.Compare( x.ServiceName, y.ServiceName );
								} );
							}
							_methods = tmpMethods;
						}
					}
				}
				return _methods;
			}
		}
		private volatile static List<IService> _methods;
		private static object methodLock = new object();
		#endregion

		public void ProcessRequest( HttpContext context ) {
			string path = context.Request.Path;
			if( string.IsNullOrEmpty( path ) ) {
				return;
			}
			if( path.ToLower().StartsWith( "/api/" ) ) {
				path = path.Substring( "/api/".Length );
			}
			IService service = null;
			foreach( IService s in Methods ) {
				if( string.Equals( s.ServiceName, path, StringComparison.OrdinalIgnoreCase ) ) {
					service = s;
					break;
				}
			}
			if( service == null ) {
				return;
			}
			if( string.Equals( context.Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase ) ) {
				Serve( context, service.Post( context.Request ) );
			} else {
				Serve( context, service.Get( context.Request ) );
			}
		}
		private void Serve( HttpContext context, object data, bool setMaxJsonLength = true ) {
			HttpResponse res = context.Response;
			JavaScriptSerializer js = new JavaScriptSerializer();
			if( setMaxJsonLength ) {
				try {
					js.MaxJsonLength = int.MaxValue;
				} catch {
					Serve( context, data, false );
					return;
				}
			}

			string str = js.Serialize( data );
			res.ClearHeaders();
			res.Clear();
			res.ContentType = "application/x-javascript";
			res.ContentEncoding = Encoding.UTF8;
			int len = Encoding.UTF8.GetByteCount( str );
			res.Headers.Add( "Content-Length", len.ToString() );
			res.Headers.Add( "Access-Control-Allow-Origin", "*" );
			res.Write( str );
		}
	}

	public interface IService {
		string ServiceName { get; }
		string Description { get; }
		object Get( HttpRequest request );
		object Post( HttpRequest request );
	}
}