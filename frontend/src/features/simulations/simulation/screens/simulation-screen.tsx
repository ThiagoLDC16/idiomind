import { Stack, useLocalSearchParams, useRouter } from 'expo-router';
import { useRef } from 'react';
import { useTranslation } from 'react-i18next';
import {
  ActivityIndicator,
  Alert,
  FlatList,
  KeyboardAvoidingView,
  Platform,
  Pressable,
  Text,
  View,
} from 'react-native';

import type { SimulationMessage } from '@/features/simulations/api/simulations-api/types';

import { ChatInput } from '../components/chat-input';
import { ChatMessageBubble } from '../components/chat-message-bubble';
import { SimulationStartedBadge } from '../components/simulation-started-badge';
import { TypingIndicator } from '../components/typing-indicator';
import { useSimulation } from '../hooks/use-simulation';

function FinishButton({ onPress, disabled }: { onPress: () => void; disabled: boolean }) {
  const { t } = useTranslation();

  return (
    <Pressable
      onPress={onPress}
      disabled={disabled}
      className="bg-md-primary-fixed/50 px-4 py-2 rounded-full active:scale-[0.98]"
      style={{ opacity: disabled ? 0.5 : 1 }}
    >
      <Text className="text-sm font-medium text-md-primary">{t('simulation.finish')}</Text>
    </Pressable>
  );
}

export default function SimulationScreen() {
  const { simulationId } = useLocalSearchParams<{ simulationId: string }>();
  const { t } = useTranslation();
  const router = useRouter();
  const {
    name,
    learningLanguage,
    messages,
    isLoading,
    isSending,
    isFinishing,
    translatingMessageIds,
    error,
    sendMessage,
    translateMessage,
    finishSimulation,
  } = useSimulation(simulationId);
  const flatListRef = useRef<FlatList<SimulationMessage>>(null);

  const confirmFinish = async () => {
    await finishSimulation();
    router.replace({
      pathname: '/simulation/completed/[simulationId]',
      params: { simulationId },
    });
  };

  const handleFinish = () => {
    if (Platform.OS === 'web') {
      const confirmed = window.confirm(
        `${t('simulation.finishConfirm.title')}\n${t('simulation.finishConfirm.message')}`,
      );
      if (confirmed) {
        confirmFinish();
      }
    } else {
      Alert.alert(t('simulation.finishConfirm.title'), t('simulation.finishConfirm.message'), [
        { text: t('simulation.finishConfirm.cancel'), style: 'cancel' },
        {
          text: t('simulation.finishConfirm.confirm'),
          style: 'destructive',
          onPress: confirmFinish,
        },
      ]);
    }
  };

  const handleSend = (content: string) => {
    sendMessage(content);
  };

  const scrollToEnd = () => {
    flatListRef.current?.scrollToEnd({ animated: true });
  };

  if (isLoading) {
    return (
      <View className="flex-1 justify-center items-center bg-md-surface">
        <Stack.Screen
          options={{
            title: name,
            headerTitleAlign: 'center',
            headerRight: () => <FinishButton onPress={handleFinish} disabled={isFinishing} />,
          }}
        />
        <ActivityIndicator size="large" color="#4343d5" />
      </View>
    );
  }

  if (error) {
    return (
      <View className="flex-1 justify-center items-center bg-md-surface p-6">
        <Stack.Screen
          options={{
            title: name,
            headerTitleAlign: 'center',
            headerRight: () => <FinishButton onPress={handleFinish} disabled={isFinishing} />,
          }}
        />
        <Text selectable className="text-md-error text-base text-center">
          {t(error)}
        </Text>
      </View>
    );
  }

  return (
    <KeyboardAvoidingView
      className="flex-1 bg-md-surface"
      behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
      keyboardVerticalOffset={Platform.OS === 'ios' ? 90 : 0}
    >
      <Stack.Screen
        options={{
          title: name,
          headerTitleAlign: 'center',
          headerRight: () => <FinishButton onPress={handleFinish} disabled={isFinishing} />,
        }}
      />

      <FlatList
        ref={flatListRef}
        data={messages}
        keyExtractor={(item) => item.id}
        renderItem={({ item }) => (
          <ChatMessageBubble
            message={item}
            onTranslate={translateMessage}
            isTranslating={translatingMessageIds.has(item.id)}
          />
        )}
        contentContainerClassName="px-6 py-4 gap-6"
        ListHeaderComponent={<SimulationStartedBadge />}
        ListFooterComponent={isSending ? <TypingIndicator /> : null}
        onContentSizeChange={scrollToEnd}
        onLayout={scrollToEnd}
        showsVerticalScrollIndicator={false}
      />

      <ChatInput
        onSend={handleSend}
        isSending={isSending || isFinishing}
        learningLanguage={learningLanguage}
      />
    </KeyboardAvoidingView>
  );
}
