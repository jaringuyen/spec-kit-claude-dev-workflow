# AI-Assisted Software Development with Human Control

A practical reference showing how an experienced software team can use AI to deliver more software while keeping human control over requirements, architecture and quality.

## The Idea

AI can now perform a significant amount of software implementation work.

The opportunity is not simply to ask AI to build software from a prompt. The challenge is to place AI inside a controlled engineering process where requirements are clear, engineering standards are enforced, quality is independently verified and humans remain accountable for important decisions.

This project demonstrates that process.

```text
Business Requirement
        ↓
Human approves WHAT to build
        ↓
Structured Specification & Plan
        ↓
Human approves HOW to build it
        ↓
AI writes and tests the code
        ↓
Local Quality Gate
        ↓
Pull Request
        ↓
Independent CI Quality Gate
        ↓
Human Merge Decision
```

## Business Goal

The question this project explores is:

> **Can a smaller, experienced development team safely deliver more software by giving AI more implementation work while retaining human governance and engineering quality?**

The objective is not to remove developers or human accountability.

The objective is to increase the leverage of experienced people by allowing AI to handle more coding, testing and routine implementation work.

## What Humans Still Control

Humans remain responsible for the decisions that matter:

- What should be built?
- Are the requirements correct?
- Is the proposed architecture appropriate?
- Are security and operational risks acceptable?
- Is the implementation ready to merge?
- Is the software ready for production?

AI can propose and implement. Humans remain accountable for approval.

## How Quality Is Protected

AI-generated code must pass the same engineering controls as human-written code.

This reference workflow currently demonstrates:

- defined engineering constitution and coding standards
- test-first development
- automated unit and integration tests
- local quality gate
- independent GitHub CI validation
- pull-request-based development
- protected default branch
- human-controlled merge

A coding agent cannot simply declare that its work is complete.

The local quality gate verifies the work before submission, and GitHub CI independently repeats the checks before the change can be merged.

## Technology Used

### Spec Kit

Spec Kit turns an approved business requirement into a structured specification, technical plan and implementation tasks.

This reduces the risk of asking an AI coding agent to work from an ambiguous prompt.

### Claude Code

Claude Code acts as the AI implementation partner.

It reads the approved specification and engineering rules, writes tests, implements code and runs the local quality gate.

### Docker

Docker provides a consistent and isolated development environment.

Every developer receives the same development toolchain without having to install and configure Claude Code, Spec Kit, .NET and supporting tools individually.

It also provides an important security boundary: the AI coding environment operates inside the development container and project workspace rather than directly in the developer's host environment.

## Reference Application

The repository contains a deliberately simple **Hello World REST API**.

The application itself is not the point.

Its purpose is to make the engineering workflow easy to understand, test and reproduce before applying the same approach to real business applications.

## Development Workflow

```text
Requirement
    ↓
Human Requirement Approval
    ↓
Spec Kit
    ↓
Engineering Constitution + Coding Standards
    ↓
Human Technical Approval
    ↓
Claude Code
  • Tests First
  • Implement
  • Refactor
    ↓
Local Quality Gate
  • Format
  • Build
  • Tests
    ↓
Feature Branch
    ↓
Pull Request
    ↓
GitHub CI Quality Gate
    ↓
Protected Default Branch
    ↓
Human Merge Decision
    ↓
main
```

## Current Status

Implemented:

- structured specification workflow
- engineering constitution
- enforced coding standards
- Claude Code development rules
- test-first development
- isolated Docker development environment
- reproducible VS Code Dev Container
- local automated quality gate
- GitHub CI quality gate
- pull-request workflow
- protected default branch
- human-controlled merge
- Hello World reference API

## Optional Production Extension

The same workflow can be extended beyond merge to include:

```text
main
    ↓
Deploy to TEST
    ↓
Smoke / E2E Verification
    ↓
Human Production Approval
    ↓
Promote the Same Build
    ↓
Production
```

Future extensions may also demonstrate:

- human-triggered AI pull-request pre-review
- Jira requirement integration
- automatic TEST deployment
- post-deployment smoke/E2E testing
- controlled production promotion

These are intentionally outside the initial reference implementation so that the core workflow remains simple and inexpensive to reproduce.

## Try It

Developers can run the complete development environment on a clean Windows or macOS computer.

See **[QUICKSTART.md](QUICKSTART.md)**.

## Core Principle

> **AI writes more of the code. Humans control the engineering system.**