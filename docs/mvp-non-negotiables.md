# MVP Non-Negotiables (v1.0)

This document defines the strict scope boundaries for **CorsairSolutions.RoundTable v1.0**.

> **The Gatekeeper Rule:** Whenever a new idea or feature appears during v1 development, ask:
>
> *"Do we need this to prove the RoundTable concept?"*
>
> If the answer is **no**, log it under **Future Enhancements** and move on.

---

## Included in v1.0 (Must-Haves)

### 1. Core Agent Support

- **Gemini support:** In-process `IAgentParticipant` adapter backed by `Google.GenAI`.
- **Copilot support:** In-process `IAgentParticipant` adapter backed by `GitHub.Copilot.SDK` (authenticated via a personal GitHub account).
- **Distinct reasoning charters:** System prompts tuned to preserve complementary perspectives.

### 2. Orchestration Pipeline

- **Parallel draft generation:** Stage 1 fans out the user prompt simultaneously (`Task.WhenAll`) with zero cross-visibility to prevent anchoring bias.
- **Structured adversarial critique stage:** Stage 2 swaps drafts using an explicit anti-sycophancy prompt.
- **Final revision/synthesis stage:** Stage 3 provides each agent with its original draft and peer critique.
- **"What I Changed & Why" summary:** Each final revision begins with a concise summary of accepted and rejected critique points.

### 3. Resilience & UI

- **Per-agent timeout support:** Every agent call is bounded by a `CancellationToken`.
- **Fault isolation and partial-result display:** One failing seat does not invalidate the entire session.
- **Side-by-side results UI:** Blazor 10.0 workbench displaying drafts, critiques, and final responses.

---

## Explicitly Out of Scope for v1.0 (Future Enhancements)

To keep v1 focused on proving the core workflow in an 8-hour weekend build, the following are excluded:

- **No persistence:** In-memory session state only.
- **No A2A (Agent2Agent) protocol:** All agents run in-process.
- **No MCP (Model Context Protocol):** No external tool integrations in the runtime loop.
- **No advanced diff engine:** Use side-by-side Markdown and "What I Changed & Why" summaries.
- **No multi-round arbitration loops:** Fixed pipeline (`Draft → Critique → Final`).
- **No N-seat dynamic registry UI:** Two seats only (Gemini + Copilot) while preserving extensibility through `IAgentParticipant`.
