export interface AnalyticsEventProperties {
  'Unavailable Language Selected': {
    language_code: string;
    language_name: string;
    native_language_code: string;
    source: 'onboarding';
  };
  'Category Selected': {
    category_id: string;
    category_name: string;
    content_language_code: string;
  };
  'Situation Selected': {
    situation_id: string;
    situation_name: string;
    category_id: string;
    category_name: string;
    content_language_code: string;
  };
}

export type AnalyticsEventName = keyof AnalyticsEventProperties;
