using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Security.Principal;
using System.Web;
using HappyIndex2.Common;

namespace HappyIndexService.Data {
	public class DataFactory {
		#region private static string ConnectionString
		/// <summary>
		/// Gets the ConnectionString of the DataFactory
		/// </summary>
		/// <value></value>
		private static string ConnectionString {
			get { return _connectionString ?? (_connectionString = ConfigurationManager.ConnectionStrings[ "DB" ].ConnectionString); }
		}
		private static string _connectionString;
		#endregion
		#region private static SqlConnection Con
		/// <summary>
		/// Gets the Con of the DataFactory
		/// </summary>
		/// <value></value>
		private static SqlConnection Con {
			get {
				return new SqlConnection( ConnectionString );
			}
		}
		#endregion
		private static Dictionary<string,int> userCache = new Dictionary<string, int>();

		public static HappyIndex GetHappyIndex( string sid, DateTime date ) {
			using( DBCommand cmd = new DBCommand( Con, CommandType.Text ) ) {
				cmd.CommandText = "SELECT * FROM HappyIndexes WHERE [User_ID] = @User_ID AND [Date] = @date";
				cmd.AddWithValue( "@User_ID", GetUserID() );
				cmd.AddWithValue( "@date", date.Format() );
				if( cmd.Read() ) {
					return new HappyIndex {
						ID = cmd.GetInt( "HappyIndex_ID" ),
						Date = date,
						EmotionalComment = cmd.GetString( "EmotionalComment" ),
						EmotionalIndex = cmd.GetDouble( "EmotionalIndex" ),
						ProductivityIndex = cmd.GetDouble( "ProductivityIndex" ),
						MotivationIndex = cmd.GetDouble( "MotivationIndex" ),
						IndexComment = cmd.GetString( "IndexComment" )
					};
				}
			}
			return null;
		}
		public static HappyIndex UpdateHappyIndex( string sid, HappyIndex hi ) {
			using( DBCommand cmd = new DBCommand( Con, CommandType.StoredProcedure ) ) {
				cmd.CommandText = "UpdateHappyIndex";
				SqlParameter id = cmd.Add( "@HappyIndex_ID", SqlDbType.Int, ParameterDirection.InputOutput, hi.ID );
				cmd.AddWithValue( "@User_ID", GetUserID() );
				cmd.AddWithValue( "@Date", hi.Date.Format() );
				cmd.AddWithValue( "@EmotionalIndex", hi.EmotionalIndex );
				cmd.AddWithValue( "@EmotionalComment", Z( hi.EmotionalComment ) );
				cmd.AddWithValue( "@ProductivityIndex", hi.ProductivityIndex );
				cmd.AddWithValue( "@MotivationIndex", hi.MotivationIndex );
				cmd.AddWithValue( "@IndexComment", Z( hi.IndexComment ) );
				cmd.ExecuteNonQuery();
				if( hi.ID <= 0 ) {
					hi.ID = (int)id.Value;
				}
			}
			return hi;
		}
		private static int GetUserID() {
			WindowsIdentity identity = (WindowsIdentity)HttpContext.Current.Request.RequestContext.HttpContext.User.Identity;
			if( identity == null || identity.User == null ) {
				throw new UnauthorizedAccessException();
			}
			string sid = identity.User.AccountDomainSid.Value;
			if( userCache.ContainsKey( sid ) ) {
				return userCache[ sid ];
			}
			string[] a = identity.Name.Split( '\\' );
			DirectoryEntry entry = new DirectoryEntry( "WinNT://" + a[ 0 ] + "/" + a[ 1 ] );
			string name = entry.Properties[ "FullName" ].Value.ToString();
			using( DBCommand cmd = new DBCommand( Con, CommandType.StoredProcedure ) ) {
				cmd.CommandText = "GetUserID";
				SqlParameter id = cmd.Add( "@User_ID", SqlDbType.Int, ParameterDirection.InputOutput, DBNull.Value );
				cmd.AddWithValue( "@SID", sid );
				cmd.AddWithValue( "@Name", name );
				cmd.ExecuteNonQuery();
				userCache[ sid ] = (int)id.Value;
			}
			return userCache[ sid ];
		}
		private static object Z( object that ) {
			return that ?? DBNull.Value;
		}
	}
}