---
name: systematic-debugging
description: Diagnose reproducible defects and unexpected behavior from evidence before applying a focused fix. Use for bugs, regressions, failing commands, and unclear runtime behavior; not for new-feature planning.
---

# Systematic debugging

Find the earliest verified cause of the failure and avoid stacking speculative fixes.

## Diagnose

1. Define the expected and actual behavior, including the smallest known reproduction.
2. Reproduce or inspect the failure with the narrowest safe command, request, log, or code path available.
3. Trace data and control flow across boundaries until the first incorrect state is identified.
4. Form one falsifiable hypothesis and run the smallest useful check that can confirm or reject it.
5. Repeat with a new hypothesis only when the evidence rejects the previous one.

Do not treat the last thrown exception as the root cause without tracing what produced it. Do not change multiple unrelated variables in one experiment, suppress errors, or broaden scope to clean up nearby code.

## Fix when authorized

If the user requested diagnosis only, explain the cause and stop. If a fix is in scope, change the smallest responsible layer and preserve established contracts unless the defect is in the contract itself.

Verify the original reproduction and check relevant adjacent behavior using existing builds, lint, type checking, logs, endpoint requests, or manual flows. Unit tests and test infrastructure are not required and must not be added unless the user explicitly requests them.

Report the root cause, evidence, files changed, verification result, and any uncertainty that remains.
