using System;
using System.Collections.Generic;

namespace JT808.Serializes
{
	public class Serializer
    {
        public Serializer(Type bodyType, MessageTypeAttribute msgType)
        {
            mBodyType = bodyType;
            MessageType = msgType;
            Init();
        }

        private Type mBodyType;

        private List<PropertySerializeHandler> mProperties = new List<PropertySerializeHandler>();

        private void Init()
        {
            try
            {
                foreach (System.Reflection.PropertyInfo p in mBodyType.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
                {
                    ReadWriteHandlerAttribute[] rwha = (ReadWriteHandlerAttribute[])p.GetCustomAttributes(typeof(ReadWriteHandlerAttribute), true);
                    if (rwha != null && rwha.Length > 0)
                    {
                        PropertySerializeHandler handler = new PropertySerializeHandler(p, rwha[0]);
                        mProperties.Add(handler);
                    }
                }
            }
            catch (Exception e_)
            {
                throw new ProtocolProcessError(string.Format("{0} init error {1}", mBodyType.Name, e_.Message), e_);
            }
        }

        public MessageTypeAttribute MessageType { get; set; }

        public void Deserialize(object obj, IProtocolBuffer buffer)
        {
            if (obj is IMessageBody)
            {
                ((IMessageBody)obj).Load(buffer);
            }
            else
            {
                foreach (PropertySerializeHandler item in mProperties)
                {
                    item.Read(obj, buffer);
                }
            }
        }

        public void Serialize(object obj, IProtocolBuffer buffer)
        {
            if (obj is IMessageBody)
                ((IMessageBody)obj).Save(buffer);
            else
            {
                foreach (PropertySerializeHandler item in mProperties)
                {
                    item.Write(obj, buffer);
                }
            }
        }

        public object CreateObject()
        {
            return Activator.CreateInstance(mBodyType);
        }

    }

    public class SerializerFactory
    {
        Dictionary<Type, Serializer> mTypeSerializersMap = new Dictionary<Type, Serializer>();
        Dictionary<ushort, Serializer> mIDSerializersMap = new Dictionary<ushort, Serializer>();

        private void Register(Type type, MessageTypeAttribute msgType)
        {
            Serializer serializer = new Serializer(type, msgType);
            mTypeSerializersMap[type] = serializer;
            mIDSerializersMap[serializer.MessageType.ID] = serializer;
        }

        public Serializer Get(ushort id)
        {
			mIDSerializersMap.TryGetValue(id, out Serializer result);
			return result;
        }

        public Serializer Get(Type type)
        {
			mTypeSerializersMap.TryGetValue(type, out Serializer result);
			return result;
        }

        public static void Init()
        {
            SerializerFactory factory = Defalut;
        }

        private static SerializerFactory mDefault = null;

        public static SerializerFactory Defalut
        {
            get
            {
                if (mDefault == null)
                {
                    mDefault = new SerializerFactory();
                    foreach (Type type in typeof(SerializerFactory).Assembly.GetTypes())
                    {
                        MessageTypeAttribute[] mta = (MessageTypeAttribute[])type.GetCustomAttributes(typeof(MessageTypeAttribute), false);
                        if (mta != null && mta.Length > 0)
                        {
                            mDefault.Register(type, mta[0]);
                        }
                    }
                }
                return mDefault;
            }

        }
    }
}
