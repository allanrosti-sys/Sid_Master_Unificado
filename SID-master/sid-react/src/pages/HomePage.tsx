import React from 'react';

const HomePage: React.FC = () => {
    return (
        <div className="p-4 bg-white rounded-lg shadow">
            <h1 className="text-2xl font-bold text-gray-800">Bem-vindo ao SID Web</h1>
            <p className="mt-2 text-gray-600">
                Esta é a interface unificada para as ferramentas da plataforma Puchta Engineering.
                Use a navegação lateral para explorar as funcionalidades disponíveis.
            </p>
        </div>
    );
};

export default HomePage;
