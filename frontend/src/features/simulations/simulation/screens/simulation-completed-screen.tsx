import { Stack, useLocalSearchParams, useRouter } from 'expo-router';
import { useEffect, useState } from 'react';
import { ActivityIndicator, ScrollView, View } from 'react-native';

import { simulationsApi } from '@/features/simulations/api/simulations-api';
import type { SimulationMessage } from '@/features/simulations/api/simulations-api/types';
import { CompletedActions } from '../components/simulation-completed/completed-actions';
import { CompletedHeader } from '../components/simulation-completed/completed-header';
import { FeedbackDetails } from '../components/simulation-completed/feedback-details';
import { ImprovementChips } from '../components/simulation-completed/improvement-chips';
import { StatsRow } from '../components/simulation-completed/stats-row';
import { useSimulationStats } from '../hooks/use-simulation-stats';

function SimulationCompletedContent({ messages }: { messages: SimulationMessage[] }) {
  const router = useRouter();
  const stats = useSimulationStats(messages);
  const isPerfect = stats.withFeedbackCount === 0;

  const handleNextSimulation = () => {
    router.dismissAll();
  };

  const handleReturnToDashboard = () => {
    router.dismissAll();
  };

  return (
    <ScrollView
      className="flex-1 bg-md-background"
      contentContainerClassName="px-6 pt-16 pb-8 gap-8 flex-grow"
    >
      <CompletedHeader isPerfect={isPerfect} />

      <View className="gap-6 flex-1">
        <StatsRow
          correctCount={stats.correctCount}
          withFeedbackCount={stats.withFeedbackCount}
        />

        <ImprovementChips categories={stats.categories} />

        <FeedbackDetails messages={stats.feedbackMessages} />
      </View>

      <CompletedActions
        onNextSimulation={handleNextSimulation}
        onReturnToDashboard={handleReturnToDashboard}
      />
    </ScrollView>
  );
}

export default function SimulationCompletedScreen() {
  const { simulationId } = useLocalSearchParams<{ simulationId: string }>();
  const [messages, setMessages] = useState<SimulationMessage[] | null>(null);

  useEffect(() => {
    let cancelled = false;
    simulationsApi.listMessages(simulationId).then((data) => {
      if (!cancelled) setMessages(data);
    });
    return () => {
      cancelled = true;
    };
  }, [simulationId]);

  return (
    <>
      <Stack.Screen options={{ headerShown: false }} />
      {messages === null ? (
        <View className="flex-1 bg-md-background items-center justify-center">
          <ActivityIndicator size="large" color="#4343d5" />
        </View>
      ) : (
        <SimulationCompletedContent messages={messages} />
      )}
    </>
  );
}
