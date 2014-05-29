using System;
using System.Collections.Generic;
using System.Text;

namespace HappyIndex2.Common {
	public class User {
		public string Name;
		public string Email;
		public List<string> Groups;

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
