using System.Collections;

namespace HappyIndex2.Common {
	public class UserList : BaseDistinctCollection<User> {
		private Hashtable sidHash = new Hashtable();
		#region public User this[ string sid ]
		/// <summary>
		/// Gets the <see cref="User"/> item identified by the given arguments of the UserList
		/// </summary>
		/// <value></value>
		public User this[ string sid ] {
			get {
				return (User)sidHash[ sid ];
			}
		}
		#endregion

		#region public override void AddDistinct( User item )
		/// <summary>
		/// 
		/// </summary>
		/// <param name="item"></param>
		public override void AddDistinct( User item ) {
			if( item == null ) {
				return;
			}
			if( sidHash.ContainsKey( item.SID ) ) {
				return;
			}
			sidHash[ item.SID ] = item;
			base.AddDistinct( item );
		}
		#endregion
		#region public bool ContainsSID( string sid )
		/// <summary>
		/// Returns a value indicating whether the specified <see cref="System.String"/>
		///  is contained in the <see cref="HappyIndex2.Common.UserList"/>.
		/// </summary>
		/// <param name="sid">The <see cref="System.String"/> to locate in the 
		/// <see cref="HappyIndex2.Common.UserList"/>.</param>
		/// <returns><b>true</b> if the <i>String</i> parameter is a member 
		/// of the <see cref="HappyIndex2.Common.UserList"/>; otherwise, <b>false</b>.</returns>
		public bool ContainsSID( string sid ) {
			return !string.IsNullOrEmpty( sid ) && sidHash.ContainsKey( sid );
		}
		#endregion
	}
}
