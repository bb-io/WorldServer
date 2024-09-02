using Apps.Worldserver.Invocables;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;


namespace Apps.Worldserver.Webhooks;

[WebhookList]
public class WebhookList : WorldserverInvocable
{
    public WebhookList(InvocationContext invocationContext) : base(invocationContext)
    {
    }
}
