using System;
using System.Windows.Forms;
using HappyIndex2.Common;

namespace HappyIndex2WindowsClient.Controls {
	public partial class EmotionControl : UserControl {
		private HappyIndex hi;
		public HappyIndex HappyIndex {
			get {
				hi.EmotionalIndex = (trackBar1.Value*1.0)/10.0;
				return hi;
			}
		}
		#region public EmotionControl()
		/// <summary>
		/// Initializes a new instance of the <b>EmotionControl</b> class.
		/// </summary>
		public EmotionControl()
			: this( null ) {
		}
		#endregion
		#region public EmotionControl( HappyIndex hi )
		/// <summary>
		/// Initializes a new instance of the <b>EmotionControl</b> class.
		/// </summary>
		/// <param name="hi"></param>
		public EmotionControl( HappyIndex hi ) {
			InitializeComponent();
			this.hi = hi ?? new HappyIndex { Date = DateTime.Now };
		}
		#endregion

		private void EmotionControl_Load( object sender, EventArgs e ) {
			UpdateValue();
			label2.Text = hi.Date.Date == DateTime.Now.Date ? "How do you feel today?" : string.Format( "How did you feel {0}", hi.Date.ToShortDateString() );
		}

		private void trackBar1_Scroll( object sender, EventArgs e ) {
			UpdateValue();
		}

		private void UpdateValue() {
			label1.Text = ((trackBar1.Value * 1.0) / 10.0).ToString( "F1" );
		}
	}
}
