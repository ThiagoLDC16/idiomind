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
  name: string;
  learningLanguage: string;
  messages: SimulationMessage[];
}

export interface TranslateMessageResponse {
  translatedContent: string;
}
