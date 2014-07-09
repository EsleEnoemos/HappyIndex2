namespace HappyIndex2.Common {
	public class Team : IUniqueID {
		#region public int ID
		/// <summary>
		/// Get/Sets the ID of the Team
		/// </summary>
		/// <value></value>
		public int ID {
			get {
				return _iD;
			}
			set {
				_iD = value;
			}
		}
		private int _iD;
		#endregion
		#region public string Name
		/// <summary>
		/// Get/Sets the Name of the Team
		/// </summary>
		/// <value></value>
		public string Name {
			get {
				return _name;
			}
			set {
				_name = value;
			}
		}
		private string _name;
		#endregion
		#region public override string ToString()
		/// <summary>
		/// Returns a <see cref="string"/> that represents the current <see cref="HappyIndex2.Common.Team"/>.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current <see cref="HappyIndex2.Common.Team"/>.</returns>
		public override string ToString() {
			return Name;
		}
		#endregion
	}
}