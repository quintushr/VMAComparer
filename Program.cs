using CommandLine;
using CommandLine.Text;
using VMAComparer.CommandLine;

namespace VMAComparer;

public static class Program
{
    public static void Main(string[] args)
    {
        var parser = new Parser(settings =>
        {
            settings.EnableDashDash = true;
            settings.HelpWriter = null;
        });

        var parserResult = parser.ParseArguments<CompareCommand, InformationCommand>(args);
        parserResult.WithParsed((CompareCommand opts) => CompareCommand.Run(opts))
            .WithParsed((InformationCommand opts) => InformationCommand.Run(opts))
            .WithNotParsed(errs => DisplayHelp(parserResult));
    }

    private static void DisplayHelp<T>(ParserResult<T> result)
    {
        var helpText = HelpText.AutoBuild(result, h =>
        {
            h.AdditionalNewLineAfterOption = false;
            h.AddDashesToOption = true;
            return HelpText.DefaultParsingErrorsHandler(result, h);
        }, e => e, verbsIndex: true);
        Console.WriteLine(helpText);
    }
}