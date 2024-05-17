using CommandLine;
using CommandLine.Text;
using System;
using System.Linq;
using System.Reflection;
using VMAComparer.CommandLine;

namespace VMAComparer;

public static class Program
{
    public static int Main(string[] args)
    {
        var parser = new Parser(settings => { settings.EnableDashDash = true; settings.HelpWriter = null; });
        var parserResult = parser.ParseArguments(args, LoadVerbs());

        return parserResult.MapResult(
            (CompareCommand param) => CompareCommand.Run(param),
            (InformationCommand param) => InformationCommand.Run(param),
            _ => DisplayHelp(parserResult));
    }

    private static int DisplayHelp<T>(ParserResult<T> result)
    {
        var helpText = HelpText.AutoBuild(result, h =>
        {
            h.AdditionalNewLineAfterOption = false;
            h.AddDashesToOption = true;
            h.AutoVersion = false;
            return HelpText.DefaultParsingErrorsHandler(result, h);
        }, e => e, verbsIndex: true);
        Console.WriteLine(helpText);
        return 1;

    }
    private static Type[] LoadVerbs()
    {
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
    }
}