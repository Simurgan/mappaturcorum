import Header from "@/views/layout/header";
import Footer from "@/views/layout/footer";
import { Outlet } from "react-router-dom";

const Layout = () => {
  return (
    <>
      <Header />
      <div className="content-body  min-h-[calc(100vh-304px)]">
        <Outlet />
      </div>
      <Footer />
    </>
  );
};
export default Layout;
