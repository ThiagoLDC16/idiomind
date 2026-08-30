import { Stack, useLocalSearchParams, useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { ActivityIndicator, ScrollView, Text, View } from 'react-native';

import type { Situation } from '@/features/simulations/api/simulations-api/types';

import { SituationList } from '../components/situation-list';
import { useSituations } from '../hooks/use-situations';

export default function SituationsScreen() {
  const { categoryId } = useLocalSearchParams<{ categoryId: string }>();
  const { t } = useTranslation();
  const router = useRouter();
  const { name, situations, isLoading, error } = useSituations(categoryId);

  const handleSituationPress = (situation: Situation) => {
    router.push({
      pathname: '/(app)/variants/[situationId]',
      params: { situationId: situation.id },
    });
  };

  if (isLoading) {
    return (
      <View className="flex-1 justify-center items-center bg-md-surface">
        <Stack.Screen options={{ title: name }} />
        <ActivityIndicator size="large" color="#4343d5" />
      </View>
    );
  }

  if (error) {
    return (
      <View className="flex-1 justify-center items-center bg-md-surface p-6">
        <Stack.Screen options={{ title: name }} />
        <Text selectable className="text-md-error text-base text-center">
          {t(error)}
        </Text>
      </View>
    );
  }

  return (
    <ScrollView
      contentInsetAdjustmentBehavior="automatic"
      className="flex-1 bg-md-surface"
      contentContainerClassName="p-6 gap-4"
    >
      <Stack.Screen
        options={{
          title: name,
          headerBackTitle: '',
        }}
      />
      <SituationList situations={situations} onSituationPress={handleSituationPress} />
    </ScrollView>
  );
}
