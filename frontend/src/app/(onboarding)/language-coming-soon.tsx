import { useLocalSearchParams, useRouter } from 'expo-router';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { Text, View } from 'react-native';

import { onboardingApi } from '@/features/onboarding/api/onboarding-api';
import { authStore } from '@/features/auth/store/auth-store';
import { Button } from '@/shared/components/Button';

export default function LanguageComingSoonScreen() {
  const { t } = useTranslation();
  const router = useRouter();
  const { languageName, nativeLanguage } = useLocalSearchParams<{
    languageName: string;
    nativeLanguage: string;
  }>();
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleContinueInEnglish = async () => {
    if (!nativeLanguage) return;

    setIsSubmitting(true);
    try {
      await onboardingApi.createProfile({ nativeLanguage, learningLanguage: 'en' });
      const user = authStore.getState().user;
      if (user) {
        authStore.getState().setUser({ ...user, hasProfile: true });
      }
      router.replace('/(app)/');
    } catch (e) {
      console.error('[language-coming-soon] createProfile failed:', e);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <SafeAreaView className="flex-1 bg-white">
      <View className="flex-1 px-6 pt-16 pb-8 justify-center">
        <Text className="text-3xl font-bold text-slate-900 mb-4">
          {t('comingSoon.title', { language: languageName })}
        </Text>

        <Text className="text-base text-slate-600 leading-6 mb-4">
          {t('comingSoon.detail', { language: languageName })}
        </Text>

        <Text className="text-base text-slate-600 leading-6 mb-10">
          {t('comingSoon.hint')}
        </Text>

        <Button
          title={t('comingSoon.cta')}
          onPress={handleContinueInEnglish}
          loading={isSubmitting}
        />
      </View>
    </SafeAreaView>
  );
}
