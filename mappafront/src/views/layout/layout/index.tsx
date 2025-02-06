import Header from "@/views/layout/header";
import Footer from "@/views/layout/footer";
import { Outlet } from "react-router-dom";
import "./style.scss";

const Layout = () => {
  return (
    <>
      <Header />
      <div className="content-body">
        <Outlet />
      </div>
      <Footer />
    </>
  );
};
export default Layout;
