using System;
using System.Collections.Generic;

namespace HappyIndex2.Common {
	public static class Extensions {
		public static string Format( this DateTime ths ) {
			return ths.ToString( "yyyy-MM-dd" );
		}
		public static string Remove( this string ths, string that ) {
			if( string.IsNullOrEmpty( ths ) || !ths.Contains( that ) ) {
				return ths;
			}
			return ths.Replace( that, "" );
		}
		public static string IfTrue( this string ths, bool condition ) {
			return condition ? ths : null;
		}
		public static string FillBlanks( this string formatString, params object[] parameters ) {
			return string.Format( formatString, parameters );
		}
		public static string ToString( this List<object> ths, string separator ) {
			return string.Join( separator, ths );
		}
		public static string ToString( this List<string> ths, string separator ) {
			return string.Join( separator, ths );
		}
		public static int ToInt( this string ths, int defaultIfFail = 0 ) {
			int i;
			return int.TryParse( ths, out i ) ? i : defaultIfFail;
		}
	}
}
