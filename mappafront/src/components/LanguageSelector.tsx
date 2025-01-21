import { useState } from "react";
import { Button } from "./ui/button";

const LanguageSelector = () => {
  const languages = ["EN", "TR"];
  const [selectedLanguage, setSelectedLanguage] = useState(languages[0]);

  const changeLanguage = () => {
    setSelectedLanguage(selectedLanguage === "EN" ? "TR" : "EN");
  };

  return (
    <Button
      className="h-[30px] rounded-3xl shadow-sm shadow-black"
      onClick={changeLanguage}
      variant={"secondary"}
    >
      {selectedLanguage}
    </Button>
  );
};

export default LanguageSelector;
