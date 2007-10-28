using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FontEditor.TTF;

namespace FontEditor {
	public partial class Form1:Form {
		public Form1() {
			InitializeComponent();
			afh.Application.LogView.Instance.Dock=System.Windows.Forms.DockStyle.Fill;
			this.tabPage2.Controls.Add(afh.Application.LogView.Instance);
		}
		private void menuItem2_Click(object sender,EventArgs e) {
			if(this.openFileDialog1.ShowDialog(this)!=System.Windows.Forms.DialogResult.OK)return;
			string fname=this.openFileDialog1.FileName;
			switch(System.IO.Path.GetExtension(fname).ToLower()){
				case ".ttf":
					TTFFile file=new TTFFile(fname);
					if(!file.Read())return;
					this.treeView1.Nodes.Add(new TTF.ttffileTreeNode(file));
					break;
				case ".mp3":
					afh.File.MP3.MP3File mp3=new afh.File.MP3.MP3File(fname);
					this.treeView1.Nodes.Add(new FontEditor.mp3fileTreeNode(mp3));
					break;
				default:
					System.Windows.Forms.MessageBox.Show("このファイル形式には対応していません");
					break;
			}
		}
		private void treeView1_AfterSelect(object sender,TreeViewEventArgs e) {
			if(e.Node is UI.ITreeNode){
				UI.ITreeNode tnode=(UI.ITreeNode)e.Node;
				tnode.AfterSelect(sender,e.Action);
				this.CurrentControl=tnode.GetControl();
			}
		}
		private void treeView1_BeforeExpand(object sender,TreeViewCancelEventArgs e) {
			if(e.Node is UI.ITreeNode){
				UI.ITreeNode tnode=(UI.ITreeNode)e.Node;
				bool cancel=e.Cancel;
				tnode.BeforeExpand(sender,e.Action,ref cancel);
				e.Cancel=cancel;
			}
		}
		private System.Windows.Forms.Control CurrentControl{
			get{return this.cCtrl;}
			set{
				if(value==this.cCtrl)return;
				this.splitContainer1.Panel2.Controls.Clear();
				this.cCtrl=value;
				if(value!=null){
					value.Dock=System.Windows.Forms.DockStyle.Fill;
					this.splitContainer1.Panel2.Controls.Add(value);
				}
			}
		}private System.Windows.Forms.Control cCtrl=null;
		private void menuItem3_Click(object sender,EventArgs e){
			//this.test_ebdtFormat7_GetBitmap();
			afh.File.StreamAccessor.dbg_I3ToInt32Benchmark();
		}
		private unsafe void test_ebdtFormat7_GetBitmap(){
			byte[] x=unchecked(new byte[]{5,5,3,3,5,3,3,5,0xA9,0x3E,0x4A,0x80});
			fixed(byte* p=&x[0]){
				ebdtFormat7* f7=(ebdtFormat7*)p;
				f7->GetBitmap().Save("testbmp.bmp0");
			}
		}

#if old
		private unsafe void treeView1_AfterSelect(object sender,TreeViewEventArgs e) {
			if(e.Node.Text!="EBLC")return;
			if(e.Node.Parent!=null&&e.Node.Parent.Tag is TTFFile){
				TTFFile file=(TTFFile)e.Node.Parent.Tag;
				TableDirectoryEntry entry=file.tables["EBLC"];
				Program.ErrorOut.WriteLine("EBLC 表を読みます。");
				Program.ErrorOut.AddIndent();
				fixed(byte* pBase=&file.image[0]){
					byte* p=pBase;
					p+=(uint)entry.offset;

					//-- header
					eblcHeader* header=(eblcHeader*)p;
					uint nSize=(uint)header->numSizes;
					Program.ErrorOut.WriteLine("表バージョン番号: {0}",header->version.Value);
					Program.ErrorOut.WriteLine("strike の数: {0}",nSize);
					p+=sizeof(eblcHeader);

					//-- bitmapSizeTables
					bitmapSizeTable* size=(bitmapSizeTable*)p;
					bitmapSizeTable* sizeM=size+nSize;
					while(size<sizeM){
						Program.ErrorOut.WriteLine("Size {{ppemX:{0} ; ppemY: {1}}}",size->ppemX,size->ppemY);
						size++;
					}
				}
				Program.ErrorOut.RemoveIndent();
			}
		}
		private unsafe void test_ebdtFormat1_GetBitmap(){
			byte[] x=unchecked(new byte[]{5,5,3,3,5,(byte)~21,(byte)~4,(byte)~31,(byte)~4,(byte)~21});
			fixed(byte* p=&x[0]){
				ebdtFormat1* f1=(ebdtFormat1*)p;
				f1->GetBitmap().Save("testbmp.bmp");
			}
		}
					System.Console.WriteLine("Sum of OffsetTable: 0x{0:X8}",header->Sum);
					System.Console.WriteLine("number of tables: {0}",(uint)header->numTables);

					uint headsumA=0;
					uint headsumB=header->Sum;
						if(entry->TagName=="head"){
							text.AppendFormat("0:{0:X8}\r\n",(uint)entry->checkSum);
							headsumA+=entry->CalculateTableSum(this.fileimage);
						}else headsumA+=(uint)entry->checkSum;
						headsumB+=entry->Sum;
					text.AppendLine("A:"+((uint)headsumA).ToString("X8"));
					text.AppendLine("B:"+((uint)headsumB).ToString("X8"));
		private unsafe void SizeOfSystem_DateTime(){
			this.textBox1.AppendText("sizeof(System.DateTime)=="+sizeof(System.DateTime).ToString()+"\r\n");
			// 結果: 8
		}
		private void TestF2DOT14(){
			this.textBox1.AppendText(new F2DOT14(1.999939).Value.ToString()+"\r\n");
			this.textBox1.AppendText(new F2DOT14(1.75).Value.ToString()+"\r\n");
			this.textBox1.AppendText(new F2DOT14(-0.000062).Value.ToString()+"\r\n");
		}
#endif
	}
}