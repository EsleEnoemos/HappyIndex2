using System;
using System.Collections.Generic;
using System.Text;

namespace HappyIndex2.Common {
	public class User {
		public string Name;
		public string Email;
		#region public List<string> Groups
		/// <summary>
		/// Get/Sets the Groups of the User
		/// </summary>
		/// <value></value>
		public List<string> Groups {
			get { return _groups ?? (_groups = new List<string>()); }
			set { _groups = value; }
		}
		private List<string> _groups;
		#endregion

		public override string ToString() {
			StringBuilder sb = new StringBuilder(string.Format( "[{0}]<{1}>", Name, Email ));
			if( Groups != null ) {
				sb.AppendLine();
				sb.Append( string.Join( Environment.NewLine, Groups ) );
			}
			return sb.ToString();
		}
	}
}
