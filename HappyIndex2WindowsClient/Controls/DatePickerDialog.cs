using System;
using System.Windows.Forms;

namespace HappyIndex2WindowsClient.Controls {
	public partial class DatePickerDialog : Form {
		#region public DateTime Value
		/// <summary>
		/// Gets the Value of the DatePickerDialog
		/// </summary>
		/// <value></value>
		public DateTime Value {
			get {
				return dateTimePicker1.Value;
			}
		}
		#endregion

		public DatePickerDialog()
			: this(DateTime.Now) {
		}
		public DatePickerDialog( DateTime d ) {
			InitializeComponent();
			dateTimePicker1.Value = d;
		}
	}
}
