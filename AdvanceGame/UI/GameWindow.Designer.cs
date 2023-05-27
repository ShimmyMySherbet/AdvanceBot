namespace AdvanceGame.UI
{
	partial class GameWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pngRender = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pngRender)).BeginInit();
			this.SuspendLayout();
			// 
			// pngRender
			// 
			this.pngRender.Location = new System.Drawing.Point(12, 23);
			this.pngRender.Name = "pngRender";
			this.pngRender.Size = new System.Drawing.Size(700, 700);
			this.pngRender.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pngRender.TabIndex = 0;
			this.pngRender.TabStop = false;
			// 
			// GameWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(732, 741);
			this.Controls.Add(this.pngRender);
			this.Name = "GameWindow";
			this.Text = "GameWindow";
			((System.ComponentModel.ISupportInitialize)(this.pngRender)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private PictureBox pngRender;
	}
}