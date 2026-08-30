import * as Localization from 'expo-localization';
import { useCallback, useEffect, useState } from 'react';

import { simulationsApi } from '@/features/simulations/api/simulations-api';
import type { Variant } from '@/features/simulations/api/simulations-api/types';

interface UseVariantsState {
  name: string;
  variants: Variant[];
  isLoading: boolean;
  error: string | null;
  selectedIndex: number;
  isStarting: boolean;
}

export function useVariants(situationId: string) {
  const [state, setState] = useState<UseVariantsState>({
    name: '',
    variants: [],
    isLoading: true,
    error: null,
    selectedIndex: 0,
    isStarting: false,
  });

  const languageCode = Localization.getLocales()[0]?.languageTag ?? 'en-US';

  useEffect(() => {
    let cancelled = false;

    const fetchVariants = async () => {
      setState((prev) => ({
        ...prev,
        name: '',
        variants: [],
        selectedIndex: 0,
        isLoading: true,
        error: null,
      }));
      try {
        const situation = await simulationsApi.getSituation(situationId, languageCode);
        if (!cancelled) {
          setState((prev) => ({
            ...prev,
            name: situation.name,
            variants: situation.variants,
            isLoading: false,
            error: null,
          }));
        }
      } catch {
        if (!cancelled) {
          setState((prev) => ({ ...prev, variants: [], isLoading: false, error: 'internalError' }));
        }
      }
    };

    fetchVariants();
    return () => {
      cancelled = true;
    };
  }, [situationId, languageCode]);

  const setSelectedIndex = useCallback((index: number) => {
    setState((prev) => ({ ...prev, selectedIndex: index }));
  }, []);

  const startSimulation = useCallback(async () => {
    const variant = state.variants[state.selectedIndex];
    if (!variant) return null;

    setState((prev) => ({ ...prev, isStarting: true, error: null }));
    try {
      const simulationId = await simulationsApi.startSimulation(variant.id);
      setState((prev) => ({ ...prev, isStarting: false }));
      return simulationId;
    } catch {
      setState((prev) => ({ ...prev, isStarting: false, error: 'internalError' }));
      return null;
    }
  }, [state.variants, state.selectedIndex]);

  const selectedVariant = state.variants[state.selectedIndex] ?? null;

  return {
    ...state,
    selectedVariant,
    setSelectedIndex,
    startSimulation,
  };
}
