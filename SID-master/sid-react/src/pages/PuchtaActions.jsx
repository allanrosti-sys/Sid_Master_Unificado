import React, { useMemo, useState } from "react";
import { sidApi } from "../services/api";

const allowedActions = [
  {
    id: "run-tiamap-dev",
    name: "Iniciar TiaMap (Dev)",
    script: "Run-TiaMap-Dev.ps1",
    desc: "Sobe Frontend (5173) e Backend (8021).",
  },
  {
    id: "export-attach",
    name: "Exportar XML (Attach)",
    script: "RunExporterWithAttach.ps1",
    desc: "Exporta blocos via TIA Openness usando instância aberta.",
  },
  {
    id: "doc-siemens",
    name: "Gerar Documentação (Siemens)",
    script: "Generate-Documentation.ps1",
    desc: "Gera documentação HTML a partir da origem Siemens.",
  },
  {
    id: "doc-rockwell",
    name: "Gerar Documentação (Rockwell)",
    script: "Generate-Documentation-Rockwell.ps1",
    desc: "Gera documentação HTML a partir de arquivo L5X/L5K.",
  },
  {
    id: "full-cycle",
    name: "Executar Ciclo Completo",
    script: "Run-Full-Cycle.ps1",
    desc: "Executa o ciclo operacional completo do painel Puchta (se habilitado).",
  },
  {
    id: "import-blocks",
    name: "Importar Blocos",
    script: "Import-New-Blocks.ps1",
    desc: "Importa blocos de volta ao projeto (quando aplicável).",
  },
];

export default function PuchtaActions() {
  const [logs, setLogs] = useState([]);
  const [executing, setExecuting] = useState(false);

  const sortedLogs = useMemo(() => logs.slice(0, 200), [logs]);

  const addLog = (text, type = "default") => {
    setLogs((prev) => [{ time: new Date().toLocaleTimeString(), text, type }, ...prev]);
  };

  const executeAction = async (action) => {
    setExecuting(true);
    addLog(`Iniciando ação: ${action.name}...`, "info");

    try {
      const res = await sidApi.proxy("puchta_panel", "/api/run", "POST", { script: action.script });
      if (res.status >= 200 && res.status < 300) {
        addLog("Comando enviado com sucesso.", "success");
        addLog(JSON.stringify(res.data, null, 2), "default");
      } else {
        addLog(`Erro ao executar: HTTP ${res.status}`, "error");
        addLog(JSON.stringify(res.data, null, 2), "error");
      }
    } catch (error) {
      addLog(`Falha na comunicação: ${error.message}`, "error");
    } finally {
      setExecuting(false);
    }
  };

  return (
    <div className="space-y-6 max-w-6xl mx-auto">
      <div className="flex justify-between items-center">
        <div>
          <h1 className="text-2xl font-bold text-slate-800">Ações do Puchta Insight</h1>
          <p className="text-slate-500">
            Dispare automações remotamente no painel do Puchta (via proxy do SID, sem CORS).
          </p>
        </div>
        <button onClick={() => setLogs([])} className="text-sm text-slate-400 hover:text-slate-600">
          Limpar logs
        </button>
      </div>

      <div className="grid md:grid-cols-2 gap-6">
        <div className="space-y-4">
          {allowedActions.map((action) => (
            <div
              key={action.id}
              className="bg-white p-4 rounded-lg shadow-sm border border-slate-200 flex justify-between items-center"
            >
              <div>
                <h3 className="font-semibold text-slate-800">{action.name}</h3>
                <p className="text-xs text-slate-500">{action.desc}</p>
                <p className="text-[11px] text-slate-400 font-mono mt-1">Script: {action.script}</p>
              </div>
              <button
                onClick={() => executeAction(action)}
                disabled={executing}
                className="bg-blue-50 text-blue-700 px-3 py-1.5 rounded text-sm font-semibold hover:bg-blue-100 disabled:opacity-50 transition"
              >
                {executing ? "..." : "Executar"}
              </button>
            </div>
          ))}
        </div>

        <div className="bg-slate-900 rounded-lg p-4 h-[420px] overflow-auto font-mono text-sm shadow-inner">
          <div className="text-slate-400 mb-2 border-b border-slate-700 pb-1">Console de saída</div>
          {sortedLogs.length === 0 && <div className="text-slate-600 italic">Aguardando comandos...</div>}
          {sortedLogs.map((log, i) => (
            <div
              key={i}
              className={`mb-1 ${
                log.type === "error"
                  ? "text-red-400"
                  : log.type === "success"
                    ? "text-green-400"
                    : log.type === "info"
                      ? "text-blue-400"
                      : "text-slate-300"
              }`}
            >
              <span className="opacity-50 mr-2">[{log.time}]</span>
              {log.text}
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}

