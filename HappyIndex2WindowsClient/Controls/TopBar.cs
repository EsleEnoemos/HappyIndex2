using System;
using System.Drawing;
using System.Windows.Forms;

namespace HappyIndex2WindowsClient.Controls {
	public partial class TopBar : UserControl {
		#region public bool AutoHideOnClose
		/// <summary>
		/// If true, clicking the close-button will slide the form downwards until it is hidden.
		/// When this is done, SlideDownComplete will be fired
		/// Setting this to true will cause the Close-event not to be fired
		/// Calling <see cref="SlideUp"/> will slide the form back up
		/// </summary>
		/// <value></value>
		public bool AutoHideOnClose {
			get { return _autoHideOnClose; }
			set { _autoHideOnClose = value; }
		}
		private bool _autoHideOnClose;
		#endregion
		#region public bool EnableFormMove
		/// <summary>
		/// If true, the user can click this control and drag it to move the window
		/// </summary>
		/// <value></value>
		public bool EnableFormMove {
			get { return _enableFormMove; }
			set { _enableFormMove = value; }
		}
		private bool _enableFormMove = true;
		#endregion
		private Form form;
		private bool down;
		private int downX;
		private int downY;
		private int targetTop;
		private int absBottom;
		private const int slideSize = 20;
		private bool working;

		#region public event EventHandler Close
		/// <summary>
		/// This event is fired when 
		/// </summary>
		public event EventHandler Close;
		#endregion
		#region protected virtual void OnClose(EventArgs e)
		/// <summary>
		/// Notifies the listeners of the Close event
		/// </summary>
		/// <param name="e">The argument to send to the listeners</param>
		protected virtual void OnClose(EventArgs e) {
			if(Close != null) {
				Close(this,e);
			}
		}
		#endregion
		#region public event EventHandler SlideDownComplete
		/// <summary>
		/// This event is fired when 
		/// </summary>
		public event EventHandler SlideDownComplete;
		#endregion
		#region protected virtual void OnSlideDownComplete(EventArgs e)
		/// <summary>
		/// Notifies the listeners of the SlideDownComplete event
		/// </summary>
		/// <param name="e">The argument to send to the listeners</param>
		protected virtual void OnSlideDownComplete(EventArgs e) {
			if(SlideDownComplete != null) {
				SlideDownComplete(this,e);
			}
		}
		#endregion

		
		public TopBar() {
			InitializeComponent();
			btnClose.MouseEnter += btnClose_MouseEnter;
			btnClose.MouseLeave += btnClose_MouseLeave;
			MouseDown += TopBarMouseDown;
			MouseMove += TopBarMouseMove;
			MouseUp += TopBarMouseUp;
		}
		#region private void TopBar_Load( object sender, EventArgs e )
		/// <summary>
		/// This method is called when the TopBar's Load event has been fired.
		/// </summary>
		/// <param name="sender">The <see cref="object"/> that fired the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> of the event.</param>
		private void TopBar_Load( object sender, EventArgs e ) {
			Control parent = Parent;
			while( parent != null ) {
				if( parent is Form ) {
					form = (Form)parent;
					break;
				}
				parent = parent.Parent;
			}
			if( form == null ) {
				return;
			}
			if( AutoHideOnClose ) {
				Rectangle wa = Screen.PrimaryScreen.WorkingArea;
				form.Top = wa.Bottom - form.Height;
				form.Left = wa.Width - form.Width;
			}
		}
		#endregion

