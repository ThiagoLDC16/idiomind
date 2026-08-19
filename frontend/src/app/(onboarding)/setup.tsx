import { useRouter } from 'expo-router';
import { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { SafeAreaView } from 'react-native-safe-area-context';
import { Text, View } from 'react-native';

import { onboardingApi } from '@/features/onboarding/api/onboarding-api';
import type { Language } from '@/features/onboarding/api/onboarding-api/types';
import { SUPPORTED_LEARNING_LANGUAGE_CODES } from '@/features/onboarding/constants';
import { LanguagePicker } from '@/features/onboarding/components/LanguagePicker';
import { authStore } from '@/features/auth/store/auth-store';
import { Button } from '@/shared/components/Button';

export default function SetupScreen() {
  const { t } = useTranslation();
  const router = useRouter();
  const [step, setStep] = useState(0);
  const [languages, setLanguages] = useState<Language[]>([]);
  const [nativeLanguage, setNativeLanguage] = useState<string | null>(null);
  const [learningLanguage, setLearningLanguage] = useState<string | null>(null);
  const [isSubmitting, setIsSubmitting] = useState(false);

  const STEPS = [
    { title: t('setup.nativeLanguage'), key: 'native' as const },
    { title: t('setup.learningLanguage'), key: 'learning' as const },
  ];

  useEffect(() => {
    onboardingApi.getLanguages().then(setLanguages);
  }, []);

  const currentStep = STEPS[step];
  const isLastStep = step === STEPS.length - 1;

  const selected = currentStep.key === 'native' ? nativeLanguage : learningLanguage;
  const setSelected = currentStep.key === 'native' ? setNativeLanguage : setLearningLanguage;

  const availableLanguages =
    currentStep.key === 'learning' && nativeLanguage
      ? languages.filter((l) => l.code !== nativeLanguage)
      : languages;

  const handleNext = async () => {
    if (!isLastStep) {
      setStep((s) => s + 1);
      return;
    }

    if (!nativeLanguage || !learningLanguage) return;

    if (!SUPPORTED_LEARNING_LANGUAGE_CODES.includes(learningLanguage as typeof SUPPORTED_LEARNING_LANGUAGE_CODES[number])) {
      const language = languages.find((l) => l.code === learningLanguage);
      router.push({
        pathname: '/(onboarding)/language-coming-soon',
        params: { languageName: language?.name ?? learningLanguage, nativeLanguage },
      });
      return;
    }

    setIsSubmitting(true);
    try {
      await onboardingApi.createProfile({ nativeLanguage, learningLanguage });
      const user = authStore.getState().user;
      if (user) {
        authStore.getState().setUser({ ...user, hasProfile: true });
      }
      router.replace('/(app)/');
    } catch (e) {
      console.error('[setup] createProfile failed:', e);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleBack = () => {
    setStep((s) => s - 1);
  };

  return (
    <SafeAreaView className="flex-1 bg-white">
      <View className="flex-1 px-6 pt-10 pb-6">
        <View className="flex-row gap-2 mb-10">
          {STEPS.map((_, i) => (
            <View
              key={i}
              className={`h-1 flex-1 rounded-full ${i <= step ? 'bg-blue-600' : 'bg-slate-200'}`}
            />
          ))}
        </View>

        <Text className="text-2xl font-bold text-slate-900 mb-1">
          {t('setup.step', { current: step + 1, total: STEPS.length })}
        </Text>
        <Text className="text-lg text-slate-600 mb-6">{currentStep.title}</Text>

        <LanguagePicker
          languages={availableLanguages}
          selected={selected}
          onSelect={setSelected}
        />

        <View className="flex-row gap-3 mt-4">
          <Button
            title={t('setup.back')}
            onPress={handleBack}
            disabled={step === 0}
            className="flex-1"
            variant="secondary"
          />
          <Button
            title={isLastStep ? t('setup.done') : t('setup.next')}
            onPress={handleNext}
            disabled={!selected}
            loading={isSubmitting}
            className="flex-1"
          />
        </View>
      </View>
    </SafeAreaView>
  );
}
