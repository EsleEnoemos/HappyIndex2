namespace HappyIndex2WindowsClient.Controls {
	partial class DateSelectorControl {
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing ) {
			if( disposing && (components != null) ) {
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.label1 = new System.Windows.Forms.Label();
			this.btnDown = new System.Windows.Forms.Button();
			this.btnUp = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(37, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(362, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnDown
			// 
			this.btnDown.Location = new System.Drawing.Point(4, 0);
			this.btnDown.Name = "btnDown";
			this.btnDown.Size = new System.Drawing.Size(27, 23);
			this.btnDown.TabIndex = 1;
			this.btnDown.Text = "<";
			this.btnDown.UseVisualStyleBackColor = true;
			this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
			// 
			// btnUp
			// 
			this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUp.Location = new System.Drawing.Point(405, 0);
			this.btnUp.Name = "btnUp";
			this.btnUp.Size = new System.Drawing.Size(27, 23);
			this.btnUp.TabIndex = 2;
			this.btnUp.Text = ">";
			this.btnUp.UseVisualStyleBackColor = true;
			this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
			// 
			// DateSelectorControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnUp);
			this.Controls.Add(this.btnDown);
			this.Controls.Add(this.label1);
			this.Name = "DateSelectorControl";
			this.Size = new System.Drawing.Size(435, 33);
			this.Load += new System.EventHandler(this.DateSelectorControl_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnDown;
		private System.Windows.Forms.Button btnUp;
	}
}
