import { MaterialIcons } from '@expo/vector-icons';
import { useTranslation } from 'react-i18next';
import { Pressable, Text, View } from 'react-native';

interface CompletedActionsProps {
  onNextSimulation: () => void;
  onViewOtherSituations: () => void;
}

export function CompletedActions({
  onNextSimulation,
  onViewOtherSituations,
}: CompletedActionsProps) {
  const { t } = useTranslation();

  return (
    <View className="gap-2 mt-auto pt-4">
      <Pressable
        onPress={onNextSimulation}
        className="w-full bg-md-primary rounded-xl py-4 flex-row items-center justify-center gap-2 active:scale-[0.98]"
        style={{ borderCurve: 'continuous', boxShadow: '0 4px 16px rgba(67, 67, 213, 0.25)' }}
      >
        <Text className="text-sm font-medium tracking-wider text-md-on-primary">
          {t('simulation.completed.nextSimulation')}
        </Text>
        <MaterialIcons name="arrow-forward" size={20} color="#ffffff" />
      </Pressable>

      <Pressable
        onPress={onViewOtherSituations}
        className="w-full rounded-xl py-4 items-center justify-center active:scale-[0.98]"
        style={{ borderCurve: 'continuous' }}
      >
        <Text className="text-sm font-medium tracking-wider text-md-primary">
          {t('simulation.completed.viewOtherSituations')}
        </Text>
      </Pressable>
    </View>
  );
}
