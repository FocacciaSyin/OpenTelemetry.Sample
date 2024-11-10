using AspectInjector.Broker;

namespace Common.AOP;

[Injection(typeof(TracingAspect))]
public class TracingAttribute : Attribute
{
    
}
