using Microsoft.AspNetCore.Mvc.Testing;

namespace HelloWorldApi.Tests;

/// <summary>
/// Boots the real API in-process so tests exercise actual routing, status codes,
/// and response bodies over HTTP without binding a TCP port.
/// </summary>
public sealed class ApiTestFixture : WebApplicationFactory<Program>
{
}

/// <summary>
/// Shares a single <see cref="ApiTestFixture"/> instance across every test class
/// tagged with <c>[Collection(ApiCollection.Name)]</c>.
/// </summary>
[CollectionDefinition(ApiCollection.Name)]
public sealed class ApiCollection : ICollectionFixture<ApiTestFixture>
{
    public const string Name = "api";
}
