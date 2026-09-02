# AI-Assisted Software Development with Human Control

A practical reference showing how an experienced software team can use AI to deliver more software while keeping human control over requirements, architecture, quality and production.

## The Idea

AI can now perform a significant amount of software implementation work.

The opportunity is not simply to ask AI to build software from a prompt. The challenge is to place AI inside a controlled engineering process where requirements are clear, quality is independently verified and humans remain accountable for important decisions.

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
Automated Quality Gates
        ↓
AI Pre-review
        ↓
Human Final Review
        ↓
Deploy & Verify
        ↓
Production
```

## Business Goal

The question this project explores is:

> **Can a smaller, experienced development team safely deliver more software by giving AI more implementation work while retaining human governance and engineering quality?**

The objective is not to remove developers or human accountability.

The objective is to increase the leverage of experienced people by allowing AI to handle more coding, testing and routine review work.

## What Humans Still Control

Humans remain responsible for the decisions that matter:

- What should be built?
- Are the requirements correct?
- Is the proposed architecture appropriate?
- Are security and operational risks acceptable?
- Is the implementation ready to merge?
- Is the software ready for production?

AI can propose and implement. Humans approve.

## How Quality Is Protected

AI-generated code must pass the same engineering controls as human-written code.

This reference workflow includes:

- defined engineering and coding standards
- test-first development
- automated unit and integration tests
- local quality gates
- independent CI validation
- AI-assisted code pre-review
- human code review
- controlled deployment
- post-deployment smoke/E2E testing

A coding agent cannot simply declare that its work is complete. The repository and CI pipeline independently verify it.

## Technology Used

The reference implementation uses three main tools.

### Spec Kit

Spec Kit turns an approved business requirement into a structured specification, technical plan and implementation tasks.

This reduces the risk of asking an AI coding agent to work from an ambiguous prompt.

### Claude Code

Claude Code acts as the AI implementation partner.

It reads the approved specification and engineering rules, writes tests, implements code, runs quality checks and assists with code review.

### Docker

Docker provides a consistent and isolated development environment.

Every developer receives the same versions of Claude Code, Spec Kit, .NET and other development tools without having to configure them individually on their computer.

This also limits the AI coding environment to the project workspace rather than giving it unrestricted access to the developer's machine: SECURITY, SECURITY, SECURITY!!!

## Reference Application

The repository contains a deliberately simple **Hello World REST API**.

The application itself is not the point.

Its purpose is to make the complete engineering workflow easy to understand, test and reproduce before applying the same process to real business applications.

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
    ↓
Pull Request
    ↓
Independent CI Quality Gate
    ↓
AI Pre-review
    ↓
Human Code Review
    ↓
Deploy to Dev/Test
    ↓
Smoke / E2E Verification
    ↓
Production
```

## Current Status

Implemented:

- structured specification workflow
- engineering constitution
- enforced coding standards
- Claude Code development rules
- isolated Docker development environment
- reproducible VS Code Dev Container
- local automated quality gate
- Hello World reference API

Being added:

- GitHub CI
- automatic AI pull-request pre-review
- protected branch and human approval
- Dev/Test deployment
- post-deployment smoke/E2E testing
- production deployment
- Jira requirement integration

## Try It

Developers can run the complete environment on a clean Windows or macOS computer.

See **[QUICKSTART.md](QUICKSTART.md)**.

## Core Principle

> **AI writes more of the code. Humans control the engineering system.**
