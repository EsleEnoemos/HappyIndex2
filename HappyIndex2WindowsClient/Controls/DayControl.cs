using System;
using HappyIndex2.Common;

namespace HappyIndex2WindowsClient.Controls {
	public partial class DayControl : ReportControlBase {
		private HappyIndex hi;
		public override HappyIndex HappyIndex {
			get {
				hi.EmotionalIndex = sliderControl1.Value;
				hi.EmotionalComment = textBox2.Text;
				hi.MotivationIndex = slMotivation.Value;
				hi.ProductivityIndex = slProduction.Value;
				hi.IndexComment = textBox1.Text;
				return hi;
			}
		}

		#region public DayControl()
		/// <summary>
		/// Initializes a new instance of the <b>DayControl</b> class.
		/// </summary>
		public DayControl()
			: this( null ) {
		}
		#endregion
		#region public DayControl( HappyIndex hi )
		/// <summary>
		/// Initializes a new instance of the <b>DayControl</b> class.
		/// </summary>
		/// <param name="hi"></param>
		public DayControl( HappyIndex hi ) {
			InitializeComponent();
			this.hi = hi ?? new HappyIndex { Date = DateTime.Now };
		}
		#endregion

		private void DayControl_Load( object sender, EventArgs e ) {
			sliderControl1.Value = hi.EmotionalIndex;
			textBox2.Text = hi.EmotionalComment;
			slProduction.Value = hi.ProductivityIndex;
			slMotivation.Value = hi.MotivationIndex;
			textBox1.Text = hi.IndexComment;
		}
	}
}
