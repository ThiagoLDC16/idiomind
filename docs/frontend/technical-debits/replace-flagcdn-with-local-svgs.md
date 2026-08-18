# Substituição das Bandeiras CDN por SVG Locais

## Descrição do Débito
Atualmente, as bandeiras dos países no componente `LanguagePicker` são exibidas utilizando o `expo-image` para buscar PNGs de um serviço externo (`https://flagcdn.com/`). Nós adotamos essa abordagem para resolver rapidamente o problema de o Windows não renderizar emojis nativos de bandeiras, além de ser uma alternativa leve que não impacta no tamanho (bundle size) do aplicativo.

Porém, essa solução traz dois pontos de atenção:
1. Dependência de rede para primeiro carregamento (não funciona 100% offline).
2. Utilização de imagens rasterizadas (PNG) ao invés de vetores (SVG), o que pode causar pequena perda de definição em telas com densidade de pixel muito alta.

## Solução Proposta (Opção 2)
A solução ideal a longo prazo e mais robusta para aplicativos offline é migrar para o uso de SVGs embutidos na aplicação por meio da biblioteca `react-native-country-flag`.

### Passos para implementação futura:
1. Instalar as dependências necessárias:
   ```bash
   npx expo install react-native-svg
   npm install react-native-country-flag
   ```

2. Atualizar o `LanguagePicker.tsx` para usar o componente `CountryFlag`:
   ```tsx
   import CountryFlag from "react-native-country-flag";

   // No lugar do <Image /> atual, utilizar:
   <CountryFlag 
     isoCode={getCountryCode(item.flagEmoji)} 
     size={24} 
     style={{ marginRight: 12, borderRadius: 2 }} 
   />
   ```

## Justificativa para Postergar (Trade-off)
Neste momento da inicialização do projeto, o uso do `FlagCDN` atende bem a necessidade e nos isenta de adicionar `react-native-svg` (que é uma dependência nativa grande) apenas para exibir bandeiras na tela de setup. O `expo-image` já faz um cache super agressivo e lida bem com falhas de rede. Se precisarmos de bandeiras em mais partes do aplicativo ou se o modo offline-first se tornar uma requisição mandatória para o fluxo de setup inicial, esse débito deverá ser pago.
