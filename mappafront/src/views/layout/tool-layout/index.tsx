import ToolHeader from "@/views/layout/tool-header";
import { Outlet } from "react-router-dom";

const ToolLayout = () => {
  return (
    <>
      <ToolHeader />
      <div className="content-body min-h-[calc(100vh-52px)]">
        <Outlet />
      </div>
    </>
  );
};
export default ToolLayout;
