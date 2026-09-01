import { useEffect } from 'react';
import { View } from 'react-native';
import Animated, {
  useSharedValue,
  useAnimatedStyle,
  withRepeat,
  withTiming,
  withDelay,
  withSequence,
} from 'react-native-reanimated';

function Dot({ delay }: { delay: number }) {
  const opacity = useSharedValue(0.3);

  useEffect(() => {
    opacity.value = withRepeat(
      withDelay(
        delay,
        withSequence(withTiming(1, { duration: 400 }), withTiming(0.3, { duration: 400 })),
      ),
      -1,
    );
  }, [delay, opacity]);

  const animatedStyle = useAnimatedStyle(() => ({
    opacity: opacity.value,
  }));

  return (
    <Animated.View
      style={[
        { width: 6, height: 6, borderRadius: 999, backgroundColor: '#4343d5' },
        animatedStyle,
      ]}
    />
  );
}

export function TypingIndicator() {
  return (
    <View className="self-end max-w-[85%] opacity-50">
      <View
        className="bg-md-primary/20 p-3 flex-row gap-1 items-center"
        style={{
          borderTopLeftRadius: 20,
          borderTopRightRadius: 20,
          borderBottomLeftRadius: 20,
          borderBottomRightRadius: 4,
        }}
      >
        <Dot delay={0} />
        <Dot delay={200} />
        <Dot delay={400} />
      </View>
    </View>
  );
}
