# CorsairSolutions.RoundTable

A local-first .NET 10.0 multi-AI peer-review workbench where independent AI models tackle a prompt, critique each other's drafts, and produce refined final responses for side-by-side human evaluation.

> *"No seat at the head of the table—equal AI peers advising a human decision-maker."*

## Why RoundTable?
Different LLMs have distinct reasoning strengths and blind spots. Rather than trusting a single model's first pass—or manually copying and pasting prompts across browser tabs—**RoundTable** automates a bounded **maker-checker peer-review loop**:

```text
User Prompt
     │
     ├──────────────────┐
     ▼                  ▼
[Gemini Seat]     [Copilot Seat]     ──► Stage 1: Parallel Draft (Fan-Out)
     │                  │
     └────────┬─────────┘
              ▼
    Structured Cross-Critique        ──► Stage 2: Adversarial Peer Review
              │
     ┌────────┴─────────┐
     ▼                  ▼
[Gemini Final]   [Copilot Final]     ──► Stage 3: Revision & Synthesis
     │                  │
     └────────┬─────────┘
              ▼
   Side-by-Side Human Verdict        ──► Stage 4: Evaluation UI
```

## Architecture & Solution Layout
Built with strict Domain-Driven Design (DDD) boundaries so the orchestration engine remains completely decoupled from specific LLM SDKs:

* RoundTable.Domain — Pure C# .NET 10.0 domain entities, value objects (RoundTableSession, CouncilSeat, DraftArtifact, PeerCritique, SeatOutcome), and the IAgentParticipant port. Zero external SDK dependencies.

* RoundTable.Application — The deterministic 3-stage state machine (FanOut → CrossCritique → Synthesize), structured adversarial critique prompts, per-agent timeout enforcement, and fault-isolation logic.

* RoundTable.Infrastructure — Thin adapters implementing IAgentParticipant for Google Gemini (Google.GenAI) and GitHub Copilot (GitHub.Copilot.SDK).

* RoundTable.Web — Minimal Blazor 10.0 side-by-side workbench displaying initial drafts, cross-critiques, and final revisions.

* RoundTable.Application.Tests — Deterministic xUnit + NSubstitute unit tests covering orchestration flow, timeouts, and partial-result handling.

## Prerequisites & Local Setup
* .NET 10.0 SDK

* A personal Gemini API Key

* A personal GitHub Copilot account authenticated via the Copilot CLI (copilot login)

## Secret Configuration
Never commit API keys or tokens to source control. Configure secrets locally via dotnet user-secrets inside the RoundTable.Web project:

```bash
Bash

cd src/RoundTable.Web
dotnet user-secrets init
dotnet user-secrets set "Gemini:ApiKey" "YOUR_GEMINI_API_KEY"
```
## Running the Project

```bash
Bash

dotnet restore
dotnet test
dotnet run --project src/RoundTable.Web
```
