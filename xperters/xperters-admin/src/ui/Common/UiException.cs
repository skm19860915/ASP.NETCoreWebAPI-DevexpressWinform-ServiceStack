using System;

using Xperters.Admin.ServiceModel.Exceptions;

namespace Xperters.Admin.UI.Common
{
    public class UiException : BaseException
    {
        public UiException()
        {
        }

        public UiException(string message)
            : base(message)
        {
        }

    }
}
