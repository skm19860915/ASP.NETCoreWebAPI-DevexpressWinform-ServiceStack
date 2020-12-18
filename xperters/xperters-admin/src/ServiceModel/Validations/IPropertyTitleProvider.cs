using System;
using System.Collections.Generic;
using System.Text;

namespace Xperters.Admin.ServiceModel.Validations
{
    public interface IPropertyTitleProvider
    {
        string GetTitle(string propertyPath);
    }
}
