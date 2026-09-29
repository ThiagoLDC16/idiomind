import { useCallback, useState } from 'react';

import { simulationsApi } from '@/features/simulations/api/simulations-api';
import type { SimulationFeedbackPreparedness } from '@/features/simulations/api/simulations-api/types';
import { getApiErrorMessage } from '@/shared/utils/api-error';

interface SubmitFeedbackInput {
  experienceRating: number | null;
  preparedness: SimulationFeedbackPreparedness | null;
  improvement: string;
  skipped: boolean;
}

export function useSimulationFeedback(simulationId: string) {
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [isSubmitted, setIsSubmitted] = useState(false);
  const [wasSkipped, setWasSkipped] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const submitFeedback = useCallback(
    async (input: SubmitFeedbackInput) => {
      setIsSubmitting(true);
      setError(null);

      try {
        await simulationsApi.submitSimulationFeedback(simulationId, {
          experienceRating: input.experienceRating,
          preparedness: input.preparedness,
          improvement: input.improvement.trim() || null,
          skipped: input.skipped,
        });
        setWasSkipped(input.skipped);
        setIsSubmitted(true);
      } catch (submissionError) {
        setError(getApiErrorMessage(submissionError, 'internalError'));
      } finally {
        setIsSubmitting(false);
      }
    },
    [simulationId],
  );

  return { isSubmitting, isSubmitted, wasSkipped, error, submitFeedback };
}
