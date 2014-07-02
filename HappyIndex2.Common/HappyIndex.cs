﻿using System;
using System.Web.Script.Serialization;

namespace HappyIndex2.Common {
	public class HappyIndex {
		#region public int ID
		/// <summary>
		/// Get/Sets the ID of the HappyIndex
		/// </summary>
		/// <value></value>
		public int ID {
			get { return _iD; }
			set { _iD = value; }
		}
		private int _iD;
		#endregion
		#region public DateTime Date
		/// <summary>
		/// Get/Sets the Date of the HappyIndex
		/// </summary>
		/// <value></value>
		[ScriptIgnore]
		public DateTime Date {
			get { return _date; }
			set { _date = value; }
		}
		private DateTime _date;
		#endregion
		#region public double EmotionalIndex
		/// <summary>
		/// Get/Sets the EmotionalIndex of the HappyIndex
		/// </summary>
		/// <value></value>
		public double EmotionalIndex {
			get { return _emotionalIndex; }
			set { _emotionalIndex = value; }
		}
		private double _emotionalIndex;
		#endregion
		#region public string EmotionalComment
		/// <summary>
		/// Get/Sets the EmotionalComment of the HappyIndex
		/// </summary>
		/// <value></value>
		public string EmotionalComment {
			get { return _emotionalComment; }
			set { _emotionalComment = value; }
		}
		private string _emotionalComment;
		#endregion
		#region public string DateString
		/// <summary>
		/// Get/Sets the DateString of the HappyIndex
		/// </summary>
		/// <value></value>
		public string DateString {
			get {
				return Date.Format();
			}
			set {
				DateTime d;
				DateTime.TryParse( value, out d );
				Date = d;
			}
		}
		#endregion

		#region public double ProductivityIndex
		/// <summary>
		/// Get/Sets the ProductivityIndex of the HappyIndex
		/// </summary>
		/// <value></value>
		public double ProductivityIndex {
			get { return _productivityIndex; }
			set { _productivityIndex = value; }
		}
		private double _productivityIndex;
		#endregion
		#region public double MotivationIndex
		/// <summary>
		/// Get/Sets the MotivationIndex of the HappyIndex
		/// </summary>
		/// <value></value>
		public double MotivationIndex {
			get { return _motivationIndex; }
			set { _motivationIndex = value; }
		}
		private double _motivationIndex;
		#endregion
		#region public string IndexComment
		/// <summary>
		/// Get/Sets the IndexComment of the HappyIndex
		/// </summary>
		/// <value></value>
		public string IndexComment {
			get { return _indexComment; }
			set { _indexComment = value; }
		}
		private string _indexComment;
		#endregion
	}
}
