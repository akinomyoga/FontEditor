namespace FontEditor.TTF{
	/// <summary>
	/// EBLC �\����Q�Ƃ���閄�ߍ��� bitmap �̈ꗗ�\�����s���N���X�B
	/// (���̃R�[�h�𗘗p���āA glyf �\�̈ꗗ��\������N���X�ɂ����p����\��)
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
			// 1. vscroll.Value ���猻�ݕ`�悷��K�v�̂��镨���
			// 2. e.ClipRectangle.IntersectsWith �ŕ`��̕K�v�����m�F�B
			// 3. �`��
			//
			// �X�N���[���������
			// 1. �X�N���[���̗ʂ����Ȃ����ɂ͌��ݕ`�悳��Ă��镨����(���͉�)�Ɉړ����邾���ɂ���B
			// 2. 1. ���s���ׂɂ̓o�b�t�@�����O���K�v�ɂȂ邩���m��Ȃ��B
			// 3. �X�N���[�����͓��e��`�悵�Ȃ��l�ɂ��Ă��ǂ����ݒ荀�ڂɓ����
			//
			// �\�����@��I���ł���悤�ɂ���B
			// a. ���`�R�[�h�ɂ��z��
			// b. ASCII Shift_JIS Unicode ���̃R�[�h (����� cmap �\�ɂ��֌W)
			base.OnPaint(e);
		}
	}
}