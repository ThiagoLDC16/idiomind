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

export const SUPPORTED_I18N_LANGUAGE_CODES = Object.keys(localeLoaders).map(
  (languageTag) => languageTag.split('-')[0],
);

function getLanguageTag(): SupportedLanguageTag {
  const locale = Localization.getLocales()[0];
  const matchingLanguageTag = (Object.keys(localeLoaders) as SupportedLanguageTag[]).find(
    (languageTag) => languageTag === locale?.languageTag || languageTag === locale?.languageCode,
  );

  return matchingLanguageTag ?? DEFAULT_LANGUAGE_TAG;
}

const languageTag = getLanguageTag();

export const i18nReady = (async () => {
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
})();

export default translationEngine;
