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
  interactionsWithUnordinary: SubObjectPair[];
};

export type OrdinaryPageResponseData = {
  pageSize: number;
  pageNumber: number;
  totalCount: number;
  totalPages: number;
  data: OrdinaryPageResponseDataItem[];
};
