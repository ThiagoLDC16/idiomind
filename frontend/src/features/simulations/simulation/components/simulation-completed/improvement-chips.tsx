import { useTranslation } from 'react-i18next';
import { ScrollView, Text, View } from 'react-native';

import { MessageFeedbackClassification } from '@/features/simulations/api/simulations-api/types';
import { CATEGORY_LABEL_KEYS, type FeedbackCategory } from '../../hooks/use-simulation-stats';

const CATEGORY_COLORS: Record<MessageFeedbackClassification, { bg: string; text: string }> = {
  [MessageFeedbackClassification.Correct]: { bg: 'rgba(22,163,74,0.12)', text: '#16a34a' },
  [MessageFeedbackClassification.Grammar]: { bg: 'rgba(67,67,213,0.12)', text: '#4343d5' },
  [MessageFeedbackClassification.Vocabulary]: { bg: 'rgba(183,86,0,0.12)', text: '#b75600' },
  [MessageFeedbackClassification.Context]: { bg: 'rgba(96,165,250,0.15)', text: '#2563eb' },
  [MessageFeedbackClassification.Spelling]: { bg: 'rgba(168,85,247,0.12)', text: '#7c3aed' },
};

interface ImprovementChipsProps {
  categories: FeedbackCategory[];
}

export function ImprovementChips({ categories }: ImprovementChipsProps) {
  const { t } = useTranslation();

  if (categories.length === 0) return null;

  return (
    <View className="gap-3">
      <Text className="text-sm font-semibold text-md-on-surface-variant tracking-wide uppercase">
        {t('simulation.completed.improvementTitle')}
      </Text>
      <ScrollView horizontal showsHorizontalScrollIndicator={false} className="-mx-1">
        <View className="flex-row gap-2 px-1">
          {categories.map(({ classification, count }) => {
            const colors = CATEGORY_COLORS[classification];
            return (
              <View
                key={classification}
                className="flex-row items-center gap-1.5 px-3 py-1.5 rounded-full"
                style={{ backgroundColor: colors.bg, borderCurve: 'continuous' }}
              >
                <Text className="text-sm font-medium" style={{ color: colors.text }}>
                  {t(CATEGORY_LABEL_KEYS[classification])}
                </Text>
                <View
                  className="w-5 h-5 rounded-full items-center justify-center"
                  style={{ backgroundColor: colors.text }}
                >
                  <Text className="text-xs font-bold text-white">{count}</Text>
                </View>
              </View>
            );
          })}
        </View>
      </ScrollView>
    </View>
  );
}
