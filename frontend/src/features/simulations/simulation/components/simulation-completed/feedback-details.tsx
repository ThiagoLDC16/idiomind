import { MaterialIcons } from '@expo/vector-icons';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Pressable, Text, View } from 'react-native';

import { MessageFeedbackClassification, type SimulationMessage } from '@/features/simulations/api/simulations-api/types';

type DiffSegment = { type: 'keep' | 'remove' | 'add'; text: string };

function diffWords(original: string, corrected: string): DiffSegment[] {
  const a = original.trim().split(/\s+/);
  const b = corrected.trim().split(/\s+/);
  const m = a.length, n = b.length;
  const dp = Array.from({ length: m + 1 }, () => new Array(n + 1).fill(0));
  for (let i = 1; i <= m; i++)
    for (let j = 1; j <= n; j++)
      dp[i][j] = a[i - 1] === b[j - 1] ? dp[i - 1][j - 1] + 1 : Math.max(dp[i - 1][j], dp[i][j - 1]);

  const result: DiffSegment[] = [];
  let i = m, j = n;
  while (i > 0 || j > 0) {
    if (i > 0 && j > 0 && a[i - 1] === b[j - 1]) {
      result.unshift({ type: 'keep', text: a[i - 1] });
      i--; j--;
    } else if (j > 0 && (i === 0 || dp[i][j - 1] >= dp[i - 1][j])) {
      result.unshift({ type: 'add', text: b[j - 1] });
      j--;
    } else {
      result.unshift({ type: 'remove', text: a[i - 1] });
      i--;
    }
  }
  return result;
}

const CORRECTION_COLOR: Record<number, string> = {
  [MessageFeedbackClassification.Grammar]: '#4343d5',
  [MessageFeedbackClassification.Vocabulary]: '#b75600',
  [MessageFeedbackClassification.Context]: '#2563eb',
  [MessageFeedbackClassification.Spelling]: '#7c3aed',
};

interface FeedbackItemProps {
  message: SimulationMessage;
}

function FeedbackItem({ message }: FeedbackItemProps) {
  const { t } = useTranslation();
  const { feedback, content } = message;
  if (!feedback) return null;

  const correctionColor = CORRECTION_COLOR[feedback.classification] ?? '#b75600';
  const segments = feedback.correction ? diffWords(content, feedback.correction) : null;

  return (
    <View
      className="bg-md-surface-container-low rounded-2xl p-4 gap-2"
      style={{ borderCurve: 'continuous' }}
    >
      {/* Original */}
      <Text className="text-sm text-md-on-surface-variant italic">"{content}"</Text>

      {/* Divider */}
      <View className="h-px bg-md-surface-variant" />

      {/* Correction label */}
      <Text className="text-xs font-semibold text-md-on-surface-variant uppercase tracking-wide">
        {t('simulation.completed.correction')}
      </Text>

      {/* Diff or explanation */}
      {segments ? (
        <Text className="text-base text-md-on-surface leading-relaxed">
          {segments.map((seg, idx) => (
            <Text key={idx}>
              {idx > 0 && ' '}
              {seg.type === 'remove' ? (
                <Text className="line-through text-md-outline">{seg.text}</Text>
              ) : seg.type === 'add' ? (
                <Text style={{ color: correctionColor }} className="font-semibold">{seg.text}</Text>
              ) : (
                seg.text
              )}
            </Text>
          ))}
        </Text>
      ) : (
        <Text className="text-base text-md-on-surface">{feedback.explanation}</Text>
      )}

      {/* Explanation below diff if both exist */}
      {segments && feedback.explanation && (
        <Text className="text-xs text-md-on-surface-variant mt-1">{feedback.explanation}</Text>
      )}
    </View>
  );
}

interface FeedbackDetailsProps {
  messages: SimulationMessage[];
}

export function FeedbackDetails({ messages }: FeedbackDetailsProps) {
  const { t } = useTranslation();
  const [expanded, setExpanded] = useState(false);

  if (messages.length === 0) return null;

  return (
    <View className="gap-3">
      <Pressable
        onPress={() => setExpanded((prev) => !prev)}
        className="flex-row items-center justify-between"
      >
        <Text className="text-sm font-semibold text-md-on-surface-variant tracking-wide uppercase">
          {t('simulation.completed.detailsTitle')}
        </Text>
        <MaterialIcons
          name={expanded ? 'keyboard-arrow-up' : 'keyboard-arrow-down'}
          size={20}
          color="#767586"
        />
      </Pressable>

      {expanded && (
        <View className="gap-3">
          {messages.map((msg) => (
            <FeedbackItem key={msg.id} message={msg} />
          ))}
        </View>
      )}
    </View>
  );
}
