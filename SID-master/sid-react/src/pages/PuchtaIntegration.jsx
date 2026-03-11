import React, { useEffect, useState } from "react";
import { sidApi } from "../services/api";

export default function PuchtaIntegration() {
  const [loading, setLoading] = useState(true);
  const [panelUrl, setPanelUrl] = useState("http://localhost:8099");

  useEffect(() => {
    let cancelled = false;

    async function load() {
      try {
        const conf = await sidApi.get("/portal/settings");
        if (cancelled) return;
        if (conf?.puchtaPanelUrl) setPanelUrl(conf.puchtaPanelUrl);
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

  // Mantém o painel 100% original: sem botões/skin do portal.
  if (loading) return <div className="p-8 text-center text-slate-500">Carregando painel do Puchta Insight...</div>;

  return (
    <div className="-m-6 h-[calc(100vh-4rem)] bg-white">
      <iframe title="Puchta Insight (Painel Original)" src={panelUrl} className="h-full w-full border-0" />
    </div>
  );
}
