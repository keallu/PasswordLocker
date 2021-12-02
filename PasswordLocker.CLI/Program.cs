using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PasswordLocker.CLI;
using PasswordLocker.Core.Interfaces;
using PasswordLocker.Core.Services;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    services.AddTransient<IPasswordService, PasswordService>()
            .AddTransient<Runner>())
    .Build();

using IServiceScope serviceScope = host.Services.CreateScope();

IServiceProvider provider = serviceScope.ServiceProvider;

Runner runner = provider.GetRequiredService<Runner>();

if (args.Length > 0)
{
    args = ExtractAndTrimArguments(args);

    switch (args[1])
    {
        case "?":
            ShowHelp();
            break;

        case "n":
            Console.WriteLine(runner.GeneratePassword());
            break;

        default:
            Console.WriteLine("");
            Console.WriteLine("Error: unrecognized or incomplete command line.");
            Console.WriteLine("");
            ShowHelp();
            break;
    }
}
else
{
    ShowHelp();
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
    Console.WriteLine("Creates and manages passwords in a safe locker.");
    Console.WriteLine("");
    Console.WriteLine("Usage: passwordlocker [-n]");
    Console.WriteLine("");
    Console.WriteLine("Options:");
    Console.WriteLine("\t-n\tGenerates new password.");
    Console.WriteLine("");
    Console.WriteLine("Hint: Use clip command to copy output such as password directly to clipboard e.g. \"passwordlocker -n | clip\"");
}