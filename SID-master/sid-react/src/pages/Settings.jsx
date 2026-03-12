import React, { useEffect, useState } from "react";
import { sidApi } from "../services/api";

const InputField = ({ label, name, value, onChange, placeholder }) => (
  <div>
    <label className="block text-sm font-medium text-slate-700 mb-1">{label}</label>
    <input
      type="text"
      name={name}
      value={value}
      onChange={onChange}
      placeholder={placeholder}
      className="w-full px-3 py-2 border border-slate-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:outline-none"
    />
  </div>
);

export default function Settings() {
  const [config, setConfig] = useState({
    puchtaPanelUrl: "",
    puchtaBackendUrl: "",
    puchtaFrontendUrl: "",
  });
  const [loading, setLoading] = useState(true);
  const [message, setMessage] = useState(null);
  const [testResult, setTestResult] = useState(null);

  useEffect(() => {
    loadSettings();
  }, []);

  const loadSettings = async () => {
    try {
      const data = await sidApi.get("/portal/settings");
      setConfig(data);
    } catch (error) {
      console.error(error);
      setMessage({ type: "error", text: "Falha ao carregar configuracoes." });
    } finally {
      setLoading(false);
    }
  };

  const handleSave = async (event) => {
    event.preventDefault();
    setMessage(null);
    setTestResult(null);

    try {
      await sidApi.put("/portal/settings", config);
      setMessage({ type: "success", text: "Configuracoes salvas com sucesso." });
    } catch (error) {
      console.error(error);
      setMessage({ type: "error", text: "Erro ao salvar configuracoes." });
    }
  };

  const handleChange = (event) => {
    setConfig({ ...config, [event.target.name]: event.target.value });
  };

  const testPanel = async () => {
    setTestResult(null);
    try {
      const result = await sidApi.proxy("puchta_panel", "/api/version");
      setTestResult({ ok: result.status === 200, data: result.data });
    } catch (error) {
      setTestResult({ ok: false, data: error.message });
    }
  };

  if (loading) {
    return <div className="p-8 text-center text-slate-500">Carregando configuracoes...</div>;
  }

  return (
    <div className="max-w-3xl mx-auto space-y-6">
      <div className="bg-white p-8 rounded-xl shadow-sm border border-slate-200">
        <h2 className="text-2xl font-bold text-slate-800 mb-2">Configuracoes do Portal</h2>
        <p className="text-sm text-slate-500">Configure o Puchta Insight para habilitar chamadas via proxy no SID Web.</p>

        {message && (
          <div
            className={`p-4 mt-6 rounded-md ${
              message.type === "success" ? "bg-green-50 text-green-700" : "bg-red-50 text-red-700"
            }`}
          >
            {message.text}
          </div>
        )}

        <form onSubmit={handleSave} className="space-y-6 mt-6">
          <div className="space-y-4">
            <h3 className="font-semibold text-slate-700 border-b pb-2">Integracao Puchta Insight</h3>
            <InputField
              label="URL do Painel (Logs/WebServer)"
              name="puchtaPanelUrl"
              value={config.puchtaPanelUrl}
              onChange={handleChange}
              placeholder="http://localhost:8099"
            />
            <InputField
              label="URL do Backend (FastAPI)"
              name="puchtaBackendUrl"
              value={config.puchtaBackendUrl}
              onChange={handleChange}
              placeholder="http://localhost:8021"
            />
            <InputField
              label="URL do Frontend (Vite)"
              name="puchtaFrontendUrl"
              value={config.puchtaFrontendUrl}
              onChange={handleChange}
              placeholder="http://localhost:5173"
            />
          </div>

          <div className="pt-2 flex flex-wrap justify-end gap-3">
            <button
              type="button"
              onClick={testPanel}
              className="bg-slate-100 hover:bg-slate-200 text-slate-800 px-6 py-2 rounded-lg font-semibold transition"
            >
              Testar painel
            </button>
            <button
              type="submit"
              className="bg-blue-600 hover:bg-blue-700 text-white px-6 py-2 rounded-lg font-semibold transition"
            >
              Salvar alteracoes
            </button>
          </div>
        </form>
      </div>

      {testResult ? (
        <div className={`bg-white p-6 rounded-xl shadow-sm border ${testResult.ok ? "border-green-200" : "border-red-200"}`}>
          <div className="font-bold text-slate-800 mb-2">Resultado do teste</div>
          <pre className="text-xs font-mono overflow-auto">{JSON.stringify(testResult.data, null, 2)}</pre>
        </div>
      ) : null}
    </div>
  );
}
