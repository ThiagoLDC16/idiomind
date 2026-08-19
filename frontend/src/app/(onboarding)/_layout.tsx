import { Redirect, Stack } from 'expo-router';

import { authStore } from '@/features/auth/store/auth-store';

export default function OnboardingLayout() {
  const user = authStore((s) => s.user);

  if (!user) return <Redirect href="/(auth)/login" />;
  if (user.hasProfile) return <Redirect href="/(app)/" />;

  return (
    <Stack screenOptions={{ headerShown: false, contentStyle: { backgroundColor: 'white' } }}>
      <Stack.Screen name="setup" />
      <Stack.Screen name="language-coming-soon" />
    </Stack>
  );
}
