import { useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import { ActivityIndicator, ScrollView, Text, View } from 'react-native';

import type { Category } from '@/features/simulations/api/simulations-api/types';

import { CategoriesHeader } from '../components/categories-header';
import { CategoryGrid } from '../components/category-grid';
import { useCategories } from '../hooks/use-categories';

export default function CategoriesScreen() {
  const { t } = useTranslation();
  const router = useRouter();
  const { categories, isLoading, error } = useCategories();

  const handleCategoryPress = (category: Category) => {
    router.push({
      pathname: '/(app)/situations/[categoryId]',
      params: { categoryId: category.id },
    });
  };

  if (isLoading) {
    return (
      <View className="flex-1 justify-center items-center bg-md-surface">
        <ActivityIndicator size="large" color="#4343d5" />
      </View>
    );
  }

  if (error) {
    return (
      <View className="flex-1 justify-center items-center bg-md-surface p-6">
        <Text selectable className="text-md-error text-base text-center">
          {t(error)}
        </Text>
      </View>
    );
  }

  return (
    <ScrollView
      contentInsetAdjustmentBehavior="automatic"
      className="flex-1 bg-md-surface"
      contentContainerClassName="p-6 gap-8"
    >
      <CategoriesHeader />
      <CategoryGrid categories={categories} onCategoryPress={handleCategoryPress} />
    </ScrollView>
  );
}
