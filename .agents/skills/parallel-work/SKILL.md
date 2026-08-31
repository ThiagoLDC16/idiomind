---
name: parallel-work
description: Plan and coordinate safe subagent execution when the user explicitly asks for parallel work, delegation, or subagents. Do not use for ordinary single-agent implementation.
---

# Parallel work

Parallelize only work that is independent in both dependencies and filesystem ownership. When that cannot be established, use serial execution.

## Build execution waves

Describe each task with:

- `ID`: short and stable;
- `Description`: one bounded outcome;
- `Files`: every file or exact area the task may create or modify;
- `Depends-on`: task IDs whose output it consumes, or `none`;
- `Owner`: the delegated role or agent.

Two tasks may share a wave only when neither depends on the other, directly or transitively, and their `Files` sets are disjoint. If files or dependencies are uncertain, move the task to a later serial wave. Read-only investigation may run in parallel with other read-only work when it has no external side effects.

## Execute safely

1. Dispatch every task in one wave together, with explicit scope, expected output, applicable instructions, and verification.
2. Implementers edit only their assigned files, preserve all other workspace changes, do not commit, and report every file touched.
3. Before the next wave, inspect the combined diff for overlap or accidental changes and run the relevant available checks.
4. Start dependent tasks only after their prerequisites are complete and their outputs have been reviewed.
5. If agents discover an overlapping file or new dependency, stop parallel writes for those tasks and continue serially.

The coordinating agent also commits only when the user explicitly requested a commit. Do not create unit-test tasks, testing infrastructure, or mandatory test steps unless the user explicitly adds them to scope; use the repository's current lint, type-check, build, and focused manual verification instead.

Summarize wave outcomes, combined verification, unresolved conflicts, and any work intentionally serialized.
