import { useEffect, useRef } from 'react';
import { AppState } from 'react-native';

import { analytics } from './analytics-client';

export function useAnalyticsLifecycle(userId: string | null, isAuthReady: boolean) {
  const previousUserId = useRef<string | null>(null);

  useEffect(() => {
    analytics.initialize();

    const subscription = AppState.addEventListener('change', (nextState) => {
      if (nextState === 'background') analytics.flush();
    });

    return () => subscription.remove();
  }, []);

  useEffect(() => {
    if (!isAuthReady) return;

    if (userId) {
      analytics.identify(userId);
    } else if (previousUserId.current) {
      analytics.reset();
    }

    previousUserId.current = userId;
  }, [isAuthReady, userId]);
}
