using Tests.Worldserver.Base;
using Apps.Worldserver.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Tests.Worldserver;

[TestClass]
public class DataHandlersTests : TestBase
{
    [TestMethod]
    public async Task ClientDataHandler_ReturnsClients()
    {
        // Arrange
        var handler = new ClientDataHandler(InvocationContext);

        // Act
        var result = await handler.GetDataAsync(new DataSourceContext { SearchString = "" }, CancellationToken.None);

        // Assert
        foreach (var item in result)
            Console.WriteLine($"{item.Value} - {item.DisplayName}");
        Assert.IsNotNull(result);
    }
}
