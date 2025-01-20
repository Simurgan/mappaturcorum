import ToolHeader from "@/views/layout/tool-header";
import { Outlet } from "react-router-dom";

const ToolLayout = () => {
  return (
    <>
      <ToolHeader />
      <Outlet />
    </>
  );
};
export default ToolLayout;
