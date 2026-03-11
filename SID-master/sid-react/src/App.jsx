import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import MainLayout from './layouts/MainLayout';
import Dashboard from './pages/Dashboard';
import Library from './pages/Library';
import MapDemo from './pages/MapDemo';
import Integrations from './pages/Integrations';
import Settings from './pages/Settings';
import PuchtaActions from './pages/PuchtaActions';
import SidModules from './pages/SidModules';
import PuchtaIntegration from './pages/PuchtaIntegration';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<MainLayout />}>
          <Route index element={<Navigate to="/dashboard" replace />} />
          <Route path="dashboard" element={<Dashboard />} />
          <Route path="configuracoes" element={<Settings />} />
          <Route path="sid/modulos" element={<SidModules />} />
          <Route path="puchta" element={<PuchtaIntegration />} />
          <Route path="puchta/acoes" element={<PuchtaActions />} />
          <Route path="bibliotecas" element={<Library />} />
          <Route path="integracoes" element={<Integrations />} />
          <Route path="mapa" element={<MapDemo />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;