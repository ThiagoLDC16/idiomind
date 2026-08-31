---
name: feature-workflow
description: Clarify, plan, and implement non-trivial product features or cross-cutting changes in this repository. Use when requirements or impact need structured analysis; skip for trivial edits and narrow explanations.
---

# Feature workflow

Turn a feature request into the smallest coherent change that satisfies the product outcome and can be verified with the repository's current tooling.

## Establish the change

1. Read the relevant product documentation, nearby implementation, nested `AGENTS.md`, and applicable focused skills.
2. State the user-visible outcome and concrete acceptance conditions.
3. Identify affected contracts, data, screens, endpoints, localization, and platform behavior.
4. Ask only about ambiguity that would materially change the outcome. Make and disclose reasonable low-risk assumptions for the rest.

## Plan proportionally

For a non-trivial change, write a short implementation plan that names the likely files or areas, dependencies between steps, and verification for each outcome. Do not impose a planning ceremony on a small, well-defined edit.

Prefer an end-to-end vertical slice over speculative infrastructure. Keep optional enhancements separate from what is required now. If the user asked only for a plan, stop before editing.

## Implement and verify

- Follow the repository and nested-area instructions.
- Preserve existing contracts unless the requested behavior requires a deliberate change.
- Keep edits focused and review the diff for accidental cleanup or generated noise.
- Verify with the relevant lint, type-check, build, formatting, and focused manual checks that already exist.
- Do not create unit tests, test projects, test configuration, or testing dependencies unless the user explicitly adds them to the scope.

Report what changed, the evidence from verification, any assumptions, and any remaining risk or blocked check.
