export type UnordinaryGraphResponseDataItem = {
  id: number;
  name: string;
  religion?: number;
  ethnicity?: number;
  profession?: number;
  birthPlace?: number;
  deathplace?: number;
  gender?: number;
  sources: number[];
  interactionsWithUnordinaryA: number[];
  interactionsWithUnordinaryB: number[];
  interactionsWithOrdinary: number[];
};
