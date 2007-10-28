namespace FontEditor.TTF{
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct OffsetTable{
		public readonly FIXED sfntversion;
		public readonly USHORT numTables;
		public readonly USHORT searchRange;
		public readonly USHORT entrySelector;
		public readonly USHORT rangeShift;
		public OffsetTable(ushort numOfTables){
			this.sfntversion=(FIXED)0x00010000;
			this.numTables=(USHORT)numOfTables;
			
			// searchRange, entrySelector の計算
			numOfTables>>=1;
			ushort srange=0x10;
			ushort eselect=0;
			while(numOfTables!=0){
				numOfTables>>=1;
				srange<<=1;
				eselect++;
			}
			this.searchRange=(USHORT)srange;
			this.entrySelector=(USHORT)eselect;

			this.rangeShift=(USHORT)((ushort)this.numTables*(int)16-srange);
		}
		public uint Sum{
			get{
				return (uint)(int)this.sfntversion
					+(uint)((int)(ushort)this.numTables<<16)+(ushort)this.searchRange
					+(uint)((ushort)this.entrySelector<<16)+(ushort)this.rangeShift;
			}
		}
		public bool Check(){
			if(this.sfntversion.Value<1||this.sfntversion.Value>9){
				Program.ErrorOut.WriteLine("ファイルのバージョンが異常です: {0}\r\n"
					+"現在は区間 [0,9] のバージョンしか受け付けない設定になっています。\r\n"
					+"このバージョン番号のファイルも読み込む事が出来る様にするにはプログラムを書き換える必要があります。"
					,this.sfntversion.Value);
				return false;
			}
			ushort nTable=(ushort)this.numTables;
			ushort srange=0x10;
			ushort eselect=0;
			nTable>>=1;
			while(nTable!=0){
				nTable>>=1;
				srange<<=1;
				eselect++;
			}
			if((uint)this.searchRange!=srange){
				Program.ErrorOut.WriteLine(
					"ファイルヘッダの searchRange 値が変です。\r\n"
					+"表の数: {0}; 実値: {1}; 理想値: {2}\r\n"
					+"フォントファイルではない可能性があります。"
					,(ushort)this.numTables
					,(uint)this.searchRange
					,srange);
				return false;
			}
			if((uint)this.entrySelector!=eselect){
				Program.ErrorOut.WriteLine("ファイルヘッダの entrySelector 値が変です。\r\nフォントファイルではない可能性があります。");
				return false;
			}
			if((uint)this.rangeShift!=(ushort)this.numTables*16-srange){
				Program.ErrorOut.WriteLine("ファイルヘッダの rangeShift 値が変です。\r\nフォントファイルではない可能性があります。");
				return false;
			}
			return true;
		}
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct TableDirectoryEntry{
		public ULONG tag;
		public ULONG checkSum;
		public ULONG offset;
		public ULONG length;
		public string TagName{get{return this.tag.GetFourCC();}}
		public unsafe bool Check(byte[] fileimage){
			if(this.TagName=="head")fixed(byte* img=&fileimage[0]){
				// reset checkSumAdjustment
				headTable* head=(headTable*)(img+(uint)this.offset);
				ULONG csa=head->checkSumAdjustment;
				head->checkSumAdjustment=(ULONG)0;

				bool ret=this.CalculateTableSum(fileimage)==(uint)this.checkSum;

				// Check checkSumAdjustment
				OffsetTable* offset=(OffsetTable*)img;
				TableDirectoryEntry* entry=(TableDirectoryEntry*)(offset+1);
				TableDirectoryEntry* end=entry+(uint)offset->numTables;
				uint sum=(uint)offset->Sum;
				for(;entry<end;entry++){
					sum+=entry->Sum;
					sum+=(uint)entry->checkSum;
				}

				// restore checkSumAdjustment
				head->checkSumAdjustment=csa;

				return ret;//&&sum==0xB1B0AFBA-(uint)csa;
			}
			return this.CalculateTableSum(fileimage)==(uint)this.checkSum;
		}
#if check
		public unsafe bool Check(byte[] fileimage){
			if(this.TagName=="head")fixed(byte* img=&fileimage[0]){
				// reset checkSumAdjustment
				headTable* head=(headTable*)(img+(uint)this.offset);
				ULONG csa=head->checkSumAdjustment;
				head->checkSumAdjustment=(ULONG)0;
				uint csaR=0xB1B0AFBA-(uint)csa;

				bool ret=this.CalculateTableSum(fileimage)==(uint)this.checkSum;

				// Check checkSumAdjustment
				OffsetTable* offset=(OffsetTable*)img;
				TableDirectoryEntry* entry=(TableDirectoryEntry*)(offset+1);
				TableDirectoryEntry* end=entry+(uint)offset->numTables;
				uint sum=(uint)offset->Sum;
				for(;entry<end;entry++){
					sum+=entry->Sum;
					sum+=(uint)entry->checkSum;
				}

				uint sum2=this.SumEntireFont(fileimage);

				// restore checkSumAdjustment
				head->checkSumAdjustment=csa;

				if(sum!=sum2){
					System.Console.WriteLine("checkSums of all the structures and the entire file are diffrent from each other.");
					if(sum==csaR)System.Console.WriteLine("checkSumAdjustment is calculated from all the structures.");
					else if(sum2==csaR)System.Console.WriteLine("checkSumAdjustment is calculated from the entire file.");
					else System.Console.WriteLine("Neither structures nor entire file is used to calculate checkSumAdjustment: 0x{0:X8}",csaR);
				}else{
					if(sum!=csaR)System.Console.WriteLine("checkSumAdjustment is unexpected value: 0x{0:X8}",csaR);
				}
				System.Console.WriteLine("Σ1: 0x{0:X8}; checkSumAdjustment: 0x{1:X8}",sum,csaR);
				System.Console.WriteLine("Σ2: 0x{0:X8}; checkSumAdjustment: 0x{1:X8}",sum2,csaR);
				System.Console.WriteLine("Σ1 ～ checkSumAdjustment: 0x{0:X8} / 0x{1:X8}",csaR-sum,sum-csaR);
				System.Console.WriteLine("Σ2 ～ checkSumAdjustment: 0x{0:X8} / 0x{1:X8}",csaR-sum2,sum2-csaR);
				return ret;
			}
			return this.CalculateTableSum(fileimage)==(uint)this.checkSum;
		}
		private unsafe uint SumEntireFont(byte[] fileimage){
			uint sum=0;
			fixed(byte* img=&fileimage[0]){
				ULONG* cptr=(ULONG*)img;
				ULONG* Endptr=(ULONG*)(img+((fileimage.Length+3)&~3));
				while(cptr<Endptr)sum+=(uint)*cptr++;
			}
			return sum;
		}
#endif
		public unsafe uint CalculateTableSum(byte[] fileimage){
			uint sum=0;
			fixed(byte* img=&fileimage[0]){
				ULONG* cptr=(ULONG*)(img+(uint)this.offset);
				ULONG* Endptr=(ULONG*)(img+(uint)this.offset+(((uint)this.length+3)&~3));
				while(cptr<Endptr)sum+=(uint)*cptr++;
			}
			//System.Console.WriteLine("Calculated Sum of the Table {0}: 0x{1:X8}",this.TagName,sum);
			return sum;
		}
		public uint Sum{
			get { return (uint)this.tag+(uint)this.checkSum+(uint)this.offset+(uint)this.length; }
		}
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct headTable{
		public FIXED TableVersionNumber;
		public FIXED fontRevision;
		public ULONG checkSumAdjustment;
		public ULONG magicNumber;
		public USHORT flags;
		public USHORT unitsPerEm;
		public longDateTime created;
		public longDateTime modified;
		public SHORT xMin;
		public SHORT yMin;
		public SHORT xMax;
		public SHORT yMax;
		public USHORT macStyle;
		public USHORT lowestRecPPEM;
		public SHORT fontDirectionHint;
		public SHORT indexToLocFormat;
		public SHORT glyphDataFormat;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct longDateTime{
		private byte b0;
		private byte b1;
		private byte b2;
		private byte b3;
		private byte b4;
		private byte b5;
		private byte b6;
		private byte b7;
		public static implicit operator System.DateTime(longDateTime dt){
			System.DateTime r=new System.DateTime(1904,1,1);
			long sec=((long)dt.b0<<8)+dt.b1;
			sec=(sec<<8)+dt.b2;
			sec=(sec<<8)+dt.b3;
			sec=(sec<<8)+dt.b4;
			sec=(sec<<8)+dt.b5;
			sec=(sec<<8)+dt.b6;
			sec=(sec<<8)+dt.b7;
			return r+new System.TimeSpan(checked((int)(sec/86400)),0,0,(int)(sec%86400));
		}
	}
	public sealed class headTreeNode:TTF.TableTreeNode{
		public headTreeNode(TTFFile file,TableDirectoryEntry entry):base(file,entry){}
		public override unsafe System.Windows.Forms.Control GetControl() {
			UI.TreeNode.ShareLog.Clear();
			fixed(byte* pImg=&this.file.image[0]){
				headTable* table=(headTable*)(pImg+(uint)entry.offset);
				UI.TreeNode.ShareLog.WriteLine(
					"《head table》\r\n表形式 {0}\r\nフォントのバージョン {1}\r\n毎 em {2} 単位\r\n作成 {3}\r\n更新 {4}\r\n矩形 ({5},{6}) - ({7},{8})"
					,table->TableVersionNumber.Value
					,table->fontRevision.Value
					,(ushort)table->unitsPerEm
					,(System.DateTime)table->created,(System.DateTime)table->modified
					,(short)table->xMin,(short)table->yMin,(short)table->xMax,(short)table->yMax);
			}
			return UI.TreeNode.ShareTxtBox;
		}
	}
}
