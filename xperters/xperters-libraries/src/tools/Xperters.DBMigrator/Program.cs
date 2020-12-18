namespace Xperters.DBMigrator
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new XperterContextFactory();
            var context = factory.CreateDbContext(args);            
        }
    }
}
