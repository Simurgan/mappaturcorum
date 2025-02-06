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
  role: string;
  image: string;
};
