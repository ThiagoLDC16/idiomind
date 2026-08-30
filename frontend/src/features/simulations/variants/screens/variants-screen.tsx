import { Stack, useLocalSearchParams, useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { ActivityIndicator, Pressable, Text, View } from 'react-native';

import { VariantCarousel } from '../components/variant-carousel';
import { useVariants } from '../hooks/use-variants';

function StartSimulationButton({
  onPress,
  isLoading,
}: {
  onPress: () => void;
  isLoading: boolean;
}) {
  const { t } = useTranslation();

  return (
    <View className="absolute bottom-0 left-0 right-0 pb-10 pt-8 px-6 bg-md-surface">
      <Pressable
        onPress={onPress}
        disabled={isLoading}
        className="bg-md-primary rounded-2xl py-4 flex-row items-center justify-center gap-2 active:scale-[0.98]"
        style={{
          borderCurve: 'continuous',
          boxShadow: '0 8px 16px rgba(67, 67, 213, 0.2)',
          opacity: isLoading ? 0.7 : 1,
        }}
      >
        {isLoading ? (
          <ActivityIndicator size="small" color="#ffffff" />
        ) : (
          <>
            <Text className="text-lg font-bold text-md-on-primary">
              {t('variants.startSimulation')}
            </Text>
          </>
        )}
      </Pressable>
    </View>
  );
}

export default function VariantsScreen() {
  const { situationId } = useLocalSearchParams<{ situationId: string }>();
  const { t } = useTranslation();
  const router = useRouter();
  const { name, variants, isLoading, error, isStarting, setSelectedIndex, startSimulation } =
    useVariants(situationId);

  const handleStartSimulation = async () => {
    const simulationId = await startSimulation();
    if (simulationId) {
      router.push({
        pathname: '/(app)/simulation/[simulationId]',
        params: {
          simulationId,
        },
      });
    }
  };

  if (isLoading) {
    return (
      <View className="flex-1 justify-center items-center bg-md-surface">
        <Stack.Screen options={{ title: name, headerBackTitle: '' }} />
        <ActivityIndicator size="large" color="#4343d5" />
      </View>
    );
  }

  if (error) {
    return (
      <View className="flex-1 justify-center items-center bg-md-surface p-6">
        <Stack.Screen options={{ title: name, headerBackTitle: '' }} />
        <Text selectable className="text-md-error text-base text-center">
          {t(error)}
        </Text>
      </View>
    );
  }

  return (
    <View className="flex-1 bg-md-surface">
      <Stack.Screen options={{ title: name, headerBackTitle: '' }} />

      <View className="pt-8 pb-4 px-6">
        <Text className="text-base text-md-on-surface-variant text-center">
          {t('variants.subtitle')}
        </Text>
      </View>

      <View className="flex-1 justify-center pb-28">
        <VariantCarousel variants={variants} onActiveIndexChange={setSelectedIndex} />
      </View>

      <StartSimulationButton onPress={handleStartSimulation} isLoading={isStarting} />
    </View>
  );
}
