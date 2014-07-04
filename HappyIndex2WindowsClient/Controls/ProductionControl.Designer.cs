﻿namespace HappyIndex2WindowsClient.Controls {
	partial class ProductionControl {
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
			this.label2 = new System.Windows.Forms.Label();
			this.slProduction = new HappyIndex2WindowsClient.Controls.SliderControl();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.slMotivation = new HappyIndex2WindowsClient.Controls.SliderControl();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 20);
			this.label2.TabIndex = 3;
			this.label2.Text = "label2";
			// 
			// slProduction
			// 
			this.slProduction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.slProduction.Location = new System.Drawing.Point(3, 43);
			this.slProduction.Name = "slProduction";
			this.slProduction.Size = new System.Drawing.Size(618, 48);
			this.slProduction.TabIndex = 5;
			this.slProduction.Value = 1D;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Productivity";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(4, 81);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Motivation";
			// 
			// slMotivation
			// 
			this.slMotivation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.slMotivation.Location = new System.Drawing.Point(3, 100);
			this.slMotivation.Name = "slMotivation";
			this.slMotivation.Size = new System.Drawing.Size(618, 45);
			this.slMotivation.TabIndex = 7;
			this.slMotivation.Value = 1D;
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.HideSelection = false;
			this.textBox1.Location = new System.Drawing.Point(4, 146);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox1.Size = new System.Drawing.Size(608, 91);
			this.textBox1.TabIndex = 9;
			// 
			// ProductionControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.slMotivation);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.slProduction);
			this.Controls.Add(this.label2);
			this.Name = "ProductionControl";
			this.Size = new System.Drawing.Size(625, 252);
			this.Load += new System.EventHandler(this.ProductionControl_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private SliderControl slProduction;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private SliderControl slMotivation;
		private System.Windows.Forms.TextBox textBox1;
	}
}
