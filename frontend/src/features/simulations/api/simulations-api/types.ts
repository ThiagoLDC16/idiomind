export interface Category {
  id: string;
  icon: string;
  name: string;
  languageCode: string;
}

export interface Situation {
  id: string;
  name: string;
  description: string;
  languageCode: string;
}

export interface CategoryDetails {
  name: string;
  situations: Situation[];
}

export interface Variant {
  id: string;
  name: string;
  userContext: string;
  languageCode: string;
  objectives: string[];
}

export interface SituationDetails {
  name: string;
  variants: Variant[];
}

export enum MessageFeedbackClassification {
  Correct = 0,
  Grammar = 1,
  Vocabulary = 2,
  Context = 3,
  Spelling = 4,
}

export interface MessageFeedback {
  classification: MessageFeedbackClassification;
  explanation: string | null;
  correction: string | null;
}

export enum MessageSender {
  AI = 1,
  USER = 2,
}

export interface SimulationMessage {
  id: string;
  sender: MessageSender;
  content: string;
  translatedContent: string | null;
  feedback: MessageFeedback | null;
}

export interface SimulationDetails {
  situationId: string;
  categoryId: string;
  name: string;
  learningLanguage: string;
  messages: SimulationMessage[];
}

export enum SimulationFeedbackPreparedness {
  No = 0,
  ALittle = 1,
  Yes = 2,
  VeryMuch = 3,
}

export interface SubmitSimulationFeedbackRequest {
  experienceRating: number | null;
  preparedness: SimulationFeedbackPreparedness | null;
  improvement: string | null;
  skipped: boolean;
}

export interface TranslateMessageResponse {
  translatedContent: string;
}
