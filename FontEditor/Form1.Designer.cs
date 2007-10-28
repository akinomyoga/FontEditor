namespace FontEditor {
	partial class Form1 {
		/// <summary>
		/// 必要なデザイナ変数です。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 使用中のリソースをすべてクリーンアップします。
		/// </summary>
		/// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows フォーム デザイナで生成されたコード

		/// <summary>
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
		private void InitializeComponent() {
			this.components=new System.ComponentModel.Container();
			this.treeView1=new System.Windows.Forms.TreeView();
			this.mainMenu1=new System.Windows.Forms.MainMenu(this.components);
			this.menuItem1=new System.Windows.Forms.MenuItem();
			this.menuItem2=new System.Windows.Forms.MenuItem();
			this.menuItem3=new System.Windows.Forms.MenuItem();
			this.openFileDialog1=new System.Windows.Forms.OpenFileDialog();
			this.splitContainer1=new System.Windows.Forms.SplitContainer();
			this.tabControl1=new System.Windows.Forms.TabControl();
			this.tabPage1=new System.Windows.Forms.TabPage();
			this.tabPage2=new System.Windows.Forms.TabPage();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// treeView1
			// 
			this.treeView1.Dock=System.Windows.Forms.DockStyle.Fill;
			this.treeView1.Location=new System.Drawing.Point(0,0);
			this.treeView1.Name="treeView1";
			this.treeView1.Size=new System.Drawing.Size(227,508);
			this.treeView1.TabIndex=0;
			this.treeView1.BeforeExpand+=new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
			this.treeView1.AfterSelect+=new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index=0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2,
            this.menuItem3});
			this.menuItem1.Text="ファイル(&F)";
			// 
			// menuItem2
			// 
			this.menuItem2.Index=0;
			this.menuItem2.Text="開く(&O)";
			this.menuItem2.Click+=new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index=1;
			this.menuItem3.Text="Fixed Test";
			this.menuItem3.Click+=new System.EventHandler(this.menuItem3_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName="openFileDialog1";
			this.openFileDialog1.Filter="フォントファイル|*.ttf;*.ttc;*.tte;*.bdf;*.otf|TrueType フォントファイル|*.ttf;*.ttc|OpenType|*.o"+
				"tf|BDF|*.bdf|全てのファイル|*.*";
			this.openFileDialog1.FilterIndex=5;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock=System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location=new System.Drawing.Point(3,3);
			this.splitContainer1.Name="splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.treeView1);
			this.splitContainer1.Size=new System.Drawing.Size(839,508);
			this.splitContainer1.SplitterDistance=227;
			this.splitContainer1.TabIndex=3;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock=System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location=new System.Drawing.Point(0,0);
			this.tabControl1.Name="tabControl1";
			this.tabControl1.SelectedIndex=0;
			this.tabControl1.Size=new System.Drawing.Size(853,539);
			this.tabControl1.TabIndex=4;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.splitContainer1);
			this.tabPage1.Location=new System.Drawing.Point(4,21);
			this.tabPage1.Name="tabPage1";
			this.tabPage1.Padding=new System.Windows.Forms.Padding(3);
			this.tabPage1.Size=new System.Drawing.Size(845,514);
			this.tabPage1.TabIndex=0;
			this.tabPage1.Text="tabPage1";
			this.tabPage1.UseVisualStyleBackColor=true;
			// 
			// tabPage2
			// 
			this.tabPage2.Location=new System.Drawing.Point(4,21);
			this.tabPage2.Name="tabPage2";
			this.tabPage2.Padding=new System.Windows.Forms.Padding(3);
			this.tabPage2.Size=new System.Drawing.Size(845,514);
			this.tabPage2.TabIndex=1;
			this.tabPage2.Text="Log";
			this.tabPage2.UseVisualStyleBackColor=true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions=new System.Drawing.SizeF(6F,12F);
			this.AutoScaleMode=System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize=new System.Drawing.Size(853,539);
			this.Controls.Add(this.tabControl1);
			this.Menu=this.mainMenu1;
			this.Name="Form1";
			this.Text="FontEditor alpha";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
	}
}

