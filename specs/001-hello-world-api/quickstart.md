# Quickstart & Validation: Hello World API

## Prerequisites

- .NET SDK 10.x (`dotnet --version` → `10.x`)
- No database, container, or other services required

## One-time build

```bash
dotnet build HelloWorldApi.sln
```

## TDD workflow (the mandated order)

1. **Tests first.** With the test project in place but before `GET /hello` is mapped in `Program.cs`, run:

   ```bash
   dotnet test HelloWorldApi.sln
   ```

   Expected at this stage: **FAIL** (the `/hello` assertions fail — 404 instead of 200 / body mismatch). This failing run is the proof the tests are real.

2. **Then implement.** Add the single `MapGet("/hello", ...)` line to `Program.cs`.

3. **Green.** Re-run:

   ```bash
   dotnet test HelloWorldApi.sln
   ```

   Expected: **PASS** — all tests green. This is the definition of done for the feature.

## Run the service

```bash
dotnet run --project src/HelloWorldApi
```

Service listens on `http://localhost:5080` (see [`contracts/hello.http.md`](./contracts/hello.http.md)).

## Manual validation scenarios

| # | Command | Expected result | Spec ref |
|---|---------|-----------------|----------|
| 1 | `curl -i http://localhost:5080/hello` | `200 OK`, `Content-Type: text/plain; charset=utf-8`, body exactly `Hello World` | SC-001, FR-002/003/004 |
| 2 | `curl -s http://localhost:5080/hello` run 100× in a loop | every response is `Hello World`, no failures | SC-004, User Story 1 scenario 2 |
| 3 | `curl -s "http://localhost:5080/hello?name=x"` | body still exactly `Hello World` | FR-007 |
| 4 | `curl -i http://localhost:5080/unknown` | `404 Not Found`, no greeting, no stack trace | FR-005, SC-005 |
| 5 | `curl -i -X POST http://localhost:5080/hello` | `405 Method Not Allowed`, no greeting | FR-006 |
| 6 | `curl -i http://localhost:5080/hello/` | `404 Not Found` (documented, deterministic) | spec Edge Cases |

## Automated coverage

`tests/HelloWorldApi.Tests/HelloEndpointTests.cs` covers scenarios 1 and 3–6 above using an in-process `WebApplicationFactory<Program>` host. Scenario 2 (repetition) is represented by an assertion that two sequential calls return identical content. Full contract in [`contracts/hello.http.md`](./contracts/hello.http.md).
