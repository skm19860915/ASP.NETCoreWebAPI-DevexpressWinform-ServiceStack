using System;

namespace Xperters.Admin.ServiceModel.Exceptions
{
	public class XpertersException : Exception
	{
		public XpertersException()
		{
		}

		public XpertersException(string message)
			: base(message)
		{
		}

		public XpertersException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
