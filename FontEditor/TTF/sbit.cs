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
	/// 'EBLC' 内で使用します。特定の字形コード範囲に対する情報を提供します。
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
					return index.ToString()+" (未知の形式)";
				}
				return indexFormatString[index];
			}
		}private static readonly string[] indexFormatString={"0 (未知の形式)","1 (変幾何 4B)","2 (定幾何)","3 (変幾何 2B)","4 (変幾何 疎)","5 (定幾何 疎)"};
		public string ImageFormatString{
			get{
				int image=(ushort)imageFormat;
				if(image<1||image>9){
					return image.ToString()+" (未知の形式)";
				}
				return imageFormatString[image];
			}
		}private static readonly string[] imageFormatString={"0 (未知の形式)","1 (小幾何 B)","2 (小幾何 bit)","3 (廃止)","4 (圧縮 <非対応>)","5 (bit)","6 (大幾何 B)","7 (大幾何 bit)","8 (小幾何 複合)","9 (大幾何 複合)"};
		/// <summary>
		/// この subTable にある Glyph の数を取得します。
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
		/// オフセット位置の配列の先頭です。
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
		/// 二値白黒画像を取得します。1 bit が 1 pixel に対応します。
		/// 高位を先に読み取り、低位を後に読み取ります。
		/// 行がバイトの途中で終わった場合、次の行は新しいバイトから始まります。
		/// </summary>
		/// <param name="width">画像の幅を指定します。</param>
		/// <param name="height">画像の高さを指定します。</param>
		/// <param name="imageData">画像のデータが格納されている配列の先頭を指定します。
		/// (width+7)/8*height 以上の長さの配列である必要があります。</param>
		/// <returns>生成した画像を System.Drawing.Bitmap として返します。</returns>
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
		/// 二値白黒画像を取得します。1 bit が 1 pixel に対応します。
		/// 高位を先に読み取り、低位を後に読み取ります。
		/// 行がバイトの途中で終わっても、次の行はその「途中」から始まります。
		/// </summary>
		/// <param name="width">画像の幅を指定します。</param>
		/// <param name="height">画像の高さを指定します。</param>
		/// <param name="imageData">
		/// 画像のデータが格納されている配列の先頭を指定します。
		/// (width*height+7)/8 以上の長さの配列である必要があります。</param>
		/// <returns>生成した画像を System.Drawing.Bitmap として返します。</returns>
		public static unsafe System.Drawing.Bitmap GetBitAlignedImage(int width,int height,byte* imageData){
			Bitmap bmp
				=new System.Drawing.Bitmap(width,height,PixelFormat.Format1bppIndexed);
			BitmapData data=bmp.LockBits(
				new Rectangle(0,0,width,height)
				,ImageLockMode.WriteOnly
				,PixelFormat.Format1bppIndexed);
			//	System.Drawing.Bitmap
			//		高位→左, 低位→右
			//	TTF file
			//		高位→左, 低位→右
			int max=width*height;
			byte* src=imageData;
			byte* pL=(byte*)data.Scan0;
			byte* pLM=pL+data.Stride*height;
			byte* px;
			byte rP;	// 残りの書き込み bit 数 of *px
			byte rS;	// 残りの書き込み bit 数 of *src
			int rL;		// 残りの書き込み bit 数 of 行
			rL=width;
			px=pL;*px=0;rP=8;
			for(int i=0;i<max;i+=8,src++){
				rS=8;
				while(rS>0){
					*px|=(byte)((*src<<(8-rS))>>(8-rP));
					if(rL>rP){
						if(rS<rP){
							rL-=rS; // 書き込んだ量は rS
							rP-=rS;
							break;	// 次の *src へ
						}else{
							rL-=rP; // 書き込んだ量は rP
							rS-=rP;
							// 次の px
							*(++px)=0;rP=8;
						}
					}else{
						if(rS<rL){
							rL-=rS; // 書き込んだ量は rS
							rP-=rS;
							break;	// 次の *src へ
						}else{
							rS-=(byte)rL; // 書き込んだ量は rL
							// 次の行
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
		/// 画像を取得します。
		/// TODO:ちゃんと画像得る事が出来るかは検証していません。
		/// </summary>
		/// <returns>取得した画像を Bitmap 形式で返します。</returns>
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