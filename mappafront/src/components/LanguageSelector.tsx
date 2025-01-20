import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "./ui/select";

const LanguageSelector = () => {
  return (
    <Select defaultValue="turkish">
      <SelectTrigger className="w-[82px] h-[30px] bg-secondary border-none shadow-[#00000040]">
        <SelectValue placeholder="Select a fruit" />
      </SelectTrigger>
      <SelectContent>
        <SelectGroup>
          <SelectItem value="turkish">Türkçe</SelectItem>
          <SelectItem value="english">English</SelectItem>
        </SelectGroup>
      </SelectContent>
    </Select>
  );
};

export default LanguageSelector;
