# RoundTable Issue Roles

RoundTable uses Arthurian characters as thematic identifiers for major work items and epics.

These roles provide memorable issue names and help organize work around the project's architecture. They do not replace GitHub labels, milestones, or architectural boundaries.

---

## Merlin

**Legendary Trait:** Architect of the Round Table itself

**Primary Responsibility:**
Solution architecture, foundational abstractions, and domain modeling.

**Typical Work:**

- Solution scaffolding
- Project structure
- Domain entities
- Core interfaces
- Architectural decisions
- `IAgentParticipant`

**Examples:**

- [Merlin] Create Solution Skeleton
- [Merlin] Define Core Domain Model
- [Merlin] Define IAgentParticipant Contract

---

## Bors

**Legendary Trait:** Steadfast, unyielding integrity

**Primary Responsibility:**
Application orchestration and workflow execution.

**Typical Work:**

- State machine implementation
- Session orchestration
- Workflow coordination
- Process management

**Examples:**

- [Bors] Implement FanOut Stage
- [Bors] Implement CrossCritique Stage
- [Bors] Implement Synthesize Stage
- [Bors] Build RoundTable Session Coordinator

**Associated Area:**

- `area:application`

---

## Gawain

**Legendary Trait:** Solar knight (clarity and directness)

**Primary Responsibility:**
Gemini integration.

**Typical Work:**

- Gemini SDK integration
- Gemini adapter implementation
- Gemini-specific configuration
- Gemini prompt handling

**Examples:**

- [Gawain] Implement Google.GenAI Adapter
- [Gawain] Add Gemini Authentication
- [Gawain] Create Gemini Prompt Templates

**Associated Area:**

- `area:infrastructure`

---

## Percival

**Legendary Trait:** Deep seeker of hidden truths

**Primary Responsibility:**
Copilot integration.

**Typical Work:**

- GitHub Copilot SDK integration
- Copilot adapter implementation
- Copilot authentication
- Copilot-specific configuration

**Examples:**

- [Percival] Implement GitHub.Copilot.SDK Adapter
- [Percival] Configure Copilot Authentication
- [Percival] Add Copilot Prompt Templates

**Associated Area:**

- `area:infrastructure`

---

## Gareth

**Legendary Trait:** Proven through rigorous trials

**Primary Responsibility:**
Peer review and critique systems.

**Typical Work:**

- Cross-critique prompts
- Anti-sycophancy safeguards
- Structured feedback templates
- Revision workflows

**Examples:**

- [Gareth] Build Critique Framework
- [Gareth] Implement Anti-Sycophancy Rules
- [Gareth] Create Structured Review Categories

**Associated Area:**

- `area:application`

---

## Galahad

**Legendary Trait:** Shielded against failure

**Primary Responsibility:**
Reliability and resilience.

**Typical Work:**

- Timeout handling
- Fault isolation
- Retry policies
- Graceful degradation
- Error handling

**Examples:**

- [Galahad] Add CancellationToken Support
- [Galahad] Implement Fault Isolation
- [Galahad] Handle Partial-Result Scenarios

**Associated Area:**

- `area:application`

---

## Bedivere

**Legendary Trait:** Trusted counsel at the final hour

**Primary Responsibility:**
User interface and presentation.

**Typical Work:**

- Blazor UI
- Side-by-side comparison views
- Markdown rendering
- Session visualization
- User workflows

**Examples:**

- [Bedivere] Create Prompt Submission Page
- [Bedivere] Build Comparison Workbench
- [Bedivere] Render Critique History

**Associated Area:**

- `area:web`

---

## Kay

**Legendary Trait:** Steward who keeps order in the hall

**Primary Responsibility:**
Testing and quality assurance.

**Typical Work:**

- Unit tests
- Integration tests
- UI tests
- Mock infrastructure
- Test coverage improvements

**Examples:**

- [Kay] Create Orchestration Tests
- [Kay] Build Adapter Test Suite
- [Kay] Add Playwright UI Coverage

**Associated Area:**

- `area:test`

---

## Arthur

**Role Within The Project:**
Milestone and decision-maker.

Arthur is not assigned as a development role.

Arthur represents:

- MVP completion
- Human evaluation
- Final decision making

Just as the knights provide counsel and Arthur renders judgment, the AI participants provide analysis while the human ultimately decides.

**Examples:**

- Milestone: Arthur (MVP)
- Human Verdict
- Final Evaluation

---

## Camelot

**Role Within The Project:**
Future vision of the completed platform.

Camelot represents the mature implementation of RoundTable beyond the MVP.

Potential future capabilities:

- Additional AI participants
- N-seat councils
- A2A integration
- MCP integration
- Session persistence
- Advanced review workflows

Camelot is the destination.

RoundTable is the journey.