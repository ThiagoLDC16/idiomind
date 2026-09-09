import '../global.css';

import { useFonts } from 'expo-font';
import { Slot } from 'expo-router';
import * as SplashScreen from 'expo-splash-screen';
import { useEffect, useState } from 'react';
import { Platform, Text, View } from 'react-native';

import { authApi } from '@/features/auth/api/auth-api';
import { authStore } from '@/features/auth/store/auth-store';
import { initializeI18n } from '@/i18n';
import { storage } from '@/shared/utils/storage';

if (Platform.OS !== 'web') {
  SplashScreen.preventAutoHideAsync();
}

export default function RootLayout() {
  const isReady = authStore((state) => state.isReady);
  const nativeLanguage = authStore((state) => state.user?.nativeLanguage);
  const [fontsLoaded, fontError] = useFonts({
    PlusJakartaSans_700Bold: require('../../assets/fonts/PlusJakartaSans_700Bold.ttf'),
    MaterialIcons: require('../../assets/fonts/MaterialIcons.ttf'),
    Ionicons: require('../../assets/fonts/Ionicons.ttf'),
  });
  const [isI18nReady, setI18nReady] = useState(false);
  const fontsReady = fontsLoaded || fontError != null;

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
    if (Platform.OS !== 'web' && isReady && fontsReady && isI18nReady) {
      void SplashScreen.hideAsync();
    }
  }, [isReady, fontsReady, isI18nReady]);

  if (!isReady || !fontsReady || !isI18nReady) {
    return (
      <View className="flex-1 items-center justify-center bg-white">
        <Text className="text-2xl font-bold text-blue-600">Idiomind</Text>
      </View>
    );
  }

  return <Slot />;
}
