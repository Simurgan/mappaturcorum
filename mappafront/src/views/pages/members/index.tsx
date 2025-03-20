import { useSearchParams } from "react-router-dom";
import "./style.scss";
import Text from "@/views/components/text";
import { useEffect } from "react";
import Button from "@/views/components/button";
import { Teams } from "@/static/members";
import MemberCard from "./member-card";

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
          {Teams.map((item) => (
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
          {Teams.map(
            (item) =>
              item.name === searchParams.get("team") &&
              item.subteams.map((subteam) => (
                <div className="subteam-container">
                  <Text fs={24} fw={500} lh={125}>
                    {subteam.name}
                  </Text>
                  <div className="members-container">
                    {subteam.members.map((member) => (
                      <MemberCard member={member} />
                    ))}
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
