using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

		public static HappyIndex GetHappyIndex( string sid, DateTime date ) {
			using( DBCommand cmd = new DBCommand( Con, CommandType.Text ) ) {
				cmd.CommandText = "SELECT * FROM HappyIndexes WHERE [SID] = @sid AND [Date] = @date";
				cmd.AddWithValue( "@sid", sid );
				cmd.AddWithValue( "@date", date.ToString( "yyyy-MM-dd" ) );
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
				cmd.AddWithValue( "@SID", sid );
				cmd.AddWithValue( "@Date", hi.Date.ToString( "yyyy-MM-dd" ) );
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
		private static object Z( object that ) {
			return that ?? DBNull.Value;
		}
	}
}