import { AllEthnicitiesResponseDataItem } from "./ethnicity";
import { AllGendersResponseDataItem } from "./gender";
import { OrdinaryGraphResponseDataItem } from "./ordinary-people";
import { AllProfessionsResponseDataItem } from "./profession";
import { AllReligionsResponseDataItem } from "./religion";
import { UnordinaryGraphResponseDataItem } from "./unordinary-people";
import { AllWrittenSourcesResponseDataItem } from "./written-source";

export type RawGraphDataType = {
  ordinaries: OrdinaryGraphResponseDataItem[];
  religions: AllReligionsResponseDataItem[];
  unordinaries: UnordinaryGraphResponseDataItem[];
  sources: AllWrittenSourcesResponseDataItem[];
  ethnicities: AllEthnicitiesResponseDataItem[];
  professions: AllProfessionsResponseDataItem[];
  genders: AllGendersResponseDataItem[];
};

export type GraphDataType = { nodes: NodeType[]; links: EdgeType[] };

export type GraphFilterType = {
  ordinaries: boolean;
  religions: boolean;
  unordinaries: boolean;
  sources: boolean;
  ethnicities: boolean;
  professions: boolean;
  genders: boolean;
};

export type NodeType = {
  id: number;
  name: string;
  dataId: number;
  type: NodeTypesEnum;
  ordinaryRelCount: number;
  unordinaryRelCount: number;
  sourceRelCount: number;
  religionRelCount: number;
  ethnicityRelCount: number;
  professionRelCount: number;
  genderRelCount: number;
};

export type EdgeType = {
  source: number;
  target: number;
};

export type PseudoEdgeType = {
  source: { type: NodeTypesEnum; id: number };
  target: { type: NodeTypesEnum; id: number };
};

export enum NodeTypesEnum {
  ReligionNode,
  EthnicityNode,
  ProfessionNode,
  GenderNode,
  SourceNode,
  OrdinaryPersonNode,
  UnordinaryPersonNode,
}
