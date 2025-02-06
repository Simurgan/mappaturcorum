import { useSearchParams } from "react-router-dom";
import "./style.scss";
import { Member, Team } from "@/models/members";
import Text from "@/views/components/text";
import { useEffect } from "react";
import Button from "@/views/components/button";

const MembersPage = () => {
  const [searchParams, setSearchParams] = useSearchParams();

  useEffect(() => {
    if (!searchParams.get("team")) {
      setSearchParams({ team: "Project Team" });
    }
  }, [searchParams, setSearchParams]);

  return (
    <section className="section members-section">
      <div className="container">
        <nav className="navbar">
          {teams.map((item) => (
            <Button
              classNames="navbar-button"
              onClick={() => setSearchParams({ team: item.name })}
            >
              <Text
                fs={24}
                fw={400}
                color="burgundy"
                lh={125}
                classNames={`${
                  item.name === searchParams.get("team") ? "active-button" : ""
                }`}
              >
                {item.name}
              </Text>
            </Button>
          ))}
        </nav>
        <div className="content-container">
          {teams.map(
            (item) =>
              item.name === searchParams.get("team") &&
              item.subteams.map((subteam) => (
                <div className="subteam-container">
                  <Text fs={24} fw={500} lh={125}>
                    {subteam.name}
                  </Text>
                  <div className="members-container">
                    {subteam.members.map((member) => MemberCard(member))}
                  </div>
                </div>
              ))
          )}
        </div>
      </div>
    </section>
  );
};
export default MembersPage;

const MemberCard = (member: Member) => (
  <div className="member-container">
    <div className="image-container" />
    <Text fs={16} fw={500} lh={140} color="burgundy">
      {member.name}
    </Text>
    <Text fs={12} fw={300} lh={125} classNames="member-description">
      {member.role}
    </Text>
  </div>
);

const teams: Team[] = [
  {
    name: "Project Team",
    subteams: [
      {
        name: "Project Founder",
        members: [
          {
            name: "Muzaffer Çakır",
            role: "Founder and Coordinator",
            image: "https://randomuser",
          },
        ],
      },
      {
        name: "Coordinators",
        members: [
          {
            name: "Muzaffer Çakır",
            role: "Founder and Coordinator",
            image: "https://randomuser",
          },
          {
            name: "Emirhan Kuşaksız",
            role: "Coordinator",
            image: "https://randomuser",
          },
          {
            name: "Mehmet Akif Top",
            role: "Coordinator",
            image: "https://randomuser",
          },
        ],
      },
      {
        name: "Researchers",
        members: [
          {
            name: "İsmail Demirtaş",
            role: "Researcher",
            image: "https://randomuser",
          },
          {
            name: "Gökçe Yılmaz",
            role: "Researcher",
            image: "https://randomuser",
          },
          {
            name: "Şeyma Sarı",
            role: "Researcher",
            image: "https://randomuser",
          },
        ],
      },
    ],
  },
  {
    name: "Technical Team",
    subteams: [
      {
        name: "Developers",
        members: [
          {
            name: "Ömer Şükrü Uyduran",
            role: "Frontend Developer",
            image: "https://randomuser",
          },
          {
            name: "Burak Kızılay",
            role: "Frontend Developer",
            image: "https://randomuser",
          },
          {
            name: "Said Yolcu",
            role: "Backend Developer",
            image: "https://randomuser",
          },
        ],
      },
      {
        name: "Designers",
        members: [
          {
            name: "Seher Doğan",
            role: "Designer",
            image: "https://randomuser",
          },
        ],
      },
    ],
  },
];
