import * as Localization from 'expo-localization';
import { useCallback, useEffect, useState } from 'react';

import { simulationsApi } from '@/features/simulations/api/simulations-api';
import {
  MessageSender,
  type SimulationMessage,
} from '@/features/simulations/api/simulations-api/types';

interface UseSimulationState {
  name: string;
  learningLanguage: string;
  messages: SimulationMessage[];
  isLoading: boolean;
  isSending: boolean;
  isFinishing: boolean;
  translatingMessageIds: Set<string>;
  error: string | null;
}

let optimisticIdCounter = 0;

export function useSimulation(simulationId: string) {
  const [state, setState] = useState<UseSimulationState>({
    name: '',
    learningLanguage: '',
    messages: [],
    isLoading: true,
    isSending: false,
    isFinishing: false,
    translatingMessageIds: new Set(),
    error: null,
  });

  const languageCode = Localization.getLocales()[0]?.languageTag ?? 'en-US';

  useEffect(() => {
    let cancelled = false;

    const fetchMessages = async () => {
      setState((prev) => ({
        ...prev,
        name: '',
        learningLanguage: '',
        messages: [],
        isLoading: true,
        error: null,
      }));
      try {
        const simulation = await simulationsApi.getSimulationMessages(simulationId, languageCode);
        if (!cancelled) {
          setState({
            name: simulation.name,
            learningLanguage: simulation.learningLanguage,
            messages: simulation.messages,
            isLoading: false,
            isSending: false,
            isFinishing: false,
            translatingMessageIds: new Set(),
            error: null,
          });
        }
      } catch {
        if (!cancelled) {
          setState({
            name: '',
            learningLanguage: '',
            messages: [],
            isLoading: false,
            isSending: false,
            isFinishing: false,
            translatingMessageIds: new Set(),
            error: 'internalError',
          });
        }
      }
    };

    fetchMessages();
    return () => {
      cancelled = true;
    };
  }, [simulationId, languageCode]);

  const sendMessage = useCallback(
    async (content: string) => {
      const optimisticMessage: SimulationMessage = {
        id: `optimistic-${++optimisticIdCounter}`,
        sender: MessageSender.USER,
        content,
        translatedContent: null,
        feedback: null,
      };

      setState((prev) => ({
        ...prev,
        messages: [...prev.messages, optimisticMessage],
        isSending: true,
        error: null,
      }));

      try {
        const newMessages = await simulationsApi.sendMessage(simulationId, content);
        setState((prev) => ({
          ...prev,
          // Replace the optimistic message with the real ones from the API
          messages: [...prev.messages.filter((m) => m.id !== optimisticMessage.id), ...newMessages],
          isSending: false,
        }));
      } catch {
        setState((prev) => ({
          ...prev,
          // Remove the optimistic message on error
          messages: prev.messages.filter((m) => m.id !== optimisticMessage.id),
          isSending: false,
          error: 'internalError',
        }));
      }
    },
    [simulationId],
  );

  const translateMessage = useCallback(
    async (messageId: string) => {
      setState((prev) => ({
        ...prev,
        translatingMessageIds: new Set([...prev.translatingMessageIds, messageId]),
      }));
      try {
        const { translatedContent } = await simulationsApi.translateMessage(
          simulationId,
          messageId,
        );
        setState((prev) => ({
          ...prev,
          messages: prev.messages.map((m) =>
            m.id === messageId ? { ...m, translatedContent } : m,
          ),
          translatingMessageIds: new Set(
            [...prev.translatingMessageIds].filter((id) => id !== messageId),
          ),
        }));
      } catch {
        setState((prev) => ({
          ...prev,
          translatingMessageIds: new Set(
            [...prev.translatingMessageIds].filter((id) => id !== messageId),
          ),
        }));
      }
    },
    [simulationId],
  );

  const finishSimulation = useCallback(async () => {
    setState((prev) => ({ ...prev, isFinishing: true, error: null }));
    try {
      await simulationsApi.finishSimulation(simulationId);
    } catch {
      setState((prev) => ({ ...prev, isFinishing: false, error: 'internalError' }));
      throw new Error('internalError');
    }
  }, [simulationId]);

  return { ...state, sendMessage, translateMessage, finishSimulation };
}
