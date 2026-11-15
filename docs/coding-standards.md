# Coding Standards \& Technology Stack

This document defines the C# coding conventions, structural rules, and approved technology stack for **CorsairSolutions.RoundTable**. Both human contributors and AI coding assistants must adhere strictly to these standards.

\---

## 1\. Project Identity

* **Project Name:** Round Table
* **Root Namespace:** `CorsairSolutions.RoundTable`
* **Developer:** Tim

\---

## 2\. Approved Technology Stack

* **Runtime \& Web Framework:** ASP.NET 10.0, Blazor 10.0
* **IDE:** Visual Studio 2026
* **AI Provider SDKs:**

  * `Google.GenAI` (Gemini adapter)
  * `GitHub.Copilot.SDK` (GitHub Copilot adapter)
* **Testing \& Mocking Frameworks:**

  * `xUnit`
  * `NSubstitute`
  * `FluentAssertions`
  * `RichardSzalay.HttpMock`
  * `bUnit`
  * `Playwright`

\---

## 3\. File \& Class Organization

### One Type Per File

* Place only **one class, interface, record, or enum** per `.cs` file.
* The file name must match the type name exactly (e.g., `RoundTableSession.cs`).

### Namespaces \& `using` Directives

* Always use **block-scoped namespaces** (`namespace CorsairSolutions.RoundTable.Domain { ... }`), never file-scoped namespaces.
* Group `using` directives into three distinct blocks separated by a single blank line, sorted **alphabetically** within each group:

  1. `System.\*` namespaces
  2. Third-party library namespaces (e.g., `FluentAssertions`, `NSubstitute`, `Xunit`)
  3. Internal solution namespaces (`CorsairSolutions.RoundTable.\*`)

### Member Grouping (`#region`)

* Group class members in the following exact order using `#region` / `#endregion` blocks:

  1. `Fields`
  2. `Properties`
  3. `Constructors`
  4. `Public methods`
  5. `Private methods`
* **Omit empty regions:** Do not include a `#region` block if there are no members of that category in the file.

\---

## 4\. Formatting \& Naming Rules

### Brace \& Whitespace Style

* **Blank line after opening brace (`{`):** Always leave a single blank line immediately following the opening brace `{` of a class, field block, property accessor, or method.
* **Control flow blocks:** Single-line `if` statements (and all other control flow blocks) **must** use curly braces `{}`.
* **Disposable resources:** Always use explicit block-scoped `using` statements (`using (var resource = ...)` `{ ... }`), never `using` declarations.

### Naming Conventions

* **Private fields:** Use `camelCase` with **no leading underscore** (e.g., `private readonly IAgentParticipant geminiAgent;`).
* **Asynchronous methods:** All asynchronous methods must use `async`/`await` and be suffixed with `Async` (e.g., `ExecuteSessionAsync`).
* **Synchronous completion in async signatures:** For methods returning a `Task` or `Task<T>` that do not require an internal `await`, return `Result.FromTask<T>(value)` (or `Task.FromResult(value)` where `Result` is not applicable) rather than marking the method `async` without an `await`.

\---

## 5\. Testing Standards

* **Framework:** Write all unit tests in `xUnit` inside `RoundTable.Application.Tests`.
* **Mocking:** Use `NSubstitute` for mocking interfaces and domain ports (`IAgentParticipant`).
* **Assertions:** Use `FluentAssertions` exclusively for test assertions.
* **Multiple Assertions:** Whenever a test verifies more than one condition, wrap the assertions in a `using (new AssertionScope())` `{ ... }` block so all failures are reported together.

\---

## 6\. Reference Example

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;

using CorsairSolutions.RoundTable.Domain.Entities;
using CorsairSolutions.RoundTable.Domain.Ports;

namespace CorsairSolutions.RoundTable.Application.Services
{
    public class SampleOrchestrator
    {

        #region Fields

        private readonly IAgentParticipant primaryParticipant;

        #endregion

        #region Properties

        public TimeSpan DefaultTimeout { get; }

        #endregion

        #region Constructors

        public SampleOrchestrator(IAgentParticipant primaryParticipant)
        {

            this.primaryParticipant = primaryParticipant;
            DefaultTimeout = TimeSpan.FromSeconds(45);
        }

        #endregion

        #region Public methods

        public async Task<DraftArtifact> GenerateDraftAsync(string prompt, CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(prompt))
            {
                throw new ArgumentException("Prompt cannot be empty.", nameof(prompt));
            }

            using (var timeoutSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken))
            {
                timeoutSource.CancelAfter(DefaultTimeout);
                return await primaryParticipant.GenerateDraftAsync(prompt, timeoutSource.Token);
            }
        }

        #endregion
    }
}
```

