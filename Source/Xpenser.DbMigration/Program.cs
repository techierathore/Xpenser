using System;
using System.Linq;
using System.Reflection;
using DbUp;

namespace Xpenser.DbMigration
{
    class Program
    {
        static int Main(string[] args)
        {
            var connectionString =
                args.FirstOrDefault()
                ?? "host=localhost;port=3306;user id=root;database=Xpenser;";
            EnsureDatabase.For.MySqlDatabase(connectionString);
            var upgrader = DeployChanges.To
                .MySqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();
            var result = upgrader.PerformUpgrade();
            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                return -1;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(value: "Success!");
            Console.ResetColor();
            return 0;
        }
    }
}
