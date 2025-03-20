import { Member } from "@/models/members";
import Text from "@/views/components/text";

const socialIcons: { [key: string]: any } = {
  linkedin: require("@/assets/icons/linkedin-logo.svg"),
  email: require("@/assets/icons/email.svg"),
  twitter: require("@/assets/icons/twitter-x-black-logo.svg"),
  github: require("@/assets/icons/github-logo.svg"),
};

interface MemberCardProps {
  member: Member;
}

const MemberCard = ({ member }: MemberCardProps) => (
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

export default MemberCard;
