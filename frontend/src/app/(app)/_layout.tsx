import { Redirect, Stack } from 'expo-router';

import { authStore } from '@/features/auth/store/auth-store';

export const unstable_settings = {
  anchor: '(main)',
};

export default function AppLayout() {
  const user = authStore((s) => s.user);

  if (!user) return <Redirect href="/(auth)/login" />;
  if (!user.hasProfile) return <Redirect href="/(onboarding)/setup" />;

  return (
    <Stack
      screenOptions={{
        contentStyle: { backgroundColor: '#F8FAFC' },
      }}
    >
      <Stack.Screen name="index" options={{ headerShown: false }} />
      <Stack.Screen name="(main)" options={{ headerShown: false }} />
      <Stack.Screen name="situations/[categoryId]" />
      <Stack.Screen name="variants/[situationId]" />
      <Stack.Screen
        name="simulation/[simulationId]"
        options={{
          headerTitleAlign: 'center',
        }}
      />
      <Stack.Screen
        name="simulation/completed/[simulationId]"
        options={{
          headerShown: false,
        }}
      />
    </Stack>
  );
}
