using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HappyIndex2.Common;
using HappyIndex2WindowsClient.Controls;

namespace HappyIndex2WindowsClient {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
			DateSelectorControl dc = new DateSelectorControl();
			dc.Dock = DockStyle.Fill;
			splitContainer1.Panel1.Controls.Add( dc );
			dc.ValueChanged += DcOnValueChanged;
			topBar1.AutoHideOnClose = true;
			topBar1.EnableFormMove = true;
		}

		private void Form1_Load( object sender, EventArgs e ) {
			LoadControl( DateTime.Now );
			//HappyIndex hi = APICaller.GetData<HappyIndex>( "index" ) ?? new HappyIndex { Date = DateTime.Now };
			//ReportControlBase c;
			//if( hi.ID <= 0 ) {
			//	c = new EmotionControl( hi );
			//} else {
			//	c = new ProductionControl( hi );
			//}
			//c.Dock = DockStyle.Fill;
			//splitContainer1.Panel2.Controls.Add( c );
		}
		private void DcOnValueChanged( object sender, EventArgs eventArgs ) {
			DateSelectorControl dc = sender as DateSelectorControl;
			if( dc == null ) {
				return;
			}
			LoadControl( dc.Value );
		}
		private void LoadControl( DateTime d ) {
			splitContainer1.Panel2.Controls.Clear();
			splitContainer1.Panel2.Controls.Add( new Label { Text = "Working... please wait", AutoSize = false, TextAlign = ContentAlignment.MiddleCenter, Dock = DockStyle.Fill } );
			Cursor = Cursors.AppStarting;
			NameValueCollection parameters = new NameValueCollection();
			parameters.Add( "date", d.Format() );
			BackgroundCall( "index", delegate( object sndr, RunWorkerCompletedEventArgs e ) {
				HappyIndex hi = e.Result as HappyIndex ?? new HappyIndex { Date = d };
				ReportControlBase c;
				if( d.Date != DateTime.Now.Date ) {
					c = new DayControl( hi );
				} else {
					if( hi.ID <= 0 ) {
						c = new EmotionControl( hi );
					} else {
						c = new ProductionControl( hi );
					}
				}
				c.Dock = DockStyle.Fill;
				splitContainer1.Panel2.Controls.Clear();
				splitContainer1.Panel2.Controls.Add( c );
				Cursor = Cursors.Default;
			}, APICaller.GetData<HappyIndex>, parameters );
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

		private void BackgroundCall( string service, RunWorkerCompletedEventHandler callback, Func<string,NameValueCollection,HappyIndex> serviceCall, NameValueCollection parameters = null ) {
			BackgroundWorker w = new BackgroundWorker();
			w.RunWorkerCompleted += callback;
			w.DoWork += WOnDoWork;
			w.RunWorkerAsync(new BackgroundArgs{ Service = service, Parameters = parameters, ServiceCall = serviceCall });
		}
		
		private void WOnDoWork( object sender, DoWorkEventArgs e ) {
			BackgroundArgs a = (BackgroundArgs)e.Argument;
			e.Result = a.ServiceCall( a.Service, a.Parameters );
		}

		private class BackgroundArgs {
			public string Service;
			public NameValueCollection Parameters;
			public Func<string, NameValueCollection, HappyIndex> ServiceCall;
		}

		private void exitToolStripMenuItem_Click( object sender, EventArgs e ) {
			Close();
		}

		private void notifyIcon1_MouseDoubleClick( object sender, MouseEventArgs e ) {
			if( Visible ) {
				topBar1.SlideDown();
				return;
			}
			topBar1.SlideUp();
		}
	}
}
