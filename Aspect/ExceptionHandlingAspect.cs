
namespace VMAComparer.Aspect;

using PostSharp.Aspects;
using PostSharp.Serialization;
using System;

[PSerializable]
public class ExceptionHandlingAspect : OnMethodBoundaryAspect
{
    public override void OnException(MethodExecutionArgs args)
    {
        base.OnException(args);
        Console.WriteLine($"An exception occurred: {args.Exception.Message}");
        args.ReturnValue = -1; // Return -1 in case of an exception
        args.FlowBehavior = FlowBehavior.Return;
    }
}