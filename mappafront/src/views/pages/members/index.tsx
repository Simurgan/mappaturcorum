import { useSearchParams } from "react-router-dom";
import "./style.scss";
import { Member, Team } from "@/models/members";
import Text from "@/views/components/text";
import { useEffect } from "react";
import Button from "@/views/components/button";

const socialIcons: { [key: string]: any } = {
  linkedin: require("@/assets/icons/linkedin-logo.svg"),
  email: require("@/assets/icons/email.svg"),
  twitter: require("@/assets/icons/twitter-x-black-logo.svg"),
};

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
    <div className="social-container">
      {member.socials?.map((social, index) => (
        <a
          key={index}
          href={`${social.type === "email" ? "mailto:" : ""}${social.url}`}
          target="_blank"
          rel="noopener noreferrer"
        >
          <img
            src={socialIcons[social.type as string]}
            width={16}
            height={16}
            alt={social.type}
          />
        </a>
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
            descs: ["Boğaziçi University", "Undergraduate", "History"],
            image: require("@/assets/images/member-images/muzaffer.png"),
            socials: [
              {
                type: "linkedin",
                url: "https://www.linkedin.com/in/muzaffer-%C3%A7ak%C4%B1r-7897a026b/",
              },
              { type: "email", url: "muzaffercakir94@gmail.com" },
            ],
          },
        ],
      },
      {
        name: "Coordinators",
        members: [
          {
            name: "Emirhan Kuşaksız",
            descs: ["Boğaziçi University", "Undergraduate", "History"],
            image: require("@/assets/images/member-images/emirhan.png"),
            socials: [
              {
                type: "linkedin",
                url: "https://www.linkedin.com/in/emirhan-ku%C5%9Faks%C4%B1z-328962292/",
              },
              { type: "email", url: "emirhankusaksiz@gmail.com" },
            ],
          },
          {
            name: "Mehmet Akif Top",
            descs: ["Istanbul University", "Undergraduate", "History"],
            image: require("@/assets/images/member-images/akif.png"),
            socials: [
              { type: "linkedin", url: "https://www.linkedin.com/in/akfmttp/" },
              { type: "email", url: "akfmttp@gmail.com" },
            ],
          },
        ],
      },
      {
        name: "Researchers",
        members: [
          {
            name: "İsmail Demirtaş",
            descs: ["Boğaziçi University", "Undergraduate", "History"],
            image: require("@/assets/images/member-images/ismail.png"),
            socials: [
              {
                type: "linkedin",
                url: "https://www.linkedin.com/in/ismail-zahid-demirta%C5%9F-2545a0353/?utm_source=share&utm_campaign=share_via&utm_content=profile&utm_medium=android_app",
              },
              { type: "email", url: "izdemirtas751@gmail.com" },
            ],
          },
          {
            name: "Gökçe Yılmaz",
            descs: [
              "Boğaziçi University",
              "Undergraduate",
              "Double Major: History & Turkish Language and Literature",
            ],
            image: require("@/assets/images/member-images/gokce.png"),
            socials: [
              {
                type: "linkedin",
                url: "https://www.linkedin.com/in/gokcenuryilmaz/",
              },
              { type: "email", url: "ylmzgokce2000@gmail.com" },
            ],
          },
          {
            name: "Şeyma Sarı",
            descs: ["Boğaziçi University", "Graduate", "History"],
            image: require("@/assets/images/member-images/seyma.png"),
            socials: [
              {
                type: "twitter",
                url: "https://x.com/SeymaSar8/status/1857832565001871761",
              },
              { type: "email", url: "seymasari79@gmail.com" },
            ],
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
            descs: [
              "Boğaziçi University",
              "Undergraduate",
              "Computer Engineering",
            ],
            image: require("@/assets/images/member-images/omar.png"),
            socials: [
              {
                type: "linkedin",
                url: "https://www.linkedin.com/in/uyduranomar/",
              },
            ],
          },
          {
            name: "Said Yolcu",
            descs: ["Boğaziçi University", "Graduate", "Computer Engineering"],
            image: require("@/assets/images/member-images/said.png"),
            socials: [
              {
                type: "linkedin",
                url: "https://www.linkedin.com/in/mehmet-said-yolcu-a1aa41232/",
              },
            ],
          },
          {
            name: "Burak Kızılay",
            descs: [
              "Manisa Celal Bayar University",
              "Graduate",
              "Software Engineering",
            ],
            image: require("@/assets/images/member-images/burak.png"),
            socials: [
              {
                type: "linkedin",
                url: "https://www.linkedin.com/in/burak-kizilay/",
              },
            ],
          },
        ],
      },
      {
        name: "Designers",
        members: [
          {
            name: "Seher Doğan",
            descs: ["Hacettepe University", "Undergraduate", "Graphic Design"],
            image: require("@/assets/images/member-images/seher.png"),
            socials: [],
          },
        ],
      },
    ],
  },
];
