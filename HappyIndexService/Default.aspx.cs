using System;
using System.Web.UI;
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

		protected override void Page_Load( object sender, EventArgs e ) {
			if( !IsPostBack ) {
				return;
			}
			if( Request.Form[ "createteam" ] != null ) {
				DataFactory.UpdateTeam( new Team { Name = Request.Form[ "name" ] } );
			} else if( Request.Form[ "updateuserteams" ] != null ) {
				User.Teams = new TeamList();
				string[] teams = Request.Form.GetValues( "team" );
				if( teams != null ) {
					foreach( string t in teams ) {
						User.Teams.AddDistinct( DataFactory.Teams.GetByID( t.ToInt() ) );
					}
				}
				DataFactory.UpdateUserTeams( User );
			}

			Response.Redirect( "/", true );
		}
	}

	public class BaseForm : Page {
		public new bool IsPostBack {
			get {
				return string.Equals( Request.HttpMethod, "POST" );
			}
		}
		protected override void OnInit( EventArgs e ) {
			Load += Page_Load;
			base.OnInit( e );
		}
		protected virtual void Page_Load( object sender, EventArgs e ) {
			
		}
	}
}