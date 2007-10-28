namespace FontEditor.TTF{
	/// <summary>
	/// EBLC 表から参照される埋め込み bitmap の一覧表示を行うクラス。
	/// (このコードを利用して、 glyf 表の一覧を表示するクラスにも流用する予定)
	/// </summary>
	public sealed class eblcGlyphList:System.Windows.Forms.Control{
		private System.Windows.Forms.VScrollBar vscroll;
		private TTFFile file;
		private TableDirectoryEntry entry;
		private bitmapSizeTable size;
		public eblcGlyphList(){
			this.InitComponents();
		}
		public eblcGlyphList(TTFFile file,bitmapSizeTable size){
			this.file=file;
			this.entry=file.tables["EBLC"];
			this.size=size;
			this.InitComponents();
		}
		private void InitComponents(){
			this.SuspendLayout();
			//
			// vscroll
			//
			this.vscroll=new System.Windows.Forms.VScrollBar();
			this.vscroll.Name="vscroll";
			this.vscroll.Width=16;
			this.vscroll.Dock=System.Windows.Forms.DockStyle.Right;
			//
			// eblcGlyphList
			//
			this.BackColor=System.Drawing.Color.White;
			this.MouseWheel+=new System.Windows.Forms.MouseEventHandler(eblcGlyphList_MouseWheel);
			this.Controls.Add(vscroll);
			this.ResumeLayout();
		}

		private void eblcGlyphList_MouseWheel(object sender,System.Windows.Forms.MouseEventArgs e){
			Program.ErrorOut.WriteLine("MouseWheel event at eblcGlyphList");
		}
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e){
			// TODO:
			// 1. vscroll.Value から現在描画する必要のある物を列挙
			// 2. e.ClipRectangle.IntersectsWith で描画の必要性を確認。
			// 3. 描画
			//
			// スクロールをする際
			// 1. スクロールの量が少ない時には現在描画されている物を上(亦は下)に移動するだけにする。
			// 2. 1. を行う為にはバッファリングが必要になるかも知れない。
			// 3. スクロール中は内容を描画しない様にしても良い→設定項目に入れる
			//
			// 表示方法を選択できるようにする。
			// a. 字形コードによる配列
			// b. ASCII Shift_JIS Unicode 等のコード (これは cmap 表にも関係)
			base.OnPaint(e);
		}
	}
}