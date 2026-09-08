---
name: code-review
description: Review a diff, branch, pull request, or selected files for concrete defects and regressions in Idiomind. Use when the user asks for a review or audit; remain read-only unless fixes are separately requested.
---

# Code review

Produce a concise, evidence-backed report ordered by risk. Review the actual change and enough surrounding code to validate each claim.

## Select relevant lenses

Use only the lenses that match the changed surface:

- Behavior and data integrity: incorrect states, edge cases, concurrency, persistence, and backward compatibility.
- Security and privacy: authentication, authorization, ownership, validation, injection, secrets, and sensitive data exposure.
- Backend: .NET async/cancellation behavior, MediatR boundaries, EF Core queries and migrations, API response contracts, JWT, and OpenAI integration boundaries.
- Frontend: React hooks and rendering, Expo Router, Android/iOS/web differences, Zustand subscriptions, asynchronous UI states, accessibility, and i18n.
- Maintainability: misleading abstractions, duplicated rules, dead paths, error handling, and changes that conflict with established architecture.

Use subagents or parallel reviewer lenses only when the user explicitly requests delegated or parallel review. A single reviewer can apply the relevant lenses sequentially.

## Finding standard

Include a finding only when the changed code creates a concrete failure, regression, security risk, or significant maintenance hazard. Verify that nearby code does not already handle it. Do not pad the report with style preferences, praise, or hypothetical concerns without a plausible trigger.

For every finding, provide:

- severity: critical, high, medium, or low;
- exact `file:line` location;
- concise explanation of what is wrong;
- concrete input, state, or sequence that triggers the problem;
- smallest useful remediation direction.

Missing automated coverage may be noted as residual risk when it materially limits confidence, but do not create tests or test infrastructure during a review.

Lead with findings. Deduplicate issues that share one root cause. If no findings survive verification, say so plainly and mention only meaningful validation gaps or residual risks.
