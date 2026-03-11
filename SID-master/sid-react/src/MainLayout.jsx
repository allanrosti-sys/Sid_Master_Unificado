import React from 'react';
import { Link, Outlet, useLocation } from 'react-router-dom';

const MainLayout = () => {
  const location = useLocation();

  const navItems = [
    { path: '/', label: 'Início' },
    { path: '/library', label: 'Bibliotecas' },
    { path: '/map-demo', label: 'Mapa Demo' },
  ];

  return (
    <div className="flex h-screen bg-gray-100 font-sans text-gray-800">
      {/* Sidebar */}
      <aside className="w-64 bg-gray-900 text-white flex flex-col shadow-lg">
        <div className="h-16 flex items-center justify-center border-b border-gray-700">
          <h1 className="text-xl font-bold tracking-wider">SID WEB</h1>
        </div>
        <nav className="flex-1 p-4 space-y-2">
          {navItems.map((item) => (
            <Link
              key={item.path}
              to={item.path}
              className={`block px-4 py-2 rounded transition-colors duration-200 ${
                location.pathname === item.path
                  ? 'bg-blue-600 text-white'
                  : 'text-gray-400 hover:bg-gray-800 hover:text-white'
              }`}
            >
              {item.label}
            </Link>
          ))}
        </nav>
        <div className="p-4 border-t border-gray-700 text-xs text-gray-500 text-center">
          v1.0.0-mvp
        </div>
      </aside>

      {/* Main Content */}
      <div className="flex-1 flex flex-col overflow-hidden">
        <main className="flex-1 overflow-auto p-8">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default MainLayout;