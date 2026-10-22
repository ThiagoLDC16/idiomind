import { Redirect, Stack } from 'expo-router';
import { useEffect } from 'react';

import { authApi } from '@/features/auth/api/auth-api';
import { authStore } from '@/features/auth/store/auth-store';

export default function AuthLayout() {
  const user = authStore((s) => s.user);

  useEffect(() => {
    if (user) return;

    void authApi.getLoggedUser().catch(() => undefined);
  }, [user]);

  if (user) {
    return <Redirect href={user.hasProfile ? '/(app)/' : '/(onboarding)/setup'} />;
  }

  return (
    <Stack screenOptions={{ headerShown: false, contentStyle: { backgroundColor: 'white' } }}>
      <Stack.Screen name="login" />
      <Stack.Screen name="register" />
    </Stack>
  );
}
