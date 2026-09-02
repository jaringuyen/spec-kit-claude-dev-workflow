# Quick Start

Run the Spec Kit + Claude Code development environment from a clean machine.

## Prerequisites

Install:

- Git
- Docker Desktop
- Visual Studio Code
- VS Code **Dev Containers** extension

You do **not** need to install Claude Code, Spec Kit, .NET, Node.js, or Python locally.

They are provided by the development container.

---

## Windows 11

### 1. Start Docker Desktop

Verify Docker:

```powershell
docker --version
docker run --rm hello-world
```

### 2. Clone the Repository

PowerShell or git bash terminal:

```powershell
git clone https://github.com/jaringuyen/spec-kit-claude-dev-workflow.git
cd spec-kit-claude-dev-workflow
code .
```

### 3. Open the Development Container

In VS Code:

```text
Ctrl+Shift+P
→ Dev Containers: Reopen in Container
```

Wait for the container to build.

### 4. Verify the Environment

Open a VS Code terminal:

```bash
claude --version
specify --version
dotnet --version
git --version
```

#### Expected Versions

As of September 2026, this project's development container has been verified with:

- Claude Code: `2.1.258`
- Spec Kit: `1.0.3`
- .NET SDK: `10.0.400`
- Git: `2.43.0`

Newer compatible versions may be installed when the container is rebuilt.

### 5. Start Claude Code

Claude Code is the AI coding assistant used by this project.

In the VS Code terminal, start Claude Code:

```bash
claude
```

If this is your first time using Claude Code on this computer:

1. Claude Code will ask you to sign in.
2. Select the option to sign in with your Claude account.
3. A browser window will open, or Claude Code will provide a link to open.
4. Sign in to your Claude account.
5. Approve access if requested.
6. Return to the VS Code terminal.
7. Claude Code will start and display its interactive prompt.

To verify Claude can access the project, enter:

What project am I working on? List the top-level files only. Do not modify anything.

Claude should identify the project and list files from the repository.

To exit Claude Code:

```bash
/exit
```

Your Claude Code login is stored in a local Docker volume and is not committed to Git. You should normally only need to sign in once on each computer.

---

## macOS

Install:

- Git
- Docker Desktop
- Visual Studio Code
- VS Code Dev Containers extension

Then:

```bash
git clone https://github.com/jaringuyen/spec-kit-claude-dev-workflow.git
cd spec-kit-claude-dev-workflow
code .
```

In VS Code:

```text
Cmd+Shift+P
→ Dev Containers: Reopen in Container
```

Then verify:

```bash
claude --version
specify --version
dotnet --version
git --version
```

Start Claude:

```bash
claude
```

---

## Ready

Your environment is now:

```text
Windows / macOS
       ↓
    VS Code
       ↓
 Dev Container
       ├── Claude Code
       ├── Spec Kit
       ├── .NET SDK
       ├── Git
       └── Project Source
```

All development should be performed inside the Dev Container.
