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
