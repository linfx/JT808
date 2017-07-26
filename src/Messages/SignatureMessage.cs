namespace JT808.Messages
{
	/// <summary>
	/// 终端鉴权
	/// </summary>
	[MessageType(ID = 0x0102)]
    public class SignatureMessage
    {
        [GBKHandler]
        public string Signature { get; set; }
    }
}