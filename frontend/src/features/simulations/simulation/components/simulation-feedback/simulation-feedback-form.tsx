import { useTranslation } from 'react-i18next';
import { Pressable, Text, TextInput, View } from 'react-native';

import {
  SimulationFeedbackPreparedness,
  type SubmitSimulationFeedbackRequest,
} from '@/features/simulations/api/simulations-api/types';
import { Button } from '@/shared/components/Button';

export type SimulationFeedbackFormValue = Omit<
  SubmitSimulationFeedbackRequest,
  'improvement'
> & { improvement: string };

interface SimulationFeedbackFormProps {
  value: SimulationFeedbackFormValue;
  isSubmitting: boolean;
  error: string | null;
  onChange: (value: SimulationFeedbackFormValue) => void;
  onSubmit: () => void;
  onSkip: () => void;
}

const preparednessOptions = [
  { value: SimulationFeedbackPreparedness.No, labelKey: 'no' },
  { value: SimulationFeedbackPreparedness.ALittle, labelKey: 'aLittle' },
  { value: SimulationFeedbackPreparedness.Yes, labelKey: 'yes' },
  { value: SimulationFeedbackPreparedness.VeryMuch, labelKey: 'veryMuch' },
] as const;

export function SimulationFeedbackForm({
  value,
  isSubmitting,
  error,
  onChange,
  onSubmit,
  onSkip,
}: SimulationFeedbackFormProps) {
  const { t } = useTranslation();
  const feedbackKey = 'simulation.completed.feedback';
  const canSubmit = value.experienceRating !== null && value.preparedness !== null;

  return (
    <View className="gap-5 rounded-2xl bg-md-surface-container-low p-5">
      <Text className="text-xl font-bold text-md-on-surface">{t(`${feedbackKey}.title`)}</Text>

      <View className="gap-3">
        <Text className="text-sm font-medium text-md-on-surface">
          {t(`${feedbackKey}.ratingLabel`)}
        </Text>
        <View className="flex-row gap-2">
          {[1, 2, 3, 4, 5].map((rating) => {
            const selected = value.experienceRating === rating;
            return (
              <Pressable
                key={rating}
                accessibilityRole="radio"
                accessibilityState={{ selected }}
                accessibilityLabel={`${t(`${feedbackKey}.ratingLabel`)}: ${rating}`}
                onPress={() => onChange({ ...value, experienceRating: rating })}
                className={`h-12 flex-1 items-center justify-center rounded-xl border ${selected ? 'border-md-primary bg-md-primary' : 'border-md-outline-variant bg-md-surface'}`}
              >
                <Text
                  className={`text-base font-semibold ${selected ? 'text-md-on-primary' : 'text-md-on-surface'}`}
                >
                  {rating}
                </Text>
              </Pressable>
            );
          })}
        </View>
      </View>

      <View className="gap-3">
        <Text className="text-sm font-medium text-md-on-surface">
          {t(`${feedbackKey}.preparednessLabel`)}
        </Text>
        <View className="gap-2">
          {preparednessOptions.map((option) => {
            const selected = value.preparedness === option.value;
            return (
              <Pressable
                key={option.value}
                accessibilityRole="radio"
                accessibilityState={{ selected }}
                onPress={() => onChange({ ...value, preparedness: option.value })}
                className={`rounded-xl border px-4 py-3 ${selected ? 'border-md-primary bg-md-primary' : 'border-md-outline-variant bg-md-surface'}`}
              >
                <Text
                  className={`text-sm font-medium ${selected ? 'text-md-on-primary' : 'text-md-on-surface'}`}
                >
                  {t(`${feedbackKey}.${option.labelKey}`)}
                </Text>
              </Pressable>
            );
          })}
        </View>
      </View>

      <View className="gap-2">
        <Text className="text-sm font-medium text-md-on-surface">
          {t(`${feedbackKey}.improvementLabel`)}
        </Text>
        <TextInput
          accessibilityLabel={t(`${feedbackKey}.improvementLabel`)}
          multiline
          maxLength={2000}
          numberOfLines={4}
          placeholder={t(`${feedbackKey}.improvementPlaceholder`)}
          placeholderTextColor="#767586"
          value={value.improvement}
          onChangeText={(improvement) => onChange({ ...value, improvement })}
          className="min-h-24 rounded-xl border border-md-outline-variant bg-md-surface px-4 py-3 text-base text-md-on-surface"
          textAlignVertical="top"
        />
      </View>

      {error ? <Text className="text-sm text-md-error">{t(error)}</Text> : null}

      <View className="gap-2">
        <Button
          title={t(`${feedbackKey}.submit`)}
          loading={isSubmitting}
          disabled={!canSubmit}
          onPress={onSubmit}
        />
        <Pressable
          accessibilityRole="button"
          disabled={isSubmitting}
          onPress={onSkip}
          className="items-center rounded-xl py-3 active:opacity-70"
        >
          <Text className="text-sm font-medium text-md-primary">{t(`${feedbackKey}.skip`)}</Text>
        </Pressable>
      </View>
    </View>
  );
}
