export type Team = {
  name: string;
  subteams: SubTeam[];
};

export type SubTeam = {
  name: string;
  members: Member[];
};

export type Member = {
  name: string;
  socials: SocialType[];
  descs: string[];
  image: string;
};

export type SocialType = {
  type: string;
  url: string;
};
