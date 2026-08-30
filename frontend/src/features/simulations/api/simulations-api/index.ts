import { apiClient } from '@/shared/api/clients/api-client';

import type {
  Category,
  CategoryDetails,
  SimulationDetails,
  SimulationMessage,
  SituationDetails,
  TranslateMessageResponse,
} from './types';

export const simulationsApi = {
  async listCategories(languageCode: string) {
    const response = await apiClient.get<Category[]>('/api/simulations/categories', {
      params: { languageCode },
    });
    return response.data;
  },

  async getCategory(categoryId: string, languageCode: string) {
    const response = await apiClient.get<CategoryDetails>(
      `/api/simulations/categories/${categoryId}`,
      { params: { languageCode } },
    );
    return response.data;
  },

  async getSituation(situationId: string, languageCode: string) {
    const response = await apiClient.get<SituationDetails>(
      `/api/simulations/situations/${situationId}`,
      { params: { languageCode } },
    );
    return response.data;
  },

  async startSimulation(variantId: string) {
    const response = await apiClient.post<string>('/api/simulations', { variantId });
    return response.data;
  },

  async getSimulationMessages(simulationId: string, languageCode: string) {
    const response = await apiClient.get<SimulationDetails>(
      `/api/simulations/${simulationId}/messages`,
      { params: { languageCode } },
    );
    return response.data;
  },

  async sendMessage(simulationId: string, content: string) {
    const response = await apiClient.post<SimulationMessage[]>(
      `/api/simulations/${simulationId}/messages`,
      { content },
    );
    return response.data;
  },

  async translateMessage(simulationId: string, messageId: string) {
    const response = await apiClient.post<TranslateMessageResponse>(
      `/api/simulations/${simulationId}/messages/${messageId}/translate`,
    );
    return response.data;
  },

  async finishSimulation(simulationId: string) {
    await apiClient.post(`/api/simulations/${simulationId}/finish`);
  },
};
