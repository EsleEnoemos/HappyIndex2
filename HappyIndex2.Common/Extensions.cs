using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;

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
		public static void Add( this ControlCollection ths, string that ) {
			ths.Add( new LiteralControl( that ) );
		}
		public static int GetWeekNumber( this DateTime ths ) {
			DateTimeFormatInfo di = DateTimeFormatInfo.CurrentInfo ?? DateTimeFormatInfo.InvariantInfo;
			return di.Calendar.GetWeekOfYear( ths, di.CalendarWeekRule, DayOfWeek.Monday );
		}
		public static DateTime GetFirstDateOfWeek( int weekNumber ) {
			DateTime dt = new DateTime( DateTime.Now.Year, 1, 1 );
			// mult by 7 to get the day number of the year
			int days = (weekNumber - 1) * 7;
			// get the date of that day
			DateTime dt1 = dt.AddDays( days );
			// check what day of week it is
			DayOfWeek dow = dt1.DayOfWeek;
			// to get the first day of that week - subtract the value of the DayOfWeek enum from the date
			DateTime startDateOfWeek = dt1.AddDays( -(((int)dow) - 1) );
			return startDateOfWeek;
		}
	}
}
