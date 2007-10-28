using Interop=System.Runtime.InteropServices;
namespace FontEditor.TTF{
	[Interop::StructLayout(Interop.LayoutKind.Sequential)]
	public struct USHORT{
		private byte data0;
		private byte data1;
		public USHORT(ushort val) {
			this.data0=(byte)(val>>8);
			this.data1=(byte)val;
		}
		#region ‰‰ŽZŽq‚»‚Ì‘¼
		public static explicit operator ushort(USHORT value) {
			return (ushort)((value.data0<<8)|value.data1);
		}
		public static explicit operator USHORT(ushort value) {
			return new USHORT(value);
		}
		public static USHORT operator +(USHORT l,USHORT r) {
			return (USHORT)((ushort)l+(ushort)r);
		}
		public static bool operator ==(USHORT l,USHORT r) {
			return l.data0==r.data0&&l.data1==r.data1;
		}
		public static bool operator !=(USHORT l,USHORT r) {return !(l==r); }
		public override bool Equals(object obj){
			if(obj is USHORT) {
				return this==(USHORT)obj;
			}else return false;
		}
		public override int GetHashCode() { return ((ushort)this).GetHashCode(); }
		public override string ToString() { return ((ushort)this).ToString(); }
		#endregion
	}
	[Interop::StructLayout(Interop.LayoutKind.Sequential)]
	public struct SHORT{
		private byte data0;
		private byte data1;
		public SHORT(short val) {
			this.data0=(byte)(val>>8);
			this.data1=(byte)val;
		}
		#region ‰‰ŽZŽq‚»‚Ì‘¼
		public static explicit operator short(SHORT value) {
			return (short)((value.data0<<8)|value.data1);
		}
		public static explicit operator SHORT(short value) {
			return new SHORT(value);
		}
		public static SHORT operator +(SHORT l,SHORT r) {
			return (SHORT)((short)l+(short)r);
		}
		public static bool operator ==(SHORT l,SHORT r) {
			return l.data0==r.data0&&l.data1==r.data1;
		}
		public static bool operator !=(SHORT l,SHORT r) {return !(l==r); }
		public override bool Equals(object obj){
			if(obj is SHORT) {
				return this==(SHORT)obj;
			}else return false;
		}
		public override int GetHashCode() { return ((short)this).GetHashCode(); }
		public override string ToString() { return ((short)this).ToString(); }
		#endregion
	}
	[Interop::StructLayout(Interop.LayoutKind.Sequential)]//,Size=4
	public struct ULONG{
		//[Interop::MarshalAs(Interop::UnmanagedType.ByValArray,SizeConst=4)]
		//private byte[] data;
		private byte data0;
		private byte data1;
		private byte data2;
		private byte data3;
		public ULONG(uint val) {
			//this.data=new byte[4];
			this.data0=(byte)(val>>24);
			this.data1=(byte)(val>>16);
			this.data2=(byte)(val>>8);
			this.data3=(byte)val;
		}
		public string GetFourCC(){
			return System.Text.Encoding.ASCII.GetString(new byte[]{this.data0,this.data1,this.data2,this.data3});
		}

		#region ‰‰ŽZŽq‚»‚Ì‘¼
		public static explicit operator uint(ULONG value){
			return (uint)(value.data3|(value.data2<<8)|(value.data1<<16)|(value.data0<<24));
		}
		public static explicit operator ULONG(uint value){
			return new ULONG(value);
		}
		public static ULONG operator +(ULONG l,ULONG r){
			return (ULONG)((uint)l+(uint)r);
		}
		public static bool operator ==(ULONG l,ULONG r){
			return l.data0==r.data0&&l.data1==r.data1&&l.data2==r.data2&&l.data3==r.data3;
		}
		public static bool operator !=(ULONG l,ULONG r){return !(l==r);}
		public override bool Equals(object obj){
			if(obj is ULONG){
				return this==(ULONG)obj;
			}else return false;
		}
		public override int GetHashCode(){return ((uint)this).GetHashCode();}
		public override string ToString(){return ((uint)this).ToString();}
		#endregion
	}
	[Interop::StructLayout(Interop.LayoutKind.Sequential)]//,Size=4
	public struct LONG{
		//[Interop::MarshalAs(Interop::UnmanagedType.ByValArray,SizeConst=4)]
		//private byte[] data;
		private byte data0;
		private byte data1;
		private byte data2;
		private byte data3;
		public LONG(int val) {
			//this.data=new byte[4];
			this.data0=(byte)(val>>24);
			this.data1=(byte)(val>>16);
			this.data2=(byte)(val>>8);
			this.data3=(byte)val;
		}
		#region ‰‰ŽZŽq‚»‚Ì‘¼
		public static explicit operator int(LONG value){
			return value.data3|(value.data2<<8)|(value.data1<<16)|(value.data0<<24);
		}
		public static explicit operator LONG(int value){
			return new LONG(value);
		}
		public static LONG operator +(LONG l,LONG r){
			return (LONG)((int)l+(int)r);
		}
		public static bool operator ==(LONG l,LONG r){
			return l.data0==r.data0&&l.data1==r.data1&&l.data2==r.data2&&l.data3==r.data3;
		}
		public static bool operator !=(LONG l,LONG r){return !(l==r);}
		public override bool Equals(object obj){
			if(obj is LONG){
				return this==(LONG)obj;
			}else return false;
		}
		public override int GetHashCode(){return ((int)this).GetHashCode();}
		public override string ToString(){return ((int)this).ToString();}
		#endregion
	}
	public struct FIXED{
		private LONG value;
		public short Mantissa{
			get{return (short)((int)this.value>>16);}
		}
		public ushort Fraction{
			get{return (ushort)this.value;}
		}
		public double Value{
			get{return (int)this.value/(double)0x10000;}
			set{
				if(value>=short.MaxValue+1||value<short.MinValue)
					throw new System.ArgumentOutOfRangeException("value");
				this.value=(LONG)(int)(value*0x10000);
			}
		}
		public FIXED(double val){
			if(val>=short.MaxValue+1||val<short.MinValue)
				throw new System.ArgumentOutOfRangeException("val");
			this.value=(LONG)(int)(val*0x10000);
		}
		public static explicit operator FIXED(int val){return new FIXED(val);}
		public static explicit operator int(FIXED val){return (int)val.value;}
		public static explicit operator double(FIXED val){return val.Value;}
	}
	public struct F2DOT14{
		private SHORT value;
		public short Mantissa{
			get{return (short)((short)this.value>>14);}
		}
		public short Fraction{
			get{return (short)((short)this.value&0x3fff);}
		}
		public double Value{
			get{return (short)this.value/(double)0x4000;}
			set{
				if(value>=short.MaxValue+1||value<short.MinValue)
					throw new System.ArgumentOutOfRangeException("value");
				this.value=(SHORT)(short)(value*0x4000);
			}
		}
		public F2DOT14(double val){
			if(val>=short.MaxValue+1||val<short.MinValue)
				throw new System.ArgumentOutOfRangeException("val");
			this.value=(SHORT)(short)(val*0x4000);
		}
	}
	
}