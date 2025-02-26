import { useEffect } from "react";
import { BrowserRouter, Route, Routes, useLocation } from "react-router-dom";
import { Urls } from "@/routers/routes";
import Layout from "@/views/layout/layout";
import ToolLayout from "@/views/layout/tool-layout";
import MapPage from "@/views/pages/map";
import SocialNetworkPage from "@/views/pages/social-network";
import HomePage from "@/views/pages/home";
import AboutPage from "@/views/pages/about";
import ContactPage from "@/views/pages/contact";
import OrdinaryPeoplePage from "@/views/pages/ordinary-people";
import UnordinaryPeoplePage from "@/views/pages/unordinary-people";
import SourcesPage from "@/views/pages/sources";
import MembersPage from "@/views/pages/members";
import ProjectHistoryPage from "@/views/pages/project-history";
import CollaborationsPage from "@/views/pages/collaborations";
// import PublicationsPage from "@/views/pages/publications";

const MainRouter = () => {
  return (
    <BrowserRouter>
      <Router />
    </BrowserRouter>
  );
};
export default MainRouter;

const Router = () => {
  const { pathname } = useLocation();

  useEffect(() => {
    window.scrollTo(0, 0);
  }, [pathname]);

  return (
    <Routes>
      <Route element={<Layout />}>
        <Route element={<HomePage />} path={Urls.Home} />
        <Route element={<AboutPage />} path={Urls.About} />
        <Route element={<ContactPage />} path={Urls.Contact} />
        <Route element={<OrdinaryPeoplePage />} path={Urls.OrdinaryPeople} />
        <Route
          element={<UnordinaryPeoplePage />}
          path={Urls.UnordinaryPeople}
        />
        <Route element={<SourcesPage />} path={Urls.Sources} />
        <Route element={<MembersPage />} path={Urls.Members} />
        {/* <Route element={<PublicationsPage />} path={Urls.Publications} /> */}
        <Route element={<ProjectHistoryPage />} path={Urls.ProjectHistory} />
        <Route element={<CollaborationsPage />} path={Urls.Collaborations} />
      </Route>
      <Route element={<ToolLayout />}>
        <Route element={<MapPage />} path={Urls.Map} />
        <Route element={<SocialNetworkPage />} path={Urls.SocialNetwork} />
      </Route>
    </Routes>
  );
};
