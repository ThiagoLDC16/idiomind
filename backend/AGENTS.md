# Backend guidance

## Stack and structure

The backend targets .NET 10 and is split into four projects under `src/`:

- `Idiomind.Domain`: entities, value objects, domain events, and business invariants.
- `Idiomind.Application`: MediatR commands and queries, use-case orchestration, validation, response contracts, and interfaces for external services.
- `Idiomind.Infrastructure`: EF Core/PostgreSQL persistence, identity, JWT, seeding, and OpenAI service implementations.
- `Idiomind.Api`: controllers, HTTP payloads, middleware pipeline, and composition root.

Keep dependencies pointing inward. Domain code must not depend on EF Core, HTTP, JWT, or OpenAI implementations. Application code defines abstractions; Infrastructure implements them; API maps transport concerns to application use cases.

## Implementation conventions

- Keep controllers thin. Follow the existing `IMediator` and `MapResponse` patterns instead of putting business logic in controllers.
- Put each use case in its relevant feature folder and use the existing command/query, handler, DTO, validation, and response patterns.
- Pass the provided `CancellationToken` through database and external-service calls.
- Enforce authorization and ownership on the server even when the frontend also restricts an action.
- Keep API error values compatible with the localization keys consumed by the frontend.
- Prefer explicit domain methods for state transitions over direct mutation of entity internals.
- Keep provider details out of application handlers. OpenAI, persistence, hashing, and token mechanics belong behind application interfaces.
- Read configuration and secrets from the existing configuration/environment flow. Never hardcode credentials or expose secret values in logs.

## Simulation content and persistence

- Before changing simulation entities or content, read `docs/simulations-domain-structure.md`.
- Preserve the hierarchy `Category -> Situation -> SituationVariant -> SituationVariantObjective` and its translation model.
- Translation records describe content in the user's native language; `LearningLanguage`, simulation prompts, and initial messages concern the language being practiced.
- Objectives must be achievable through the chat interaction, not physical actions outside the application.
- Before creating a content seed migration, read and follow `docs/seed-migration-best-practices.md`. Keep schema and content-seed migrations separate, use stable globally unique slugs, and use the existing insertion extensions.
- Use EF Core tooling for schema migrations and inspect all generated changes. Do not casually hand-edit generated designer or model snapshot files.

## Verification

- Run `dotnet build Idiomind.sln` from `backend/` for a backend-wide change.
- When diagnosing a known solution-wide failure, build the narrowest affected project to distinguish new failures from the baseline.
- Exercise an affected endpoint with the existing `.http` files or another focused manual request when behavior needs runtime verification and required services are available.
- Do not add unit tests or test infrastructure unless the user explicitly asks for them.
