import { useMemo } from 'react';

import {
  MessageFeedbackClassification,
  MessageSender,
  type SimulationMessage,
} from '@/features/simulations/api/simulations-api/types';

export interface FeedbackCategory {
  classification: MessageFeedbackClassification;
  count: number;
}

export interface SimulationStats {
  /** User messages without errors (or with classification = Correct) */
  correctCount: number;
  /** User messages that have a non-correct feedback */
  withFeedbackCount: number;
  /** Feedback breakdown by category, sorted by count desc */
  categories: FeedbackCategory[];
  /** Ordered list of user messages that have corrections */
  feedbackMessages: SimulationMessage[];
}

const CATEGORY_LABEL_KEYS: Record<MessageFeedbackClassification, string> = {
  [MessageFeedbackClassification.Correct]: 'simulation.completed.grammar', // won't be shown
  [MessageFeedbackClassification.Grammar]: 'simulation.completed.grammar',
  [MessageFeedbackClassification.Vocabulary]: 'simulation.completed.vocabulary',
  [MessageFeedbackClassification.Context]: 'simulation.completed.context',
  [MessageFeedbackClassification.Spelling]: 'simulation.completed.spelling',
};

export { CATEGORY_LABEL_KEYS };

export function useSimulationStats(messages: SimulationMessage[]): SimulationStats {
  return useMemo(() => {
    const userMessages = messages.filter((m) => m.sender === MessageSender.USER);

    const feedbackMessages = userMessages.filter(
      (m) =>
        m.feedback !== null &&
        m.feedback.classification !== MessageFeedbackClassification.Correct,
    );

    const correctCount = userMessages.length - feedbackMessages.length;
    const withFeedbackCount = feedbackMessages.length;

    const countByCategory = feedbackMessages.reduce<
      Partial<Record<MessageFeedbackClassification, number>>
    >((acc, m) => {
      const cls = m.feedback!.classification;
      acc[cls] = (acc[cls] ?? 0) + 1;
      return acc;
    }, {});

    const categories: FeedbackCategory[] = (
      Object.entries(countByCategory) as [string, number][]
    )
      .map(([cls, count]) => ({
        classification: Number(cls) as MessageFeedbackClassification,
        count,
      }))
      .sort((a, b) => b.count - a.count);

    return { correctCount, withFeedbackCount, categories, feedbackMessages };
  }, [messages]);
}
