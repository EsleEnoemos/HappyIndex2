using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using HappyIndex2.Common;
using HappyIndexService.Data;

namespace HappyIndexService {
	public class GraphicsHandler : BaseHandler {
		#region internal static DirectoryInfo SaveDir
		/// <summary>
		/// Gets the SaveDir of the GraphicsHandler
		/// </summary>
		/// <value></value>
		internal static DirectoryInfo SaveDir {
			get {
				return _saveDir ?? (_saveDir = new DirectoryInfo( ConfigurationManager.AppSettings[ "GraphicsFolder" ] ));
			}
		}
		private static DirectoryInfo _saveDir;
		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		public override void ProcessRequest( HttpContext context ) {
			FileInfo fi = MakePicture( context.Request.Path );
			context.Response.ContentType = "image/png";
			context.Response.AddHeader( "Content-Length", fi.Length.ToString() );
			context.Response.WriteFile( fi.FullName, false );
		}
		private FileInfo MakePicture( string path ) {
			if( path.StartsWith( "/" ) ) {
				path = path.Substring( 1 );
			}
			path = path.Substring( path.IndexOf( "/" ) + 1 );
			string[] parts = path.Split( '/' );
			FileInfo fi = null;
			switch( parts[ 0 ].ToLower() ) {
				case "team":
					fi = GetTeamPicture( parts );
					break;
				case "user":
					fi = GetUserPicture( parts );
					break;
			}
			return fi ?? new FileInfo( string.Format( "{0}\\Green.png", SaveDir.FullName ) );
		}
		private FileInfo GetUserPicture( string[] parts ) {
			DateTime fromDate = DateTime.Now;
			DateTime toDate = fromDate;
			FileInfo f = new FileInfo( parts[ parts.Length - 1 ] );
			User user = DataFactory.GetUser();
			switch( parts[ 1 ].ToLower() ) {
				case "week":
					int week = f.Name.Replace( f.Extension, "" ).ToInt();
					fromDate = Extensions.GetFirstDateOfWeek( week );
					toDate = fromDate.AddDays( 6 );
					break;
			}
			string[] dim = parts[ parts.Length - 2 ].Split( 'x' );
			int width = dim[ 0 ].ToInt();
			int height = dim[ 1 ].ToInt();
			string fn = string.Format( "{3}_User_{0}_{1}x{2}_{4}", user.ID, width, height, DateTime.Now.Year, f.Name );
			List<HappyIndex> stats = DataFactory.GetUserIndexes( user.ID, fromDate, toDate );
			FileInfo fi = new FileInfo( string.Format( "{0}\\{1}", SaveDir.FullName, fn ) );
			if( !File.Exists( fi.FullName ) ) {
				int barWidth = (width / (4 * stats.Count));
				//barWidth += barWidth/stats.Count;
				using( Bitmap img = new Bitmap( width, height ) ) {
					using( Graphics g = Graphics.FromImage( img ) ) {
						g.FillRectangle( Brushes.White, 0, 0, img.Width, img.Height );
						int x = 0;
						foreach( HappyIndex h in stats ) {
							int eHeight = (int)((h.EmotionalIndex / 5.0) * height);
							g.FillRectangle( Brushes.Blue, x, height - eHeight, barWidth, eHeight );
							x += barWidth;
							int pHeight = (int)((h.ProductivityIndex / 5.0) * height);
							g.FillRectangle( Brushes.Green, x, height - pHeight, barWidth, pHeight );
							x += barWidth;
							int mHeight = (int)((h.MotivationIndex / 5.0) * height);
							g.FillRectangle( Brushes.Yellow, x, height - mHeight, barWidth, mHeight );
							x += barWidth * 2;
						}
					}
					img.MakeTransparent( Color.White );
					img.Save( fi.FullName, ImageFormat.Png );
				}
			}
			return fi;
		}
		private FileInfo GetTeamPicture( string[] parts ) {
			DateTime fromDate = DateTime.Now;
			DateTime toDate = fromDate;
			FileInfo f = new FileInfo( parts[ parts.Length - 1 ] );
			int teamID = parts[ 1 ].ToInt();
			switch( parts[ 2 ].ToLower() ) {
				case "week":
					int week = f.Name.Replace( f.Extension, "" ).ToInt();
					fromDate = Extensions.GetFirstDateOfWeek( week );
					toDate = fromDate.AddDays( 6 );
					break;
			}
			string[] dim = parts[ parts.Length - 2 ].Split( 'x' );
			int width = dim[ 0 ].ToInt();
			int height = dim[ 1 ].ToInt();
			string fn = string.Format( "{3}_Teams_{0}_{1}x{2}_{4}", teamID, width, height, DateTime.Now.Year, f.Name );
			List<HappyIndex> stats = DataFactory.GetTeamStatistics( teamID, fromDate, toDate );
			FileInfo fi = new FileInfo( string.Format( "{0}\\{1}", SaveDir.FullName, fn ) );
			if( !File.Exists( fi.FullName ) ) {
				int barWidth = (width / (4 * stats.Count));
				//barWidth += barWidth/stats.Count;
				using( Bitmap img = new Bitmap( width, height ) ) {
					using( Graphics g = Graphics.FromImage( img ) ) {
						g.FillRectangle( Brushes.White, 0, 0, img.Width, img.Height );
						int x = 0;
						foreach( HappyIndex h in stats ) {
							int eHeight = (int)((h.EmotionalIndex / 5.0) * height);
							g.FillRectangle( Brushes.Blue, x, height - eHeight, barWidth, eHeight );
							x += barWidth;
							int pHeight = (int)((h.ProductivityIndex / 5.0) * height);
							g.FillRectangle( Brushes.Green, x, height - pHeight, barWidth, pHeight );
							x += barWidth;
							int mHeight = (int)((h.MotivationIndex / 5.0) * height);
							g.FillRectangle( Brushes.Yellow, x, height - mHeight, barWidth, mHeight );
							x += barWidth*2;
						}
					}
					img.MakeTransparent( Color.White );
					img.Save( fi.FullName, ImageFormat.Png );
				}
			}
			return fi;
		}
	}
}