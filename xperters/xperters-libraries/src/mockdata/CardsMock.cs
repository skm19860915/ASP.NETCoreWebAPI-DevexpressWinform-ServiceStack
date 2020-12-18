using System;
using System.Collections.Generic;
using System.Linq;
using xperters.domain;
using xperters.entities.Entities;
using xperters.models.Enums;

namespace xperters.mockdata
{
    public static class CardsMock
    {
        private static readonly List<Card> Items;
        public static readonly Card FirstCard;
        public static readonly Card SecondCard;
        public static readonly Card ThirdCard;

        static CardsMock()
        {
            
            int expYear = DateTime.Now.AddYears(1).Year;
            int expMonth = DateTime.Now.Month;

            FirstCard = new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000001}"), UserId = Users.CustomerFirst.Id, Country = "US", Number = "4242424242424241", NumberSuffix = "4241", CardScheme = CardScheme.Visa, CardType=CardType.CardIncorrectNumber, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, };
            SecondCard = new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000002}"), UserId = Users.CustomerFirst.Id, Country ="US", Number = "4242424242424242", NumberSuffix = "4242", CardScheme = CardScheme.Visa, CardType=CardType.InvalidData, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = 1970, ExpMonth = expMonth, };
            ThirdCard = new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000013}"), UserId = Users.CustomerSecond.Id, Country ="US", Number = "5105105105105100", NumberSuffix = "5100", CardScheme = CardScheme.MastercardPrepaid, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, };
            Items = new List<Card>
            {
                FirstCard,
                SecondCard,
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000003}"), UserId = Users.CustomerSecond.Id, Country="US", Number = "4242424242424243", NumberSuffix = "4243",  CardScheme = CardScheme.Visa, CardType=CardType.InvalidData, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = 13, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000004}"), UserId = Users.CustomerSecond.Id, Country="US", Number = "4242424242424244", NumberSuffix = "4244",  CardScheme = CardScheme.Visa, CardType=CardType.InvalidData, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = 1999, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000005}"), UserId = Users.CustomerSecond.Id, Country="BR", Number = "4000000760000002", NumberSuffix = "0002",  CardScheme = CardScheme.Visa, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000006}"), UserId = Users.CustomerSecond.Id, Country="CA", Number = "4000001240000000", NumberSuffix = "0000",  CardScheme = CardScheme.Visa, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000007}"), UserId = Users.CustomerSecond.Id, Country="CA", Number = "4012888888881881", NumberSuffix = "1881",  CardScheme = CardScheme.Visa, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000008}"), UserId = Users.CustomerSecond.Id, Country="MX", Number = "4000004840000008", NumberSuffix = "0008",  CardScheme = CardScheme.Visa, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000009}"), UserId = Users.CustomerSecond.Id, Country="US", Number = "4000056655665556", NumberSuffix = "5556",  CardScheme = CardScheme.VisaDebit, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth,},
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000010}"), UserId = Users.CustomerSecond.Id, Country="US", Number = "5555555555554444", NumberSuffix = "4444",  CardScheme = CardScheme.Mastercard, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth,},
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000011}"), UserId = Users.CustomerSecond.Id, Country="US", Number = "2223003122003222", NumberSuffix = "3222",  CardScheme = CardScheme.Mastercard2Series, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth,},
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000012}"), UserId = Users.CustomerSecond.Id, Country="US", Number = "5200828282828210", NumberSuffix = "8210",  CardScheme = CardScheme.MastercardDebit, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth,},
                ThirdCard,                                                                                                                                                      
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000014}"), UserId = Users.CustomerThird.Id, Country="US", Number = "378282246310005", NumberSuffix = "0005", CardScheme = CardScheme.AmericanExpress1, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth,},
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000015}"), UserId = Users.CustomerThird.Id, Country="US", Number = "371449635398431", NumberSuffix = "8431", CardScheme = CardScheme.AmericanExpress2, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000016}"), UserId = Users.CustomerThird.Id, Country="US", Number = "6011111111111117", NumberSuffix = "1117", CardScheme = CardScheme.Discover1, IsNotSupported = true, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000017}"), UserId = Users.CustomerThird.Id, Country="US", Number = "6011000990139424", NumberSuffix = "9424", CardScheme = CardScheme.Discover2, IsNotSupported = true, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth,},
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000018}"), UserId = Users.CustomerThird.Id, Country="US", Number = "30569309025904", NumberSuffix = "5904", CardScheme = CardScheme.DinersClub1, IsNotSupported = true, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000019}"), UserId = Users.CustomerThird.Id, Country="US", Number = "38520000023237", NumberSuffix = "3237", CardScheme = CardScheme.DinersClub2, IsNotSupported = true, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000020}"), UserId = Users.CustomerThird.Id, Country="US", Number = "3566002020360505", NumberSuffix = "0505", CardScheme = CardScheme.JCB, IsNotSupported = true, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000021}"), UserId = Users.CustomerThird.Id, Country="US", Number = "6200000000000005", NumberSuffix = "0005", CardScheme = CardScheme.UnionPay, IsNotSupported = true, CardType=CardType.Normal, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000022}"), UserId = Users.CustomerThird.Id, Country="US", Number = "4000000000000077", NumberSuffix = "0077", CardScheme = CardScheme.Visa, CardType=CardType.NormalBypass, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth,},
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000023}"), UserId = Users.CustomerThird.Id, Country="US", Number = "4000000000000093", NumberSuffix = "0093", CardScheme = CardScheme.Visa, CardType=CardType.DomesticPricing, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000024}"), UserId = Users.CustomerFirst.Id, Country="US", Number = "4000000000000028", NumberSuffix = "0028", CardScheme = CardScheme.Visa, CardType=CardType.AddressVerifyFails, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth,},
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000025}"), UserId = Users.CustomerFirst.Id, Country="US", Number = "4000000000000036", NumberSuffix = "0036", CardScheme = CardScheme.Visa, CardType=CardType.AddressZipVerifyFails, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000026}"), UserId = Users.CustomerFirst.Id, Country="US", Number = "4000000000000127", NumberSuffix = "0127", CardScheme = CardScheme.Visa, CardType=CardType.CvcIncorrect, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000027}"), UserId = Users.CustomerFirst.Id, Country="US", Number = "4242424242424241", NumberSuffix = "4241", CardScheme = CardScheme.Visa, CardType=CardType.CardIncorrectNumber, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000028}"), UserId = Users.CustomerFirst.Id, Country="US", Number = "4100000000000019", NumberSuffix = "0019", CardScheme = CardScheme.Visa, CardType=CardType.RiskVerifyFails, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000029}"), UserId = Users.CustomerFirst.Id, Country="US", Number = "4000000000000069", NumberSuffix = "0069", CardScheme = CardScheme.Visa, CardType=CardType.CardExpired, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth,},
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000030}"), UserId = Users.CustomerFirst.Id, Country="US", Number = "4000000000009995", NumberSuffix = "9995", CardScheme = CardScheme.Visa, CardType=CardType.InsufficientFunds, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth,},
                new Card{Id = Guid.Parse("{70000000-0000-0000-0000-000000000031}"), UserId = Users.CustomerFirst.Id, Country="US", Number = "4000000000009979", NumberSuffix = "9979", CardScheme = CardScheme.Visa, CardType=CardType.CardStolen, Name = $"{Users.CustomerFirst.FirstName} {Users.CustomerFirst.LastName}", ExpYear = expYear, ExpMonth = expMonth, },
            };

            AddAddressDetails(Items);
        }

        private static void AddAddressDetails(List<Card> items)
        {
            foreach (var card in Items)
            {
                card.AddressLine1 = "This address";
                card.AddressLine2 = "That address";
                card.AddressCity = "London";
                card.AddressState = "Middlesex";
                card.AddressZip = "WC2L0TP";
                card.AddressCountry = "United Kingdom";
                card.Currency = "USD";
            }
        }

        public static List<Card> Get()
        {
            return Items;
        }

        public static List<CardDto> GetDtos()
        {
            return Items.Select(x=>new CardDto
            {
                Id = x.Id,
                CreatedDate = x.CreatedDate,
                ModifiedDate = x.ModifiedDate,
                ExpMonth = x.ExpMonth,
                ExpYear = x.ExpYear,
                Number = x.Number,
                NumberSuffix = x.NumberSuffix,
                AddressCity = x.AddressCity,
                AddressCountry = x.AddressCountry,
                AddressLine1 = x.AddressLine1,
                AddressLine2 = x.AddressLine2,
                AddressState = x.AddressState,
                AddressZip = x.AddressZip,
                Currency = x.Currency,
                Name = x.Name,
                UserId = x.UserId
            }).ToList();
        }
    }
}