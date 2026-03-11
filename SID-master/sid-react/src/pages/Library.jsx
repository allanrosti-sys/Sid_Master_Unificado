import React, { useEffect, useMemo, useState } from "react";

export default function Library() {
  const [components, setComponents] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [filter, setFilter] = useState("");

  useEffect(() => {
    fetch("/api/library/components")
      .then((res) => {
        if (!res.ok) throw new Error("Falha ao conectar com a API");
        return res.json();
      })
      .then((data) => {
        setComponents(data);
        setLoading(false);
      })
      .catch((err) => {
        console.error(err);
        setError("Não foi possível carregar as bibliotecas. Verifique se a API está rodando.");
        setLoading(false);
      });
  }, []);

  const filtered = useMemo(() => {
    const f = filter.toLowerCase().trim();
    if (!f) return components;
    return components.filter((c) => {
      const name = (c.name || "").toLowerCase();
      const tags = Array.isArray(c.tags) ? c.tags : [];
      return name.includes(f) || tags.some((t) => String(t).toLowerCase().includes(f));
    });
  }, [components, filter]);

  if (loading) return <div className="p-8 text-center text-slate-500 animate-pulse">Carregando catálogo...</div>;

  if (error) {
    return (
      <div className="bg-red-50 border border-red-200 text-red-700 p-6 rounded-lg shadow-sm">
        <h3 className="text-lg font-bold mb-2">Erro de conexão</h3>
        <p>{error}</p>
        <button
          onClick={() => window.location.reload()}
          className="mt-4 bg-red-100 hover:bg-red-200 text-red-800 px-4 py-2 rounded transition"
        >
          Tentar novamente
        </button>
      </div>
    );
  }

  return (
    <div className="space-y-6">
      <div className="flex flex-col md:flex-row md:items-center justify-between gap-4">
        <div>
          <h2 className="text-2xl font-bold text-slate-800">Biblioteca de componentes</h2>
          <p className="text-slate-500">Gerencie e reutilize blocos de automação padronizados.</p>
        </div>
        <input
          type="text"
          placeholder="Buscar por nome ou tag..."
          className="px-4 py-2 border border-slate-300 rounded-lg w-full md:w-80 focus:ring-2 focus:ring-blue-500 focus:outline-none"
          value={filter}
          onChange={(e) => setFilter(e.target.value)}
        />
      </div>

      {filtered.length === 0 ? (
        <div className="text-center py-12 bg-white rounded-lg border border-dashed border-slate-300">
          <p className="text-slate-500 text-lg">Nenhum componente encontrado.</p>
          {filter ? (
            <button onClick={() => setFilter("")} className="text-blue-600 mt-2 hover:underline">
              Limpar filtros
            </button>
          ) : null}
        </div>
      ) : (
        <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
          {filtered.map((comp) => (
            <div
              key={comp.id}
              className="bg-white rounded-xl shadow-sm hover:shadow-md transition-all p-5 border border-slate-200 flex flex-col"
            >
              <div className="flex items-start justify-between">
                <h3 className="font-bold text-lg text-slate-800 mb-1 leading-tight">{comp.name}</h3>
                <span className="text-xs bg-slate-100 text-slate-500 px-2 py-1 rounded font-mono">
                  {comp.version ?? "—"}
                </span>
              </div>
              <p className="text-slate-600 text-sm mt-2 mb-4 flex-1 line-clamp-3">{comp.description}</p>

              <div className="flex flex-wrap gap-2 mb-4">
                {(comp.tags ?? []).map((tag) => (
                  <span
                    key={tag}
                    className="bg-blue-50 text-blue-700 text-xs px-2 py-1 rounded-md font-medium border border-blue-100"
                  >
                    {tag}
                  </span>
                ))}
              </div>

              <button className="w-full mt-auto bg-slate-50 hover:bg-slate-100 text-slate-700 border border-slate-200 py-2 rounded-lg text-sm font-semibold transition">
                Ver detalhes (MVP)
              </button>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}

