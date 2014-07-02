using System;

namespace HappyIndex2.Common {
	public static class Exts {
		public static string Format( this DateTime ths ) {
			return ths.ToString( "yyyy-MM-dd" );
		}
	}
}
