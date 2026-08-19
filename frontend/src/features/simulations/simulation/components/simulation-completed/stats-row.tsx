import { MaterialIcons } from '@expo/vector-icons';
import { useTranslation } from 'react-i18next';
import { Text, View } from 'react-native';

interface StatCardProps {
  icon: keyof typeof MaterialIcons.glyphMap;
  label: string;
  iconColor: string;
  bgColor: string;
}

function StatCard({ icon, label, iconColor, bgColor }: StatCardProps) {
  return (
    <View
      className="flex-1 rounded-2xl p-4 items-center justify-center gap-2"
      style={{ backgroundColor: bgColor, borderCurve: 'continuous' }}
    >
      <MaterialIcons name={icon} size={28} color={iconColor} />
      <Text className="text-sm font-semibold text-center" style={{ color: iconColor }}>
        {label}
      </Text>
    </View>
  );
}

interface StatsRowProps {
  correctCount: number;
  withFeedbackCount: number;
}

export function StatsRow({ correctCount, withFeedbackCount }: StatsRowProps) {
  const { t } = useTranslation();

  return (
    <View className="flex-row gap-3">
      <StatCard
        icon="check-circle"
        label={t('simulation.completed.correct', { count: correctCount })}
        iconColor="#16a34a"
        bgColor="rgba(22, 163, 74, 0.12)"
      />
      <StatCard
        icon="rate-review"
        label={t('simulation.completed.withFeedback', { count: withFeedbackCount })}
        iconColor="#b75600"
        bgColor="rgba(183, 86, 0, 0.10)"
      />
    </View>
  );
}
