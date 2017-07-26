using System;

namespace JT808
{
	[AttributeUsage(AttributeTargets.Class)]
    public class MessageTypeAttribute : Attribute
    {
        public MessageTypeAttribute()
        {
        }

		public MessageTypeAttribute(ushort id)
		{
			ID = id;
		}

		public ushort ID { get; set; }

		public bool NoBody { get; set; } = false;
    }
}
