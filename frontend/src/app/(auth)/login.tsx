import { zodResolver } from '@hookform/resolvers/zod';
import { Link, useRouter } from 'expo-router';
import { useMemo, useState } from 'react';
import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import { KeyboardAvoidingView, Platform, ScrollView, Text, View } from 'react-native';
import { z } from 'zod';

import { authApi } from '@/features/auth/api/auth-api';
import { authStore } from '@/features/auth/store/auth-store';
import { initializeI18n } from '@/i18n';
import { Button } from '@/shared/components/Button';
import { ControlledInput } from '@/shared/components/ControlledInput';

type LoginForm = { email: string; password: string };

export default function LoginScreen() {
  const { t } = useTranslation();
  const router = useRouter();
  const [isLoading, setIsLoading] = useState(false);
  const [errorMsg, setErrorMsg] = useState('');

  const loginSchema = useMemo(
    () =>
      z.object({
        email: z.string().email(t('login.validation.email')),
        password: z.string().min(6, t('login.validation.password')),
      }),
    [t],
  );

  const { control, handleSubmit } = useForm<LoginForm>({
    resolver: zodResolver(loginSchema),
    defaultValues: { email: '', password: '' },
  });

  const onSubmit = async (data: LoginForm) => {
    setIsLoading(true);
    setErrorMsg('');
    try {
      const tokens = await authApi.login(data);
      authStore.getState().setTokens(tokens);

      const user = await authApi.getLoggedUser();
      await initializeI18n(user.nativeLanguage);
      authStore.getState().setUser(user);
      router.replace(user.hasProfile ? '/(app)/' : '/(onboarding)/setup');
    } catch (e: any) {
      if (e.response?.data?.errors?.length) {
        setErrorMsg(t(e.response.data.errors[0]));
      } else {
        setErrorMsg(t('internalError'));
      }
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <KeyboardAvoidingView
      behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
      className="flex-1 bg-white"
    >
      <ScrollView contentContainerStyle={{ flexGrow: 1, justifyContent: 'center', padding: 24 }}>
        <View className="items-center mb-10">
          <Text className="text-4xl font-extrabold text-blue-600 mb-2">Idiomind</Text>
          <Text className="text-lg text-slate-500">{t('login.subtitle')}</Text>
        </View>

        {errorMsg ? (
          <Text className="text-red-500 text-center mb-4 font-medium">{errorMsg}</Text>
        ) : null}

        <ControlledInput
          name="email"
          control={control}
          label={t('login.email.label')}
          placeholder={t('login.email.placeholder')}
          keyboardType="email-address"
          autoCapitalize="none"
        />

        <ControlledInput
          name="password"
          control={control}
          label={t('login.password.label')}
          placeholder={t('login.password.placeholder')}
          secureTextEntry
        />

        <Button
          title={t('login.button')}
          onPress={handleSubmit(onSubmit)}
          loading={isLoading}
          className="mt-6"
        />

        <View className="flex-row justify-center mt-6 gap-1">
          <Text className="text-slate-600">{t('login.noAccount')}</Text>
          <Link href="/(auth)/register" asChild>
            <Text className="text-blue-600 font-semibold">{t('login.signUp')}</Text>
          </Link>
        </View>
      </ScrollView>
    </KeyboardAvoidingView>
  );
}
