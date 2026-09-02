---

description: "Task list for Hello World API implementation"
---

# Tasks: Hello World API

**Input**: Design documents from `/specs/001-hello-world-api/`

**Prerequisites**: plan.md, spec.md, research.md, data-model.md, contracts/hello.http.md, quickstart.md

**Tests**: MANDATORY. The user explicitly requested test-driven development — every user story writes failing tests first, and no implementation task is done until its tests pass.

**Organization**: Tasks are grouped by user story (US1 = P1 MVP, US2 = P2) so each story can be implemented and tested independently.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependency on an incomplete task)
- **[Story]**: US1 or US2 (setup / foundational / polish tasks have no story label)
- Exact file paths are included in every task

## Path Conventions

Single web-service project plus companion test project (per plan.md):

- `src/HelloWorldApi/` — Minimal API project (`net10.0`)
- `tests/HelloWorldApi.Tests/` — xUnit + `Microsoft.AspNetCore.Mvc.Testing`
- `HelloWorldApi.sln` — solution at repository root

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Create the solution, both projects, and the pinned local port. No `GET /hello` logic yet.

- [X] T001 Create repository skeleton: `HelloWorldApi.sln` at repo root plus empty `src/` and `tests/` directories (`dotnet new sln -n HelloWorldApi`)
- [X] T002 Create the API project at `src/HelloWorldApi/HelloWorldApi.csproj` using the web SDK targeting `net10.0`, with `<Nullable>enable</Nullable>` and `<TreatWarningsAsErrors>true</TreatWarningsAsErrors>`; scaffold `src/HelloWorldApi/Program.cs` with only `WebApplication.CreateBuilder(args)` / `app.Run()` (NO `/hello` route) and append `public partial class Program { }` so the test host can reference it
- [X] T003 Pin the local HTTP port in `src/HelloWorldApi/Properties/launchSettings.json` to `http://localhost:5080` (HTTP only, no HTTPS profile), per research.md
- [X] T004 Create the test project at `tests/HelloWorldApi.Tests/HelloWorldApi.Tests.csproj` targeting `net10.0` with package references `xunit`, `xunit.runner.visualstudio`, `Microsoft.NET.Test.Sdk`, `Microsoft.AspNetCore.Mvc.Testing`, and a `<ProjectReference>` to `src/HelloWorldApi/HelloWorldApi.csproj`
- [X] T005 Add both projects to `HelloWorldApi.sln` (`dotnet sln add`) and run `dotnet build HelloWorldApi.sln` to confirm the empty solution compiles

**Checkpoint**: Solution builds; no endpoint behavior exists yet.

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Shared in-process test host used by every user story's tests.

**⚠️ CRITICAL**: No user story test can be written to run until this is complete.

- [X] T006 Create the shared test host fixture at `tests/HelloWorldApi.Tests/ApiTestFixture.cs` — a class exposing an `HttpClient` from `WebApplicationFactory<Program>` (with an xUnit collection definition or `IClassFixture` hook) so tests issue real in-process HTTP requests against the app

**Checkpoint**: Test project builds and can boot the API in-process; story work can begin.

---

## Phase 3: User Story 1 - Retrieve a greeting message (Priority: P1) 🎯 MVP

**Goal**: `GET /hello` returns HTTP 200 with the exact plain-text body `Hello World`, deterministically, ignoring query strings.

**Independent Test**: Start the service, `curl -i http://localhost:5080/hello`, confirm `200`, `Content-Type: text/plain; charset=utf-8`, body exactly `Hello World`. Covered automatically by `HelloEndpointTests`.

### Tests for User Story 1 (write first, must FAIL before implementation) ⚠️

- [X] T007 [P] [US1] Write `tests/HelloWorldApi.Tests/HelloEndpointTests.cs` using the T006 fixture, with test methods asserting: (a) `GET /hello` → status `200`; (b) response body is exactly `"Hello World"` — no surrounding quotes, no trailing newline; (c) `Content-Type` is `text/plain; charset=utf-8`; (d) `GET /hello?name=x` still returns exactly `Hello World` (FR-007); (e) two sequential `GET /hello` calls return byte-identical bodies (determinism). Maps to contracts/hello.http.md `GET /hello` section.
- [X] T008 [US1] Run `dotnet test HelloWorldApi.sln` and confirm all `HelloEndpointTests` **FAIL** (route not mapped → 404). Record the failing output as TDD evidence.

### Implementation for User Story 1

- [X] T009 [US1] In `src/HelloWorldApi/Program.cs` add `app.MapGet("/hello", () => Results.Text("Hello World"));` (explicit `text/plain`, no JSON envelope) per research.md
- [X] T010 [US1] Run `dotnet test HelloWorldApi.sln` and confirm every `HelloEndpointTests` method **PASSES**; if the body assertion sees a trailing newline or quotes, adjust the handler until the body is exactly `Hello World`

**Checkpoint**: MVP complete — `GET /hello` is fully functional and independently testable.

---

## Phase 4: User Story 2 - Predictable handling of unknown paths (Priority: P2)

**Goal**: Undefined paths return `404`; non-GET methods on `/hello` return `405`; no greeting text leaks on any negative case; no stack traces exposed.

**Independent Test**: With the service running, `curl -i http://localhost:5080/unknown` → `404`; `curl -i -X POST http://localhost:5080/hello` → `405`; neither body contains `Hello World`. Covered automatically by `RoutingBehaviorTests`.

