import { NavLink, Outlet } from "react-router-dom";

function navClass(isActive: boolean) {
  return `block rounded-lg px-3 py-2 text-sm font-semibold transition ${
    isActive ? "bg-slate-900 text-white" : "text-slate-700 hover:bg-slate-100"
  }`;
}

export default function Layout() {
  return (
    <div className="flex h-screen bg-slate-50">
      <aside className="w-72 border-r bg-white">
        <div className="border-b p-4">
          <div className="text-lg font-extrabold text-slate-900">SID Web</div>
          <div className="text-xs text-slate-500">Portal unificado (MVP)</div>
        </div>

        <nav className="p-3 space-y-6">
          <div>
            <div className="px-2 text-xs font-bold uppercase tracking-wide text-slate-400">Visão geral</div>
            <div className="mt-2 space-y-1">
              <NavLink to="/dashboard" className={({ isActive }) => navClass(isActive)}>
                Dashboard
              </NavLink>
              <NavLink to="/integracoes" className={({ isActive }) => navClass(isActive)}>
                Integrações
              </NavLink>
            </div>
          </div>

          <div>
            <div className="px-2 text-xs font-bold uppercase tracking-wide text-slate-400">SID</div>
            <div className="mt-2 space-y-1">
              <NavLink to="/sid/modulos" className={({ isActive }) => navClass(isActive)}>
                Módulos
              </NavLink>
              <NavLink to="/bibliotecas" className={({ isActive }) => navClass(isActive)}>
                Bibliotecas
              </NavLink>
              <NavLink to="/mapa-demo" className={({ isActive }) => navClass(isActive)}>
                Mapa (demo)
              </NavLink>
            </div>
          </div>

          <div>
            <div className="px-2 text-xs font-bold uppercase tracking-wide text-slate-400">Puchta Insight</div>
            <div className="mt-2 space-y-1">
              <NavLink to="/puchta" className={({ isActive }) => navClass(isActive)}>
                Painel (original)
              </NavLink>
              <NavLink to="/puchta/acoes" className={({ isActive }) => navClass(isActive)}>
                Ações
              </NavLink>
            </div>
          </div>

          <div>
            <div className="px-2 text-xs font-bold uppercase tracking-wide text-slate-400">Portal</div>
            <div className="mt-2 space-y-1">
              <NavLink to="/configuracoes" className={({ isActive }) => navClass(isActive)}>
                Configurações
              </NavLink>
            </div>
          </div>
        </nav>
      </aside>

      <div className="flex-1 flex flex-col">
        <header className="border-b bg-white">
          <div className="flex h-16 items-center justify-between px-6">
            <div className="text-sm font-semibold text-slate-700">Puchta Engineering Platform</div>
            <div className="text-xs text-slate-500">SID Web + Puchta Insight</div>
          </div>
        </header>

        <main className="flex-1 overflow-auto p-6">
          <Outlet />
        </main>
      </div>
    </div>
  );
}
