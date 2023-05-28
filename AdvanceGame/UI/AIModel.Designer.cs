namespace AdvanceGame.UI
{
	partial class AIModel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblTitle = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.lblIntensity = new System.Windows.Forms.Label();
			this.btnSelect = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblTitle.Location = new System.Drawing.Point(1, 0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(103, 30);
			this.lblTitle.TabIndex = 0;
			this.lblTitle.Text = "AI Level 4";
			// 
			// txtDescription
			// 
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtDescription.BackColor = System.Drawing.SystemColors.Control;
			this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.txtDescription.Enabled = false;
			this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.txtDescription.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.txtDescription.Location = new System.Drawing.Point(25, 32);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.ReadOnly = true;
			this.txtDescription.Size = new System.Drawing.Size(702, 93);
			this.txtDescription.TabIndex = 1;
			this.txtDescription.TabStop = false;
			this.txtDescription.Text = "Advance AI bot implemented to level 4 (Pass).\r\nSelects a random valid move each t" +
    "urn. Does not employ any strategy or move planning or prediction.\r\n";
			// 
			// lblIntensity
			// 
			this.lblIntensity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblIntensity.AutoSize = true;
			this.lblIntensity.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblIntensity.Location = new System.Drawing.Point(24, 120);
			this.lblIntensity.Name = "lblIntensity";
			this.lblIntensity.Size = new System.Drawing.Size(256, 25);
			this.lblIntensity.TabIndex = 2;
			this.lblIntensity.Text = "Computational Intensity: Low";
			// 
			// btnSelect
			// 
			this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSelect.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnSelect.Location = new System.Drawing.Point(650, 117);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(77, 30);
			this.btnSelect.TabIndex = 3;
			this.btnSelect.Text = "Select";
			this.btnSelect.UseVisualStyleBackColor = true;
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// AIModel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.btnSelect);
			this.Controls.Add(this.lblIntensity);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.lblTitle);
			this.Name = "AIModel";
			this.Size = new System.Drawing.Size(730, 151);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label lblTitle;
		private TextBox txtDescription;
		private Label lblIntensity;
		private Button btnSelect;
	}
}
