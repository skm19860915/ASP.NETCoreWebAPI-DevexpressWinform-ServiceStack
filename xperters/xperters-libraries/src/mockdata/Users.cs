using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using xperters.domain;
using xperters.entities;
using xperters.enums;
using xperters.mockdata.Extensions;
using xperters.mockdata.Mapping;

namespace xperters.mockdata
{
    public static class Users
    {
        public static IEnumerable<UserDto> Clients { get; }
        public static IEnumerable<UserDto> Freelancers { get; }
        public const int RandomSeed = 8675309;
        public static string ClientId1 { get; }
        public static string ClientId2{ get; }
        public static string ClientId3 { get; }
        public static string FreelancerId1{ get; }
        public static string FreelancerId2{ get; }
        public static string FreelancerId3{ get; }
        public static string FreelancerId4{ get; }
        public static string FreelancerId5{ get; }
        private static List<UserDto> UsersList { get; }
        public const int UsersCount = 100;

        public static UserDto CustomerFirst { get; }
        public static UserDto CustomerSecond { get; }
        public static UserDto FreelancerFirst { get; }
        public static UserDto FreelancerSecond { get; }
        public static UserDto FreelancerThird { get; }
        public static UserDto CustomerThird { get; }
        public static UserDto CustomerFour { get; }
        public static UserDto FreelancerInactive { get;  }
        public static UserDto FreelancerDisabled { get;  }

        static Users()
        {
            var mapper = AutoMapperConfig.InitializedMapper;
            var items = MasterDataFactory.GetCountryData();
            var userId = Guid.Parse("{30000000-0000-0000-0000-000000000000}");

            var countries = mapper.Map<IEnumerable<CountryDto>>(items);
            //Set the randomizer seed if you wish to generate repeatable data sets.
            Randomizer.Seed = new Random(RandomSeed);
           
            var mobilePhoneNumber = 15552120001;
            var created = DateTime.Now.AddYears(-5);

            var userFakes = new Faker<UserDto>()
                .RuleFor(o => o.Id, f =>
                {

                    var oldguid = userId;
                    userId = userId.Increment();
                    return oldguid;
                })
                .RuleFor(o => o.FirstName, (f, u) => f.Name.FirstName())
                .RuleFor(o => o.LastName, (f, u) => f.Name.LastName())
                .RuleFor(o => o.DisplayName, (f, u) => u.FirstName + " " + u.LastName)
                .RuleFor(o => o.Avatar, f => f.Internet.Avatar())
                .RuleFor(o => o.MobilePhone, f => (mobilePhoneNumber++).ToString())
                .RuleFor(o => o.CreatedDate, f =>
                {
                    created = created.AddDays(1).AddHours(1).AddMinutes(1).AddSeconds(1);
                    return created;
                })
                .RuleFor(o => o.ModifiedDate, f => created)
                // only a one percent chance of being false
                // https://stackoverflow.com/a/28793411/3581466
                .RuleFor(o => o.IsEnabled, f => Randomizer.Seed.NextBool(99))  
                .RuleFor(o => o.Country, f => f.PickRandom(countries))
                .RuleFor(o => o.CountryCode, (f, u) => u.Country.CountryCode)
                .RuleFor(o => o.CountryId, (f, u) => u.Country.Id)
                .RuleFor(o => o.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(o => o.UserRole, f => f.PickRandom<Enums.UserRole>());

            UsersList = userFakes.Generate(UsersCount);

            Clients = UsersList.Where(x=>x.UserRole == Enums.UserRole.Client);
            Freelancers = UsersList.Where(x=>x.UserRole == Enums.UserRole.Freelancer);
            var clients = Clients as UserDto[] ?? Clients.ToArray();
            CustomerFirst = clients.ElementAt(0);

            CustomerSecond = clients.ElementAt(1);
            CustomerThird = clients.ElementAt(2);
            CustomerFour = clients.ElementAt(3);

            var freelancers = Freelancers as UserDto[] ?? Freelancers.ToArray();
            FreelancerFirst  = freelancers.ElementAt(0);
            FreelancerSecond  = freelancers.ElementAt(1);
            FreelancerThird  = freelancers.ElementAt(2);

            FreelancerDisabled  = freelancers.ElementAt(1);
            FreelancerInactive  = freelancers.ElementAt(2);
            
            ClientId1 = clients.ElementAt(0).Id.ToString();
            ClientId2 = clients.ElementAt(1).Id.ToString();
            ClientId3 = clients.ElementAt(2).Id.ToString();
            FreelancerId1 = freelancers.ElementAt(0).Id.ToString();
            FreelancerId2 = freelancers.ElementAt(1).Id.ToString();
            FreelancerId3 = freelancers.ElementAt(2).Id.ToString();
            FreelancerId4 = freelancers.ElementAt(3).Id.ToString();
            FreelancerId5 = freelancers.ElementAt(4).Id.ToString();
        }

        public static IEnumerable<UserDto> Get()
        {
            return UsersList;
        }
    }
}
