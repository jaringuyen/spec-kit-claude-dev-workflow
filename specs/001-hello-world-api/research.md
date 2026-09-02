# Phase 0 Research: Hello World API

All Technical Context items are resolved. No `NEEDS CLARIFICATION` markers remain.

## Decision: Target framework — .NET 10 (`net10.0`), C#

- **Rationale**: `dotnet --list-sdks` confirms SDK `10.0.301` is installed and is the newest available on this machine. .NET 10 is the current LTS train, so it is a safe long-lived target. Older installed SDKs (6.0, 7.0) are out of support or near end-of-life.
- **Alternatives considered**:
  - *.NET 8 LTS* — also viable and widely deployed, but no 8.0 SDK is installed here, so it would require an extra install for no benefit.
  - *.NET 6/7* — present on the machine but out of / near end of support; rejected.

## Decision: Minimal API (not MVC controllers)

- **Rationale**: The feature is a single route returning a string. Minimal API expresses this in one line (`app.MapGet("/hello", () => "Hello World")`) with no controller class, routing attributes, or ceremony. Fewer files to test and maintain.
- **Note on response shape**: Returning a bare `string` from a Minimal API handler produces `Content-Type: text/plain; charset=utf-8` and writes the raw string with **no** surrounding quotes — this satisfies FR-002 and FR-004 (no JSON envelope). Returning `Results.Text("Hello World")` is the explicit equivalent and will be used for clarity.
- **Alternatives considered**:
  - *MVC controller* — more idiomatic for larger APIs but adds a class and attribute routing for zero gain here.
  - *Returning `Results.Json(...)`* — would wrap the value in quotes / JSON; violates FR-004. Rejected.

## Decision: Test stack — xUnit + `Microsoft.AspNetCore.Mvc.Testing`

- **Rationale**: `WebApplicationFactory<Program>` boots the real app in-process and exposes an `HttpClient`, so tests exercise the actual routing, status codes, and response body — the exact contract in the spec — without binding a TCP port or running a separate process. xUnit is the default .NET test framework and integrates with `dotnet test`.
- **Enabling detail**: Minimal API uses top-level statements, so `Program` is internal by default. Add `public partial class Program { }` at the end of `Program.cs` (or `<InternalsVisibleTo>`) so the test project can reference the entry point as a generic type argument.
- **Alternatives considered**:
  - *Spin up the server on a real port and use a plain `HttpClient`* — slower, flakier (port contention), needs lifecycle management. Rejected.
  - *Unit-test a handler method in isolation* — would not verify routing, HTTP status, or content type, which are core acceptance criteria. Rejected as the sole approach (acceptable only as a supplement).
  - *MSTest / NUnit* — equivalent capability; xUnit chosen as the ecosystem default.

## Decision: TDD sequencing

- **Rationale**: User input mandates tests first. Concretely: create both projects and the solution, write `HelloEndpointTests.cs` with all assertions, run `dotnet test` and observe it **fail to compile / fail** (no `MapGet` yet), then add the single `MapGet` line and re-run until green. The failing run is the evidence that the tests are meaningful.
- **Alternatives considered**: Writing implementation and tests together — explicitly disallowed by user input. Rejected.

## Decision: Local HTTP port

- **Rationale**: Spec SC-002 / FR-008 require a documented single-command start on a documented port. Pin `applicationUrl` to `http://localhost:5080` in `launchSettings.json` and document it in `quickstart.md`, rather than relying on the SDK's random dev port. HTTP only (no HTTPS/dev-cert friction) since this is a demo service and TLS is explicitly out of scope.
- **Alternatives considered**: Default Kestrel random port — undocumented and non-reproducible; rejected. HTTPS with dev certificate — adds `dotnet dev-certs` setup step contrary to the "under 5 minutes, documented steps only" goal; rejected.

## Decision: Trailing slash & query string behavior

- **Rationale**: ASP.NET Core routing tolerates a trailing slash, so `/hello/` is matched to the `/hello` endpoint and returns `200` with `Hello World` — deterministic and acceptable per the spec edge case ("either matched to the same endpoint or returned as not found; deterministic and documented"). Query strings are ignored by a parameterless handler, so `GET /hello?name=x` returns `Hello World` (satisfies FR-007). Both behaviors are covered by tests and noted in the contract. *(Corrected during implementation: an earlier draft of this note assumed `/hello/` would 404; the framework's actual, deterministic behavior is to match it.)*
- **Alternatives considered**: Adding middleware to normalize trailing slashes — unnecessary complexity for a documented, acceptable default. Rejected.
