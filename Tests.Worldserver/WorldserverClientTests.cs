using System.Net;
using System.Net.Sockets;
using System.Text;
using Apps.Worldserver.Api;
using Apps.Worldserver.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Tests.Worldserver;

[TestClass]
public class WorldserverClientTests
{
    [TestMethod]
    public void Constructor_LoginReturnsUnauthorizedWithoutBody_ThrowsPluginApplicationException()
    {
        using var server = FakeLoginServer.Start(HttpStatusCode.Unauthorized, string.Empty, "application/json");

        var exception = Assert.ThrowsExactly<PluginApplicationException>(() => new WorldserverClient(CredentialsFor(server.Url)));

        StringAssert.Contains(exception.Message, "Unauthorized");
    }

    [TestMethod]
    public void Constructor_LoginReturnsHtmlPage_ThrowsPluginMisconfigurationException()
    {
        using var server = FakeLoginServer.Start(HttpStatusCode.OK, "<html><body>Sign in</body></html>", "text/html");

        Assert.ThrowsExactly<PluginMisconfigurationException>(() => new WorldserverClient(CredentialsFor(server.Url)));
    }

    [TestMethod]
    public void Constructor_LoginReturnsNoSessionId_ThrowsPluginMisconfigurationExceptionWithLoginOutcome()
    {
        using var server = FakeLoginServer.Start(HttpStatusCode.OK, "{\"loginOutcome\":\"INVALID_CREDENTIALS\"}", "application/json");

        var exception = Assert.ThrowsExactly<PluginMisconfigurationException>(() => new WorldserverClient(CredentialsFor(server.Url)));

        StringAssert.Contains(exception.Message, "INVALID_CREDENTIALS");
    }

    [TestMethod]
    public void Constructor_LoginReturnsErrorStatus_ThrowsPluginMisconfigurationExceptionWithServerMessage()
    {
        const string errorBody = "{\"status\":\"ERROR\",\"errors\":[{\"code\":1,\"type\":\"AUTHENTICATION\",\"message\":\"Invalid username or password\"}]}";
        using var server = FakeLoginServer.Start(HttpStatusCode.OK, errorBody, "application/json");

        var exception = Assert.ThrowsExactly<PluginMisconfigurationException>(() => new WorldserverClient(CredentialsFor(server.Url)));

        StringAssert.Contains(exception.Message, "Invalid username or password");
    }

    [TestMethod]
    public void Constructor_LoginReturnsBadRequestWithHtmlBody_ThrowsPluginApplicationException()
    {
        using var server = FakeLoginServer.Start(HttpStatusCode.BadRequest, "<html><body>Bad request</body></html>", "text/html");

        Assert.ThrowsExactly<PluginApplicationException>(() => new WorldserverClient(CredentialsFor(server.Url)));
    }

    [TestMethod]
    public void Constructor_LoginSucceeds_UsesSessionIdAsTokenHeader()
    {
        using var server = FakeLoginServer.Start(HttpStatusCode.OK, "{\"sessionId\":\"session-1\",\"loginOutcome\":\"LOGIN_SUCCEEDED\"}", "application/json");

        var client = new WorldserverClient(CredentialsFor(server.Url));

        var tokenHeader = client.DefaultParameters.Single(parameter => parameter.Name == "token");
        Assert.AreEqual("session-1", tokenHeader.Value);
    }

    private static IEnumerable<AuthenticationCredentialsProvider> CredentialsFor(string url) =>
    [
        new(CredsNames.Url, url),
        new(CredsNames.Username, "user-1"),
        new(CredsNames.Password, "password-1")
    ];

    private sealed class FakeLoginServer : IDisposable
    {
        private readonly HttpListener _listener;

        public string Url { get; }

        private FakeLoginServer(HttpListener listener, string url, HttpStatusCode statusCode, string body, string contentType)
        {
            _listener = listener;
            Url = url;
            _ = Task.Run(() => ServeLoginRequests(statusCode, body, contentType));
        }

        public static FakeLoginServer Start(HttpStatusCode statusCode, string body, string contentType)
        {
            var url = $"http://localhost:{FindFreePort()}/";
            var listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            return new FakeLoginServer(listener, url, statusCode, body, contentType);
        }

        public void Dispose()
        {
            _listener.Stop();
            _listener.Close();
        }

        private async Task ServeLoginRequests(HttpStatusCode statusCode, string body, string contentType)
        {
            while (_listener.IsListening)
            {
                HttpListenerContext context;
                try
                {
                    context = await _listener.GetContextAsync();
                }
                catch (Exception exception) when (exception is HttpListenerException or ObjectDisposedException)
                {
                    return;
                }

                var payload = Encoding.UTF8.GetBytes(body);
                context.Response.StatusCode = (int)statusCode;
                context.Response.ContentType = contentType;
                context.Response.ContentLength64 = payload.Length;
                await context.Response.OutputStream.WriteAsync(payload);
                context.Response.Close();
            }
        }

        private static int FindFreePort()
        {
            var portProbe = new TcpListener(IPAddress.Loopback, 0);
            portProbe.Start();
            var port = ((IPEndPoint)portProbe.LocalEndpoint).Port;
            portProbe.Stop();
            return port;
        }
    }
}
