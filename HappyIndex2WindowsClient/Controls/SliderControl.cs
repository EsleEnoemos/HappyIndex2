using System;
using System.Windows.Forms;

namespace HappyIndex2WindowsClient.Controls {
	public partial class SliderControl : UserControl {
		public double Value {
			get {
				return ((trackBar1.Value * 1.0) / 10.0);
			}
			set {
				int i = (int)(value*10.0);
				if( i < trackBar1.Minimum ) {
					i = trackBar1.Minimum;
				}
				trackBar1.Value = i;
			}
		}
		public SliderControl() {
			InitializeComponent();
			UpdateValue();
		}
		private void UpdateValue() {
			label1.Text = Value.ToString( "F1" );
		}

		#region public event EventHandler Change
		/// <summary>
		/// This event is fired when 
		/// </summary>
		public event EventHandler Change;
		#endregion
		#region protected virtual void OnChange(EventArgs e)
		/// <summary>
		/// Notifies the listeners of the Change event
		/// </summary>
		/// <param name="e">The argument to send to the listeners</param>
		protected virtual void OnChange(EventArgs e) {
			if(Change != null) {
				Change(this,e);
			}
		}
		#endregion
		#region private void OnChange()
		/// <summary>
		/// 
		/// </summary>
		private void OnChange() {
			OnChange( new EventArgs() );
		}
		#endregion
		#region private void trackBar1_Scroll( object sender, EventArgs e )
		/// <summary>
		/// This method is called when the trackBar1's Scroll event has been fired.
		/// </summary>
		/// <param name="sender">The <see cref="object"/> that fired the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> of the event.</param>
		private void trackBar1_Scroll( object sender, EventArgs e ) {
			UpdateValue();
			OnChange();
		}
		#endregion
	}
}
