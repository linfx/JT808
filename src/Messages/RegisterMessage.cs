namespace JT808.Messages
{
	/// <summary>
	/// 终端注册
	/// </summary>
	[MessageType(ID = 0x0100)]
    public class RegisterMessage 
    {
		/// <summary>
		/// 省ID
		/// </summary>
		[UInt16Handler]
        public ushort Province { get; set; }
		/// <summary>
		/// 市、县域ID
		/// </summary>
		[UInt16Handler]
        public ushort City { get; set; }
		/// <summary>
		/// 五个字节，终端制造商编码。
		/// </summary>
		[ASCIIHandler(5)]
        public string Provider { get; set; }
		/// <summary>
		/// 终端型号 
		/// </summary>
		[ASCIIHandler(8)]
        public string DeviceNo { get; set; }
		/// <summary>
		/// 终端ID 
		/// </summary>
		[ASCIIHandler(7)]
        public string DeviceID { get; set; }
		/// <summary>
		/// 车牌颜色，按照JT/T 415-2006的5.4.12
		/// </summary>
		[ByteHandler]
        public byte Color { get; set; }
		/// <summary>
		/// 公安交通管理部门颁发的机动车号牌
		/// </summary>
		[GBKHandler]
        public string PlateNo { get; set; }
    }

	/// <summary>
	/// 终端注册应答 
	/// </summary>
	[MessageType(0x8100)]
	public class RegisterResponseMessage
	{
		/// <summary>
		/// 应答流水号 
		/// </summary>
		[UInt16Handler]
		public ushort No { get; set; }
		/// <summary>
		/// 结果 
		/// </summary>
		[ByteHandler]
		public RegisterResult Result { get; set; }
		/// <summary>
		/// 鉴权码 
		/// </summary>
		[GBKHandler]
		public string Signature { get; set; }
	}

	[MessageType(NoBody = true, ID = 0x0003)]
	public class RegisterCancelMessage
	{
	}

	public enum RegisterResult : byte
	{
		成功             = 0,
		车辆已被注       = 1,
		数据库中无该车辆 = 2,
		终端已被注册     = 3,
		数据库中无该终端 = 4,
	}
}
