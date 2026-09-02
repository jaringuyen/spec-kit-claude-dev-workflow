# Hello World API Constitution

## Core Principles

### I. Test-First Development

- Tests MUST be written before implementation.
- Tests MUST fail for the expected reason before implementation.
- Tests MUST NOT be skipped, disabled, or weakened to make code pass.
- All tests MUST pass before completion.

### II. Coding Standards

- All code MUST follow `.editorconfig` and `Directory.Build.props`.
- Nullable reference types MUST be enabled.
- Configured compiler/analyzer warnings MUST be treated as errors.
- AI-generated code follows the same standards as human-written code.

### III. Testing Strategy

- Unit tests MUST cover business logic.
- Integration tests MUST cover API behaviour and integration boundaries.
- E2E tests MUST cover critical workflows where appropriate.
- Do not add E2E tests when integration tests sufficiently cover the behaviour.

### IV. Quality Gate

Before completion, the applicable checks MUST pass:

- Build
- Format/lint
- Static analysis/type checking
- Unit tests
- Integration tests
- E2E tests

Claude MUST run the local quality gate.

CI MUST independently repeat the same checks.

### V. Simplicity

- Implement only what the approved specification requires.
- Follow YAGNI.
- Avoid unnecessary abstractions and dependencies.
- Prefer standard .NET and ASP.NET Core capabilities.

## Development Workflow

Requirement → Human Approval → Spec Kit → Technical Plan → Human Technical Approval → Tests → Implementation → Local Quality Gate → PR → CI → AI Pre-review → Human Review → Merge → Dev/Test → Smoke/E2E → Production.

Direct pushes to protected branches are prohibited.

## Governance

- Specifications, plans, tasks, and implementations MUST comply with this constitution.
- Claude MUST NOT bypass these rules.
- Exceptions require explicit human approval.
- Constitution changes require human review.

**Version**: 1.0.0 | **Ratified**: 2026-09-02 | **Last Amended**: 2026-09-02
