import * as Localization from 'expo-localization';
import translationEngine from 'i18next';
import { initReactI18next } from 'react-i18next';

const DEFAULT_LANGUAGE_TAG = 'en-US';

const localeLoaders = {
  'en-US': () => import('./locales/en-US'),
  'pt-BR': () => import('./locales/pt-BR'),
  es: () => import('./locales/es'),
};

type SupportedLanguageTag = keyof typeof localeLoaders;

const languageTagsByLanguageCode: Record<string, SupportedLanguageTag> = {
  en: 'en-US',
  pt: 'pt-BR',
  es: 'es',
};

export const SUPPORTED_I18N_LANGUAGE_CODES = Object.keys(localeLoaders).map(
  (languageTag) => languageTag.split('-')[0],
);

function getSupportedLanguageTag(language: string | null | undefined): SupportedLanguageTag | null {
  if (!language) return null;

  const normalizedLanguage = language.toLowerCase();
  const matchingLanguageTag = (Object.keys(localeLoaders) as SupportedLanguageTag[]).find(
    (languageTag) => languageTag.toLowerCase() === normalizedLanguage,
  );

  const languageCode = normalizedLanguage.split('-')[0];
  return matchingLanguageTag ?? languageTagsByLanguageCode[languageCode] ?? null;
}

function getDeviceLanguageTag(): SupportedLanguageTag {
  const locale = Localization.getLocales()[0];
  return (
    getSupportedLanguageTag(locale?.languageTag) ??
    getSupportedLanguageTag(locale?.languageCode) ??
    DEFAULT_LANGUAGE_TAG
  );
}

export function resolveI18nLanguage(nativeLanguage?: string | null): SupportedLanguageTag {
  return getSupportedLanguageTag(nativeLanguage) ?? getDeviceLanguageTag();
}

let isInitialized = false;

async function loadLanguage(languageTag: SupportedLanguageTag) {
  if (translationEngine.hasResourceBundle(languageTag, 'translation')) return;

  const resources = await localeLoaders[languageTag]();
  translationEngine.addResourceBundle(
    languageTag,
    'translation',
    resources.default.translation,
    true,
    true,
  );
}

export async function initializeI18n(nativeLanguage?: string | null) {
  const languageTag = resolveI18nLanguage(nativeLanguage);

  if (isInitialized) {
    await loadLanguage(languageTag);
    if (translationEngine.language === languageTag) return;

    // eslint-disable-next-line import/no-named-as-default-member
    await translationEngine.changeLanguage(languageTag);
    return;
  }

  const resources = await localeLoaders[languageTag]();

  // i18next registers React integration through its default instance.
  // eslint-disable-next-line import/no-named-as-default-member
  await translationEngine.use(initReactI18next).init({
    resources: { [languageTag]: resources.default },
    lng: languageTag,
    fallbackLng: DEFAULT_LANGUAGE_TAG,
    interpolation: {
      escapeValue: false,
    },
  });
  isInitialized = true;
}

export default translationEngine;
