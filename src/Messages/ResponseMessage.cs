namespace JT808.Messages
{
	/// <summary>
	/// 平台通用应答
	/// </summary>
	[MessageType(ID = 0x8001)]
    public class CenterResponseMessage
    {
		/// <summary>
		/// 应答流水号 (对应的终端消息的流水号)
		/// </summary>
		[UInt16Handler]
        public ushort No { get; set; }
		/// <summary>
		/// 应答 ID  (对应的终端消息的 ID )
		/// </summary>
		[UInt16Handler]
        public ushort ResultID { get; set; }
		/// <summary>
		/// 结果
		/// </summary>
		[ByteHandler]
        public ResultType Result { get; set; }
    }

	/// <summary>
	/// 终端通用应答 
	/// </summary>
	[MessageType(ID = 0x0001)]
	public class ClientResponseMessage
	{
		/// <summary>
		/// 应答流水号
		/// </summary>
		[UInt16Handler]
		public ushort No { get; set; }
		/// <summary>
		/// 应答 ID
		/// </summary>
		[UInt16Handler]
		public ushort ResultID { get; set; }
		/// <summary>
		/// 结果
		/// </summary>
		[ByteHandler]
		public ResultType Result { get; set; }
	}

	public enum ResultType : byte
	{
		Success = 0,
		Failure = 1,
		Error = 2,
		NotSupport = 3
	}
}
