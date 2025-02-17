type OrdinaryPeopleTableDataType = {
  name: string;
  alternateName?: string;
  ethnonym?: string;
  religion?: string;
  formerReligion?: string;
  profession?: string;
  gender?: "Male" | "Female" | "Other";
  interestingFeature?: string;
  interactionOrdinary?: string;
  interactionOrdinaryExplanation?: string;
  interactionUnordinary?: string;
  interactionUnordinaryExplanation?: string;
  sources?: string[];
  biography?: string;
  descriptionInSource?: string;
  explanationOfEthnicity?: string;
  version?: string;
};

export type { OrdinaryPeopleTableDataType };
