using System;

namespace JT808
{
	public interface IMessage
    {
        ushort ID { get; set; }
        MessageBodyProperty Property { get; set; }
        string SIM { get; set; }
        ushort No { get; set; }
        PacketInfo Packet { get; set; }
        void Write(IProtocolBuffer buffer);
        void Read(IProtocolBuffer buffer);
        object Body { get; set; }
        byte CRC { get; set; }
        T GetBody<T>();
    }

	public interface IMessageBody
	{
		void Save(IProtocolBuffer buffer);
		void Load(IProtocolBuffer buffer);
	}

	public class MessageFactory
	{
		public static Message DecodeMessage(IProtocolBuffer buffer)
		{
			Message msg = new Message();
			msg.Read(buffer);
			return msg;
		}

		public static IProtocolBuffer CreateMessage<T>(ushort businessNO, string sim, Action<IMessage, T> handler) where T : new()
		{
			var buffer = ProtocolBufferPool.Default.Pop();
			var msg = new Message
			{
				No = businessNO,
				SIM = sim
			};
			T body = new T();
			msg.Body = body;
			handler?.Invoke(msg, body);
			msg.Write(buffer);
			return buffer;
		}

		public static ushort GetMessageID<T>()
		{
			Type type = typeof(T);
			Serializes.Serializer serializer = Serializes.SerializerFactory.Defalut.Get(type);
			if(serializer == null)
				throw new ProtocolProcessError(string.Format("{0} serializer not found!", type));
			return serializer.MessageType.ID;
		}
	}
}
