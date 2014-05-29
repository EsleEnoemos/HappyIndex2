using System;
using System.Windows.Forms;
using HappyIndex2.Common;
using HappyIndex2WindowsClient.Controls;

namespace HappyIndex2WindowsClient {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load( object sender, EventArgs e ) {
			//dateTimePicker1.MaxDate = DateTime.Now;
			//splitContainer1.Panel2.Controls.Clear();
			//EmotionControl c = new EmotionControl( new HappyIndex{Date = DateTime.Now.AddDays( -2 )});
			//c.Dock = DockStyle.Fill;
			//splitContainer1.Panel2.Controls.Add( c );
		}

		private void button1_Click( object sender, EventArgs e ) {
			User me = APICaller.GetData<User>( "me", null );
			MessageBox.Show( me.ToString() );
		}
	}
}
