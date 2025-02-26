import { SubObjectPair } from "./ordinary-people";

export type AllWrittenSourcesResponseDataItem = {
  id: number;
  name: string;
  author?: string;
  alternateNames?: string[];
  yearWritten?: number[];
  genre?: SubObjectPair;
  language?: SubObjectPair;
};

export type WrittenSourceRequestBody = {
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

export type WrittenSourceResponseItemType = {
  ordinaryPersons: SubObjectPair[];
  unordinaryPersons: SubObjectPair[];
  alternateNames?: string[];
  author?: string;
  yearWritten?: number[];
  genre?: SubObjectPair;
  language?: SubObjectPair;
  id: number;
  name?: string;
};

export type WrittenSourceResponseType = {
  data: WrittenSourceResponseItemType[];
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
};
