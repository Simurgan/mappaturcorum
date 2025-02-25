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
