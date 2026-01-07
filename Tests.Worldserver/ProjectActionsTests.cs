using Tests.Worldserver.Base;
using Apps.Worldserver.Actions;
using Apps.Worldserver.Models.Projects.Request;

namespace Tests.Worldserver;

[TestClass]
public class ProjectActionsTests : TestBase
{
    [TestMethod]
    public async Task SearchProjects_ReturnsProjects()
    {
		// Arrange
		var actions = new ProjectActions(InvocationContext);
        var request = new SearchProjectsRequest { };

        // Act
        var result = await actions.SearchProjects(request);

        // Assert
        PrintJsonResult(result);
        Assert.IsNotNull(result);
    }
}
