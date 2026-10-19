import { Platform } from 'react-native';
import { z } from 'zod';

const envSchema = z.object({
  EXPO_PUBLIC_APP_ENV: z.enum(['development', 'production']).default('development'),
  EXPO_PUBLIC_MIXPANEL_TOKEN: z.string().trim().min(1).optional(),
  EXPO_PUBLIC_API_BASE_URL: z.string().optional(),
  EXPO_PUBLIC_API_BASE_URL_ANDROID: z.string().optional(),
  EXPO_PUBLIC_API_BASE_URL_PRODUCTION: z.string().optional(),
});

const parsedEnv = envSchema.parse({
  EXPO_PUBLIC_APP_ENV: process.env.EXPO_PUBLIC_APP_ENV,
  EXPO_PUBLIC_MIXPANEL_TOKEN: process.env.EXPO_PUBLIC_MIXPANEL_TOKEN || undefined,
  EXPO_PUBLIC_API_BASE_URL: process.env.EXPO_PUBLIC_API_BASE_URL,
  EXPO_PUBLIC_API_BASE_URL_ANDROID: process.env.EXPO_PUBLIC_API_BASE_URL_ANDROID,
  EXPO_PUBLIC_API_BASE_URL_PRODUCTION: process.env.EXPO_PUBLIC_API_BASE_URL_PRODUCTION,
});

const localApiBaseUrl =
  Platform.OS === 'android'
    ? (parsedEnv.EXPO_PUBLIC_API_BASE_URL_ANDROID ??
      parsedEnv.EXPO_PUBLIC_API_BASE_URL ??
      'http://10.0.2.2:5172')
    : (parsedEnv.EXPO_PUBLIC_API_BASE_URL ?? 'http://localhost:5172');

const productionApiBaseUrl =
  parsedEnv.EXPO_PUBLIC_API_BASE_URL_PRODUCTION ??
  parsedEnv.EXPO_PUBLIC_API_BASE_URL ??
  localApiBaseUrl;

export const env = {
  appEnv: parsedEnv.EXPO_PUBLIC_APP_ENV,
  mixpanelToken: parsedEnv.EXPO_PUBLIC_MIXPANEL_TOKEN,
  apiBaseUrl:
    parsedEnv.EXPO_PUBLIC_APP_ENV === 'production' ? productionApiBaseUrl : localApiBaseUrl,
};
