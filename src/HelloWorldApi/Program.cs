var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// US1: return the exact plain-text body "Hello World" with 200.
// Results.Text writes the raw string as text/plain; charset=utf-8 (no JSON envelope, no quotes).
app.MapGet("/hello", () => Results.Text("Hello World"));

// US2 negative cases need no code: ASP.NET Core routing returns 404 for undefined
// paths and 405 for non-GET methods on "/hello" by default. "/hello/" is matched to
// the endpoint above (trailing slash tolerated) — see contracts/hello.http.md.

app.Run();

// Exposed so the test project can reference the entry point via WebApplicationFactory<Program>.
public partial class Program { }
