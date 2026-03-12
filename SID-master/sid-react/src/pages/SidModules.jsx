import React, { useEffect, useMemo, useState } from "react";
import { sidApi } from "../services/api";

const ICONS = {
  ClipboardList: "[CLIP]",
  Puzzle: "[MOD]",
  Database: "[DB]",
  Factory: "[PLC]",
  FileText: "[DOC]",
};

function statusBadge(status) {
  const normalized = (status || "").toLowerCase();
  if (normalized.includes("planejado")) return "bg-slate-100 text-slate-600";
  if (normalized.includes("beta")) return "bg-yellow-100 text-yellow-700";
  if (normalized.includes("mvp")) return "bg-green-100 text-green-700";
  return "bg-slate-100 text-slate-600";
}

export default function SidModules() {
  const [modules, setModules] = useState([]);
  const [loading, setLoading] = useState(true);
  const [actionResult, setActionResult] = useState(null);

  useEffect(() => {
    sidApi
      .get("/sid/modules")
      .then((data) => {
        setModules(data);
        setLoading(false);
      })
      .catch((error) => {
        console.error(error);
        setLoading(false);
      });
  }, []);

  const runAction = async (endpoint) => {
    setActionResult(null);
    try {
      const path = endpoint.startsWith("/api") ? endpoint.substring(4) : endpoint;
      const response = await sidApi.get(path);
      setActionResult({ success: true, data: response });
    } catch (error) {
      setActionResult({ success: false, error: error.message });
    }
  };

  const moduleCount = useMemo(() => modules.length, [modules]);

  if (loading) return <div className="p-8 text-center text-slate-500">Carregando modulos...</div>;

  return (
    <div className="max-w-6xl mx-auto space-y-8">
      <div className="flex justify-between items-center">
        <div>
          <h1 className="text-2xl font-bold text-slate-800">Modulos do SID</h1>
          <p className="text-slate-500">Inventario inicial para diagnostico e migracao para web. Total: {moduleCount}.</p>
        </div>
      </div>

      {actionResult && (
        <div
          className={`p-4 rounded-lg border ${
            actionResult.success
              ? "bg-green-50 border-green-200 text-green-800"
              : "bg-red-50 border-red-200 text-red-800"
          }`}
        >
          <h4 className="font-bold text-sm mb-1">Resultado da acao</h4>
          <pre className="text-xs font-mono overflow-auto">{JSON.stringify(actionResult.data || actionResult.error, null, 2)}</pre>
          <button onClick={() => setActionResult(null)} className="text-xs underline mt-2">
            Fechar
          </button>
        </div>
      )}

      <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
        {modules.map((moduleItem) => (
          <div key={moduleItem.id} className="bg-white p-6 rounded-xl shadow-sm border border-slate-200 flex flex-col">
            <div className="flex items-start justify-between mb-4">
              <div className="text-sm font-mono text-slate-500">{ICONS[moduleItem.icon] || "[MOD]"}</div>
              <span className={`px-2 py-1 text-xs font-semibold rounded-full ${statusBadge(moduleItem.status)}`}>
                {moduleItem.status}
              </span>
            </div>
            <h3 className="font-bold text-lg text-slate-800 mb-2">{moduleItem.name}</h3>
            <p className="text-slate-600 text-sm flex-1">{moduleItem.description}</p>

            {moduleItem.hasAction && moduleItem.actionEndpoint ? (
              <button
                onClick={() => runAction(moduleItem.actionEndpoint)}
                className="mt-4 w-full bg-slate-50 hover:bg-slate-100 text-blue-700 border border-slate-200 py-2 rounded-lg text-sm font-semibold transition"
              >
                Testar / Status
              </button>
            ) : (
              <div className="mt-4 text-xs text-slate-400">Sem acao no MVP.</div>
            )}
          </div>
        ))}
      </div>
    </div>
  );
}
