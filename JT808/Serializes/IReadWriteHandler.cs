using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JT808
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ReadWriteHandlerAttribute : Attribute
    {
        public abstract object Read(IProtocolBuffer buffer);

        public abstract void Write(object value, IProtocolBuffer buffer);
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ByteHandler : ReadWriteHandlerAttribute
    {

        public override object Read(IProtocolBuffer buffer)
        {
            return buffer.Read();
        }

        public override void Write(object value, IProtocolBuffer buffer)
        {
            buffer.Write((byte)value);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class BytesHandler : ReadWriteHandlerAttribute
    {

        public BytesHandler(int length)
        {
            Length = length;
        }
        public int Length { get; set; }

        public override object Read(IProtocolBuffer buffer)
        {
            return buffer.Read(Length);
        }

        public override void Write(object value, IProtocolBuffer buffer)
        {
            buffer.Write((byte[])value);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class UInt16Handler : ReadWriteHandlerAttribute
    {

        public override object Read(IProtocolBuffer buffer)
        {
            return buffer.ReadUInt16();
        }

        public override void Write(object value, IProtocolBuffer buffer)
        {
            buffer.Write((UInt16)value);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class UIntHandler : ReadWriteHandlerAttribute
    {

        public override object Read(IProtocolBuffer buffer)
        {
            return buffer.ReadUInt();
        }

        public override void Write(object value, IProtocolBuffer buffer)
        {
            buffer.Write((uint)value);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ASCIIHandler : ReadWriteHandlerAttribute
    {
        public ASCIIHandler(int length)
        {
            Length = length;
        }
        public int Length { get; set; }

        public override object Read(IProtocolBuffer buffer)
        {
            return buffer.ReadASCII(Length).TrimStart(' ');
        }

        public override void Write(object value, IProtocolBuffer buffer)
        {
            buffer.WriteASCII((string)value, Length);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CBDHandler : ReadWriteHandlerAttribute
    {
        public CBDHandler(int length)
        {
            Length = length;
        }

        public int Length { get; set; }

        public override object Read(IProtocolBuffer buffer)
        {
            return buffer.ReadBCD(Length);
        }

        public override void Write(object value, IProtocolBuffer buffer)
        {
            buffer.WriteBCD((string)value);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class GBKHandler : ReadWriteHandlerAttribute
    {
        public GBKHandler(int length = -1)
        {
            Length = length;
        }

        public int Length { get; set; }

        public override object Read(IProtocolBuffer buffer)
        {
            return buffer.ReadGBK(Length);
        }

        public override void Write(object value, IProtocolBuffer buffer)
        {
            buffer.WriteGBK((string)value);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class TimeBCD : ReadWriteHandlerAttribute
    {
        public override object Read(IProtocolBuffer buffer)
        {
            string value = buffer.ReadBCD(6);
            int year = int.Parse("20" + value.Substring(0, 2));
            int month = int.Parse(value.Substring(2, 2));
            int day = int.Parse(value.Substring(4, 2));
            int hh = int.Parse(value.Substring(6, 2));
            int mm = int.Parse(value.Substring(8, 2));
            int ss = int.Parse(value.Substring(10, 2));
            return new DateTime(year, month, day, hh, mm, ss);
        }

        public override void Write(object value, IProtocolBuffer buffer)
        {
            string time = ((DateTime)value).ToString("yyMMddHHmmss");
            buffer.WriteBCD(time);
        }
    }


    [AttributeUsage(AttributeTargets.Property)]
    public class ByteBitHandler : ReadWriteHandlerAttribute
    {
        public ByteBitHandler(Type type)
        {
            mType = type;
        }
        private Type mType;

        public override object Read(IProtocolBuffer buffer)
        {
            byte data = buffer.Read();
            IBitCustomType result = (IBitCustomType)Activator.CreateInstance(mType);
            result.Load(data);
            return result;
        }

        public override void Write(object value, IProtocolBuffer buffer)
        {
            byte data = (byte)((IBitCustomType)value).Save();
            buffer.Write(data);
        }

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class UInt16BitHandler : ReadWriteHandlerAttribute
    {
        public UInt16BitHandler(Type type)
        {
            mType = type;
        }
        private Type mType;

        public override object Read(IProtocolBuffer buffer)
        {
            UInt16 data = buffer.ReadUInt16();
            IBitCustomType result = (IBitCustomType)Activator.CreateInstance(mType);
            result.Load(data);
            return result;
        }

        public override void Write(object value, IProtocolBuffer buffer)
        {
            UInt16 data = (UInt16)((IBitCustomType)value).Save();
            buffer.Write(data);
        }

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class UIntBitHandler : ReadWriteHandlerAttribute
    {
        public UIntBitHandler(Type type)
        {
            mType = type;
        }
        private Type mType;

        public override object Read(IProtocolBuffer buffer)
        {
            uint data = buffer.ReadUInt();
            IBitCustomType result = (IBitCustomType)Activator.CreateInstance(mType);
            result.Load(data);
            return result;
        }

        public override void Write(object value, IProtocolBuffer buffer)
        {
            uint data = (uint)((IBitCustomType)value).Save();
            buffer.Write(data);
        }

    }
}
