# Seed Migration — Best Practices

Reference for creating new content seed migrations in this project.

---

## 1. Creating the migration

Always use a dedicated migration for seed data — never mix schema changes with seed inserts.

```bash
dotnet ef migrations add Seed_<DescriptiveName> --project src/Idiomind.Infrastructure
```

---

## 2. Migration structure

One `private static` method per top-level entity group (e.g. one per category). Call all of them from `Up`.

```csharp
public partial class Seed_MyContent : Migration
{
    private const string MyCategorySlug = "my-category";

    protected override void Up(MigrationBuilder migrationBuilder)
    {
        SeedMyCategory(migrationBuilder);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
    }

    private static void SeedMyCategory(MigrationBuilder migration)
    {
        // ...
    }
}
```

---

## 3. Slugs and IDs

Slugs are the stable, human-readable keys used to derive deterministic UUIDs via `ContentId.From(slug)`.

Rules:
- Use `kebab-case`
- Slugs must be **globally unique** across all seed data
- Never reuse or rename a slug after it has been applied to any environment — it changes the UUID

```csharp
var categoryId = ContentId.From("my-category");
var situationId = ContentId.From("my-situation");
var variantId   = ContentId.From("my-situation-variant");

// Objectives use the variant slug as prefix
// Pattern: "{variantSlug}:obj:{n}" (1-based)
"my-situation-variant:obj:1"
"my-situation-variant:obj:2"
```

---

## 4. Inserting entities

Use the extension methods from `Idiomind.Infrastructure.Seeding.Extensions`. Always use **named parameters**.

### Category

```csharp
migration.InsertCategory("my-category", new Category(
    icon: "icon-name",
    translations: [
        new("pt-BR", "Nome da Categoria")
    ]
));
```

### Situation

```csharp
migration.InsertSituation("my-situation", new Situation(
    categoryId: categoryId,
    translations: [
        new("pt-BR", "Nome da Situação", "Descrição curta e engajante.")
    ]
));
var mySituationId = ContentId.From("my-situation");
```

### SituationVariant

```csharp
migration.InsertSituationVariant("my-situation-variant", new SituationVariant(
    situationId: mySituationId,
    learningLanguage: "en",
    promptInstructions: "[TODO]",
    initialMessage: "[TODO]",
    translations: [
        new("pt-BR", "Nome do Variant", "Contexto exibido ao usuário antes de iniciar a simulação.")
    ]
));
var myVariantId = ContentId.From("my-situation-variant");
```

### SituationVariantObjective

```csharp
migration.InsertObjective("my-situation-variant:obj:1", new SituationVariantObjective(
    situationVariantId: myVariantId,
    translations: [
        new("pt-BR", "Descrição do objetivo que o usuário deve cumprir.")
    ]
));
```

---

## 5. Adding translations

The `translations` array is the place to add new languages. Keep each language on its own line.

```csharp
translations: [
    new("pt-BR", "Nome"),
    new("en",    "Name")      // add new languages here
]
```

This applies to all entity types: `Category`, `Situation`, `SituationVariant`, and `SituationVariantObjective`.

---

## 6. `promptInstructions` and `initialMessage`

These fields on `SituationVariant` are not sourced from the content JSON. Seed them as `"[TODO]"` and fill them in a separate pass once the AI prompt design for that variant is ready.

---

## 7. `Down` method

Leave `Down` empty for content seeds. Deleting seed rows causes data loss in production and is not reversible in a safe way.

```csharp
protected override void Down(MigrationBuilder migrationBuilder)
{
}
```