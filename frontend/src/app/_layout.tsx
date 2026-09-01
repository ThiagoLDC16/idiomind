import '../global.css';

import { PlusJakartaSans_700Bold, useFonts } from '@expo-google-fonts/plus-jakarta-sans';
import { Slot } from 'expo-router';
import * as SplashScreen from 'expo-splash-screen';
import { useEffect, useState } from 'react';

import { authApi } from '@/features/auth/api/auth-api';
import { authStore } from '@/features/auth/store/auth-store';
import { initializeI18n } from '@/i18n';
import { storage } from '@/shared/utils/storage';

SplashScreen.preventAutoHideAsync();

export default function RootLayout() {
  const isReady = authStore((state) => state.isReady);
  const nativeLanguage = authStore((state) => state.user?.nativeLanguage);
  const [fontsLoaded] = useFonts({ PlusJakartaSans_700Bold });
  const [isI18nReady, setI18nReady] = useState(false);

  useEffect(() => {
    if (!isReady) return;

    let isActive = true;

    initializeI18n(nativeLanguage)
      .then(() => {
        if (isActive) setI18nReady(true);
      })
      .catch((error) => {
        console.error('Failed to load translations during initialization:', error);
        if (isActive) setI18nReady(true);
      });

    return () => {
      isActive = false;
    };
  }, [isReady, nativeLanguage]);

  useEffect(() => {
    const restoreSession = async () => {
      try {
        const accessToken = await storage.getItem('accessToken');
        const refreshToken = await storage.getItem('refreshToken');

        if (accessToken && refreshToken) {
          authStore.getState().setTokens({ accessToken, refreshToken });
          try {
            const loggedUser = await authApi.getLoggedUser();
            authStore.getState().setUser(loggedUser);
          } catch (e) {
            console.error('Failed to restore user session during initialization:', e);
            authStore.getState().logout();
          }
        }
      } catch (error) {
        console.error('Failed to restore session via storage', error);
      } finally {
        authStore.getState().setReady(true);
      }
    };

    restoreSession();
  }, []);

  useEffect(() => {
    if (isReady && fontsLoaded && isI18nReady) {
      SplashScreen.hideAsync();
    }
  }, [isReady, fontsLoaded, isI18nReady]);

  if (!isReady || !fontsLoaded || !isI18nReady) {
    return null;
  }

  return <Slot />;
}
