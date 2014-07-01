using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using HappyIndex2.Common;
using HappyIndex2WindowsClient.Controls;

namespace HappyIndex2WindowsClient {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load( object sender, EventArgs e ) {
			DateSelectorControl dc = new DateSelectorControl();
			dc.Dock = DockStyle.Fill;
			splitContainer1.Panel1.Controls.Add( dc );
			dc.ValueChanged += DcOnValueChanged;
			HappyIndex hi = APICaller.GetData<HappyIndex>( "index" ) ?? new HappyIndex { Date = DateTime.Now };
			ReportControlBase c;
			if( hi.ID <= 0 ) {
				c = new EmotionControl( hi );
			} else {
				c = new ProductionControl( hi );
			}
			c.Dock = DockStyle.Fill;
			splitContainer1.Panel2.Controls.Add( c );
		}
		private void DcOnValueChanged( object sender, EventArgs eventArgs ) {
			DateSelectorControl dc = sender as DateSelectorControl;
			if( dc == null ) {
				return;
			}
			NameValueCollection parameters = new NameValueCollection();
			parameters.Add( "date", dc.Value.ToString( "yyyy-MM-dd" ) );
			HappyIndex hi = APICaller.GetData<HappyIndex>( "index", parameters ) ?? new HappyIndex { Date = dc.Value };
			splitContainer1.Panel2.Controls.Clear();
			ReportControlBase c;
			if( hi.ID <= 0 ) {
				c = new EmotionControl( hi );
			} else {
				c = new ProductionControl( hi );
			}
			c.Dock = DockStyle.Fill;
			splitContainer1.Panel2.Controls.Add( c );
		}

		private void btnReport_Click( object sender, EventArgs e ) {
			if( splitContainer1.Panel2.Controls.Count == 0 ) {
				return;
			}
			ReportControlBase c = splitContainer1.Panel2.Controls[ 0 ] as ReportControlBase;
			if( c == null ) {
				return;
			}
			HappyIndex happyIndex = APICaller.PostData<HappyIndex, HappyIndex>( "index", c.HappyIndex );
		}
	}
}
