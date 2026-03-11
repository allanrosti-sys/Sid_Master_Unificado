import React, { useEffect, useState } from "react";

export default function Integrations() {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const checkIntegrations = () => {
    setLoading(true);
    setError(null);
    fetch("/api/integrations/status")
      .then((res) => {
        if (!res.ok) throw new Error("Falha ao consultar /api/integrations/status");
        return res.json();
      })
      .then((payload) => {
        setData(payload);
        setLoading(false);
      })
      .catch((err) => {
        console.error(err);
        setError("Não foi possível consultar as integrações. Verifique se a API está no ar.");
        setLoading(false);
      });
  };

  useEffect(() => {
    checkIntegrations();
  }, []);

  return (
    <div className="max-w-4xl mx-auto space-y-6">
      <div className="flex items-center justify-between">
        <div>
          <h1 className="text-2xl font-bold text-slate-800">Status das integrações</h1>
          <p className="text-slate-500">Monitoramento (MVP) dos serviços conectados.</p>
        </div>
        <button
          onClick={checkIntegrations}
          className="bg-white border border-slate-300 text-slate-700 px-4 py-2 rounded-lg hover:bg-slate-50 transition shadow-sm font-medium"
        >
          {loading ? "Verificando..." : "Atualizar"}
        </button>
      </div>

      {error ? (
        <div className="bg-red-50 border border-red-200 text-red-700 p-5 rounded-xl">{error}</div>
      ) : null}

      <div className="space-y-4">
        {loading && !data ? (
          <div className="p-8 text-center text-slate-400">Consultando serviços externos...</div>
        ) : null}

        {(data?.modules ?? []).map((mod, index) => (
          <div
            key={index}
            className={`bg-white border-l-4 rounded-r-lg shadow-sm p-6 flex items-start justify-between ${
              mod.status === "online"
                ? "border-green-500"
                : mod.status === "warning"
                  ? "border-yellow-500"
                  : "border-red-500"
            }`}
          >
            <div>
              <h3 className="text-lg font-bold text-slate-800">{mod.module}</h3>
              <p className="text-slate-600 mt-1">{mod.details}</p>
              <p className="text-xs text-slate-400 mt-3">
                Última verificação: {mod.lastCheck ? new Date(mod.lastCheck).toLocaleTimeString() : "—"}
              </p>
            </div>
            <div
              className={`px-4 py-1 rounded-full text-sm font-bold border capitalize ${
                mod.status === "online"
                  ? "bg-green-50 text-green-700 border-green-200"
                  : mod.status === "warning"
                    ? "bg-yellow-50 text-yellow-700 border-yellow-200"
                    : "bg-red-50 text-red-700 border-red-200"
              }`}
            >
              {mod.status}
            </div>
          </div>
        ))}

        {!loading && (data?.modules?.length ?? 0) === 0 ? (
          <div className="p-8 text-center text-slate-400">Nenhuma integração encontrada.</div>
        ) : null}
      </div>
    </div>
  );
}

