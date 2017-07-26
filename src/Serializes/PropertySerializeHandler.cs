using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JT808.Serializes
{
    class PropertySerializeHandler
    {
        public PropertySerializeHandler(System.Reflection.PropertyInfo property, ReadWriteHandlerAttribute readwritehandler)
        {
            Handler = new PropertyHandler(property);
            ReadWriteHandler = readwritehandler;
        }

        public PropertyHandler Handler { get; private set; }

        public ReadWriteHandlerAttribute ReadWriteHandler { get; private set; }

        public void Read(object target, IProtocolBuffer buffer)
        {
            object value = ReadWriteHandler.Read(buffer);
            Handler.Set(target, value);
        }

        public void Write(object target, IProtocolBuffer buffer)
        {
            object value = Handler.Get(target);
            if (value == null)
                throw new ProtocolProcessError(string.Format("{0}.{1} value can't be null!", Handler.Property.DeclaringType, Handler.Property.PropertyType));
            ReadWriteHandler.Write(value, buffer);
        }
    }
}
