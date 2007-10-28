namespace FontEditor.TTF{
	public class TTFFile{
		public string path;
		public byte[] image;
		public System.Collections.Generic.Dictionary<string,TableDirectoryEntry> tables
			=new System.Collections.Generic.Dictionary<string,TableDirectoryEntry>();
		public TTFFile(string path){
			this.path=path;
		}
		public System.Collections.IEnumerable TableDirectoryNames{
			get{return tables.Keys;}
		}
		public string FileName{
			get{return System.IO.Path.GetFileName(path);}
		}
		public string DirectoryName{
			get{return System.IO.Path.GetDirectoryName(path);}
		}
		public bool Read(){
			Program.ErrorOut.WriteLine("TTF ファイル '{0}' を読み込みます。",path);
			Program.ErrorOut.AddIndent();
			try{
				if(!System.IO.File.Exists(path)){
					Program.ErrorOut.WriteLine("読み込もうとしたファイルは存在しませんので読み込む事は出来ません",path);
					return false;
				}
				this.image=System.IO.File.ReadAllBytes(this.path);

				//-- TableDirectoryEntries の読込
				unsafe{
					fixed(byte* img=&this.image[0]){
						OffsetTable* header=(OffsetTable*)img;
						if(!header->Check()){
							Program.ErrorOut.WriteLine("ファイルヘッダに異常がありましたので読込を中止します。");
							return false;
						}
						TableDirectoryEntry* entry=(TableDirectoryEntry*)(header+1);
						TableDirectoryEntry* mEntry=entry+(ushort)header->numTables;
						while(entry<mEntry){
							this.tables[entry->TagName]=*entry;
							if(!entry->Check(image)){
								Program.ErrorOut.WriteLine("表 '{0}' の checkSum が合いません",entry->TagName);
							}
							entry++;
						}
					}
				}
			}finally{
				Program.ErrorOut.RemoveIndent();
			}
			return true;
		}
	}
	public partial class TableTreeNode:System.Windows.Forms.TreeNode,FontEditor.UI.ITreeNode{
		public static TableTreeNode CreateTreeNode(TTFFile file,TableDirectoryEntry entry){
			switch(entry.TagName){
				case "EBLC":return new eblcTreeNode(file,entry);
				case "head":return new headTreeNode(file,entry);
				default:
					return new TableTreeNode(file,entry);
			}
		}
		public virtual void AfterExpand(){}
		public virtual void AfterSelect(object sender,System.Windows.Forms.TreeViewAction action) { }
		public virtual void BeforeExpand(object sender,System.Windows.Forms.TreeViewAction action,ref bool cancel) { }
		public virtual void BeforeSelect(){}
		public virtual unsafe System.Windows.Forms.Control GetControl(){
			afh.Application.Log l=UI.TreeNode.ShareLog;
			l.Clear();
			l.Lock();
			l.WriteLine("Binary dump of first 512 bytes:");
			fixed(byte* img=&this.file.image[0]){
				byte* table=img+(uint)entry.offset;
				byte* end=table+System.Math.Min((uint)entry.length,512u);
				for(int i=1;table<end;table++,i++){
					l.AppendText(table->ToString("X2"));
					if(i>=16){
						i=0;
						l.AppendText("\r\n");
					}else l.AppendText(" ");
				}
			}
			l.Unlock();
			return UI.TreeNode.ShareTxtBox;
		}
		protected TTFFile file;
		protected TableDirectoryEntry entry;
		internal TableTreeNode(TTFFile file,TableDirectoryEntry entry):base(entry.TagName){
			this.file=file;
			this.entry=entry;
		}
	}
}