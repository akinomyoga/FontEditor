namespace FontEditor.TTF{
	public sealed class ttffileControl:System.Windows.Forms.UserControl{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lFilename;
		private System.Windows.Forms.Label lLocation;
		private System.Windows.Forms.Label lVersion;
		private System.Windows.Forms.Label lNumTables;
		private System.Windows.Forms.Label lSearchRange;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label lEntrySelector;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label lRangeShift;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private TTFFile file=null;
		public TTFFile File{
			get{return this.file;}
			set{
				this.file=value;
				if(value!=null)unsafe{
					this.lFilename.Text=value.FileName;
					this.lLocation.Text=value.DirectoryName;
					fixed(byte* img=&this.file.image[0]){
						OffsetTable* header=(OffsetTable*)img;
						this.lVersion.Text=((double)header->sfntversion).ToString();
						this.lNumTables.Text=((ushort)header->numTables).ToString();
						this.lSearchRange.Text=((ushort)header->searchRange).ToString();
						this.lEntrySelector.Text=((ushort)header->entrySelector).ToString();
						this.lRangeShift.Text=((ushort)header->rangeShift).ToString();
						TableDirectoryEntry* entry=(TableDirectoryEntry*)(header+1);
						TableDirectoryEntry* mEntry=entry+(ushort)header->numTables;
						string[] items;
						bool sum_ok;
						System.Windows.Forms.ListViewItem lvitem;
						this.listView1.Items.Clear();
						while(entry<mEntry){
							sum_ok=entry->Check(this.file.image);
							items=new string[]{
								entry->TagName,entry->offset.ToString()+"B",entry->length.ToString()+"B"
								,"0x"+((uint)entry->checkSum).ToString("X8")+(sum_ok?" OK":"×")
							};
							lvitem=new System.Windows.Forms.ListViewItem(items);
							lvitem.UseItemStyleForSubItems=false;
							lvitem.SubItems[3].BackColor=unchecked(sum_ok?System.Drawing.Color.FromArgb((int)0xffeeffee):System.Drawing.Color.FromArgb((int)0xffffeeee));
							this.listView1.Items.Add(lvitem);
							entry++;
						}
					}
				}
			}
		}
		public ttffileControl():base(){
			this.InitializeComponent();
		}

		#region Designer Code
		private void InitializeComponent() {
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lFilename = new System.Windows.Forms.Label();
			this.lLocation = new System.Windows.Forms.Label();
			this.lVersion = new System.Windows.Forms.Label();
			this.lNumTables = new System.Windows.Forms.Label();
			this.lSearchRange = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.lEntrySelector = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.lRangeShift = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3,3);
			this.label1.Margin = new System.Windows.Forms.Padding(3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51,12);
			this.label1.TabIndex = 0;
			this.label1.Text = "ファイル名";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3,21);
			this.label2.Margin = new System.Windows.Forms.Padding(3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29,12);
			this.label2.TabIndex = 1;
			this.label2.Text = "場所";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3,39);
			this.label3.Margin = new System.Windows.Forms.Padding(3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29,12);
			this.label3.TabIndex = 2;
			this.label3.Text = "形式";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3,66);
			this.label4.Margin = new System.Windows.Forms.Padding(3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39,12);
			this.label4.TabIndex = 3;
			this.label4.Text = "表の数";
			// 
			// lFilename
			// 
			this.lFilename.AutoSize = true;
			this.lFilename.Location = new System.Drawing.Point(60,3);
			this.lFilename.Margin = new System.Windows.Forms.Padding(3);
			this.lFilename.Name = "lFilename";
			this.lFilename.Size = new System.Drawing.Size(10,12);
			this.lFilename.TabIndex = 4;
			this.lFilename.Text = "?";
			// 
			// lLocation
			// 
			this.lLocation.AutoSize = true;
			this.lLocation.Location = new System.Drawing.Point(60,21);
			this.lLocation.Margin = new System.Windows.Forms.Padding(3);
			this.lLocation.Name = "lLocation";
			this.lLocation.Size = new System.Drawing.Size(10,12);
			this.lLocation.TabIndex = 5;
			this.lLocation.Text = "?";
			// 
			// lVersion
			// 
			this.lVersion.AutoSize = true;
			this.lVersion.Location = new System.Drawing.Point(60,39);
			this.lVersion.Margin = new System.Windows.Forms.Padding(3);
			this.lVersion.Name = "lVersion";
			this.lVersion.Size = new System.Drawing.Size(10,12);
			this.lVersion.TabIndex = 6;
			this.lVersion.Text = "?";
			// 
			// lNumTables
			// 
			this.lNumTables.AutoSize = true;
			this.lNumTables.Location = new System.Drawing.Point(60,66);
			this.lNumTables.Margin = new System.Windows.Forms.Padding(3);
			this.lNumTables.Name = "lNumTables";
			this.lNumTables.Size = new System.Drawing.Size(10,12);
			this.lNumTables.TabIndex = 7;
			this.lNumTables.Text = "?";
			// 
			// lSearchRange
			// 
			this.lSearchRange.AutoSize = true;
			this.lSearchRange.Location = new System.Drawing.Point(177,66);
			this.lSearchRange.Margin = new System.Windows.Forms.Padding(3);
			this.lSearchRange.Name = "lSearchRange";
			this.lSearchRange.Size = new System.Drawing.Size(10,12);
			this.lSearchRange.TabIndex = 9;
			this.lSearchRange.Text = "?";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(100,66);
			this.label6.Margin = new System.Windows.Forms.Padding(3);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(71,12);
			this.label6.TabIndex = 8;
			this.label6.Text = "searchRange";
			// 
			// lEntrySelector
			// 
			this.lEntrySelector.AutoSize = true;
			this.lEntrySelector.Location = new System.Drawing.Point(295,66);
			this.lEntrySelector.Margin = new System.Windows.Forms.Padding(3);
			this.lEntrySelector.Name = "lEntrySelector";
			this.lEntrySelector.Size = new System.Drawing.Size(10,12);
			this.lEntrySelector.TabIndex = 11;
			this.lEntrySelector.Text = "?";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(216,66);
			this.label8.Margin = new System.Windows.Forms.Padding(3);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(73,12);
			this.label8.TabIndex = 10;
			this.label8.Text = "entrySelector";
			// 
			// lRangeShift
			// 
			this.lRangeShift.AutoSize = true;
			this.lRangeShift.Location = new System.Drawing.Point(396,66);
			this.lRangeShift.Margin = new System.Windows.Forms.Padding(3);
			this.lRangeShift.Name = "lRangeShift";
			this.lRangeShift.Size = new System.Drawing.Size(10,12);
			this.lRangeShift.TabIndex = 13;
			this.lRangeShift.Text = "?";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(333,66);
			this.label10.Margin = new System.Windows.Forms.Padding(3);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(57,12);
			this.label10.TabIndex = 12;
			this.label10.Text = "rangeShift";
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.listView1.Location = new System.Drawing.Point(5,84);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(401,174);
			this.listView1.TabIndex = 14;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "表の名前";
			this.columnHeader1.Width = 63;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "offset 位置";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader2.Width = 81;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "大きさ";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader3.Width = 73;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "CheckSum";
			this.columnHeader4.Width = 102;
			// 
			// ttffileControl
			// 
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.lRangeShift);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.lEntrySelector);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.lSearchRange);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.lNumTables);
			this.Controls.Add(this.lVersion);
			this.Controls.Add(this.lLocation);
			this.Controls.Add(this.lFilename);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "ttffileControl";
			this.Size = new System.Drawing.Size(462,284);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
	}
	public sealed class ttffileTreeNode:System.Windows.Forms.TreeNode,UI.ITreeNode{
		TTFFile file;
		public ttffileTreeNode(TTFFile file):base(file.FileName+" (場所 "+file.DirectoryName+")"){
			this.file=file;
			this.Nodes.Add("null");
		}
		private bool expanded=false;
		public void BeforeExpand(object sender,System.Windows.Forms.TreeViewAction action,ref bool cancel){
			if(this.expanded)return;
			this.expanded=true;
			this.Nodes.Clear();
			foreach(string key in file.TableDirectoryNames) {
				this.Nodes.Add(TTF.TableTreeNode.CreateTreeNode(file,file.tables[key]));
			}
		}
		public void AfterExpand(){}
		public void BeforeSelect(){}
		public void AfterSelect(object sender,System.Windows.Forms.TreeViewAction action){}
		public System.Windows.Forms.Control GetControl(){
			ctrl.File=this.file;
			return ctrl;
		}
		private static ttffileControl ctrl=new ttffileControl();
	}
}