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
			this.gbTeam = new System.Windows.Forms.GroupBox();
			this.btnDebugBlack = new System.Windows.Forms.Button();
			this.btnDebugWhite = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnLoad = new System.Windows.Forms.Button();
			this.btnReset = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.lblWinner = new System.Windows.Forms.Label();
			this.lblTurn = new System.Windows.Forms.Label();
			this.gbWhite = new System.Windows.Forms.GroupBox();
			this.btnRunMove = new System.Windows.Forms.Button();
			this.lblWhiteStatus = new System.Windows.Forms.Label();
			this.lblWhiteMoves = new System.Windows.Forms.Label();
			this.btnSelectWhite = new System.Windows.Forms.Button();
			this.lblWhiteAI = new System.Windows.Forms.Label();
			this.cbWhiteAI = new System.Windows.Forms.CheckBox();
			this.gbBlack = new System.Windows.Forms.GroupBox();
			this.button2 = new System.Windows.Forms.Button();
			this.lblBlackStatus = new System.Windows.Forms.Label();
			this.lblBlackMoves = new System.Windows.Forms.Label();
			this.btnSelectBlack = new System.Windows.Forms.Button();
			this.lblBlack = new System.Windows.Forms.Label();
			this.cbBlackAI = new System.Windows.Forms.CheckBox();
			this.cbAutoPlay = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.pngRender)).BeginInit();
			this.gbTeam.SuspendLayout();
			this.gbWhite.SuspendLayout();
			this.gbBlack.SuspendLayout();
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
			// gbTeam
			// 
			this.gbTeam.Controls.Add(this.cbAutoPlay);
			this.gbTeam.Controls.Add(this.btnDebugBlack);
			this.gbTeam.Controls.Add(this.btnDebugWhite);
			this.gbTeam.Controls.Add(this.button3);
			this.gbTeam.Controls.Add(this.btnSave);
			this.gbTeam.Controls.Add(this.btnLoad);
			this.gbTeam.Controls.Add(this.btnReset);
			this.gbTeam.Controls.Add(this.button1);
			this.gbTeam.Controls.Add(this.lblWinner);
			this.gbTeam.Controls.Add(this.lblTurn);
			this.gbTeam.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.gbTeam.Location = new System.Drawing.Point(718, 23);
			this.gbTeam.Name = "gbTeam";
			this.gbTeam.Size = new System.Drawing.Size(262, 251);
			this.gbTeam.TabIndex = 1;
			this.gbTeam.TabStop = false;
			this.gbTeam.Text = "Game";
			// 
			// btnDebugBlack
			// 
			this.btnDebugBlack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDebugBlack.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnDebugBlack.Location = new System.Drawing.Point(131, 178);
			this.btnDebugBlack.Name = "btnDebugBlack";
			this.btnDebugBlack.Size = new System.Drawing.Size(121, 31);
			this.btnDebugBlack.TabIndex = 8;
			this.btnDebugBlack.Text = "Debug Black";
			this.btnDebugBlack.UseVisualStyleBackColor = true;
			this.btnDebugBlack.Click += new System.EventHandler(this.btnDebugBlack_Click);
			// 
			// btnDebugWhite
			// 
			this.btnDebugWhite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDebugWhite.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnDebugWhite.Location = new System.Drawing.Point(6, 178);
			this.btnDebugWhite.Name = "btnDebugWhite";
			this.btnDebugWhite.Size = new System.Drawing.Size(121, 31);
			this.btnDebugWhite.TabIndex = 7;
			this.btnDebugWhite.Text = "Debug White";
			this.btnDebugWhite.UseVisualStyleBackColor = true;
			this.btnDebugWhite.Click += new System.EventHandler(this.btnDebug_Click);
			// 
			// button3
			// 
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.button3.Location = new System.Drawing.Point(133, 141);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(121, 31);
			this.button3.TabIndex = 6;
			this.button3.Text = "Play Recording";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// btnSave
			// 
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.btnSave.Location = new System.Drawing.Point(6, 141);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(121, 31);
			this.btnSave.TabIndex = 5;
			this.btnSave.Text = "Save Recoding";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnLoad
			// 
			this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnLoad.Location = new System.Drawing.Point(134, 104);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(119, 31);
			this.btnLoad.TabIndex = 4;
			this.btnLoad.Text = "Load File";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// btnReset
			// 
			this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnReset.Location = new System.Drawing.Point(6, 104);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(121, 31);
			this.btnReset.TabIndex = 3;
			this.btnReset.Text = "Reset Board";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(6, 70);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(246, 31);
			this.button1.TabIndex = 2;
			this.button1.Text = "Undo Move";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// lblWinner
			// 
			this.lblWinner.AutoSize = true;
			this.lblWinner.Location = new System.Drawing.Point(13, 46);
			this.lblWinner.Name = "lblWinner";
			this.lblWinner.Size = new System.Drawing.Size(96, 21);
			this.lblWinner.TabIndex = 1;
			this.lblWinner.Text = "Winner: N/A";
			// 
			// lblTurn
			// 
			this.lblTurn.AutoSize = true;
			this.lblTurn.Location = new System.Drawing.Point(13, 25);
			this.lblTurn.Name = "lblTurn";
			this.lblTurn.Size = new System.Drawing.Size(147, 21);
			this.lblTurn.TabIndex = 0;
			this.lblTurn.Text = "Current Turn: White";
			// 
			// gbWhite
			// 
			this.gbWhite.Controls.Add(this.btnRunMove);
			this.gbWhite.Controls.Add(this.lblWhiteStatus);
			this.gbWhite.Controls.Add(this.lblWhiteMoves);
			this.gbWhite.Controls.Add(this.btnSelectWhite);
			this.gbWhite.Controls.Add(this.lblWhiteAI);
			this.gbWhite.Controls.Add(this.cbWhiteAI);
			this.gbWhite.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.gbWhite.Location = new System.Drawing.Point(718, 280);
			this.gbWhite.Name = "gbWhite";
			this.gbWhite.Size = new System.Drawing.Size(262, 136);
			this.gbWhite.TabIndex = 2;
			this.gbWhite.TabStop = false;
			this.gbWhite.Text = "White";
			// 
			// btnRunMove
			// 
			this.btnRunMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRunMove.Location = new System.Drawing.Point(131, 49);
			this.btnRunMove.Name = "btnRunMove";
			this.btnRunMove.Size = new System.Drawing.Size(112, 33);
			this.btnRunMove.TabIndex = 5;
			this.btnRunMove.Text = "Run Move";
			this.btnRunMove.UseVisualStyleBackColor = true;
			this.btnRunMove.Click += new System.EventHandler(this.btnRunMove_Click);
			// 
			// lblWhiteStatus
			// 
			this.lblWhiteStatus.AutoSize = true;
			this.lblWhiteStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblWhiteStatus.Location = new System.Drawing.Point(13, 116);
			this.lblWhiteStatus.Name = "lblWhiteStatus";
			this.lblWhiteStatus.Size = new System.Drawing.Size(73, 17);
			this.lblWhiteStatus.TabIndex = 4;
			this.lblWhiteStatus.Text = "Status: N/A";
			// 
			// lblWhiteMoves
			// 
			this.lblWhiteMoves.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblWhiteMoves.AutoSize = true;
			this.lblWhiteMoves.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblWhiteMoves.Location = new System.Drawing.Point(191, 15);
			this.lblWhiteMoves.Name = "lblWhiteMoves";
			this.lblWhiteMoves.Size = new System.Drawing.Size(61, 17);
			this.lblWhiteMoves.TabIndex = 3;
			this.lblWhiteMoves.Text = "Moves: 0";
			// 
			// btnSelectWhite
			// 
			this.btnSelectWhite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSelectWhite.Location = new System.Drawing.Point(13, 49);
			this.btnSelectWhite.Name = "btnSelectWhite";
			this.btnSelectWhite.Size = new System.Drawing.Size(112, 33);
			this.btnSelectWhite.TabIndex = 2;
			this.btnSelectWhite.Text = "Select AI";
			this.btnSelectWhite.UseVisualStyleBackColor = true;
			this.btnSelectWhite.Click += new System.EventHandler(this.btnSelectWhite_Click);
			// 
			// lblWhiteAI
			// 
			this.lblWhiteAI.AutoSize = true;
			this.lblWhiteAI.Location = new System.Drawing.Point(13, 25);
			this.lblWhiteAI.Name = "lblWhiteAI";
			this.lblWhiteAI.Size = new System.Drawing.Size(131, 21);
			this.lblWhiteAI.TabIndex = 1;
			this.lblWhiteAI.Text = "Selected AI: None";
			// 
			// cbWhiteAI
			// 
			this.cbWhiteAI.AutoSize = true;
			this.cbWhiteAI.Location = new System.Drawing.Point(13, 88);
			this.cbWhiteAI.Name = "cbWhiteAI";
			this.cbWhiteAI.Size = new System.Drawing.Size(120, 25);
			this.cbWhiteAI.TabIndex = 0;
			this.cbWhiteAI.Text = "AI Controlled";
			this.cbWhiteAI.UseVisualStyleBackColor = true;
			this.cbWhiteAI.CheckedChanged += new System.EventHandler(this.cbWhiteAI_CheckedChanged);
			// 
			// gbBlack
			// 
			this.gbBlack.Controls.Add(this.button2);
			this.gbBlack.Controls.Add(this.lblBlackStatus);
			this.gbBlack.Controls.Add(this.lblBlackMoves);
			this.gbBlack.Controls.Add(this.btnSelectBlack);
			this.gbBlack.Controls.Add(this.lblBlack);
			this.gbBlack.Controls.Add(this.cbBlackAI);
			this.gbBlack.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.gbBlack.Location = new System.Drawing.Point(718, 432);
			this.gbBlack.Name = "gbBlack";
			this.gbBlack.Size = new System.Drawing.Size(262, 132);
			this.gbBlack.TabIndex = 4;
			this.gbBlack.TabStop = false;
			this.gbBlack.Text = "Black";
			// 
			// button2
			// 
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.Location = new System.Drawing.Point(131, 49);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(112, 33);
			this.button2.TabIndex = 6;
			this.button2.Text = "Run Move";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// lblBlackStatus
			// 
			this.lblBlackStatus.AutoSize = true;
			this.lblBlackStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblBlackStatus.Location = new System.Drawing.Point(13, 111);
			this.lblBlackStatus.Name = "lblBlackStatus";
			this.lblBlackStatus.Size = new System.Drawing.Size(73, 17);
			this.lblBlackStatus.TabIndex = 5;
			this.lblBlackStatus.Text = "Status: N/A";
			// 
			// lblBlackMoves
			// 
			this.lblBlackMoves.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblBlackMoves.AutoSize = true;
			this.lblBlackMoves.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.lblBlackMoves.Location = new System.Drawing.Point(192, 15);
			this.lblBlackMoves.Name = "lblBlackMoves";
			this.lblBlackMoves.Size = new System.Drawing.Size(61, 17);
			this.lblBlackMoves.TabIndex = 3;
			this.lblBlackMoves.Text = "Moves: 0";
			// 
			// btnSelectBlack
			// 
			this.btnSelectBlack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSelectBlack.Location = new System.Drawing.Point(13, 49);
			this.btnSelectBlack.Name = "btnSelectBlack";
			this.btnSelectBlack.Size = new System.Drawing.Size(112, 33);
			this.btnSelectBlack.TabIndex = 2;
			this.btnSelectBlack.Text = "Select AI";
			this.btnSelectBlack.UseVisualStyleBackColor = true;
			this.btnSelectBlack.Click += new System.EventHandler(this.btnSelectBlack_Click);
			// 
			// lblBlack
			// 
			this.lblBlack.AutoSize = true;
			this.lblBlack.Location = new System.Drawing.Point(13, 25);
			this.lblBlack.Name = "lblBlack";
			this.lblBlack.Size = new System.Drawing.Size(131, 21);
			this.lblBlack.TabIndex = 1;
			this.lblBlack.Text = "Selected AI: None";
			// 
			// cbBlackAI
			// 
			this.cbBlackAI.AutoSize = true;
			this.cbBlackAI.Location = new System.Drawing.Point(13, 88);
			this.cbBlackAI.Name = "cbBlackAI";
			this.cbBlackAI.Size = new System.Drawing.Size(120, 25);
			this.cbBlackAI.TabIndex = 0;
			this.cbBlackAI.Text = "AI Controlled";
			this.cbBlackAI.UseVisualStyleBackColor = true;
			this.cbBlackAI.CheckedChanged += new System.EventHandler(this.cbBlackAI_CheckedChanged);
			// 
			// cbAutoPlay
			// 
			this.cbAutoPlay.AutoSize = true;
			this.cbAutoPlay.Location = new System.Drawing.Point(7, 215);
			this.cbAutoPlay.Name = "cbAutoPlay";
			this.cbAutoPlay.Size = new System.Drawing.Size(95, 25);
			this.cbAutoPlay.TabIndex = 9;
			this.cbAutoPlay.Text = "Auto Play";
			this.cbAutoPlay.UseVisualStyleBackColor = true;
			this.cbAutoPlay.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// GameWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(992, 741);
			this.Controls.Add(this.gbBlack);
			this.Controls.Add(this.gbWhite);
			this.Controls.Add(this.gbTeam);
			this.Controls.Add(this.pngRender);
			this.Name = "GameWindow";
			this.Text = "GameWindow";
			((System.ComponentModel.ISupportInitialize)(this.pngRender)).EndInit();
			this.gbTeam.ResumeLayout(false);
			this.gbTeam.PerformLayout();
			this.gbWhite.ResumeLayout(false);
			this.gbWhite.PerformLayout();
			this.gbBlack.ResumeLayout(false);
			this.gbBlack.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private PictureBox pngRender;
		private GroupBox gbTeam;
		private GroupBox gbWhite;
		private CheckBox cbWhiteAI;
		private Label lblWhiteMoves;
		private Button btnSelectWhite;
		private Label lblWhiteAI;
		private GroupBox gbBlack;
		private Label lblBlackMoves;
		private Button btnSelectBlack;
		private Label lblBlack;
		private CheckBox cbBlackAI;
		private Label lblTurn;
		private Label lblWhiteStatus;
		private Label lblBlackStatus;
		private Label lblWinner;
		private Button btnReset;
		private Button button1;
		private Button btnLoad;
		private Button button3;
		private Button btnSave;
		private Button btnRunMove;
		private Button button2;
		private Button btnDebugWhite;
		private Button btnDebugBlack;
		private CheckBox cbAutoPlay;
	}
}