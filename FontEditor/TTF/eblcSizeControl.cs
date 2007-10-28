namespace FontEditor.TTF{
	public sealed class eblcSizeControl:System.Windows.Forms.UserControl{
		private TTFFile file;
		private TableDirectoryEntry entry;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage1;
		private eblcLineMetricsControl horiLineMetrics;
		private eblcLineMetricsControl vertLineMetrics;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox pictColorRef;
		private System.Windows.Forms.Label lColorRef;
		private System.Windows.Forms.Label lBitDepth;
		private System.Windows.Forms.Label lPpemY;
		private System.Windows.Forms.Label lPpemX;
		private System.Windows.Forms.Label lEndGlyphIndex;
		private System.Windows.Forms.Label lStartGlyphIndex;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox chkHoriLayout;
		private System.Windows.Forms.CheckBox chkVertLayout;
		private eblcGlyphList eblcGlyphList1;
		private bitmapSizeTable size;
		public eblcSizeControl():base(){
			this.InitializeComponent();
		}

		public eblcSizeControl(TTFFile file,TableDirectoryEntry entry,bitmapSizeTable size):this(){
			this.SetStrike(file,entry,size);
		}
		public unsafe void SetStrike(TTFFile file,TableDirectoryEntry entry,bitmapSizeTable size){
			this.file=file;
			this.entry=entry;
			this.size=size;
			this.horiLineMetrics.SetLineMetrics(size.hori);
			this.vertLineMetrics.SetLineMetrics(size.vert);

			// set text to labels
			this.lStartGlyphIndex.Text="0x"+((uint)this.size.startGlyphIndex).ToString("X4");
			this.lEndGlyphIndex.Text="0x"+((uint)this.size.endGlyphIndex).ToString("X4");
			this.lPpemX.Text=this.size.ppemX.ToString();
			this.lPpemY.Text=this.size.ppemY.ToString();
			this.lBitDepth.Text=this.size.bitDepth.ToString();
			this.lColorRef.Text="0x"+((uint)this.size.colorRef).ToString("X8");
			this.pictColorRef.BackColor=System.Drawing.Color.FromArgb((int)(uint)this.size.colorRef);
			this.chkHoriLayout.Checked=this.size.HorizontalLayout;
			this.chkVertLayout.Checked=this.size.VerticalLayout;

			// indexSubTableArray
			this.listView1.Items.Clear();
			fixed(byte* pImg=&this.file.image[0]){
				indexSubTableArray* subtableAB=(indexSubTableArray*)(pImg+(uint)entry.offset+(uint)size.indexSubTableArrayOffset);
				indexSubTableArray* subtableAM=subtableAB+(uint)size.numberOfIndexSubTables;
				indexSubTableArray* subtableA=subtableAB;
				indexSubHeader* subheader;
				while(subtableA<subtableAM){
					subheader=(indexSubHeader*)((byte*)subtableAB+(uint)subtableA->additionalOffsetToIndexSubTable);

					string[] items=new string[]{
						string.Format("{0:X4} - {1:X4}",(ushort)subtableA->firstGlyphIndex,(ushort)subtableA->lastGlyphIndex)
						,subheader->IndexFormatString
						,subheader->ImageFormatString
						,subheader->NumberOfGlyph(*subtableA).ToString()
					};
					this.listView1.Items.Add(new System.Windows.Forms.ListViewItem(items));
					subtableA++;
				}
			}
		}
		public void SetText(string text){this.label1.Text="bitmapSizeTable "+text;}

		#region designer
		private void InitializeComponent() {
			this.listView1=new System.Windows.Forms.ListView();
			this.columnHeader1=new System.Windows.Forms.ColumnHeader();
			this.columnHeader2=new System.Windows.Forms.ColumnHeader();
			this.columnHeader3=new System.Windows.Forms.ColumnHeader();
			this.columnHeader4=new System.Windows.Forms.ColumnHeader();
			this.splitter1=new System.Windows.Forms.Splitter();
			this.label1=new System.Windows.Forms.Label();
			this.tabControl1=new System.Windows.Forms.TabControl();
			this.tabPage4=new System.Windows.Forms.TabPage();
			this.chkVertLayout=new System.Windows.Forms.CheckBox();
			this.chkHoriLayout=new System.Windows.Forms.CheckBox();
			this.pictColorRef=new System.Windows.Forms.PictureBox();
			this.lColorRef=new System.Windows.Forms.Label();
			this.lBitDepth=new System.Windows.Forms.Label();
			this.lPpemY=new System.Windows.Forms.Label();
			this.lPpemX=new System.Windows.Forms.Label();
			this.lEndGlyphIndex=new System.Windows.Forms.Label();
			this.lStartGlyphIndex=new System.Windows.Forms.Label();
			this.label7=new System.Windows.Forms.Label();
			this.label6=new System.Windows.Forms.Label();
			this.label5=new System.Windows.Forms.Label();
			this.label4=new System.Windows.Forms.Label();
			this.label3=new System.Windows.Forms.Label();
			this.label2=new System.Windows.Forms.Label();
			this.tabPage3=new System.Windows.Forms.TabPage();
			this.horiLineMetrics=new FontEditor.TTF.eblcLineMetricsControl();
			this.tabPage2=new System.Windows.Forms.TabPage();
			this.vertLineMetrics=new FontEditor.TTF.eblcLineMetricsControl();
			this.tabPage1=new System.Windows.Forms.TabPage();
			this.eblcGlyphList1=new FontEditor.TTF.eblcGlyphList();
			this.tabControl1.SuspendLayout();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictColorRef)).BeginInit();
			this.tabPage3.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.listView1.Dock=System.Windows.Forms.DockStyle.Fill;
			this.listView1.Location=new System.Drawing.Point(0,0);
			this.listView1.Name="listView1";
			this.listView1.Size=new System.Drawing.Size(676,210);
			this.listView1.TabIndex=0;
			this.listView1.UseCompatibleStateImageBehavior=false;
			this.listView1.View=System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text="範囲";
			this.columnHeader1.Width=109;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text="索引形式";
			this.columnHeader2.Width=90;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text="画像形式";
			this.columnHeader3.Width=93;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text="字形の数";
			// 
			// splitter1
			// 
			this.splitter1.Dock=System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location=new System.Drawing.Point(0,260);
			this.splitter1.Name="splitter1";
			this.splitter1.Size=new System.Drawing.Size(684,3);
			this.splitter1.TabIndex=1;
			this.splitter1.TabStop=false;
			// 
			// label1
			// 
			this.label1.AutoSize=true;
			this.label1.Dock=System.Windows.Forms.DockStyle.Top;
			this.label1.Location=new System.Drawing.Point(0,0);
			this.label1.Name="label1";
			this.label1.Padding=new System.Windows.Forms.Padding(5);
			this.label1.Size=new System.Drawing.Size(126,22);
			this.label1.TabIndex=2;
			this.label1.Text="bitmapSizeTable 0×0";
			// 
			// tabControl1
			// 
			this.tabControl1.Appearance=System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Dock=System.Windows.Forms.DockStyle.Top;
			this.tabControl1.Location=new System.Drawing.Point(0,22);
			this.tabControl1.Name="tabControl1";
			this.tabControl1.SelectedIndex=0;
			this.tabControl1.Size=new System.Drawing.Size(684,238);
			this.tabControl1.TabIndex=3;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.chkVertLayout);
			this.tabPage4.Controls.Add(this.chkHoriLayout);
			this.tabPage4.Controls.Add(this.pictColorRef);
			this.tabPage4.Controls.Add(this.lColorRef);
			this.tabPage4.Controls.Add(this.lBitDepth);
			this.tabPage4.Controls.Add(this.lPpemY);
			this.tabPage4.Controls.Add(this.lPpemX);
			this.tabPage4.Controls.Add(this.lEndGlyphIndex);
			this.tabPage4.Controls.Add(this.lStartGlyphIndex);
			this.tabPage4.Controls.Add(this.label7);
			this.tabPage4.Controls.Add(this.label6);
			this.tabPage4.Controls.Add(this.label5);
			this.tabPage4.Controls.Add(this.label4);
			this.tabPage4.Controls.Add(this.label3);
			this.tabPage4.Controls.Add(this.label2);
			this.tabPage4.Location=new System.Drawing.Point(4,24);
			this.tabPage4.Name="tabPage4";
			this.tabPage4.Size=new System.Drawing.Size(676,210);
			this.tabPage4.TabIndex=3;
			this.tabPage4.Text="全般";
			this.tabPage4.UseVisualStyleBackColor=true;
			// 
			// chkVertLayout
			// 
			this.chkVertLayout.AutoSize=true;
			this.chkVertLayout.Enabled=false;
			this.chkVertLayout.Location=new System.Drawing.Point(5,133);
			this.chkVertLayout.Name="chkVertLayout";
			this.chkVertLayout.Size=new System.Drawing.Size(105,16);
			this.chkVertLayout.TabIndex=14;
			this.chkVertLayout.Text="垂直配置を使用";
			this.chkVertLayout.UseVisualStyleBackColor=true;
			// 
			// chkHoriLayout
			// 
			this.chkHoriLayout.AutoSize=true;
			this.chkHoriLayout.Enabled=false;
			this.chkHoriLayout.Location=new System.Drawing.Point(5,111);
			this.chkHoriLayout.Name="chkHoriLayout";
			this.chkHoriLayout.Size=new System.Drawing.Size(105,16);
			this.chkHoriLayout.TabIndex=13;
			this.chkHoriLayout.Text="水平配置を使用";
			this.chkHoriLayout.UseVisualStyleBackColor=true;
			// 
			// pictColorRef
			// 
			this.pictColorRef.BackColor=System.Drawing.Color.White;
			this.pictColorRef.BorderStyle=System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictColorRef.Location=new System.Drawing.Point(170,93);
			this.pictColorRef.Name="pictColorRef";
			this.pictColorRef.Size=new System.Drawing.Size(40,12);
			this.pictColorRef.TabIndex=12;
			this.pictColorRef.TabStop=false;
			// 
			// lColorRef
			// 
			this.lColorRef.AutoSize=true;
			this.lColorRef.Location=new System.Drawing.Point(99,93);
			this.lColorRef.Margin=new System.Windows.Forms.Padding(3);
			this.lColorRef.Name="lColorRef";
			this.lColorRef.Size=new System.Drawing.Size(65,12);
			this.lColorRef.TabIndex=11;
			this.lColorRef.Text="0x00000000";
			// 
			// lBitDepth
			// 
			this.lBitDepth.AutoSize=true;
			this.lBitDepth.Location=new System.Drawing.Point(99,75);
			this.lBitDepth.Margin=new System.Windows.Forms.Padding(3);
			this.lBitDepth.Name="lBitDepth";
			this.lBitDepth.Size=new System.Drawing.Size(29,12);
			this.lBitDepth.TabIndex=10;
			this.lBitDepth.Text="0x00";
			// 
			// lPpemY
			// 
			this.lPpemY.AutoSize=true;
			this.lPpemY.Location=new System.Drawing.Point(99,57);
			this.lPpemY.Margin=new System.Windows.Forms.Padding(3);
			this.lPpemY.Name="lPpemY";
			this.lPpemY.Size=new System.Drawing.Size(29,12);
			this.lPpemY.TabIndex=9;
			this.lPpemY.Text="0x00";
			// 
			// lPpemX
			// 
			this.lPpemX.AutoSize=true;
			this.lPpemX.Location=new System.Drawing.Point(99,39);
			this.lPpemX.Margin=new System.Windows.Forms.Padding(3);
			this.lPpemX.Name="lPpemX";
			this.lPpemX.Size=new System.Drawing.Size(29,12);
			this.lPpemX.TabIndex=8;
			this.lPpemX.Text="0x00";
			// 
			// lEndGlyphIndex
			// 
			this.lEndGlyphIndex.AutoSize=true;
			this.lEndGlyphIndex.Location=new System.Drawing.Point(99,21);
			this.lEndGlyphIndex.Margin=new System.Windows.Forms.Padding(3);
			this.lEndGlyphIndex.Name="lEndGlyphIndex";
			this.lEndGlyphIndex.Size=new System.Drawing.Size(41,12);
			this.lEndGlyphIndex.TabIndex=7;
			this.lEndGlyphIndex.Text="0x0000";
			// 
			// lStartGlyphIndex
			// 
			this.lStartGlyphIndex.AutoSize=true;
			this.lStartGlyphIndex.Location=new System.Drawing.Point(99,3);
			this.lStartGlyphIndex.Margin=new System.Windows.Forms.Padding(3);
			this.lStartGlyphIndex.Name="lStartGlyphIndex";
			this.lStartGlyphIndex.Size=new System.Drawing.Size(41,12);
			this.lStartGlyphIndex.TabIndex=6;
			this.lStartGlyphIndex.Text="0x0000";
			// 
			// label7
			// 
			this.label7.AutoSize=true;
			this.label7.Location=new System.Drawing.Point(3,93);
			this.label7.Margin=new System.Windows.Forms.Padding(3);
			this.label7.Name="label7";
			this.label7.Size=new System.Drawing.Size(48,12);
			this.label7.TabIndex=5;
			this.label7.Text="colorRef";
			// 
			// label6
			// 
			this.label6.AutoSize=true;
			this.label6.Location=new System.Drawing.Point(3,75);
			this.label6.Margin=new System.Windows.Forms.Padding(3);
			this.label6.Name="label6";
			this.label6.Size=new System.Drawing.Size(48,12);
			this.label6.TabIndex=4;
			this.label6.Text="bitDepth";
			// 
			// label5
			// 
			this.label5.AutoSize=true;
			this.label5.Location=new System.Drawing.Point(3,57);
			this.label5.Margin=new System.Windows.Forms.Padding(3);
			this.label5.Name="label5";
			this.label5.Size=new System.Drawing.Size(39,12);
			this.label5.TabIndex=3;
			this.label5.Text="ppemY";
			// 
			// label4
			// 
			this.label4.AutoSize=true;
			this.label4.Location=new System.Drawing.Point(3,39);
			this.label4.Margin=new System.Windows.Forms.Padding(3);
			this.label4.Name="label4";
			this.label4.Size=new System.Drawing.Size(39,12);
			this.label4.TabIndex=2;
			this.label4.Text="ppemX";
			// 
			// label3
			// 
			this.label3.AutoSize=true;
			this.label3.Location=new System.Drawing.Point(3,21);
			this.label3.Margin=new System.Windows.Forms.Padding(3);
			this.label3.Name="label3";
			this.label3.Size=new System.Drawing.Size(90,12);
			this.label3.TabIndex=1;
			this.label3.Text="最後の字形コード";
			// 
			// label2
			// 
			this.label2.AutoSize=true;
			this.label2.Location=new System.Drawing.Point(3,3);
			this.label2.Margin=new System.Windows.Forms.Padding(3);
			this.label2.Name="label2";
			this.label2.Size=new System.Drawing.Size(90,12);
			this.label2.TabIndex=0;
			this.label2.Text="最初の字形コード";
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.horiLineMetrics);
			this.tabPage3.Location=new System.Drawing.Point(4,24);
			this.tabPage3.Name="tabPage3";
			this.tabPage3.Size=new System.Drawing.Size(676,210);
			this.tabPage3.TabIndex=2;
			this.tabPage3.Text="水平行";
			this.tabPage3.UseVisualStyleBackColor=true;
			// 
			// horiLineMetrics
			// 
			this.horiLineMetrics.Dock=System.Windows.Forms.DockStyle.Fill;
			this.horiLineMetrics.Location=new System.Drawing.Point(0,0);
			this.horiLineMetrics.Name="horiLineMetrics";
			this.horiLineMetrics.Size=new System.Drawing.Size(676,210);
			this.horiLineMetrics.TabIndex=0;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.vertLineMetrics);
			this.tabPage2.Location=new System.Drawing.Point(4,24);
			this.tabPage2.Name="tabPage2";
			this.tabPage2.Size=new System.Drawing.Size(676,210);
			this.tabPage2.TabIndex=1;
			this.tabPage2.Text="垂直行";
			this.tabPage2.UseVisualStyleBackColor=true;
			// 
			// vertLineMetrics
			// 
			this.vertLineMetrics.Dock=System.Windows.Forms.DockStyle.Fill;
			this.vertLineMetrics.Location=new System.Drawing.Point(0,0);
			this.vertLineMetrics.Name="vertLineMetrics";
			this.vertLineMetrics.Size=new System.Drawing.Size(676,210);
			this.vertLineMetrics.TabIndex=0;
			this.vertLineMetrics.Vertical=true;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.listView1);
			this.tabPage1.Location=new System.Drawing.Point(4,24);
			this.tabPage1.Name="tabPage1";
			this.tabPage1.Size=new System.Drawing.Size(676,210);
			this.tabPage1.TabIndex=0;
			this.tabPage1.Text="准表";
			this.tabPage1.UseVisualStyleBackColor=true;
			// 
			// eblcGlyphList1
			// 
			this.eblcGlyphList1.BackColor=System.Drawing.Color.White;
			this.eblcGlyphList1.Dock=System.Windows.Forms.DockStyle.Left;
			this.eblcGlyphList1.Location=new System.Drawing.Point(0,263);
			this.eblcGlyphList1.Name="eblcGlyphList1";
			this.eblcGlyphList1.Size=new System.Drawing.Size(292,299);
			this.eblcGlyphList1.TabIndex=4;
			this.eblcGlyphList1.Text="eblcGlyphList1";
			// 
			// eblcSizeControl
			// 
			this.Controls.Add(this.eblcGlyphList1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.label1);
			this.Name="eblcSizeControl";
			this.Size=new System.Drawing.Size(684,562);
			this.tabControl1.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictColorRef)).EndInit();
			this.tabPage3.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
	}
	public sealed class eblcTreeNode:TableTreeNode{
		public eblcTreeNode(TTFFile file,TableDirectoryEntry entry):base(file,entry){
			this.Nodes.Add("null");
		}
		private bool expanded=false;
		public override unsafe void BeforeExpand(object sender,System.Windows.Forms.TreeViewAction action,ref bool cancel){
			base.BeforeExpand(sender,action,ref cancel);
			if(this.expanded)return;
			this.expanded=true;
			this.Nodes.Clear();
			//Program.ErrorOut.WriteLine("EBLC 表を読みます。");
			//Program.ErrorOut.AddIndent();
			fixed(byte* pBase=&file.image[0]) {
				byte* p=pBase;
				p+=(uint)entry.offset;

				//-- header
				eblcHeader* header=(eblcHeader*)p;
				uint nSize=(uint)header->numSizes;
				//Program.ErrorOut.WriteLine("表バージョン番号: {0}",header->version.Value);
				//Program.ErrorOut.WriteLine("strike の数: {0}",nSize);
				p+=sizeof(eblcHeader);

				//-- bitmapSizeTables
				bitmapSizeTable* size=(bitmapSizeTable*)p;
				bitmapSizeTable* sizeM=size+nSize;
				while(size<sizeM) {
					this.Nodes.Add(new eblcSizeTreeNode(file,entry,*size));
					size++;
				}
			}
			//Program.ErrorOut.RemoveIndent();
		}

	}
	public sealed class eblcSizeTreeNode:UI.TreeNode{
		private TTFFile file;
		private TableDirectoryEntry entry;
		private bitmapSizeTable size;
		public eblcSizeTreeNode(TTFFile file,TableDirectoryEntry entry,bitmapSizeTable size)
			:base(size.ppemX.ToString()+"×"+size.ppemY.ToString()){
			this.file=file;
			this.entry=entry;
			this.size=size;
		}
		public override unsafe System.Windows.Forms.Control GetControl(){
			sharectrl.SetStrike(this.file,this.entry,this.size);
			sharectrl.SetText(this.Text);
			return sharectrl;
		}
#if test
		public unsafe void SaveImages(){
			fixed(byte* pImg=&this.file.image[0]){
				indexSubTableArray* subtableB=(indexSubTableArray*)(pImg+(uint)entry.offset+(uint)size.indexSubTableArrayOffset);
				indexSubTableArray* subtableM=subtableB+(uint)size.numberOfIndexSubTables;
				indexSubTableArray* subtable=subtableB;
				indexSubHeader* subheader;
				//-----
				System.Drawing.Bitmap bmp;
				System.Drawing.Bitmap bmp0=new System.Drawing.Bitmap(300,300);
				System.Drawing.Graphics g=System.Drawing.Graphics.FromImage(bmp0);
				g.Clear(System.Drawing.Color.Gray);
				int x=0,y=0;
				//-----
				while(subtable<subtableM){
					subheader=(indexSubHeader*)((byte*)subtableB+(uint)subtable->additionalOffsetToIndexSubTable);
					if((ushort)subheader->indexFormat==1&&(ushort)subheader->imageFormat==7){
						indexSubTable1* table=(indexSubTable1*)subheader;
						ULONG* offset=&table->offsetArray;
						ULONG* offsetM=offset+((ushort)subtable->lastGlyphIndex-(ushort)subtable->firstGlyphIndex+1);
						byte* imageDataOffset=pImg+(uint)this.file.tables["EBDT"].offset+(uint)subheader->imageDataOffset;

						while(offset<offsetM){
							ebdtFormat7* f7=(ebdtFormat7*)(imageDataOffset+(uint)*offset);
							g.DrawImageUnscaled(bmp=f7->GetBitmap(),x,y);
							bmp.Dispose();
							offset++;
							x+=10;if(x>=300){x=0;y+=10;}
						}
					}
					subtable++;
				}
				//-----
				g.Dispose();
				System.Drawing.Bitmap bmp1=afh.Drawing.BitmapEffect.Invert(bmp0);
				bmp0.Dispose();
				bmp1.Save(System.IO.Path.Combine(afh.Application.Path.ExecutableDirectory,"images\\"+this.Text+".bmp"));
				bmp1.Dispose();
				//-----
			}
		}
#endif
		private static eblcSizeControl sharectrl=new eblcSizeControl();
	}
}