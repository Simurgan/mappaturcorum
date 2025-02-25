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
}

export type UnordinaryPageRequestBody = {
  pageSize: number;
  pageNumber: number;
  filter?: {
    religion?: number[];
    ethnicity?: number[];
    deathYear?: number[];
    deathPlace?: number[];
    interactionsWithOrdinary?: number[];
  };
};

export type SubObjectPair = {
  id: number;
  name: string;
};

export type UnordinaryPageResponseDataItem = {
  alternateName: any;
  religion: SubObjectPair;
  ethnicity?: SubObjectPair;
  deathYear?: number[];
  deathPlace: any;
  interactionsWithOrdinary: SubObjectPair[];
  name: string;
};

export type UnordinaryPageResponseData = {
  data: UnordinaryPageResponseDataItem[];
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
};
