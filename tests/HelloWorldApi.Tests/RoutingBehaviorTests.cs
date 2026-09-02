using System.Net;
using System.Net.Http;

namespace HelloWorldApi.Tests;

/// <summary>
/// Negative-case contract tests: undefined paths return 404, non-GET methods on
/// <c>/hello</c> return 405, and no greeting text leaks on any error path.
/// See the "Negative cases" table in specs/001-hello-world-api/contracts/hello.http.md.
/// </summary>
[Collection(ApiCollection.Name)]
public sealed class RoutingBehaviorTests
{
    private readonly HttpClient _client;

    public RoutingBehaviorTests(ApiTestFixture fixture)
    {
        _client = fixture.CreateClient();
    }

    [Fact]
    public async Task Get_root_returns_404()
    {
        var response = await _client.GetAsync("/");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Get_unknown_path_returns_404_without_greeting()
    {
        var response = await _client.GetAsync("/unknown");
        string body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.DoesNotContain("Hello World", body);
    }

    [Fact]
    public async Task Get_hello_with_trailing_slash_is_matched_to_the_same_endpoint()
    {
        // ASP.NET Core routing tolerates a trailing slash: "/hello/" matches the
        // "/hello" endpoint. The spec's Edge Cases permit this as long as it is
        // deterministic and documented (see contracts/hello.http.md).
        var response = await _client.GetAsync("/hello/");
        string body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Hello World", body);
    }

    [Fact]
    public async Task Post_hello_returns_405_without_greeting()
    {
        var response = await _client.PostAsync("/hello", content: null);
        string body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        Assert.DoesNotContain("Hello World", body);
    }

    [Theory]
    [InlineData("PUT")]
    [InlineData("DELETE")]
    public async Task Non_get_methods_on_hello_return_405(string method)
    {
        using var request = new HttpRequestMessage(new HttpMethod(method), "/hello");

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
    }
}
