using System;
using System.Collections.Generic;
using System.Windows.Forms;

using afh.File;
namespace FontEditor {
	static class Program{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main(){
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Program.ErrorOut=afh.Application.LogView.Instance.CreateLog("FontEditor-Debug");

			//Application.Run(new Form1());
		}
		public static afh.Application.Log ErrorOut;
	}
}
namespace FontEditor.UI{
	public interface ITreeNode{
		void AfterExpand();
		void AfterSelect(object sender,System.Windows.Forms.TreeViewAction action);
		void BeforeExpand(object sender,System.Windows.Forms.TreeViewAction action,ref bool cancel);
		void BeforeSelect();
		System.Windows.Forms.Control GetControl();
	}
	public class TreeNode:System.Windows.Forms.TreeNode,FontEditor.UI.ITreeNode{
		public virtual void AfterExpand(){}
		public virtual void AfterSelect(object sender,System.Windows.Forms.TreeViewAction action){}
		public virtual void BeforeExpand(object sender,System.Windows.Forms.TreeViewAction action,ref bool cancel){}
		public virtual void BeforeSelect(){}
		public virtual System.Windows.Forms.Control GetControl(){return null;}
		internal TreeNode(string name):base(name){}
		internal static readonly afh.Application.Log ShareLog=new afh.Application.Log("ShareTxtBox");
		internal static readonly afh.Application.LogBox ShareTxtBox=new afh.Application.LogBox();
		static TreeNode(){
			ShareTxtBox.Dock=System.Windows.Forms.DockStyle.Fill;
			ShareTxtBox.ScrollBars=System.Windows.Forms.ScrollBars.Both;
			ShareTxtBox.BackColor=System.Drawing.Color.White;
			ShareTxtBox.ForeColor=System.Drawing.Color.Black;
			ShareLog.LogBox=ShareTxtBox;
		}
	}
}