import React, { useEffect, useMemo, useState } from "react";
import { Link } from "react-router-dom";
import { sidApi } from "../services/api";

const StatusDot = ({ ok }) => (
  <span className={`inline-block h-2.5 w-2.5 rounded-full ${ok ? "bg-green-500" : "bg-red-500"}`} />
);

const Card = ({ title, children }) => (
  <div className="bg-white p-6 rounded-xl shadow-sm border border-slate-200 hover:shadow-md transition">
    <div className="flex items-center justify-between mb-4">
      <h3 className="font-semibold text-slate-700">{title}</h3>
    </div>
    {children}
  </div>
);

export default function Dashboard() {
  const [loading, setLoading] = useState(true);
  const [sidHealth, setSidHealth] = useState(null);
  const [sidVersion, setSidVersion] = useState(null);
  const [integrations, setIntegrations] = useState(null);
  const [settings, setSettings] = useState(null);
  const [puchtaPanel, setPuchtaPanel] = useState({ ok: false, version: null });
  const [puchtaProject, setPuchtaProject] = useState(null);
  const [error, setError] = useState(null);

  const puchtaConfigured = useMemo(() => Boolean(settings?.puchtaPanelUrl), [settings]);

  useEffect(() => {
    let cancelled = false;

    async function load() {
      setLoading(true);
      setError(null);

      try {
        const [health, version, intStatus, conf] = await Promise.all([
          sidApi.get("/health"),
          sidApi.get("/version"),
          sidApi.get("/integrations/status"),
          sidApi.get("/portal/settings"),
        ]);

        if (cancelled) return;
        setSidHealth(health);
        setSidVersion(version);
        setIntegrations(intStatus);
        setSettings(conf);

        if (conf?.puchtaPanelUrl) {
          try {
            const [panelResp, projectResp] = await Promise.all([
              sidApi.proxy("puchta_panel", "/api/version"),
              sidApi.proxy("puchta_panel", "/api/project-path"),
            ]);

            setPuchtaPanel({
              ok: panelResp.status >= 200 && panelResp.status < 300,
              version: panelResp.data?.version ?? null,
            });

            setPuchtaProject(projectResp.status === 200 ? projectResp.data : null);
          } catch {
            setPuchtaPanel({ ok: false, version: null });
            setPuchtaProject(null);
          }
        }
      } catch (e) {
        setError(e?.message || "Falha ao carregar o dashboard.");
      } finally {
        setLoading(false);
      }
    }

    load();
    return () => {
      cancelled = true;
    };
  }, []);

  if (loading)
    return <div className="p-10 text-center text-slate-500 animate-pulse">Carregando painel de controle...</div>;

  return (
    <div className="max-w-6xl mx-auto space-y-8">
      <div className="bg-gradient-to-r from-slate-900 to-slate-800 rounded-2xl p-8 text-white shadow-lg">
        <h1 className="text-3xl font-extrabold mb-2">Portal Unificado (SID Web + Puchta Insight)</h1>
        <p className="text-slate-200 max-w-3xl text-base">
          Use este portal para navegar pelas bibliotecas do SID, executar diagnósticos e integrar com o Puchta Insight
          para visualizar projetos existentes (Siemens/Rockwell).
        </p>

        <div className="mt-6 flex flex-wrap gap-3">
          <Link
            to="/bibliotecas"
            className="bg-white text-slate-900 px-5 py-2 rounded-lg font-semibold hover:bg-slate-100 transition"
          >
            Abrir Bibliotecas
          </Link>
          <Link
            to="/sid/modulos"
            className="bg-slate-700 text-white border border-slate-500 px-5 py-2 rounded-lg font-semibold hover:bg-slate-600 transition"
          >
            Ver Módulos do SID
          </Link>
          <Link
            to="/puchta"
            className="bg-blue-600 text-white px-5 py-2 rounded-lg font-semibold hover:bg-blue-500 transition"
          >
            Ir para Puchta Insight
          </Link>
          <Link
            to="/configuracoes"
            className="bg-slate-700 text-white border border-slate-500 px-5 py-2 rounded-lg font-semibold hover:bg-slate-600 transition"
          >
            Configurações
          </Link>
        </div>

        {error ? <div className="mt-5 bg-red-500/20 border border-red-400/30 p-3 rounded-lg">{error}</div> : null}
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        <Card title="SID API">
          <div className="flex items-center gap-2 text-slate-800">
            <StatusDot ok={sidHealth?.status === "ok"} />
            <div className="text-xl font-bold">{sidHealth?.status === "ok" ? "Operacional" : "Instável"}</div>
          </div>
          <div className="mt-2 text-sm text-slate-500">Versão: {sidVersion?.version ?? "—"}</div>
          <div className="mt-1 text-xs text-slate-400">Health timestamp: {sidHealth?.timestampUtc ?? "—"}</div>
        </Card>

        <Card title="Integrações (SID)">
          <div className="text-3xl font-extrabold text-slate-900">
            {integrations?.modules?.filter((m) => m.status === "online").length || 0} /{" "}
            {integrations?.modules?.length || 0}
          </div>
          <div className="mt-1 text-sm text-slate-500">{integrations?.summary ?? "—"}</div>
          <div className="mt-4">
            <Link to="/integracoes" className="text-sm font-semibold text-blue-700 hover:underline">
              Ver detalhes
            </Link>
          </div>
        </Card>

        <Card title="Puchta Insight">
          <div className="flex items-center gap-2 text-slate-800">
            <StatusDot ok={puchtaConfigured && puchtaPanel.ok} />
            <div className="text-xl font-bold">
              {!puchtaConfigured ? "Não configurado" : puchtaPanel.ok ? "Conectado" : "Indisponível"}
            </div>
          </div>
          <div className="mt-2 text-sm text-slate-500">
            Painel: {puchtaPanel.version ? `v${puchtaPanel.version}` : "—"}
          </div>
          <div className="mt-1 text-xs text-slate-400">
            Projeto: {puchtaProject?.tiaPath ? puchtaProject.tiaPath : "—"}
          </div>
        </Card>
      </div>

      <div className="bg-white rounded-xl shadow-sm border border-slate-200 overflow-hidden">
        <div className="px-6 py-4 border-b border-slate-100 bg-slate-50 flex justify-between items-center">
          <h3 className="font-bold text-slate-700">Monitoramento de Integrações (detalhes)</h3>
          <Link to="/integracoes" className="text-sm text-blue-600 hover:underline">
            Abrir página
          </Link>
        </div>
        <div className="divide-y divide-slate-100">
          {(integrations?.modules ?? []).map((mod, idx) => (
            <div key={idx} className="px-6 py-4 flex items-center justify-between hover:bg-slate-50 transition">
              <div>
                <p className="font-medium text-slate-800">{mod.module}</p>
                <p className="text-sm text-slate-500">{mod.details}</p>
              </div>
              <span
                className={`px-3 py-1 rounded-full text-xs font-bold border capitalize ${
                  mod.status === "online"
                    ? "bg-green-50 text-green-700 border-green-200"
                    : mod.status === "warning"
                      ? "bg-yellow-50 text-yellow-700 border-yellow-200"
                      : "bg-red-50 text-red-700 border-red-200"
                }`}
              >
                {mod.status}
              </span>
            </div>
          ))}

          {!integrations?.modules?.length ? (
            <div className="px-6 py-8 text-center text-slate-400">Sem dados de integrações.</div>
          ) : null}
        </div>
      </div>
    </div>
  );
}

