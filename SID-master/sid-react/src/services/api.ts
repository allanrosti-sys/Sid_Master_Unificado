type HttpMethod = "GET" | "POST" | "PUT";

async function readError(response: Response): Promise<string> {
  try {
    const text = await response.text();
    return text || `${response.status} ${response.statusText}`;
  } catch {
    return `${response.status} ${response.statusText}`;
  }
}

async function requestJson<T>(url: string, init?: RequestInit): Promise<T> {
  const response = await fetch(url, init);
  if (!response.ok) throw new Error(await readError(response));
  return (await response.json()) as T;
}

function apiUrl(path: string): string {
  // Permite usar paths como "/health" -> "/api/health" ou "/api/health" diretamente.
  if (path.startsWith("/api/")) return path;
  if (path.startsWith("/")) return `/api${path}`;
  return `/api/${path}`;
}

export const sidApi = {
  async get<T = any>(path: string): Promise<T> {
    return await requestJson<T>(apiUrl(path), { method: "GET" });
  },

  async post<T = any>(path: string, body?: unknown): Promise<T> {
    return await requestJson<T>(apiUrl(path), {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(body ?? {}),
    });
  },

  async put<T = any>(path: string, body?: unknown): Promise<T> {
    return await requestJson<T>(apiUrl(path), {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(body ?? {}),
    });
  },

  async proxy(base: string, path: string, method: HttpMethod = "GET", body?: unknown) {
    return await requestJson<{ target: string; status: number; data: any }>("/api/portal/proxy", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ base, path, method, body }),
    });
  },
};

