import React, { useEffect, useMemo, useState } from "react";
import { sidApi } from "../services/api";

function ModuleIcon({ type }) {
  const base = "h-10 w-10 rounded-lg border flex items-center justify-center";
  if (type === "ClipboardList") {
    return (
      <div className={`${base} bg-blue-50 border-blue-200 text-blue-700`}>
        <svg viewBox="0 0 24 24" className="h-5 w-5" fill="none" stroke="currentColor" strokeWidth="2">
          <rect x="7" y="4" width="10" height="16" rx="2" />
          <path d="M9 4h6v3H9z" />
          <path d="M9 11h6M9 15h4" />
        </svg>
      </div>
    );
  }

  if (type === "Puzzle") {
    return (
      <div className={`${base} bg-violet-50 border-violet-200 text-violet-700`}>
        <svg viewBox="0 0 24 24" className="h-5 w-5" fill="none" stroke="currentColor" strokeWidth="2">
          <path d="M8 3h4a2 2 0 1 1 4 0h3v5a2 2 0 1 1 0 4v5h-5a2 2 0 1 1-4 0H5v-5a2 2 0 1 1 0-4V3h3z" />
        </svg>
      </div>
    );
  }

  if (type === "Database") {
    return (
      <div className={`${base} bg-emerald-50 border-emerald-200 text-emerald-700`}>
        <svg viewBox="0 0 24 24" className="h-5 w-5" fill="none" stroke="currentColor" strokeWidth="2">
          <ellipse cx="12" cy="6" rx="7" ry="3" />
          <path d="M5 6v6c0 1.7 3.1 3 7 3s7-1.3 7-3V6" />
          <path d="M5 12v6c0 1.7 3.1 3 7 3s7-1.3 7-3v-6" />
        </svg>
      </div>
    );
  }

  if (type === "Factory") {
    return (
      <div className={`${base} bg-amber-50 border-amber-200 text-amber-700`}>
        <svg viewBox="0 0 24 24" className="h-5 w-5" fill="none" stroke="currentColor" strokeWidth="2">
          <path d="M3 20V8h6v4l4-3v3l4-3v11H3z" />
          <path d="M8 20V5h8v5" />
        </svg>
      </div>
    );
  }

  return (
    <div className={`${base} bg-slate-50 border-slate-200 text-slate-700`}>
      <svg viewBox="0 0 24 24" className="h-5 w-5" fill="none" stroke="currentColor" strokeWidth="2">
        <path d="M6 3h9l3 3v15H6z" />
        <path d="M15 3v3h3" />
        <path d="M9 12h6M9 16h6" />
      </svg>
    </div>
  );
}

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
              <ModuleIcon type={moduleItem.icon} />
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
