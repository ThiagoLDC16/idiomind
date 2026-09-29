import { MaterialIcons } from '@expo/vector-icons';
import { useTranslation } from 'react-i18next';
import { Text, View } from 'react-native';

export function SimulationFeedbackSuccess() {
  const { t } = useTranslation();

  return (
    <View className="items-center gap-3 rounded-2xl bg-md-surface-container-low p-6">
      <MaterialIcons name="check-circle" size={32} color="#16a34a" />
      <Text className="text-lg font-semibold text-md-on-surface text-center">
        {t('simulation.completed.feedback.successTitle')}
      </Text>
      <Text className="text-sm text-md-on-surface-variant text-center">
        {t('simulation.completed.feedback.successSubmitted')}
      </Text>
    </View>
  );
}
