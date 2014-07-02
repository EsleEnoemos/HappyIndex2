using System;
using HappyIndex2.Common;

namespace HappyIndex2WindowsClient.Controls {
	public partial class EmotionControl : ReportControlBase {
		private HappyIndex hi;
		public override HappyIndex HappyIndex {
			get {
				hi.EmotionalIndex = sliderControl1.Value;
				hi.EmotionalComment = textBox1.Text;
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
			label2.Text = hi.Date.Date == DateTime.Now.Date ? "How do you feel today?" : string.Format( "How did you feel {0}", hi.Date.Format() );
			textBox1.Text = hi.EmotionalComment;
		}
	}
}
