using System;
using System.Collections.Generic;
using System.Text;
using xperters.domain;

namespace xperters.mockdata
{
    public class CurrencyMock
    {
        private static readonly List<CurrencyDto> _currencyDto;
        public static CurrencyDto currencies1 => Get()[0];
        static CurrencyMock()
        {
            _currencyDto = new List<CurrencyDto>
            {
                new CurrencyDto
                {
                    CurrencyId = 1,
                    CurrencyCode = "USD"
                }
            };
        }
        public static List<CurrencyDto> Get()
        {
            return _currencyDto;
        }
    }
}
