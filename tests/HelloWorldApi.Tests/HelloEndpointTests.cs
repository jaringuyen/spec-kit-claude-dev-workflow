using System.Net;

namespace HelloWorldApi.Tests;

/// <summary>
/// Contract tests for the success path of <c>GET /hello</c>.
/// See specs/001-hello-world-api/contracts/hello.http.md.
/// </summary>
[Collection(ApiCollection.Name)]
public sealed class HelloEndpointTests
{
    private readonly HttpClient _client;

    public HelloEndpointTests(ApiTestFixture fixture)
    {
        _client = fixture.CreateClient();
    }

    [Fact]
    public async Task Get_hello_returns_200()
    {
        var response = await _client.GetAsync("/hello");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_hello_body_is_exactly_hello_world()
    {
        var response = await _client.GetAsync("/hello");
        string body = await response.Content.ReadAsStringAsync();

        Assert.Equal("Hello World", body);
    }

    [Fact]
    public async Task Get_hello_content_type_is_plain_text_utf8()
    {
        var response = await _client.GetAsync("/hello");

        Assert.NotNull(response.Content.Headers.ContentType);
        Assert.Equal("text/plain", response.Content.Headers.ContentType!.MediaType);
        Assert.Equal("utf-8", response.Content.Headers.ContentType.CharSet);
    }

    [Fact]
    public async Task Get_hello_ignores_query_string()
    {
        var response = await _client.GetAsync("/hello?name=x");
        string body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Hello World", body);
    }

    [Fact]
    public async Task Get_hello_is_deterministic_across_calls()
    {
        string first = await (await _client.GetAsync("/hello")).Content.ReadAsStringAsync();
        string second = await (await _client.GetAsync("/hello")).Content.ReadAsStringAsync();

        Assert.Equal(first, second);
        Assert.Equal("Hello World", first);
    }
}
