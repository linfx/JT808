using System;

namespace JT808
{
	public class ProtocolProcessError : Exception
    {
        public ProtocolProcessError(){ }

        public ProtocolProcessError(string error) : base(error) { }

        public ProtocolProcessError(string error, Exception e) : base(error, e) { }
    }
}
