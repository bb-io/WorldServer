using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication.OAuth2;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.Worldserver;

public class Application : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.Cms];
        set { }
    }

    public string Name
    {
        get => "Worldserver";
        set { }
    }

    public Application()
    {
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}