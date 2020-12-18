using System;
using System.Collections.Generic;
using System.Text;
using xperters.enums;

namespace xperters.business.Extensions
{
    public static class Utility
    {
        public static string GetCurrencyDescription(int id)
        {
            if (id == Enums.CurrencyType.USD.GetEnumValue())
                return Enums.CurrencyType.USD.GetDescription();

            return Enums.CurrencyType.EUR.GetDescription();
        }

        public static string GetRequestPayerStatusValue(int id)
        {
            var value = string.Empty;

            switch (id)
            {
                case (int)Enums.RequestPayerStatus.Successful:
                    value = Enums.RequestPayerStatus.Successful.GetDescription();
                    break;
                case (int)Enums.RequestPayerStatus.Pending:
                    value = Enums.RequestPayerStatus.Pending.GetDescription();
                    break;
                case (int)Enums.RequestPayerStatus.Cancelled:
                    value = Enums.RequestPayerStatus.Cancelled.GetDescription();
                    break;
                case (int)Enums.RequestPayerStatus.Failed:
                    value = Enums.RequestPayerStatus.Failed.GetDescription();
                    break;
            }

            return value;
        }
    }
}
