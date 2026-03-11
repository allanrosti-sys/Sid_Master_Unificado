import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import Layout from "./components/Layout";

import Dashboard from "./pages/Dashboard.jsx";
import Integrations from "./pages/Integrations.jsx";
import Library from "./pages/Library.jsx";
import MapDemo from "./pages/MapDemo.jsx";
import PuchtaActions from "./pages/PuchtaActions.jsx";
import PuchtaIntegration from "./pages/PuchtaIntegration.jsx";
import Settings from "./pages/Settings.jsx";
import SidModules from "./pages/SidModules.jsx";

import "./index.css";

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Navigate to="/dashboard" replace />} />

          <Route path="dashboard" element={<Dashboard />} />
          <Route path="bibliotecas" element={<Library />} />
          <Route path="integracoes" element={<Integrations />} />

          <Route path="sid/modulos" element={<SidModules />} />

          <Route path="puchta" element={<PuchtaIntegration />} />
          <Route path="puchta/acoes" element={<PuchtaActions />} />

          <Route path="configuracoes" element={<Settings />} />
          <Route path="mapa-demo" element={<MapDemo />} />

          <Route path="*" element={<Navigate to="/dashboard" replace />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