### Tests for User Story 2 (write first, must run before any routing changes) ⚠️

- [X] T011 [P] [US2] Write `tests/HelloWorldApi.Tests/RoutingBehaviorTests.cs` using the T006 fixture, with test methods asserting: (a) `GET /` → `404`; (b) `GET /unknown` → `404` and body does not contain `Hello World`; (c) `GET /hello/` (trailing slash) → matched to `/hello`, `200` + `Hello World` (documented deterministic behavior — framework tolerates the trailing slash); (d) `POST /hello` → `405` and body does not contain `Hello World`; (e) `PUT /hello` and `DELETE /hello` → `405`. Maps to the "Negative cases" table in contracts/hello.http.md.
- [X] T012 [US2] Run `dotnet test HelloWorldApi.sln`; record which `RoutingBehaviorTests` already pass via ASP.NET Core routing defaults and which (if any) fail

### Implementation for User Story 2

- [X] T013 [US2] If any `RoutingBehaviorTests` failed in T012, adjust routing/handlers in `src/HelloWorldApi/Program.cs` (e.g. ensure no catch-all/fallback route, keep the map GET-only) so all negative-case assertions hold; if all passed by framework default, add a brief comment in `Program.cs` noting the contract is met by default routing
- [X] T014 [US2] Run `dotnet test HelloWorldApi.sln` and confirm all `RoutingBehaviorTests` **PASS** with no regression in `HelloEndpointTests`

**Checkpoint**: Both user stories independently functional; full suite green.

---

## Phase 5: Polish & Cross-Cutting Concerns

**Purpose**: Documentation, hygiene, and end-to-end validation.

- [X] T015 [P] Create `.gitignore` at repo root covering `bin/`, `obj/`, `*.user` (standard .NET ignores)
- [X] T016 [P] Create `README.md` at repo root documenting prerequisites (.NET 10 SDK), `dotnet build`, `dotnet test`, `dotnet run --project src/HelloWorldApi`, and the pinned port `http://localhost:5080`
- [X] T017 Run `dotnet test HelloWorldApi.sln` one final time and confirm the entire suite passes with zero warnings (warnings-as-errors is on)
- [X] T018 Execute the manual validation scenarios 1 and 3–6 in `specs/001-hello-world-api/quickstart.md` against a running instance and confirm each expected result

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: no dependencies — start immediately. T002 → T003; T002 → T004; (T002, T004) → T005
- **Foundational (Phase 2)**: T006 depends on T004/T005 — BLOCKS all user stories
- **User Story 1 (Phase 3)**: depends on T006. T007 → T008 → T009 → T010
- **User Story 2 (Phase 4)**: depends on T006. T011 → T012 → T013 → T014. Independent of US1 for test authoring; T013 edits the same `Program.cs` as T009, so run US2 implementation after US1's T009/T010 to avoid a merge conflict
- **Polish (Phase 5)**: depends on T010 and T014. T015/T016 [P]; then T017; then T018

### User Story Dependencies

- **US1 (P1)**: no dependency on other stories — the MVP
- **US2 (P2)**: no behavioral dependency on US1; shares `Program.cs`, so sequence implementation tasks after US1

### Within Each User Story

- Tests written and observed FAILING before implementation (T007→T008 before T009; T011→T012 before T013)
- Implementation before the confirming green test run
- Story complete (green) before moving to the next priority

### Parallel Opportunities

- T007 [P] (US1 test file) and T011 [P] (US2 test file) are different files with no dependency on each other — both can be authored in parallel once T006 is done
- T015 [P] and T016 [P] (root `.gitignore` / `README.md`) can run in parallel
- Setup tasks are mostly sequential due to project references; no [P] there

---

## Parallel Example

```bash
# After T006 (shared fixture) is complete, author both story test files in parallel:
Task: "Write tests/HelloWorldApi.Tests/HelloEndpointTests.cs for GET /hello success contract (T007)"
Task: "Write tests/HelloWorldApi.Tests/RoutingBehaviorTests.cs for 404/405 negative cases (T011)"

# In Polish phase:
Task: "Create .gitignore at repo root (T015)"
Task: "Create README.md at repo root (T016)"
```

---

## Implementation Strategy

### MVP First (User Story 1 only)

1. Phase 1: Setup (T001–T005)
2. Phase 2: Foundational (T006)
3. Phase 3: User Story 1 (T007–T010) — write failing tests, then the single `MapGet` line, then green
4. **STOP and VALIDATE**: `curl http://localhost:5080/hello` → `Hello World`; deploy/demo

### Incremental Delivery

1. Setup + Foundational → test host ready
2. Add US1 → failing tests → implement → green → **MVP demo**
3. Add US2 → failing negative-case tests → adjust routing if needed → green → demo
4. Polish → docs, `.gitignore`, final full-suite run, quickstart validation

---

## Notes

- [P] = different files, no dependency on an incomplete task
- TDD is mandatory here: every implementation task is bracketed by a failing test run before and a passing test run after
- `Program.cs` ends with `public partial class Program { }` — required for `WebApplicationFactory<Program>`
- Commit after each task or logical group (test-authoring commit, then implementation commit, per TDD)
- No data model, database, auth, or external integration — those phases are intentionally absent
