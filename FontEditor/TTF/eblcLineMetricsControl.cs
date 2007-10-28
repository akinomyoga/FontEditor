namespace FontEditor.TTF{
	public sealed class eblcLineMetricsControl:System.Windows.Forms.UserControl{
		public eblcLineMetricsControl(){
			this.InitializeComponent();
		}
		private bool isVertical=false;
		[System.ComponentModel.DefaultValue(false)]
		public bool Vertical{
			get{return this.isVertical;}
			set{
				this.isVertical=value;
				this.pictureBox1.Image=
					value
					?FontEditor.Properties.Resources.VerticalMetrics
					:FontEditor.Properties.Resources.HorizontalMetrics;
			}
		}
		public void SetLineMetrics(sbitLineMetrics lmetrics){
			this.lAscender.Text=lmetrics.ascender.ToString();
			this.lDescender.Text=lmetrics.descender.ToString();
			this.lMinOriginSB.Text=lmetrics.minOriginSB.ToString();
			this.lMinAdvanceSB.Text=lmetrics.minAdvanceSB.ToString();
			this.lMaxBeforeBL.Text=lmetrics.maxBeforeBL.ToString();
			this.lMinAfterBL.Text=lmetrics.minAfterBL.ToString();
			this.lCaretSlopeNumerator.Text=lmetrics.caretSlopeNumerator.ToString();
			this.lCaretSlopeDenominator.Text=lmetrics.caretSlopeDenominator.ToString();
			this.lCaretOffset.Text=lmetrics.caretOffset.ToString();
		}
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label lMinAdvanceSB;
		private System.Windows.Forms.Label lMinOriginSB;
		private System.Windows.Forms.Label lMaxBeforeBL;
		private System.Windows.Forms.Label lMinAfterBL;
		private System.Windows.Forms.Label lCaretSlopeNumerator;
		private System.Windows.Forms.Label lCaretOffset;
		private System.Windows.Forms.Label lMaxWidth;
		private System.Windows.Forms.Label lDescender;
		private System.Windows.Forms.Label lAscender;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label lCaretSlopeDenominator;
		private System.Windows.Forms.Label label2;
		private void InitializeComponent() {
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.lMinAdvanceSB = new System.Windows.Forms.Label();
			this.lMinOriginSB = new System.Windows.Forms.Label();
			this.lMaxBeforeBL = new System.Windows.Forms.Label();
			this.lMinAfterBL = new System.Windows.Forms.Label();
			this.lCaretSlopeNumerator = new System.Windows.Forms.Label();
			this.lCaretOffset = new System.Windows.Forms.Label();
			this.lMaxWidth = new System.Windows.Forms.Label();
			this.lDescender = new System.Windows.Forms.Label();
			this.lAscender = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.lCaretSlopeDenominator = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(295,6);
			this.label1.Margin = new System.Windows.Forms.Padding(3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51,12);
			this.label1.TabIndex = 0;
			this.label1.Text = "ascender";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(295,23);
			this.label2.Margin = new System.Windows.Forms.Padding(3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57,12);
			this.label2.TabIndex = 1;
			this.label2.Text = "descender";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::FontEditor.Properties.Resources.HorizontalMetrics;
			this.pictureBox1.InitialImage = null;
			this.pictureBox1.Location = new System.Drawing.Point(3,3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(286,207);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(295,41);
			this.label3.Margin = new System.Windows.Forms.Padding(3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(54,12);
			this.label3.TabIndex = 3;
			this.label3.Text = "maxWidth";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(295,59);
			this.label4.Margin = new System.Windows.Forms.Padding(3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(68,12);
			this.label4.TabIndex = 4;
			this.label4.Text = "minOriginSB";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(295,77);
			this.label5.Margin = new System.Windows.Forms.Padding(3);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(82,12);
			this.label5.TabIndex = 5;
			this.label5.Text = "minAdvanceSB";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(295,95);
			this.label6.Margin = new System.Windows.Forms.Padding(3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(74,12);
			this.label6.TabIndex = 6;
			this.label6.Text = "maxBeforeBL";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(295,113);
			this.label7.Margin = new System.Windows.Forms.Padding(3);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(63,12);
			this.label7.TabIndex = 7;
			this.label7.Text = "minAfterBL";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(295,131);
			this.label8.Margin = new System.Windows.Forms.Padding(3);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(59,12);
			this.label8.TabIndex = 8;
			this.label8.Text = "caretSlope";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(295,149);
			this.label9.Margin = new System.Windows.Forms.Padding(3);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(63,12);
			this.label9.TabIndex = 9;
			this.label9.Text = "caretOffset";
			// 
			// lMinAdvanceSB
			// 
			this.lMinAdvanceSB.AutoSize = true;
			this.lMinAdvanceSB.Location = new System.Drawing.Point(383,77);
			this.lMinAdvanceSB.Margin = new System.Windows.Forms.Padding(3);
			this.lMinAdvanceSB.Name = "lMinAdvanceSB";
			this.lMinAdvanceSB.Size = new System.Drawing.Size(11,12);
			this.lMinAdvanceSB.TabIndex = 10;
			this.lMinAdvanceSB.Text = "0";
			// 
			// lMinOriginSB
			// 
			this.lMinOriginSB.AutoSize = true;
			this.lMinOriginSB.Location = new System.Drawing.Point(383,59);
			this.lMinOriginSB.Margin = new System.Windows.Forms.Padding(3);
			this.lMinOriginSB.Name = "lMinOriginSB";
			this.lMinOriginSB.Size = new System.Drawing.Size(11,12);
			this.lMinOriginSB.TabIndex = 11;
			this.lMinOriginSB.Text = "0";
			// 
			// lMaxBeforeBL
			// 
			this.lMaxBeforeBL.AutoSize = true;
			this.lMaxBeforeBL.Location = new System.Drawing.Point(383,95);
			this.lMaxBeforeBL.Margin = new System.Windows.Forms.Padding(3);
			this.lMaxBeforeBL.Name = "lMaxBeforeBL";
			this.lMaxBeforeBL.Size = new System.Drawing.Size(11,12);
			this.lMaxBeforeBL.TabIndex = 12;
			this.lMaxBeforeBL.Text = "0";
			// 
			// lMinAfterBL
			// 
			this.lMinAfterBL.AutoSize = true;
			this.lMinAfterBL.Location = new System.Drawing.Point(383,113);
			this.lMinAfterBL.Margin = new System.Windows.Forms.Padding(3);
			this.lMinAfterBL.Name = "lMinAfterBL";
			this.lMinAfterBL.Size = new System.Drawing.Size(11,12);
			this.lMinAfterBL.TabIndex = 13;
			this.lMinAfterBL.Text = "0";
			// 
			// lCaretSlopeNumerator
			// 
			this.lCaretSlopeNumerator.AutoSize = true;
			this.lCaretSlopeNumerator.Location = new System.Drawing.Point(383,131);
			this.lCaretSlopeNumerator.Margin = new System.Windows.Forms.Padding(3);
			this.lCaretSlopeNumerator.Name = "lCaretSlopeNumerator";
			this.lCaretSlopeNumerator.Size = new System.Drawing.Size(11,12);
			this.lCaretSlopeNumerator.TabIndex = 14;
			this.lCaretSlopeNumerator.Text = "0";
			// 
			// lCaretOffset
			// 
			this.lCaretOffset.AutoSize = true;
			this.lCaretOffset.Location = new System.Drawing.Point(383,149);
			this.lCaretOffset.Margin = new System.Windows.Forms.Padding(3);
			this.lCaretOffset.Name = "lCaretOffset";
			this.lCaretOffset.Size = new System.Drawing.Size(11,12);
			this.lCaretOffset.TabIndex = 15;
			this.lCaretOffset.Text = "0";
			// 
			// lMaxWidth
			// 
			this.lMaxWidth.AutoSize = true;
			this.lMaxWidth.Location = new System.Drawing.Point(383,41);
			this.lMaxWidth.Margin = new System.Windows.Forms.Padding(3);
			this.lMaxWidth.Name = "lMaxWidth";
			this.lMaxWidth.Size = new System.Drawing.Size(11,12);
			this.lMaxWidth.TabIndex = 16;
			this.lMaxWidth.Text = "0";
			// 
			// lDescender
			// 
			this.lDescender.AutoSize = true;
			this.lDescender.Location = new System.Drawing.Point(383,23);
			this.lDescender.Margin = new System.Windows.Forms.Padding(3);
			this.lDescender.Name = "lDescender";
			this.lDescender.Size = new System.Drawing.Size(11,12);
			this.lDescender.TabIndex = 17;
			this.lDescender.Text = "0";
			// 
			// lAscender
			// 
			this.lAscender.AutoSize = true;
			this.lAscender.Location = new System.Drawing.Point(383,6);
			this.lAscender.Margin = new System.Windows.Forms.Padding(3);
			this.lAscender.Name = "lAscender";
			this.lAscender.Size = new System.Drawing.Size(11,12);
			this.lAscender.TabIndex = 18;
			this.lAscender.Text = "0";
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(400,131);
			this.label18.Margin = new System.Windows.Forms.Padding(3);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(11,12);
			this.label18.TabIndex = 19;
			this.label18.Text = "/";
			// 
			// lCaretSlopeDenominator
			// 
			this.lCaretSlopeDenominator.AutoSize = true;
			this.lCaretSlopeDenominator.Location = new System.Drawing.Point(417,131);
			this.lCaretSlopeDenominator.Margin = new System.Windows.Forms.Padding(3);
			this.lCaretSlopeDenominator.Name = "lCaretSlopeDenominator";
			this.lCaretSlopeDenominator.Size = new System.Drawing.Size(11,12);
			this.lCaretSlopeDenominator.TabIndex = 20;
			this.lCaretSlopeDenominator.Text = "0";
			// 
			// eblcLineMetricsControl
			// 
			this.Controls.Add(this.lCaretSlopeDenominator);
			this.Controls.Add(this.label18);
			this.Controls.Add(this.lAscender);
			this.Controls.Add(this.lDescender);
			this.Controls.Add(this.lMaxWidth);
			this.Controls.Add(this.lCaretOffset);
			this.Controls.Add(this.lCaretSlopeNumerator);
			this.Controls.Add(this.lMinAfterBL);
			this.Controls.Add(this.lMaxBeforeBL);
			this.Controls.Add(this.lMinOriginSB);
			this.Controls.Add(this.lMinAdvanceSB);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "eblcLineMetricsControl";
			this.Size = new System.Drawing.Size(560,217);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}