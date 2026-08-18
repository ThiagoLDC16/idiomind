import { Alert, Platform, Pressable, Text } from 'react-native';
import { useTranslation } from 'react-i18next';
import { useRouter } from 'expo-router';
import { Ionicons } from '@expo/vector-icons';

import { authApi } from '@/features/auth/api/auth-api';
import { authStore } from '@/features/auth/store/auth-store';

export const LogoutButton = () => {
  const { t } = useTranslation();
  const router = useRouter();
  const logout = authStore((state) => state.logout);
  const refreshToken = authStore((state) => state.refreshToken);

  const executeLogout = async () => {
    if (refreshToken) {
      await authApi.logout({ refreshToken }).catch(() => {});
    }
    logout();
    router.replace('/(auth)/login');
  };

  const handlePress = () => {
    if (Platform.OS === 'web') {
      const confirmLogout = window.confirm(
        `${t('logout.dialog.title')}\n\n${t('logout.dialog.message')}`
      );
      if (confirmLogout) {
        executeLogout();
      }
      return;
    }

    Alert.alert(t('logout.dialog.title'), t('logout.dialog.message'), [
      { text: t('logout.dialog.cancel'), style: 'cancel' },
      {
        text: t('logout.dialog.confirm'),
        style: 'destructive',
        onPress: executeLogout,
      },
    ]);
  };

  return (
    <Pressable
      className="flex-row items-center px-4 py-3 gap-3 active:bg-slate-100"
      onPress={handlePress}
    >
      <Ionicons name="log-out-outline" size={22} color="#ef4444" />
      <Text className="text-base font-medium text-red-500">{t('sidebar.logout')}</Text>
    </Pressable>
  );
};
