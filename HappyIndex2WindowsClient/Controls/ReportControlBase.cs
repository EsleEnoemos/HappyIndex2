using System.Windows.Forms;
using HappyIndex2.Common;

namespace HappyIndex2WindowsClient.Controls {
	public class ReportControlBase : UserControl {
		public virtual HappyIndex HappyIndex {
			get {
				return null;
			}
		}
	}
}
