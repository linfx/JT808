using System.Text;

namespace JT808
{
	class BufferUtils
    {
        public byte[] Encode(string value)
        {
            return Encoding.GetEncoding("GBK").GetBytes(value);
        }

        public string Decode(byte[] data, int offset, int count)
        {
            return Encoding.GetEncoding("GBK").GetString(data, offset, count);
        }

        public static short SwapInt16(short v)
        {
            return (short)(((v & 0xff) << 8) | ((v >> 8) & 0xff));
        }
        public static ushort SwapUInt16(ushort v)
        {
            return (ushort)(((v & 0xff) << 8) | ((v >> 8) & 0xff));
        }
        public static int SwapInt32(int v)
        {
            return ((SwapInt16((short)v) & 0xffff) << 0x10) | (SwapInt16((short)(v >> 0x10)) & 0xffff);
        }
        public static uint SwapUInt32(uint v)
        {
            return (uint)(((SwapUInt16((ushort)v) & 0xffff) << 0x10) | (SwapUInt16((ushort)(v >> 0x10)) & 0xffff));
        }
        public static long SwapInt64(long v)
        {
            return ((SwapInt32((int)v) & 0xffffffffL) << 0x20) | (SwapInt32((int)(v >> 0x20)) & 0xffffffffL);
        }
        public static ulong SwapUInt64(ulong v)
        {
            return (ulong)(((SwapUInt32((uint)v) & 0xffffffffL) << 0x20) | (SwapUInt32((uint)(v >> 0x20)) & 0xffffffffL));
        }

        public static byte GetCRC(byte[] array, int offset, int count)
        {
            byte crc = 1;
            for (int i = offset; i < offset + count; i++)
            {
                if (i == 1)
                    crc = array[i];
                else
                    crc = (byte)(crc ^ array[i]);
            }
            return crc;
        }

        public static bool GetBitValue(uint value, int index)
        {
            uint tag = 1;
            tag = tag << (index);
            return (value & tag) > 0;
        }

        public static byte GetByteBitValue(params bool[] values)
        {
            byte result = 0;
            for (int i = 0; i < values.Length; i++)
                result = (byte)(result | ((values[i] ? 1 : 0) << i));
            return result;
        }

        public static ushort GetUShortBitValue(params bool[] values)
        {
            ushort result = 0;
            for (int i = 0; i < values.Length; i++)
                result = (ushort)(result | ((values[i] ? 1 : 0) << i));
            return result;
        }

        public static uint GetUIntBitValue(params bool[] values)
        {
            uint result = 0;
            for (int i = 0; i < values.Length; i++)
                result = (uint)(result | ((values[i] ? (uint)1 : (uint)0) << i));
            return result;
        }
    }
}
