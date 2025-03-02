export type OrdinaryPageRequestBody = {
  pageSize: number;
  pageNumber: number;
  filter?: {
    name?: string;
    religion?: number[];
    ethnicity?: number[];
    profession?: number[];
    location?: number[];
    sources?: number[];
    gender?: number[];
    interactionsWithUnordinary?: number[];
  };
};

export type SubObjectPair = {
  id: number;
  name: string;
};

export type OrdinaryPageResponseDataItem = {
  id: number;
  name: string;
  alternateName?: string;
  ethnicity?: SubObjectPair;
  gender?: SubObjectPair;
  location?: SubObjectPair;
  profession?: SubObjectPair;
  religion?: SubObjectPair;
  sources: SubObjectPair[];
  formerReligion: SubObjectPair;
  interactionsWithUnordinary: SubObjectPair[];
};

export type OrdinaryPageResponseData = {
  pageSize: number;
  pageNumber: number;
  totalCount: number;
  totalPages: number;
  data: OrdinaryPageResponseDataItem[];
};

export type OrdinaryGraphResponseDataItem = {
  id: number;
  name: string;
  religion?: number;
  formerReligion?: number;
  ethnicity?: number;
  profession?: number;
  gender?: number;
  location?: number;
  sources: number[];
  interactionsWithOrdinaryA: number[];
  interactionsWithOrdinaryB: number[];
  interactionsWithUnordinary: number[];
};

export type SingleOrdinaryObject = {
  id: number;
  name: string;
  alternateName?: string;
  ethnicity?: SubObjectPair;
  religion?: SubObjectPair;
  formerReligion?: SubObjectPair;
  profession?: SubObjectPair;
  professionExplanation?: string;
  gender?: SubObjectPair;
  interestingFeature?: string;
  interactionsWithOrdinaryA?: SubObjectPair[];
  interactionsWithOrdinaryB?: SubObjectPair[];
  interactionWithOrdinaryExplanation?: string;
  interactionsWithUnordinary?: SubObjectPair[];
  interactionWithUnordinaryExplanation?: string;
  sources?: SubObjectPair[];
  biography?: string;
  descriptionInTheSource?: string;
  explanationOfEthnicity?: string;
  location?: { id: number; name: string; asciiName: string };
  backgroundCity?: { id: number; name: string; asciiName: string };
  // below will not be shown
  birthYear?: number[]; //tbd
  deathYear?: number[]; //tbd
  description?: string; //tbd
  probableBirthYear?: number;
  probableDeathYear?: number;
  religionExplanation?: string;
};
