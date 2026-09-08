# Repository guidance

## Product

Idiomind is a language-practice application built around realistic, AI-assisted conversations. It helps learners turn theoretical knowledge into confidence in situations such as immigration, restaurants, hotels, travel, and work. Read `docs/about-app.md` when product intent affects a decision.

## Repository map

- `frontend/`: Expo and React Native application for Android, iOS, and web. Its local conventions are in `frontend/AGENTS.md`.
- `backend/`: .NET API, application, domain, and infrastructure projects. Its local conventions are in `backend/AGENTS.md`.
- `docs/`: product-level documentation.
- `docker-compose.yml`: local orchestration; do not treat it as a complete deployment definition without checking its referenced files and configuration.

Instructions closer to a file take precedence over this document.

## Working agreements

- Inspect the relevant implementation and nearby conventions before editing.
- Make the smallest coherent change that solves the requested problem. Avoid speculative abstractions and unrelated cleanup.
- Ask a question only when missing information would materially change behavior, data, architecture, or user experience. Otherwise, make a reasonable assumption and state it.
- Preserve user changes and unrelated work in a dirty worktree. Never discard or rewrite them to simplify a task.
- Do not turn a scoped task into repository-wide sanitation, dependency upgrades, formatting, lint cleanup, or tooling migration.
- Do not add dependencies, change public contracts, create migrations, or alter infrastructure unless the requested change requires it.
- Do not add or require unit tests or unit-test infrastructure unless the user explicitly changes that scope. Use the available static checks, builds, and focused manual verification instead.
- Do not commit, push, open pull requests, or mutate external systems unless the user explicitly asks.
- Never place secrets, tokens, passwords, or production credentials in code, documentation, logs, or committed configuration.

## Reusable AI workflows

The repository-level skills are optional workflows, not ceremonies required for every task:

- `.agents/skills/feature-workflow/SKILL.md`: clarify and plan a non-trivial feature or cross-cutting change.
- `.agents/skills/systematic-debugging/SKILL.md`: diagnose a reproducible defect from evidence before changing code.
- `.agents/skills/code-review/SKILL.md`: perform a read-only, risk-ranked review.
- `.agents/skills/parallel-work/SKILL.md`: coordinate subagents only when the user explicitly requests parallel or delegated work.

Frontend-specific skills remain under `frontend/.agents/skills/`; `frontend/AGENTS.md` explains when to consult them.

## Verification

- Match verification effort to the change and run commands from the directory whose tooling they use.
- For frontend changes, available checks include `npm run lint`, `npx tsc --noEmit`, and `npm run format` from `frontend/`. A targeted Prettier check is acceptable for a small change when the repository-wide command reports unrelated files.
- For backend changes, use `dotnet build Idiomind.sln` from `backend/`, or build the narrowest affected project when isolating a failure.
- For behavior that static checks cannot establish, describe and perform the smallest useful manual check when the environment permits it.
- If a check fails because of a pre-existing or unrelated problem, identify that explicitly and show whether the change introduced any new failure. Never claim a check passed when it did not.
