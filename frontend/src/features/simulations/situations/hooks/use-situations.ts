import * as Localization from 'expo-localization';
import { useEffect, useState } from 'react';

import { simulationsApi } from '@/features/simulations/api/simulations-api';
import type { Situation } from '@/features/simulations/api/simulations-api/types';

interface UseSituationsState {
  name: string;
  situations: Situation[];
  isLoading: boolean;
  error: string | null;
}

export function useSituations(categoryId: string) {
  const [state, setState] = useState<UseSituationsState>({
    name: '',
    situations: [],
    isLoading: true,
    error: null,
  });

  const languageCode = Localization.getLocales()[0]?.languageTag ?? 'en-US';

  useEffect(() => {
    let cancelled = false;

    const fetchSituations = async () => {
      setState({ name: '', situations: [], isLoading: true, error: null });
      try {
        const category = await simulationsApi.getCategory(categoryId, languageCode);
        if (!cancelled) {
          setState({
            name: category.name,
            situations: category.situations,
            isLoading: false,
            error: null,
          });
        }
      } catch {
        if (!cancelled) {
          setState({ name: '', situations: [], isLoading: false, error: 'internalError' });
        }
      }
    };

    fetchSituations();
    return () => {
      cancelled = true;
    };
  }, [categoryId, languageCode]);

  return state;
}