		#region public void SlideUp()
		/// <summary>
		/// 
		/// </summary>
		public void SlideUp() {
			if( working ) {
				return;
			}
			working = true;
			btnClose.Enabled = false;
			form.Visible = true;
			Rectangle bounds = Screen.PrimaryScreen.WorkingArea;
			targetTop = bounds.Height - form.Height;
			absBottom = Screen.PrimaryScreen.Bounds.Height;
			Location = new Point( bounds.Width - form.Width, absBottom );
			Timer showTimer = new Timer();
			showTimer.Interval = 1;
			showTimer.Tick += SlideUpTimerOnTick;
			showTimer.Start();
		}
		#endregion
		#region private void SlideUpTimerOnTick( object sender, EventArgs eventArgs )
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		private void SlideUpTimerOnTick( object sender, EventArgs eventArgs ) {
			if( form.Top <= targetTop ) {
				Timer timer = (Timer)sender;
				timer.Stop();
				form.Top = targetTop;
				btnClose.Enabled = true;
				working = false;
				return;
			}
			if( (form.Top - slideSize) <= targetTop ) {
				Timer timer = (Timer)sender;
				timer.Stop();
				form.Top = targetTop;
				btnClose.Enabled = true;
				working = false;
				return;
			}
			form.Top -= slideSize;
		}
		#endregion
		#region public void SlideDown()
		/// <summary>
		/// 
		/// </summary>
		public void SlideDown() {
			if( working ) {
				return;
			}
			working = true;
			form.Visible = true;
			absBottom = Screen.PrimaryScreen.Bounds.Height;
			Timer hideTimer = new Timer();
			hideTimer.Interval = 1;
			hideTimer.Tick += SlideDownTimerOnTick;
			hideTimer.Start();
		}
		#endregion
		#region private void SlideDownTimerOnTick( object sender, EventArgs e )
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SlideDownTimerOnTick( object sender, EventArgs e ) {
			if( form.Top >= absBottom ) {
				Timer timer = (Timer)sender;
				timer.Stop();
				form.Top = absBottom;
				form.Visible = false;
				btnClose.Enabled = true;
				working = false;
				OnSlideDownComplete( new EventArgs() );
				return;
			}
			form.Top += slideSize;
		}
		#endregion

		#region private void btnClose_Click( object sender, EventArgs e )
		/// <summary>
		/// This method is called when the btnClose's Click event has been fired.
		/// </summary>
		/// <param name="sender">The <see cref="object"/> that fired the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> of the event.</param>
		private void btnClose_Click( object sender, EventArgs e ) {
			if( AutoHideOnClose ) {
				btnClose.Enabled = false;
				SlideDown();
				return;
			}
			OnClose( new EventArgs() );
		}
		#endregion
		#region private void btnClose_MouseEnter( object sender, EventArgs e )
		/// <summary>
		/// This method is called when the btnClose's MouseEnter event has been fired.
		/// </summary>
		/// <param name="sender">The <see cref="object"/> that fired the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> of the event.</param>
		private void btnClose_MouseEnter( object sender, EventArgs e ) {
			btnClose.ForeColor = Color.Red;
		}
		#endregion
		#region private void btnClose_MouseLeave( object sender, EventArgs e )
		/// <summary>
		/// This method is called when the btnClose's MouseLeave event has been fired.
		/// </summary>
		/// <param name="sender">The <see cref="object"/> that fired the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> of the event.</param>
		private void btnClose_MouseLeave( object sender, EventArgs e ) {
			btnClose.ForeColor = Color.Black;
		}
		#endregion
		#region private void TopBarMouseMove( object sender, MouseEventArgs e )
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TopBarMouseMove( object sender, MouseEventArgs e ) {
			if( !down ) {
				return;
			}
			form.Location = new Point( form.Location.X + e.X - downX, form.Location.Y + e.Y - downY );
		}
		#endregion
		#region private void TopBarMouseDown( object sender, MouseEventArgs e )
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TopBarMouseDown( object sender, MouseEventArgs e ) {
			if( !EnableFormMove ) {
				return;
			}
			down = true;
			downX = e.X;
			downY = e.Y;
			Cursor = Cursors.NoMove2D;
		}
		#endregion
		#region private void TopBarMouseUp( object sender, MouseEventArgs e )
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TopBarMouseUp( object sender, MouseEventArgs e ) {
			down = false;
			Cursor = Cursors.Default;
		}
		#endregion
	}
}
