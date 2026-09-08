# Entidades de Configuração de Simulações — Idiomind

## Visão Geral

O Idiomind organiza seu conteúdo em uma hierarquia de quatro níveis: **Category → Situation → SituationVariant → SituationVariantObjective**. Todas essas entidades suportam múltiplos idiomas via um padrão de tradução consistente.

---

## Padrão Comum: Tradução (`ITranslatable`)

Todas as entidades de configuração implementam `ITranslatable<TTranslation>` e possuem uma coleção de traduções. Cada `*Translation` herda de `TranslationBase`, que contém:

- `EntityId` — FK para a entidade pai
- `LanguageCode` — ex: `"en"`, `"pt"`, `"es"`

Toda entidade herda de `BaseEntity`:
- `Id: Guid`
- `CreatedAt: DateTime`
- `UpdatedAt: DateTime?`

---

## 1. `Category`

Agrupa situações

**Propriedades:**
| Campo | Tipo | Descrição |
|---|---|---|
| `Icon` | `string?` | Ícone visual da categoria |
| `Translations` | `IReadOnlyCollection<CategoryTranslation>` | Traduções da categoria |

**`CategoryTranslation`:**
| Campo | Tipo |
|---|---|
| `Name` | `string` — nome da categoria no idioma |

---

## 2. `Situation`

Uma situação real específica dentro de uma categoria (ex: *Passar pela imigração*, *Pedir comida em restaurante*).

**Propriedades:**
| Campo | Tipo | Descrição |
|---|---|---|
| `CategoryId` | `Guid` | FK → `Category` |
| `Translations` | `IReadOnlyCollection<SituationTranslation>` | Traduções da situação |

**`SituationTranslation`:**
| Campo | Tipo |
|---|---|
| `Name` | `string` — nome da situação |
| `Description` | `string` — descrição do contexto |

---

## 3. `SituationVariant`

Uma variante específica de uma situação para um idioma de aprendizado.
Uma mesma situação pode ter variantes para diversificar a exposição ao idioma pelo usuário. Exemplo: Uma situação "Imigração" pode incluir variantes para "Visita de 2 semanas", "Voo de Conexão", etc.
Cada variante é exclusiva por idioma de aprendizado, isso é necessário para permitir instruções personalizadas por idioma, evitando que as simulações do app se tornem muito genéricas para o usuário final. 
É aqui que ficam as instruções que configuram o comportamento da IA durante a simulação.

**Propriedades:**
| Campo | Tipo | Descrição |
|---|---|---|
| `SituationId` | `Guid` | FK → `Situation` |
| `LearningLanguage` | `string` | Idioma sendo praticado (ex: `"en"`) |
| `PromptInstructions` | `string` | Instruções ao modelo de IA sobre como conduzir a conversa |
| `InitialMessage` | `string` | Primeira mensagem que a IA envia ao iniciar a simulação |
| `Translations` | `IReadOnlyCollection<SituationVariantTranslation>` | Traduções da variante |
| `Objectives` | `IReadOnlyCollection<SituationVariantObjective>` | Objetivos que o usuário deve cumprir |
| `SituationVariantBaseId` | `Guid` | Id interno da variante base, usado para identificar variantes que foram clonadas de uma variante base e traduzidas para o idioma de aprendizado alvo |

**`SituationVariantTranslation`:**
| Campo | Tipo |
|---|---|
| `Name` | `string` — nome da variante no idioma nativo do usuário |
| `UserContext` | `string` — contexto explicativo para o usuário antes de iniciar (ex: "Você acabou de chegar ao aeroporto e precisa passar pela imigração") |

---

## 4. `SituationVariantObjective`

Um objetivo específico que o usuário deve cumprir durante a simulação (ex: *Informar o motivo da viagem*, *Explicar onde vai se hospedar*). Avaliados pela IA durante a conversa.
IMPORTANTE: Esses objetivos não devem representar uma ação física (exemplo: Mostrar o passaporte, subir as escadas), já que o usuário está em um aplicativo no estilo de Chat e não teria como realizar essas ações.

**Propriedades:**
| Campo | Tipo | Descrição |
|---|---|---|
| `SituationVariantId` | `Guid` | FK → `SituationVariant` |
| `Translations` | `IReadOnlyCollection<SituationVariantObjectiveTranslation>` | Traduções do objetivo |

**`SituationVariantObjectiveTranslation`:**
| Campo | Tipo |
|---|---|
| `Name` | `string` — descrição do objetivo no idioma nativo do usuário |

---

## Diagrama de Relacionamentos

```
Category
  │  (1:N)
  ├─ Icon
  └─ CategoryTranslation[]: Name
       │
       ▼
    Situation
      │  (1:N)
      ├─ CategoryId → Category
      └─ SituationTranslation[]: Name, Description
           │
           ▼
        SituationVariant
          │  (1:N)
          ├─ SituationId → Situation
          ├─ LearningLanguage  ← define o idioma praticado
          ├─ PromptInstructions ← instrução para a IA
          ├─ InitialMessage ← primeira fala da IA
          ├─ SituationVariantTranslation[]: Name, UserContext
          └─ SituationVariantObjective[]
               │  (1:N)
               ▼
          SituationVariantObjective
            ├─ SituationVariantId → SituationVariant
            └─ SituationVariantObjectiveTranslation[]: Name
```

---

## Regras de Negócio Implícitas

1. **Conteúdo bilíngue** — os campos de tradução (`*Translation`) são apresentados no idioma **nativo do usuário**, enquanto `LearningLanguage` e `InitialMessage` operam no **idioma sendo aprendido**.
2. **`UserContext`** é o texto que prepara o usuário psicologicamente para a situação antes de começar — equivale ao "briefing" do cenário.
3. **`PromptInstructions`** é o system prompt da IA para aquela variante — controla o papel, tom e comportamento do interlocutor simulado.
