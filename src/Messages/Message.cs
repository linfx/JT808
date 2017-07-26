using System;
using JT808.Serializes;

namespace JT808
{
	public class Message : IMessage
	{ 
		/// <summary>
		/// 消息ID 
		/// </summary>
		public ushort ID { get; set; }
		/// <summary>
		/// 消息体属性 
		/// </summary>
		public MessageBodyProperty Property { get; set; } = new MessageBodyProperty();
		/// <summary>
		/// 终端手机号
		/// </summary>
		public string SIM { get; set; }
		/// <summary>
		/// 消息流水号
		/// </summary>
		public ushort No { get; set; }
		/// <summary>
		/// 消息包封装项 
		/// </summary>
		public PacketInfo Packet { get; set; }
		/// <summary>
		/// 消息体 
		/// </summary>
		public object Body { get; set; }
		/// <summary>
		/// 校验码
		/// </summary>
		public byte CRC { get; set; }

		public T GetBody<T>()
		{
			return (T)Body;
		}

		public void Read(IProtocolBuffer buffer)
        {
            //byte crc = Core.GetCRC(buffer.Array, 1, buffer.Length - 3);
			//CRC = buffer.Array[buffer.Length - 2];
			//if(CRC != crc)
			//	throw new ProtocolProcessError("message check CRC error!");

			buffer.Read();              //read start      标识位 
			ID = buffer.ReadUInt16();   //read id         消息 ID 
			Property.Read(buffer);      //read property   消息体属性
			SIM = buffer.ReadBCD(6);    //read sim        终端手机号 
			No = buffer.ReadUInt16();   //read no 消息流水号 
			if (Property.IsPacket)      //read packet     消息包封装项 
			{
                Packet = new PacketInfo();
                Packet.Load(buffer);
            }
            if (Property.BodyLength > 0) //read body      消息体 
			{
                IProtocolBuffer bodybuffer = ProtocolBufferPool.Default.Pop();
                try
                {
                    buffer.ReadSubBuffer(bodybuffer, Property.BodyLength);
                    Serializer serializer = SerializerFactory.Defalut.Get(ID);
					if(serializer != null)
					{
						Body = serializer.CreateObject();
						serializer.Deserialize(Body, bodybuffer);
					}
				}
                finally
                {
                    ProtocolBufferPool.Default.Push(bodybuffer);
                }
            }
			CRC = buffer.Read(); //read crc  检验码 
			buffer.Read();       //read end  标识位 
		}

        public void Write(IProtocolBuffer buffer)
        {
            IProtocolBuffer bodybuffer = null;
            try
            {
                if (Packet != null)
                    Property.IsPacket = true;
                if (Body != null)
                {
                    Serializer serializer = SerializerFactory.Defalut.Get(Body.GetType());
                    if (serializer == null)
                        throw new ProtocolProcessError(string.Format("{0} serializer not found!", Body));
                    ID = serializer.MessageType.ID;
                    if (!serializer.MessageType.NoBody)
                    {
                        bodybuffer = ProtocolBufferPool.Default.Pop();
                        serializer.Serialize(Body, bodybuffer);
                        if (bodybuffer.Length > MessageBodyProperty.BODY_LENGTH)
                            throw new ProtocolProcessError("message body to long!");
                        Property.BodyLength = (ushort)bodybuffer.Length;
                    }
                }
                buffer.WriteTag();           //write start
                buffer.Write(ID);            //write id
                Property.Write(buffer);      //write body property
                buffer.WriteBCD(SIM);        //write sim
                buffer.Write(No);            //write no
                if (Packet != null)          //write packet
                    Packet.Save(buffer);
                if (bodybuffer != null)      //write body
                    buffer.WriteSubBuffer(bodybuffer);
                byte crc = BufferUtils.GetCRC(buffer.Array, 1, buffer.Length - 1);
                buffer.Write(crc);           //write crc         
                buffer.WriteTag();           //write end
            }
            finally
            {
                if (bodybuffer != null)
                    ProtocolBufferPool.Default.Push(bodybuffer);
            }
        }
    }

	public class MessageBodyProperty
	{
		public const ushort CUSTOM_HEIGHT = 0x8000;
		public const ushort CUSTOM_LOW = 0x4000;
		public const ushort IS_PACKET = 0x2000;
		public const ushort ENCRYPT_HEIGHT = 0x1000;
		public const ushort ENCRYPT_MIDDLE = 0x0400;
		public const ushort ENCRYPT_LOW = 0x0200;
		public const ushort BODY_LENGTH = 0x01FF;

		//保留位15
		public bool CustomHigh { get; set; }

		//保留位14
		public bool CustomLow { get; set; }

		//分包位13
		public bool IsPacket { get; set; }

		//加密位12
		public bool EncryptHigh { get; set; }

		//加密位11
		public bool EncryptMiddle { get; set; }

		//加密位10
		public bool EncryptLow { get; set; }

		//消息长度9-0
		public ushort BodyLength { get; set; }

		public void Read(IProtocolBuffer buffer)
		{
			ushort value = buffer.ReadUInt16();
			CustomHigh = (CUSTOM_HEIGHT & value) > 0;
			CustomLow = (CUSTOM_LOW & value) > 0;
			IsPacket = (IS_PACKET & value) > 0;
			EncryptHigh = (ENCRYPT_HEIGHT & value) > 0;
			EncryptMiddle = (ENCRYPT_MIDDLE & value) > 0;
			EncryptLow = (ENCRYPT_LOW & value) > 0;
			BodyLength = (ushort)(BODY_LENGTH & value);
		}

		public void Write(IProtocolBuffer buffer)
		{
			ushort value = (ushort)(BodyLength & BODY_LENGTH);
			if(CustomHigh)
				value |= CUSTOM_HEIGHT;
			if(CustomLow)
				value |= CUSTOM_LOW;
			if(IsPacket)
				value |= IS_PACKET;
			if(EncryptHigh)
				value |= ENCRYPT_HEIGHT;
			if(EncryptMiddle)
				value |= ENCRYPT_MIDDLE;
			if(EncryptLow)
				value |= ENCRYPT_LOW;
			buffer.Write(value);
		}
	}

	public class PacketInfo
	{
		public ushort Count { get; set; }

		public ushort Index { get; set; }

		public void Save(IProtocolBuffer buffer)
		{
			Count = BufferUtils.SwapUInt16(Count);
			Index = BufferUtils.SwapUInt16(Index);
		}

		public void Load(IProtocolBuffer buffer)
		{
			byte[] data = buffer.Read(2);
			Count = BitConverter.ToUInt16(data, 0);
			Count = BufferUtils.SwapUInt16(Count);

			data = buffer.Read(2);
			Index = BitConverter.ToUInt16(data, 0);
			Index = BufferUtils.SwapUInt16(Count);
		}
	}
}