using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PasswordLocker.CLI;
using PasswordLocker.Core.Entities;
using PasswordLocker.Core.Interfaces;
using PasswordLocker.Core.Services;
using PasswordLocker.Infrastructure;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    services.AddSingleton<IDbContext, DbContext>(_ => new DbContext("passwordlocker-database.db"))
        .AddTransient<IPasswordService, PasswordService>()
        .AddTransient<ILockerService, LockerService>()
        .AddTransient<ILockerRepository, LockerRepository>()
        .AddTransient<Runner>())
        .Build();

using IServiceScope serviceScope = host.Services.CreateScope();

IServiceProvider provider = serviceScope.ServiceProvider;

Runner runner = provider.GetRequiredService<Runner>();

try
{

    if (args.Length > 0)
    {
        args = ExtractAndTrimArguments(args);

        switch (args[1])
        {
            case "?":
                ShowHelp();
                break;

            case "g":
                int letters = 6;
                int digits = 4;

                if (args.Length > 2)
                {
                    if (args[2].StartsWith("l:"))
                    {
                        letters = int.Parse(args[2][2..]);
                    }
                }

                if (args.Length > 3)
                {
                    if (args[3].StartsWith("d:"))
                    {
                        digits = int.Parse(args[3][2..]);
                    }
                }

                Console.WriteLine(runner.GeneratePassword(letters, digits));
                break;

            case "o":
                string name;

                if (args.Length > 2)
                {
                    if (args[2].StartsWith("n:"))
                    {
                        name = args[2][2..];

                        Console.Write("Please enter password for locker: ");
                        string? password = Console.ReadLine();

                        Locker? locker = runner.Find(name);

                        if (locker == null && password != null)
                        {
                            locker = runner.Add(name, password);
                        }

                        if (locker != null && password != null && password.Equals(locker.Password))
                        {
                            bool doContinue = true;

                            do
                            {
                                Console.Write("> ");
                                string? command = Console.ReadLine();

                                if (command != null)
                                {
                                    if (command.StartsWith("a"))
                                    {
                                        string[] parts = command.Split(' ');

                                        if (parts.Length == 4)
                                        {
                                            locker.Entries.Add(new Entry { Name = parts[1], UserName = parts[2], Password = parts[3] });
                                            runner.Update(locker);
                                        }
                                        else
                                        {
                                            Console.WriteLine("");
                                            Console.WriteLine("Error: Syntax for command was incorrect.");
                                        }
                                    }
                                    else if (command.StartsWith("r"))
                                    {

                                    }
                                    else if (command.StartsWith("s"))
                                    {
                                        string[] parts = command.Split(' ');

                                        if (parts.Length == 2)
                                        {
                                            IEnumerable<Entry>? entries = runner.GetEntries(locker.Name, parts[1]);

                                            if (entries != null)
                                            {
                                                Console.WriteLine("Entries:");
                                                Console.WriteLine("");
                                                Console.WriteLine($"{"Name",-20} {"User Name",-20} {"Password",-20}");
                                                Console.WriteLine($"{"----",-20} {"---------",-20} {"--------",-20}");

                                                foreach (Entry entry in entries)
                                                {
                                                    Console.WriteLine($"{entry.Name,-20} {entry.UserName,-20} {entry.Password,-20}");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("");
                                            Console.WriteLine("Error: Syntax for command was incorrect.");
                                        }
                                    }
                                    else if (command.StartsWith("l"))
                                    {
                                        IEnumerable<Entry>? entries = runner.GetAllEntries(locker.Name);

                                        if (entries != null)
                                        {
                                            Console.WriteLine("Entries:");
                                            Console.WriteLine("");
                                            Console.WriteLine($"{"Name",-20} {"User Name",-20} {"Password",-20}");
                                            Console.WriteLine($"{"----",-20} {"---------",-20} {"--------",-20}");

                                            foreach (Entry entry in entries)
                                            {
                                                Console.WriteLine($"{entry.Name,-20} {entry.UserName,-20} {entry.Password,-20}");
                                            }
                                        }
                                    }
                                    else if (command.StartsWith("c"))
                                    {
                                        doContinue = false;
                                    }
                                }

                            } while (doContinue);
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Error: Password for locker was incorrect.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Error: Name for locker was not provided.");
                }

                break;

            case "d":
                if (args.Length > 2)
                {
                    if (args[2].StartsWith("n:"))
                    {
                        name = args[2][2..];

                        Console.Write("Please enter password for locker: ");
                        string? password = Console.ReadLine();

                        Locker? locker = runner.Find(name);

                        if (locker != null)
                        {
                            if (password != null && password.Equals(locker.Password))
                            {
                                if (runner.Remove(name))
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("Locker was deleted.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Error: Password for locker was incorrect.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Error: Locker was not found.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Error: Name for locker was not provided.");
                }

                break;

            default:
                Console.WriteLine("");
                Console.WriteLine("Error: Unrecognized or incomplete command line.");
                Console.WriteLine("");
                ShowHelp();
                break;
        }
    }
    else
    {
        ShowHelp();
    }
}
catch (Exception ex)
{
    Console.WriteLine("Exception: " + ex.Message);
}

static string[] ExtractAndTrimArguments(string[] args)
{
    string[] arguments = Environment.GetCommandLineArgs();

    for (int i = 0; i < arguments.Length; i++)
    {
        arguments[i] = arguments[i].Trim('/', '-');
    }

    return arguments;
}

static void ShowHelp()
{
    Console.WriteLine("");
    Console.WriteLine("Creates and manages passwords in a locker.");
    Console.WriteLine("");
    Console.WriteLine("Usage: passwordlocker [-g] [-o] [-d]");
    Console.WriteLine("");
    Console.WriteLine("Options:");
    Console.WriteLine("\t-g\tGenerates new random password without storing it to a locker.");
    Console.WriteLine("\t\tl: Numbers of letters.");
    Console.WriteLine("\t\td: Numbers of digits.");
    Console.WriteLine("");
    Console.WriteLine("\t-o\tCreates a new and/or open an existing locker.");
    Console.WriteLine("\t\tn: Name of locker.");
    Console.WriteLine("");
    Console.WriteLine("\t\tCommands inside locker are:");
    Console.WriteLine("\t\ta - adds new entry to the locker with syntax \"a <name> <username> <password>\".");
    Console.WriteLine("\t\tr - removes existing entry from the locker with syntax \"r <name>\".");
    Console.WriteLine("\t\ts - search entire locker for entries with syntax \"s <name>\".");
    Console.WriteLine("\t\tl - list all entries in the locker.");
    Console.WriteLine("\t\tc - close locker.");
    Console.WriteLine("");
    Console.WriteLine("\t-d\tDeletes an existing locker.");
    Console.WriteLine("\t\tn: Name of locker.");
    Console.WriteLine("");
}