using Tests.Worldserver.Base;
using Apps.Worldserver.Connections;
using Blackbird.Applications.Sdk.Common.Authentication;

namespace Tests.Worldserver;

[TestClass]
public class ConnectionValidatorTests : TestBase
{
    [TestMethod]
    public async Task ValidateConnection_ValidData_ShouldBeSuccessful()
    {
        // Arrange
        var validator = new ConnectionValidator();

        // Act
        var result = await validator.ValidateConnection(Creds, CancellationToken.None);

        // Assert
        Console.WriteLine(result.Message);
        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public async Task ValidateConnection_InvalidData_ShouldFail()
    {
        // Arrange
        var validator = new ConnectionValidator(); 
        var newCredentials = Creds.Select(x => new AuthenticationCredentialsProvider(x.KeyName, x.Value + "_incorrect"));

        // Act
        var result = await validator.ValidateConnection(newCredentials, CancellationToken.None);

        // Assert
        Console.WriteLine(result.Message);
        Assert.IsFalse(result.IsValid);
    }
}
