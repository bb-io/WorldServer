using Apps.Worldserver.Invocables;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.Worldserver.Actions;

[ActionList]
public class FileActions : WorldserverInvocable
{
    public FileActions(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}
