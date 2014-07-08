namespace HappyIndex2.Common {
	public class User : IUniqueID {
		#region public int ID
		/// <summary>
		/// Get/Sets the ID of the User
		/// </summary>
		/// <value></value>
		public int ID {
			get { return _iD; }
			set { _iD = value; }
		}
		private int _iD;
		#endregion
		#region public string Name
		/// <summary>
		/// Get/Sets the Name of the User
		/// </summary>
		/// <value></value>
		public string Name {
			get { return _name; }
			set { _name = value; }
		}
		private string _name;
		#endregion
		#region public string SID
		/// <summary>
		/// Get/Sets the SID of the User
		/// </summary>
		/// <value></value>
		public string SID {
			get { return _sID; }
			set { _sID = value; }
		}
		private string _sID;
		#endregion
		#region public TeamList Teams
		/// <summary>
		/// Get/Sets the Teams of the User
		/// </summary>
		/// <value></value>
		public TeamList Teams {
			get { return _teams ?? (_teams = new TeamList()); }
			set { _teams = value; }
		}
		private TeamList _teams;
		#endregion

		public override string ToString() {
			return string.Format( "[{0}]<{1}>", Name, string.Join( ",", Teams ) );
		}
	}
}
