import { useMemo, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { FlatList, Text, TextInput, TouchableOpacity, View } from 'react-native';
import { Image } from 'expo-image';

import type { Language } from '../api/onboarding-api/types';

interface LanguagePickerProps {
  languages: Language[];
  selected: string | null;
  onSelect: (code: string) => void;
}

function getCountryCode(emoji: string) {
  if (!emoji) return 'us';
  const chars = Array.from(emoji);
  if (chars.length < 2) return 'us';
  const c1 = chars[0].codePointAt(0);
  const c2 = chars[1].codePointAt(0);
  if (c1 && c2 && c1 >= 0x1f1e6 && c2 >= 0x1f1e6) {
    return String.fromCharCode(c1 - 127397, c2 - 127397).toLowerCase();
  }
  return 'us';
}

export function LanguagePicker({ languages, selected, onSelect }: LanguagePickerProps) {
  const { t } = useTranslation();
  const [query, setQuery] = useState('');

  const filtered = useMemo(
    () =>
      languages.filter((l) => l.name.toLowerCase().includes(query.toLowerCase())),
    [languages, query],
  );

  return (
    <View className="flex-1">
      <View className="flex-row items-center bg-slate-100 rounded-xl px-3 mb-3 h-11">
        <Text className="text-slate-400 mr-2 text-base">🔍</Text>
        <TextInput
          value={query}
          onChangeText={setQuery}
          placeholder={t('setup.searchLanguage')}
          placeholderTextColor="#94a3b8"
          className="flex-1 text-slate-900 text-base"
        />
      </View>

      <FlatList
        data={filtered}
        keyExtractor={(item) => item.code}
        renderItem={({ item }) => {
          const isSelected = item.code === selected;
          return (
            <TouchableOpacity
              onPress={() => onSelect(item.code)}
              activeOpacity={0.7}
              className={`flex-row items-center px-4 py-3 rounded-xl mb-2 ${
                isSelected ? 'bg-blue-50 border border-blue-300' : 'bg-white border border-slate-100'
              }`}
            >
              <Image
                source={{ uri: `https://flagcdn.com/w40/${getCountryCode(item.flagEmoji)}.png` }}
                style={{ width: 28, height: 20, marginRight: 12, borderRadius: 2 }}
                contentFit="cover"
              />
              <Text
                className={`text-base ${isSelected ? 'text-blue-700 font-semibold' : 'text-slate-800'}`}
              >
                {item.name}
              </Text>
            </TouchableOpacity>
          );
        }}
        showsVerticalScrollIndicator={false}
      />
    </View>
  );
}
