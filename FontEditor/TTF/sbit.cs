using afh.Drawing;
using System.Drawing;
using System.Drawing.Imaging;
namespace FontEditor.TTF{
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct eblcHeader{
		public FIXED version;
		public ULONG numSizes;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct bitmapSizeTable{
		public ULONG indexSubTableArrayOffset;
		public ULONG indexTablesSize;
		public ULONG numberOfIndexSubTables;
		public ULONG colorRef;
		public sbitLineMetrics hori;
		public sbitLineMetrics vert;
		public USHORT startGlyphIndex;
		public USHORT endGlyphIndex;
		public byte ppemX;
		public byte ppemY;
		public byte bitDepth;
		public sbyte flags;

		public bool HorizontalLayout{
			get{return (this.flags&0x01)>0;}
			set{if(value)this.flags|=0x01;else this.flags&=~0x01;}
		}
		public bool VerticalLayout{
			get{return (this.flags&0x02)>0;}
			set{if(value)this.flags|=0x02;else this.flags&=~0x02;}
		}
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct sbitLineMetrics{
		public sbyte ascender;
		public sbyte descender;
		public byte widthMax;
		public sbyte caretSlopeNumerator;
		public sbyte caretSlopeDenominator;
		public sbyte caretOffset;
		public sbyte minOriginSB;
		public sbyte minAdvanceSB;
		public sbyte maxBeforeBL;
		public sbyte minAfterBL;
		private sbyte pad1;
		private sbyte pad2;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct sbitBigGlyphMetrics{
		public byte height;
		public byte width;
		public sbyte horiBearingX;
		public sbyte horiBearingY;
		public byte horiAdvance;
		public sbyte vertBearingX;
		public sbyte vertBearingY;
		public byte vertAdvance;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct sbitSmallGlyphMetrics{
		public byte height;
		public byte width;
		public sbyte BearingX;
		public sbyte BearingY;
		public byte Advance;
	}
	/// <summary>
	/// 'EBLC' ���Ŏg�p���܂��B����̎��`�R�[�h�͈͂ɑ΂������񋟂��܂��B
	/// </summary>
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct indexSubTableArray{
		public USHORT firstGlyphIndex;
		public USHORT lastGlyphIndex;
		public ULONG additionalOffsetToIndexSubTable;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct indexSubHeader{
		public USHORT indexFormat;
		public USHORT imageFormat;
		public ULONG imageDataOffset;
		public string IndexFormatString{
			get{
				int index=(ushort)indexFormat;
				if(index<1||index>5){
					return index.ToString()+" (���m�̌`��)";
				}
				return indexFormatString[index];
			}
		}private static readonly string[] indexFormatString={"0 (���m�̌`��)","1 (�ϊ� 4B)","2 (���)","3 (�ϊ� 2B)","4 (�ϊ� �a)","5 (��� �a)"};
		public string ImageFormatString{
			get{
				int image=(ushort)imageFormat;
				if(image<1||image>9){
					return image.ToString()+" (���m�̌`��)";
				}
				return imageFormatString[image];
			}
		}private static readonly string[] imageFormatString={"0 (���m�̌`��)","1 (���� B)","2 (���� bit)","3 (�p�~)","4 (���k <��Ή�>)","5 (bit)","6 (��� B)","7 (��� bit)","8 (���� ����)","9 (��� ����)"};
		/// <summary>
		/// ���� subTable �ɂ��� Glyph �̐����擾���܂��B
		/// </summary>
		public unsafe uint NumberOfGlyph(indexSubTableArray subtableA){
			if(this.indexFormat==(USHORT)4){
				fixed(indexSubHeader* sh=&this)return (uint)((indexSubTable4*)sh)->numGlyphs;
			}
			if(this.indexFormat==(USHORT)5){
				fixed(indexSubHeader* sh=&this)return (uint)((indexSubTable5*)sh)->numGlyphs;
			}
			return (uint)(1+(ushort)subtableA.lastGlyphIndex-(ushort)subtableA.firstGlyphIndex);
		}
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct indexSubTable1 {
		public indexSubHeader header;
		/// <summary>
		/// �I�t�Z�b�g�ʒu�̔z��̐擪�ł��B
		/// </summary>
		public ULONG offsetArray;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct indexSubTable2 {
		public indexSubHeader header;
		public ULONG imageSize;
		public sbitBigGlyphMetrics bigMetrics;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct indexSubTable3 {
		public indexSubHeader header;
		public USHORT offsetArray;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct indexSubTable4 {
		public indexSubHeader header;
		public ULONG numGlyphs;
		public codeOffsetPair glyphArray;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct indexSubTable5 {
		public indexSubHeader header;
		public ULONG imageData;
		public sbitBigGlyphMetrics bigMetrics;
		public ULONG numGlyphs;
		public USHORT glyphCodeArray;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct codeOffsetPair{
		public USHORT glyphCode;
		public USHORT offset;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct ebdtComponent{
		public USHORT glyphCode;
		public sbyte xOffset;
		public sbyte yOffset;
	}
	public static class EBDT{
		/// <summary>
		/// ��l�����摜���擾���܂��B1 bit �� 1 pixel �ɑΉ����܂��B
		/// ���ʂ��ɓǂݎ��A��ʂ���ɓǂݎ��܂��B
		/// �s���o�C�g�̓r���ŏI������ꍇ�A���̍s�͐V�����o�C�g����n�܂�܂��B
		/// </summary>
		/// <param name="width">�摜�̕����w�肵�܂��B</param>
		/// <param name="height">�摜�̍������w�肵�܂��B</param>
		/// <param name="imageData">�摜�̃f�[�^���i�[����Ă���z��̐擪���w�肵�܂��B
		/// (width+7)/8*height �ȏ�̒����̔z��ł���K�v������܂��B</param>
		/// <returns>���������摜�� System.Drawing.Bitmap �Ƃ��ĕԂ��܂��B</returns>
		public static unsafe System.Drawing.Bitmap GetByteAlignedImage(int width,int height,byte* imageData){
			Bitmap bmp
				=new System.Drawing.Bitmap(width,height,PixelFormat.Format1bppIndexed);
			BitmapData data=bmp.LockBits(
				new Rectangle(0,0,width,height)
				,ImageLockMode.WriteOnly
				,PixelFormat.Format1bppIndexed);
			byte* px=(byte*)data.Scan0;
			byte* pxM=px+((width+7)>>3)*height*4;
			int i=0;
			while(px<pxM){*px=imageData[i++];px+=4;}
			bmp.UnlockBits(data);
			return bmp;
		}
		/// <summary>
		/// ��l�����摜���擾���܂��B1 bit �� 1 pixel �ɑΉ����܂��B
		/// ���ʂ��ɓǂݎ��A��ʂ���ɓǂݎ��܂��B
		/// �s���o�C�g�̓r���ŏI����Ă��A���̍s�͂��́u�r���v����n�܂�܂��B
		/// </summary>
		/// <param name="width">�摜�̕����w�肵�܂��B</param>
		/// <param name="height">�摜�̍������w�肵�܂��B</param>
		/// <param name="imageData">
		/// �摜�̃f�[�^���i�[����Ă���z��̐擪���w�肵�܂��B
		/// (width*height+7)/8 �ȏ�̒����̔z��ł���K�v������܂��B</param>
		/// <returns>���������摜�� System.Drawing.Bitmap �Ƃ��ĕԂ��܂��B</returns>
		public static unsafe System.Drawing.Bitmap GetBitAlignedImage(int width,int height,byte* imageData){
			Bitmap bmp
				=new System.Drawing.Bitmap(width,height,PixelFormat.Format1bppIndexed);
			BitmapData data=bmp.LockBits(
				new Rectangle(0,0,width,height)
				,ImageLockMode.WriteOnly
				,PixelFormat.Format1bppIndexed);
			//	System.Drawing.Bitmap
			//		���ʁ���, ��ʁ��E
			//	TTF file
			//		���ʁ���, ��ʁ��E
			int max=width*height;
			byte* src=imageData;
			byte* pL=(byte*)data.Scan0;
			byte* pLM=pL+data.Stride*height;
			byte* px;
			byte rP;	// �c��̏������� bit �� of *px
			byte rS;	// �c��̏������� bit �� of *src
			int rL;		// �c��̏������� bit �� of �s
			rL=width;
			px=pL;*px=0;rP=8;
			for(int i=0;i<max;i+=8,src++){
				rS=8;
				while(rS>0){
					*px|=(byte)((*src<<(8-rS))>>(8-rP));
					if(rL>rP){
						if(rS<rP){
							rL-=rS; // �������񂾗ʂ� rS
							rP-=rS;
							break;	// ���� *src ��
						}else{
							rL-=rP; // �������񂾗ʂ� rP
							rS-=rP;
							// ���� px
							*(++px)=0;rP=8;
						}
					}else{
						if(rS<rL){
							rL-=rS; // �������񂾗ʂ� rS
							rP-=rS;
							break;	// ���� *src ��
						}else{
							rS-=(byte)rL; // �������񂾗ʂ� rL
							// ���̍s
							pL+=data.Stride;rL=width;
							if(pL>=pLM)break;
							px=pL;*px=0;rP=8;
						}
					}
				}
			}
			bmp.UnlockBits(data);
			return bmp;
		}
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct ebdtFormat1{
		public sbitSmallGlyphMetrics smallMetrics;
		public byte imageData;
		/// <summary>
		/// �摜���擾���܂��B
		/// TODO:�����Ɖ摜���鎖���o���邩�͌��؂��Ă��܂���B
		/// </summary>
		/// <returns>�擾�����摜�� Bitmap �`���ŕԂ��܂��B</returns>
		public unsafe System.Drawing.Bitmap GetBitmap(){
			fixed(byte* img=&this.imageData)
				return EBDT.GetByteAlignedImage(this.smallMetrics.width,this.smallMetrics.height,img);
		}
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct ebdtFormat2 {
		public sbitSmallGlyphMetrics smallMetrics;
		public byte imageData;
		public unsafe System.Drawing.Bitmap GetBitmap(){
			fixed(byte* img=&this.imageData)
				return EBDT.GetBitAlignedImage(this.smallMetrics.width,this.smallMetrics.height,img);
		}
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct ebdtFormat5 {
		public byte imageData;
		public unsafe System.Drawing.Bitmap GetBitmap(int width,int height) {
			fixed(byte* img=&this.imageData)
				return EBDT.GetBitAlignedImage(width,height,img);
		}
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct ebdtFormat6 {
		public sbitBigGlyphMetrics bigMetrics;
		public byte imageData;
		public unsafe System.Drawing.Bitmap GetBitmap() {
			fixed(byte* img=&this.imageData)
				return EBDT.GetByteAlignedImage(this.bigMetrics.width,this.bigMetrics.height,img);
		}
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct ebdtFormat7 {
		public sbitBigGlyphMetrics bigMetrics;
		public byte imageData;
		public unsafe System.Drawing.Bitmap GetBitmap(){
			fixed(byte* img=&this.imageData)
				return EBDT.GetBitAlignedImage(this.bigMetrics.width,this.bigMetrics.height,img);
		}
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct ebdtFormat8 {
		public sbitSmallGlyphMetrics smallMetrics;
		public byte pad;
		public USHORT numComponents;
		public ebdtComponent[] componentArray;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct ebdtFormat9{
		public sbitBigGlyphMetrics bigMetrics;
		public USHORT numComponents;
		public ebdtComponent[] componentArray;
	}
}