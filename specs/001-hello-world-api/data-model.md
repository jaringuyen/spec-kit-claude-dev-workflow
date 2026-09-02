# Phase 1 Data Model: Hello World API

This feature has **no persistent data, no domain entities, and no state transitions**.

- No database, file store, cache, or session state.
- The `GET /hello` response is a compile-time constant string (`Hello World`); it is not derived from any input or stored record.
- The only "model" is the HTTP response contract itself, which is documented in [`contracts/hello.http.md`](./contracts/hello.http.md).

Spec cross-reference: the **Key Entities** section of [`spec.md`](./spec.md) is explicitly marked "Not applicable".
