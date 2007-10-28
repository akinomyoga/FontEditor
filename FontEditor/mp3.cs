using afh.File.MP3;
using afh.File.ID3v2_3;
using Gen=System.Collections.Generic;

namespace FontEditor{
	public sealed class mp3fileTreeNode:System.Windows.Forms.TreeNode,UI.ITreeNode{
		MP3File file;
		string controltype;
		//=================================================
		//		Constructor
		//=================================================
		public mp3fileTreeNode(MP3File file):base(System.IO.Path.GetFileName(file.FilePath)+" (èÍèä "+System.IO.Path.GetDirectoryName(file.FilePath)+")"){
			this.file=file;
			this.controltype="";
			this.Nodes.Add(new mp3fileTreeNode(file,"Text Information"));
			this.Nodes.Add(new mp3fileTreeNode(file,"Content-Type"));
		}
		private mp3fileTreeNode(MP3File file,string controltype):base(controltype){
			this.file=file;
			this.controltype=controltype;
		}

		private bool expanded=false;
		public void BeforeExpand(object sender,System.Windows.Forms.TreeViewAction action,ref bool cancel){
			if(this.expanded)return;
			this.expanded=true;
			/*
			this.Nodes.Clear();
			foreach(string key in file.TableDirectoryNames) {
				this.Nodes.Add(TTF.TableTreeNode.CreateTreeNode(file,file.tables[key]));
			}//*/
		}
		public void AfterExpand(){}
		public void BeforeSelect(){}
		public void AfterSelect(object sender,System.Windows.Forms.TreeViewAction action){}
		public System.Windows.Forms.Control GetControl(){
			return GetControl(this.file,this.controltype);
		}

		//=================================================
		//		Controls
		//=================================================
		private static Gen::Dictionary<string,Mp3DataControl> ctrls
			=new Gen::Dictionary<string,Mp3DataControl>();
		private static System.Windows.Forms.Control GetControl(MP3File file,string ctrltype){
			Mp3DataControl ctrl;
			if(ctrls.ContainsKey(ctrltype)){
				ctrl=ctrls[ctrltype];
			}else{
				switch(ctrltype){
					case "Content-Type":
						ctrl=new ContentTypeEditor();
						break;
					case "Text Information":
						ctrl=new TextInformationPanel();
						break;
					default:
						return null;
				}
				ctrls[ctrltype]=ctrl;
			}
			ctrl.File=file;
			return ctrl;
		}
	}
}