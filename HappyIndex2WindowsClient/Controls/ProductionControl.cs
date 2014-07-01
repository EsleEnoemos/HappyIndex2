using System;
using HappyIndex2.Common;

namespace HappyIndex2WindowsClient.Controls {
	public partial class ProductionControl : ReportControlBase {
		private HappyIndex hi;

		public override HappyIndex HappyIndex {
			get {
				hi.MotivationIndex = slMotivation.Value;
				hi.ProductivityIndex = slProduction.Value;
				hi.IndexComment = textBox1.Text;
				return hi;
			}
		}
		public ProductionControl()
			: this( null ) {
		}
		public ProductionControl( HappyIndex hi ) {
			this.hi = hi ?? new HappyIndex();
			InitializeComponent();
		}

		private void ProductionControl_Load( object sender, EventArgs e ) {
			label2.Text = hi.Date.Date == DateTime.Now.Date ? "How's your day been?" : string.Format( "How was your day {0}", hi.Date.ToShortDateString() );
			slMotivation.Value = hi.MotivationIndex;
			slProduction.Value = hi.ProductivityIndex;
			textBox1.Text = hi.IndexComment;
		}
	}
}
