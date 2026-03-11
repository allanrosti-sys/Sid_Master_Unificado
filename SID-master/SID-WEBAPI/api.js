// Utilitário para chamadas à API do SID e Proxy Puchta

export const sidApi = {
  get: async (endpoint) => {
    const res = await fetch(`/api${endpoint}`);
    if (!res.ok) throw new Error(`Erro SID API: ${res.status}`);
    return res.json();
  },
  
  post: async (endpoint, body) => {
    const res = await fetch(`/api${endpoint}`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body)
    });
    if (!res.ok) throw new Error(`Erro SID API: ${res.status}`);
    return res.json();
  },

  // Chama serviços externos via backend do SID (evita CORS)
  proxy: async (base, path, method = 'GET', body = null) => {
    const res = await fetch('/api/portal/proxy', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ base, path, method, body })
    });
    return res.json(); // Retorna o objeto envelope { status, data, target } ou erro
  }
};