using System;
using System.Windows.Forms;
using HappyIndex2.Common;

namespace HappyIndex2WindowsClient.Controls {
	public partial class DateSelectorControl : UserControl {
		private DateTime date = DateTime.Now;
		#region public DateTime Value
		/// <summary>
		/// Get/Sets the Value of the DateSelectorControl
		/// </summary>
		/// <value></value>
		public DateTime Value {
			get {
				return date;
			}
			set {
				date = value;
				UpdateValue();
			}
		}
		#endregion

		public DateSelectorControl() {
			InitializeComponent();
		}

		private void btnDown_Click( object sender, EventArgs e ) {
			date = date.AddDays( -1 );
			UpdateValue();
			OnValueChanged( new EventArgs() );
		}

		private void btnUp_Click( object sender, EventArgs e ) {
			date = date.AddDays( 1 );
			UpdateValue();
			OnValueChanged( new EventArgs() );
		}
		private void UpdateValue() {
			label1.Text = date.Format();
		}

		private void DateSelectorControl_Load( object sender, EventArgs e ) {
			UpdateValue();
		}
		#region public event EventHandler ValueChanged
		/// <summary>
		/// This event is fired when the Value property is changed.
		/// </summary>
		public event EventHandler ValueChanged;
		#endregion
		#region protected virtual void OnValueChanged(EventArgs e)
		/// <summary>
		/// Notifies the listeners of the ValueChanged event
		/// </summary>
		/// <param name="e">The argument to send to the listeners</param>
		protected virtual void OnValueChanged(EventArgs e) {
			if(ValueChanged != null) {
				ValueChanged(this,e);
			}
		}
		#endregion

		private void label1_DoubleClick( object sender, EventArgs e ) {
			DatePickerDialog f = new DatePickerDialog(Value);
			if( f.ShowDialog( this ) == DialogResult.OK ) {
				Value = f.Value;
				OnValueChanged( new EventArgs() );
			}
		}
	}
}
