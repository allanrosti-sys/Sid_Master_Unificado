import React, { useEffect, useMemo, useRef, useState } from "react";
import { Link } from "react-router-dom";
import { sidApi } from "../services/api";

export default function PuchtaIntegration() {
  const [loading, setLoading] = useState(true);
  const [config, setConfig] = useState(null);
  const [panelStatus, setPanelStatus] = useState({ ok: false, version: null });
  const [project, setProject] = useState(null);
  const [showHelp, setShowHelp] = useState(false);

  const wrapperRef = useRef(null);

  const configured = useMemo(() => Boolean(config?.puchtaPanelUrl), [config]);

  useEffect(() => {
    let cancelled = false;

    async function load() {
      try {
        const conf = await sidApi.get("/portal/settings");
        if (cancelled) return;
        setConfig(conf);

        // Importante: a validação do "ONLINE/OFFLINE" aqui depende do proxy do SID.
        // Se o proxy não estiver configurado, isso pode dar OFFLINE mesmo com o painel aberto no iframe.
        if (conf?.puchtaPanelUrl) {
          const [panelRes, projectRes] = await Promise.allSettled([
            sidApi.proxy("puchta_panel", "/api/version"),
            sidApi.proxy("puchta_panel", "/api/project-path"),
          ]);

          if (panelRes.status === "fulfilled" && panelRes.value.status === 200) {
            setPanelStatus({ ok: true, version: panelRes.value.data?.version ?? null });
          } else {
            setPanelStatus({ ok: false, version: null });
          }

          if (projectRes.status === "fulfilled" && projectRes.value.status === 200) {
            setProject(projectRes.value.data);
          } else {
            setProject(null);
          }
        }
      } catch (err) {
        console.error(err);
      } finally {
        if (!cancelled) setLoading(false);
      }
    }

    load();
    return () => {
      cancelled = true;
    };
  }, []);

  const panelUrl = config?.puchtaPanelUrl || "http://localhost:8099";
  const mapUrl = config?.puchtaFrontendUrl || "http://localhost:5173";

  async function toggleFullscreen() {
    try {
      const el = wrapperRef.current;
      if (!el) return;

      // Tela cheia ajuda a “parecer” o painel original, sem interferência do portal.
      if (document.fullscreenElement) {
        await document.exitFullscreen();
      } else {
        await el.requestFullscreen();
      }
    } catch (err) {
      console.warn("Falha ao alternar tela cheia:", err);
    }
  }

  if (loading) return <div className="p-8 text-center text-slate-500">Verificando conexão com o Puchta Insight...</div>;

  return (
    <div
      ref={wrapperRef}
      className="-m-6 h-[calc(100vh-4rem)] flex flex-col bg-white border border-slate-200 rounded-xl overflow-hidden shadow-sm"
    >
      <div className="flex items-center justify-between gap-3 px-4 py-3 border-b bg-slate-50">
        <div className="min-w-0">
          <div className="text-sm font-extrabold text-slate-900 truncate">Puchta Insight (original)</div>
          <div className="text-xs text-slate-500 truncate">
            {configured ? `URL: ${panelUrl}` : "URL não configurada (usando padrão). Ajuste em Configurações se necessário."}
          </div>
        </div>

        <div className="flex flex-wrap items-center justify-end gap-2">
          <span
            className={`px-2.5 py-1 rounded-full text-xs font-bold border ${
              panelStatus.ok ? "bg-green-50 text-green-700 border-green-200" : "bg-red-50 text-red-700 border-red-200"
            }`}
            title={panelStatus.ok ? "Consegui chamar /api/version via proxy." : "Não consegui chamar /api/version via proxy."}
          >
            {panelStatus.ok ? `ONLINE${panelStatus.version ? ` v${panelStatus.version}` : ""}` : "OFFLINE"}
          </span>

          <button
            type="button"
            onClick={toggleFullscreen}
            className="rounded-lg px-3 py-1.5 text-sm font-semibold bg-white border hover:bg-slate-100 transition"
            title="Alterna tela cheia (mantém o painel com aparência original)"
          >
            Tela cheia
          </button>

          <a
            href={mapUrl}
            target="_blank"
            rel="noreferrer"
            className="rounded-lg px-3 py-1.5 text-sm font-semibold bg-blue-600 text-white hover:bg-blue-500 transition"
            title="Abre o TiaMap em outra aba"
          >
            TiaMap
          </a>

          <Link
            to="/puchta/acoes"
            className="rounded-lg px-3 py-1.5 text-sm font-semibold bg-white border hover:bg-slate-100 transition"
            title="Executa scripts permitidos no painel do Puchta"
          >
            Ações
          </Link>

          <Link
            to="/configuracoes"
            className="rounded-lg px-3 py-1.5 text-sm font-semibold bg-white border hover:bg-slate-100 transition"
            title="Ajusta URLs e origem do proxy"
          >
            Configurações
          </Link>

          <a
            href={panelUrl}
            target="_blank"
            rel="noreferrer"
            className="rounded-lg px-3 py-1.5 text-sm font-semibold bg-slate-900 text-white hover:bg-slate-800 transition"
            title="Abre o painel original em uma nova aba (recomendado se o iframe for bloqueado)"
          >
            Abrir em nova aba
          </a>

          <button
            type="button"
            onClick={() => setShowHelp((v) => !v)}
            className="rounded-lg px-3 py-1.5 text-sm font-semibold bg-white border hover:bg-slate-100 transition"
          >
            Ajuda
          </button>
        </div>
      </div>

      {showHelp ? (
        <div className="px-4 py-3 border-b bg-amber-50 text-amber-900">
          <div className="text-sm font-bold">Dica rápida</div>
          <div className="text-sm mt-1">
            Este portal carrega o <b>painel original</b> do Puchta Insight em um iframe para preservar aparência e funcionalidades.
            Se o conteúdo não aparecer, normalmente é bloqueio por <b>X-Frame-Options</b> ou <b>CSP</b>. Nesse caso, use{" "}
            <b>“Abrir em nova aba”</b>.
          </div>
          <div className="text-xs text-amber-800 mt-2">
            Projeto (path): <span className="font-mono">{project?.tiaPath || "Não identificado"}</span>
          </div>
        </div>
      ) : null}

      {!configured ? (
        <div className="px-4 py-3 border-b bg-slate-50 text-slate-700 text-sm">
          Integração ainda não configurada. Mesmo assim o portal tenta usar <b>{panelUrl}</b>. Para evitar falhas, configure as URLs em{" "}
          <Link to="/configuracoes" className="text-blue-700 font-semibold hover:underline">
            Configurações
          </Link>
          .
        </div>
      ) : null}

      <div className="flex-1 bg-slate-100">
        <iframe title="Puchta Insight (Painel Original)" src={panelUrl} className="h-full w-full" />
      </div>
    </div>
  );
}
