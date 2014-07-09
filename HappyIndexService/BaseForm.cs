using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using HappyIndex2.Common;

namespace HappyIndexService {
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
		protected void SetSaveMessage( string msg ) {
			Session[ "SaveMessage" ] = msg;
		}
		protected void ShowSaveMessage( PlaceHolder plhSaveMessage ) {
			if( IsPostBack ) {
				return;
			}
			if( plhSaveMessage == null || plhSaveMessage == null ) {
				return;
			}
			string msg = Session[ "SaveMessage" ] as string;
			if( msg == null ) {
				return;
			}
			plhSaveMessage.Controls.Add( string.Format( "<div id=\"savemessage\">{0}</div>", msg ) );
			plhSaveMessage.Visible = true;
			plhSaveMessage.Controls.Add( @"
<script type=""text/javascript"">
	function hideSaveMessage() {
		if( !document.getElementById(""savemessage"") ) {
			return;
		}
		$( ""#savemessage"" ).slideUp( 300 );
	}
	$( ""document"" ).ready( function() { setTimeout( ""hideSaveMessage()"", 2000 ); } );
</script>
" );
			try {
				Session.Remove( "SaveMessage" );
			} catch {
			}
		}
	}
}