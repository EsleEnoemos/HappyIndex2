using System;
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
		#region public static TeamList Teams
		/// <summary>
		/// Gets the Teams of the DataFactory
		/// </summary>
		/// <value></value>
		public static TeamList Teams {
			get {
				return _teams ?? (_teams = GetTeams());
			}
		}
		private static TeamList _teams;
		#endregion
		private static UserList Users = new UserList();

		#region private static TeamList GetTeams()
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private static TeamList GetTeams() {
			TeamList list = new TeamList();
			using( DBCommand cmd = new DBCommand( Con, CommandType.StoredProcedure ) ) {
				cmd.CommandText = "GetTeams";
				while( cmd.Read() ) {
					list.AddDistinct( new Team { ID = cmd.GetInt( "Team_ID" ), Name = cmd.GetString( "Name" ) } );
				}
			}
			return list;
		}
		#endregion
		#region public static HappyIndex GetHappyIndex( string sid, DateTime date )
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sid"></param>
		/// <param name="date"></param>
		/// <returns></returns>
		public static HappyIndex GetHappyIndex( string sid, DateTime date ) {
			using( DBCommand cmd = new DBCommand( Con, CommandType.Text ) ) {
				cmd.CommandText = "SELECT * FROM HappyIndexes WHERE [User_ID] = @User_ID AND [Date] = @date";
				cmd.AddWithValue( "@User_ID", GetUser().ID );
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
		#endregion
		#region public static HappyIndex UpdateHappyIndex( string sid, HappyIndex hi )
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sid"></param>
		/// <param name="hi"></param>
		/// <returns></returns>
		public static HappyIndex UpdateHappyIndex( string sid, HappyIndex hi ) {
			using( DBCommand cmd = new DBCommand( Con, CommandType.StoredProcedure ) ) {
				cmd.CommandText = "UpdateHappyIndex";
				SqlParameter id = cmd.Add( "@HappyIndex_ID", SqlDbType.Int, ParameterDirection.InputOutput, hi.ID );
				cmd.AddWithValue( "@User_ID", GetUser().ID );
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
		#endregion
		#region public static User GetUser()
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		/// <exception cref="UnauthorizedAccessException"></exception>
		public static User GetUser() {
			WindowsIdentity identity = (WindowsIdentity)HttpContext.Current.Request.RequestContext.HttpContext.User.Identity;
			if( identity == null || identity.User == null ) {
				throw new UnauthorizedAccessException();
			}
			string sid = identity.User.AccountDomainSid.Value;
			if( Users.ContainsSID( sid ) ) {
				return Users[ sid ];
			}
			string[] a = identity.Name.Split( '\\' );
			DirectoryEntry entry = new DirectoryEntry( "WinNT://" + a[ 0 ] + "/" + a[ 1 ] );
			string name = entry.Properties[ "FullName" ].Value.ToString();
			using( DBCommand cmd = new DBCommand( Con, CommandType.StoredProcedure ) ) {
				cmd.CommandText = "GetUser";
				SqlParameter id = cmd.Add( "@User_ID", SqlDbType.Int, ParameterDirection.InputOutput, DBNull.Value );
				cmd.AddWithValue( "@SID", sid );
				cmd.AddWithValue( "@Name", name );
				User user = null;
				while( cmd.Read() ) {
					if( user == null ) {
						user = new User { ID = cmd.GetInt( "User_ID" ), Name = cmd.GetString( "Name" ), SID = sid };
					}
					if( !cmd.IsDBNull( "Team_ID" ) ) {
						user.Teams.AddDistinct( Teams.GetByID( cmd.GetInt( "Team_ID" ) ) );
					}
				}
				Users.AddDistinct( user );
			}
			return Users[ sid ];
		}
		#endregion

		public static void UpdateTeam( Team t ) {
			using( DBCommand cmd = new DBCommand( Con, CommandType.StoredProcedure, "UpdateTeam" ) ) {
				SqlParameter id = cmd.Add( "@Team_ID", SqlDbType.Int, ParameterDirection.InputOutput, t.ID );
				cmd.AddWithValue( "@Name", t.Name );
				cmd.ExecuteNonQuery();
				if( t.ID <= 0 ) {
					t.ID = (int)id.Value;
					Teams.AddDistinct( t );
				}
			}
		}
		public static void UpdateUserTeams( User user ) {
			using( DBCommand cmd = new DBCommand( Con, CommandType.StoredProcedure, "ClearUsersTeams" ) ) {
				cmd.AddWithValue( "@User_ID", user.ID );
				cmd.ExecuteNonQuery();
				if( user.Teams.Count > 0 ) {
					cmd.CommandText = "AddUserTeam";
					SqlParameter t = cmd.Add( "@Team_ID", SqlDbType.Int );
					foreach( Team team in user.Teams ) {
						t.Value = team.ID;
						cmd.ExecuteNonQuery();
					}
				}
			}
		}

		#region private static object Z( object that )
		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		private static object Z( object that ) {
			return that ?? DBNull.Value;
		}
		#endregion
	}
}