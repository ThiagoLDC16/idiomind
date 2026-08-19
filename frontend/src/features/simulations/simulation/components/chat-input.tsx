import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { Pressable, TextInput, View } from 'react-native';
import { MaterialIcons } from '@expo/vector-icons';
import { useSafeAreaInsets } from 'react-native-safe-area-context';

import { SuggestionChips } from './suggestion-chips';

interface ChatInputProps {
  onSend: (content: string) => void;
  isSending: boolean;
  suggestions?: string[];
}

export function ChatInput({ onSend, isSending, suggestions = [] }: ChatInputProps) {
  const [text, setText] = useState('');
  const { t } = useTranslation();
  const insets = useSafeAreaInsets();

  const handleSend = () => {
    const trimmed = text.trim();
    if (!trimmed || isSending) return;
    onSend(trimmed);
    setText('');
  };

  const handleSuggestionSelect = (suggestion: string) => {
    if (isSending) return;
    onSend(suggestion);
  };

  return (
    <View
      className="bg-md-surface-container-lowest border-t border-md-surface-variant gap-2 px-6 pt-2"
      style={{
        paddingBottom: Math.max(insets.bottom, 8),
        boxShadow: '0 -10px 20px rgba(93, 95, 239, 0.03)',
      }}
    >
      <SuggestionChips
        suggestions={suggestions}
        onSelect={handleSuggestionSelect}
      />

      <View
        className="flex-row items-end gap-2 bg-md-surface-container-low rounded-2xl p-2 border-2 border-md-surface-variant"
        style={{ borderCurve: 'continuous' }}
      >
        {/* Futura implementação: gravação de áudio
        <Pressable className="p-3 rounded-full active:bg-md-secondary-fixed/30">
          <MaterialIcons name="mic" size={24} color="#00677f" />
        </Pressable>
        */}

        <TextInput
          className="flex-1 text-base text-md-on-surface py-3"
          placeholder={t('simulation.inputPlaceholder')}
          placeholderTextColor="#767586"
          value={text}
          onChangeText={setText}
          multiline
          maxLength={500}
          style={{ maxHeight: 128 }}
          editable={!isSending}
          onSubmitEditing={handleSend}
          blurOnSubmit={false}
        />

        <Pressable
          onPress={handleSend}
          disabled={!text.trim() || isSending}
          className="p-3 bg-md-primary rounded-xl active:bg-md-primary-container"
          style={{
            borderCurve: 'continuous',
            boxShadow: '0 2px 6px rgba(67, 67, 213, 0.15)',
            opacity: !text.trim() || isSending ? 0.5 : 1,
          }}
        >
          <MaterialIcons name="send" size={20} color="#ffffff" />
        </Pressable>
      </View>
    </View>
  );
}
