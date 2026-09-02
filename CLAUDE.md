# Claude Development Rules

Before coding:

1. Read `.specify/memory/constitution.md`.
2. Read the active Spec Kit specification, plan, and tasks.
3. Follow `.editorconfig` and `Directory.Build.props`.
4. Do not implement until the technical plan is human-approved.

## Implementation

- Write tests before production code.
- Confirm new tests fail for the expected reason.
- Implement only the approved requirements.
- Do not disable or weaken tests.
- Do not suppress warnings without justification.
- Avoid unnecessary dependencies and abstractions.
- Prefer direct file edits over shell-based bulk modifications.

## Before Completion

Run:

```powershell
./scripts/quality-gate.ps1
```
