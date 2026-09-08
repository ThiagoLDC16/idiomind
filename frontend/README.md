# Idiomind Frontend

Aplicativo Expo com Expo Router, NativeWind e fluxo de autenticação JWT.

## Scripts

```bash
npm install
npm run start
npm run lint
npm run format
```

## Ambiente

Crie um arquivo `.env` a partir de `.env.example`.

Valores esperados:

- `EXPO_PUBLIC_APP_ENV=development`
- `EXPO_PUBLIC_API_BASE_URL=http://localhost:5172`
- `EXPO_PUBLIC_API_BASE_URL_ANDROID=http://10.0.2.2:5172`
- `EXPO_PUBLIC_API_BASE_URL_PRODUCTION=https://api.seu-dominio.com`

Em dispositivo físico, use o IP da sua máquina no mesmo Wi-Fi em `EXPO_PUBLIC_API_BASE_URL`.

## Estrutura

```text
src/
  app/
  features/
    auth/
  shared/
```

## Fluxo de autenticação

- Login e registro via `React Hook Form` + `Zod`
- Persistência de tokens com `Expo SecureStore`
- Refresh automático via interceptors do Axios
- Rotas protegidas por grupos `(auth)` e `(app)`
