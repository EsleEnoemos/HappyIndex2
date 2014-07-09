using System;
using HappyIndex2.Common;
using HappyIndexService.Data;

namespace HappyIndexService {
	public partial class Default : BaseForm {

		#region protected HtmlHelper Html
		/// <summary>
		/// Gets the Html of the Default
		/// </summary>
		/// <value></value>
		protected HtmlHelper Html {
			get {
				return HtmlHelper.Instance;
			}
		}
		#endregion
		#region protected User User
		/// <summary>
		/// Gets the User of the Default
		/// </summary>
		/// <value></value>
		protected User User {
			get { return _user ?? (_user = DataFactory.GetUser()); }
		}
		private User _user;
		#endregion
		#region protected int Week
		/// <summary>
		/// Gets the Week of the Default
		/// </summary>
		/// <value></value>
		protected int Week {
			get {
				if( !_week.HasValue ) {
					_week = DateTime.Now.GetWeekNumber();
				}
				return _week.Value;
			}
		}
		private int? _week;
		#endregion
		#region protected DateTime FirstDateOfWeek
		/// <summary>
		/// Gets the FirstDateOfWeek of the Default
		/// </summary>
		/// <value></value>
		protected DateTime FirstDateOfWeek {
			get {
				if( !_firstDateOfWeek.HasValue ) {
					_firstDateOfWeek = Extensions.GetFirstDateOfWeek( Week );
				}
				return _firstDateOfWeek.Value;
			}
		}
		private DateTime? _firstDateOfWeek;
		#endregion

		protected override void Page_Load( object sender, EventArgs e ) {
			ShowSaveMessage( plhSaveMessage );
			if( !IsPostBack ) {
				return;
			}
			if( Request.Form[ "createteam" ] != null ) {
				DataFactory.UpdateTeam( new Team { Name = Request.Form[ "name" ] } );
				SetSaveMessage( "Ett nytt team skapades" );
			} else if( Request.Form[ "updateuserteams" ] != null ) {
				User.Teams = new TeamList();
				string[] teams = Request.Form.GetValues( "team" );
				if( teams != null ) {
					foreach( string t in teams ) {
						User.Teams.AddDistinct( DataFactory.Teams.GetByID( t.ToInt() ) );
					}
				}
				DataFactory.UpdateUserTeams( User );
				SetSaveMessage( "Dina teamtillhörigheter uppdaterades" );
			}

			Response.Redirect( "/", true );
		}
	}
}