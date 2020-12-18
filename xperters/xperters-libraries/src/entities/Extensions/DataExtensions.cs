using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using xperters.entities.Entities;

namespace xperters.entities.Extensions
{
    public static class DataExtensions
    {
        private static ILogger _logger;
        public static void SeedData(this IServiceScopeFactory scopeFactory, ILogger logger)
        {
            _logger = logger;

            using (var serviceScope = scopeFactory.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<XpertersContext>();

                _logger.LogDebug("Starting database table data seeding.");

                ApplyData(context);
            }
        }

        private static void ApplyData(XpertersContext context)
        {
            if (context.AnyPendingMigrations())
            {
                if (!context.IsInMemoryDatabase())
                    context.Database.OpenConnection();

                InitializeIdentity(context);
            }

        }

        public static bool AnyPendingMigrations(this DbContext context)
        {
            if (!context.Database.ProviderName.Contains("InMemory"))
            {
                var anyToApply = context.Database.GetPendingMigrations().Any();
                return !anyToApply;
            }

            return true;
        }

        public static bool IsInMemoryDatabase(this DbContext context)
        {
            if (context.Database.ProviderName.Contains("InMemory"))
            {
                return true;
            }

            return false;
        }

        public static void AddJobStatus(this JobStatus jobStatus, XpertersContext context, ILogger logger)
        {
            try
            {
                var result = context.JobStatus.Add(jobStatus);
                if (result == null)
                {
                    throw new Exception($"JobStatus not added");
                }

                logger.LogDebug("Added default user. Succeeded. User id is {0}.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw ex;
            }
        }

        public static void AddCurrency(this Currency currency, XpertersContext context, ILogger logger)
        {
            try
            {
                var result = context.Currencies.Add(currency);
                if (result == null)
                {
                    throw new Exception($"currency not added");
                }

                logger.LogDebug("Added default user. Succeeded. User id is {0}.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw ex;
            }
        }
        public static void AddMilestoneStatus(this MilestoneStatus status, XpertersContext context, ILogger logger)
        {
            try
            {
                var result = context.MilestoneStatus.Add(status);
                if (result == null)
                {
                    throw new Exception($"MilestoneStatus  not added");
                }

                logger.LogDebug("Added default user. Succeeded. User id is {0}.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw ex;
            }
        }
        public static void AddUserToStore(this User user, XpertersContext context, ILogger logger)
        {
            try
            {
                var result = context.Users.FirstOrDefault(w => w.Id == user.Id || w.MobilePhone == user.MobilePhone);

                if (result==null)
                {
                    user.MobilePhone = user.MobilePhone.Replace(" ", string.Empty);
                    user.UserBalances = null;
                    user.UserPayments = null;
                    user.UserWithdrawals = null;
                    context.Users.Add(user);
                    context.SaveChanges();
                    logger.LogDebug("Added default user. Succeeded. User id is {0}.", user.Id);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw ex;
            }
        }

        private static void InitializeIdentity(XpertersContext context)
        {

            const string name = "admin@xperters.com";
            const string password = "123456aZ.";
            const string adminRole = "Admin";
            
            //
            var user = context.Users.FirstOrDefault(x=>x.Email == name);
            if (user == null)
            {
                user = new User
                {
                    Email = name,
                    FirstName = "Xperters",
                    LastName = "Admin",
                    CreatedDate = DateTime.UtcNow
                };

                user = context.Users.Add(user).Entity;
            }

            // Add user admin to Role Admin if not already added                
//            if (!userManager.IsInRoleAsync(user, adminRole).Result)
//            {
//                var result = userManager.AddToRoleAsync(user, role.Name).Result;
//                if (!result.Succeeded)
//                {
//                    _logger.LogError("Error when trying to add default user to admin role: {0}.", result.Errors.FirstOrDefault());
//                }
//            }
        }
    }
}
