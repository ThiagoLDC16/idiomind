import { MaterialIcons } from '@expo/vector-icons';
import { useTranslation } from 'react-i18next';
import { Text, View } from 'react-native';

interface CompletedHeaderProps {
  isPerfect?: boolean;
}

export function CompletedHeader({ isPerfect = false }: CompletedHeaderProps) {
  const { t } = useTranslation();

  return (
    <View className="items-center gap-4 mt-4">
      <View
        className="w-24 h-24 rounded-full items-center justify-center"
        style={{
          backgroundColor: isPerfect ? '#4343d5' : '#22c55e',
          boxShadow: isPerfect
            ? '0 8px 24px rgba(67, 67, 213, 0.30)'
            : '0 8px 24px rgba(34, 197, 94, 0.25)',
        }}
      >
        <MaterialIcons
          name={isPerfect ? 'stars' : 'emoji-events'}
          size={48}
          color="#ffffff"
        />
      </View>

      <View className="items-center">
        <Text className="font-jakarta-bold text-[32px] leading-[38px] font-bold text-md-on-background text-center">
          {isPerfect ? t('simulation.completed.perfectTitle') : t('simulation.completed.title')}
        </Text>
        <Text className="text-lg leading-[29px] text-md-on-surface-variant text-center mt-2">
          {isPerfect
            ? t('simulation.completed.perfectSubtitle')
            : t('simulation.completed.subtitle')}
        </Text>
      </View>
    </View>
  );
}
