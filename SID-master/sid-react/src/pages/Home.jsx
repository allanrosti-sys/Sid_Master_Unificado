import React from 'react';
import { Link } from 'react-router-dom';

const Home = () => {
  return (
    <div className="max-w-4xl mx-auto">
      <h2 className="text-3xl font-bold mb-6 text-gray-900">Bem-vindo ao SID Web</h2>
      <p className="text-lg text-gray-600 mb-8">
        A plataforma unificada para padronização e aceleração de desenvolvimento de automação.
      </p>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
        <div className="bg-white p-6 rounded-lg shadow-md border-l-4 border-blue-500">
          <h3 className="text-xl font-semibold mb-2">Bibliotecas</h3>
          <p className="text-gray-600 mb-4">Explore componentes padronizados para Siemens e Rockwell.</p>
          <Link to="/library" className="text-blue-600 font-medium hover:underline">Acessar Catálogo &rarr;</Link>
        </div>
        <div className="bg-white p-6 rounded-lg shadow-md border-l-4 border-purple-500">
          <h3 className="text-xl font-semibold mb-2">Visualização</h3>
          <p className="text-gray-600 mb-4">Teste a renderização de grafos de automação.</p>
          <Link to="/map-demo" className="text-purple-600 font-medium hover:underline">Ver Demo &rarr;</Link>
        </div>
      </div>
    </div>
  );
};

export default Home;