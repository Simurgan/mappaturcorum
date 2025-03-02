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
  <div
    className={`member-container${
      member.name === "Gökçe Yılmaz" || member.name === "Şeyma Sarı"
        ? " reposition"
        : member.name === "Said Yolcu"
        ? " rereposition"
        : member.name === "Burak Kızılay"
        ? " rerereposition"
        : ""
    }`}
  >
    <div className="image-container">
      <img src={member.image} alt="" />
    </div>
    <Text fs={16} fw={500} lh={140} color="burgundy">
      {member.name}
    </Text>
    <div className="member-descs">
      {member.descs.map((desc) => (
        <Text fs={12} fw={300} lh={125} classNames="member-description">
          {desc}
        </Text>
      ))}
    </div>
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
            descs: ["Boğaziçi University", "History Student"],
            image: require("@/assets/images/member-images/muzaffer.png"),
          },
        ],
      },
      {
        name: "Coordinators",
        members: [
          {
            name: "Emirhan Kuşaksız",
            descs: ["Boğaziçi University", "History Student"],
            image: require("@/assets/images/member-images/emirhan.png"),
          },
          {
            name: "Mehmet Akif Top",
            descs: ["Istanbul University", "History Student"],
            image: require("@/assets/images/member-images/akif.png"),
          },
        ],
      },
      {
        name: "Researchers",
        members: [
          {
            name: "İsmail Demirtaş",
            descs: ["Boğaziçi University", "History Student"],
            image: require("@/assets/images/member-images/ismail.png"),
          },
          {
            name: "Gökçe Yılmaz",
            descs: [
              "Boğaziçi University",
              "History & Turkish Language and Literature",
              "Double Major Student",
            ],
            image: require("@/assets/images/member-images/gokce.png"),
          },
          {
            name: "Şeyma Sarı",
            descs: [
              "Boğaziçi University",
              "Turkish Language and Literature",
              "Student",
            ],
            image: require("@/assets/images/member-images/seyma.png"),
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
            descs: ["Boğaziçi University", "Computer Engineering", "Student"],
            image: require("@/assets/images/member-images/omar.png"),
          },
          {
            name: "Said Yolcu",
            descs: ["Boğaziçi University", "Computer Engineering", "Student"],
            image: require("@/assets/images/member-images/said.png"),
          },
          {
            name: "Burak Kızılay",
            descs: [
              "Manisa Celal Bayar University",
              "Software Engineering",
              "Master's Student",
            ],
            image: require("@/assets/images/member-images/burak.png"),
          },
        ],
      },
      {
        name: "Designers",
        members: [
          {
            name: "Seher Doğan",
            descs: ["Hacettepe University", "Graphic Design Student"],
            image: require("@/assets/images/member-images/seher.png"),
          },
        ],
      },
    ],
  },
];
