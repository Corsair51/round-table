# Architecture Specification

## 1\. Architectural Vision

**CorsairSolutions.RoundTable** is a local-first .NET 10.0 multi-AI deliberation workbench built using **Domain-Driven Design (DDD)** and **Clean Architecture** principles.

The core architectural goal is to keep the multi-agent orchestration state machine completely decoupled from vendor SDKs and UI frameworks. Every AI model sits as an equal peer behind a domain-defined port (`IAgentParticipant`), allowing independent draft generation, structured adversarial cross-critique, and final synthesis under human oversight.

\---

## 2\. Solution \& Project Topology

The solution (`RoundTable.slnx`) is organized into four source projects under `/src` and one test project under `/tests`:

```text
RoundTable.Web (Blazor 10.0 Presentation \& Composition Root)
   ├──► RoundTable.Application ──► RoundTable.Domain
   └──► RoundTable.Infrastructure ──► RoundTable.Domain
```

### `RoundTable.Domain` (Class Library)

* **Responsibility:** Encapsulates core business concepts, invariants, and external boundaries.
* **Dependencies:** None (zero external NuGet packages or project references).
* **Key Types:**

  * `RoundTableSession` — Aggregate root tracking the user prompt, participating seats, stage progression, and outcomes.
  * `CouncilSeat` — Represents an AI seat at the table (identity, display name, and reasoning charter).
  * `DraftArtifact` — Value object capturing an agent's initial independent response.
  * `PeerCritique` — Value object capturing one agent's structured critique of a peer's draft.
  * `SeatOutcome` — Value object holding an agent's final revised response (including its *"What I Changed \& Why"* summary) or fault/timeout details.
  * `IAgentParticipant` — Primary port defining the contract for drafting, critiquing, and synthesizing.

### `RoundTable.Application` (Class Library)

* **Responsibility:** Executes the bounded 3-stage deliberation pipeline, enforces timeouts, and isolates faults.
* **Dependencies:** `RoundTable.Domain` only.
* **Key Responsibilities:**

  * Deterministic state machine (`FanOut` → `CrossCritique` → `Synthesize`).
  * Adversarial critique prompt construction (anti-sycophancy instructions).
  * Per-agent `CancellationToken` timeout enforcement.
  * Partial-result preservation when an individual seat times out or throws an exception.

### `RoundTable.Infrastructure` (Class Library)

* **Responsibility:** Implements `IAgentParticipant` adapters for external LLM providers.
* **Dependencies:** `RoundTable.Domain`, `Google.GenAI`, `GitHub.Copilot.SDK`.
* **Key Adapters:**

  * `GeminiAgentParticipant` — Wraps `Google.GenAI` using a personal API key via `IConfiguration` / `dotnet user-secrets`.
  * `CopilotAgentParticipant` — Wraps `GitHub.Copilot.SDK` authenticated via the local personal GitHub Copilot CLI session.

### `RoundTable.Web` (Blazor 10.0 Web App)

* **Responsibility:** Composition root (`Program.cs` DI registration) and interactive side-by-side workbench UI.
* **Dependencies:** `RoundTable.Application`, `RoundTable.Infrastructure`.
* **Key Responsibilities:**

  * Prompt submission and session execution trigger.
  * Collapsible panels for Stage 1 initial drafts and Stage 2 cross-critiques.
  * Side-by-side comparison cards for Stage 3 final revisions and partial error states.

### `RoundTable.Application.Tests` (xUnit Test Project)

* **Responsibility:** Fast, deterministic unit test suite verifying orchestration behavior without external network calls or UI hosting.
* **Dependencies:** `RoundTable.Application`, `RoundTable.Domain`, `xUnit`, `NSubstitute`, `FluentAssertions`.

\---

## 3\. Deliberation Pipeline (3-Stage State Machine)

To avoid anchoring bias and prevent polite agreement loops, every `RoundTableSession` executes as a strict, bounded 3-stage workflow:

```text
\[User Prompt]
      │
      ├───────────────────────┐
      ▼                       ▼
\[Gemini Draft]         \[Copilot Draft]      ──► Stage 1: Fan-Out (Parallel, Isolated)
      │                       │
      └───────────┬───────────┘
                  ▼
       \[Swapped Cross-Critique]             ──► Stage 2: Adversarial Peer Review
                  │
      ┌───────────┴───────────┐
      ▼                       ▼
\[Gemini Final]         \[Copilot Final]      ──► Stage 3: Revision \& Synthesis
      │                       │
      └───────────┬───────────┘
                  ▼
     \[Side-by-Side Blazor View]             ──► Human Evaluation \& Verdict
```

### Stage 1: Parallel Fan-Out (`Drafting`)

* The orchestrator dispatches the user prompt and each seat's reasoning charter simultaneously via `Task.WhenAll`.
* Neither agent sees the other's response during Stage 1, eliminating anchoring bias.

### Stage 2: Adversarial Cross-Critique (`Critiquing`)

* Each surviving draft is passed to the opposing seat with a structured adversarial critique prompt.
* Agents are explicitly instructed **not** to rewrite the draft yet, and instead to identify factual flaws, missed edge cases, unstated assumptions, or unnecessary over-engineering.
* *Fault Handling:* If a seat failed in Stage 1, Stage 2 is skipped for that pair and the surviving seat proceeds with its Stage 1 draft preserved.

### Stage 3: Revision \& Synthesis (`Synthesizing`)

* Each agent receives its own Stage 1 draft alongside the peer critique generated in Stage 2.
* The agent must evaluate each critique point, accept valid improvements, reject invalid critiques with brief reasoning, and output a final response prefaced by a **"What I Changed \& Why"** bullet summary.

\---

## 4\. Resilience \& Security Boundaries

* **Per-Agent Timeout Isolation:** Every call into `IAgentParticipant` is wrapped in a linked `CancellationTokenSource` with a configurable timeout. A hung upstream API call cancels cleanly without blocking the sibling seat.
* **Graceful Degradation:** Exceptions and timeouts are captured inside `SeatOutcome` rather than crashing the pipeline, ensuring the user always sees whatever partial results succeeded.
* **In-Memory Lifecycle (v1):** Sessions live strictly in memory for the duration of the Blazor circuit/request. No database or disk persistence is used in v1.
* **Secret Management:** API keys are never committed to source control and are loaded exclusively through `dotnet user-secrets` or local environment variables in `RoundTable.Web`.

