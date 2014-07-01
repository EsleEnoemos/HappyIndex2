namespace HappyIndexService.Data {
	public static class Exts {
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
	}
}