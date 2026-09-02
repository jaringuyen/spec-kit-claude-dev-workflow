# HTTP Contract: Hello World API

Base URL (local): `http://localhost:5080`

## `GET /hello`

### Request

| Aspect | Value |
|--------|-------|
| Method | `GET` |
| Path | `/hello` (exact; no trailing slash) |
| Headers | none required |
| Query string | ignored if present |
| Body | none |

### Response — success

| Aspect | Value |
|--------|-------|
| Status | `200 OK` |
| `Content-Type` | `text/plain; charset=utf-8` |
| Body | `Hello World` — exact bytes, no surrounding quotes, no JSON envelope, no trailing newline |

Maps to: **FR-001, FR-002, FR-003, FR-004, FR-007**.

### Behavior guarantees

- **Deterministic** (FR-007): the response is byte-identical for every call regardless of request headers, query parameters, or timing.
- **Idempotent / safe**: repeated calls have no side effects (spec User Story 1, scenario 2).

## Negative cases

| Request | Expected status | Body | Maps to |
|---------|-----------------|------|---------|
| `GET /` | `404 Not Found` | framework default (no greeting) | FR-005, SC-005 |
| `GET /unknown` | `404 Not Found` | framework default (no greeting) | FR-005, User Story 2 scenario 1 |
| `GET /hello/` (trailing slash) | `200 OK` | `Hello World` (matched to the `/hello` endpoint) | spec Edge Cases (documented, deterministic) |
| `POST /hello` | `405 Method Not Allowed` | none / framework default (no greeting) | FR-006, User Story 2 scenario 2 |
| `PUT /hello`, `DELETE /hello` | `405 Method Not Allowed` | none (no greeting) | FR-006 |

## Non-requirements (explicitly out of scope for this contract)

- Authentication / authorization headers
- CORS headers
- Rate-limit headers
- OpenAPI/Swagger document
- Health-check endpoint
