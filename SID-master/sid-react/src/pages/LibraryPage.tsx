import React, { useState, useEffect } from 'react';

interface Component {
    id: string;
    nome: string;
    descricao: string;
    tags: string[];
}

const LibraryPage: React.FC = () => {
    const [components, setComponents] = useState<Component[]>([]);
    const [status, setStatus] = useState<'loading' | 'error' | 'success'>('loading');

    useEffect(() => {
        const fetchComponents = async () => {
            setStatus('loading');
            try {
                const response = await fetch('/api/library/components');
                if (!response.ok) {
                    throw new Error('A resposta da rede não foi OK');
                }
                const data = await response.json();
                setComponents(data);
                setStatus('success');
            } catch (error) {
                console.error("Erro ao buscar componentes:", error);
                setStatus('error');
            }
        };

        fetchComponents();
    }, []);

    const renderContent = () => {
        if (status === 'loading') {
            return <div className="text-center text-gray-500">Carregando componentes...</div>;
        }

        if (status === 'error') {
            return <div className="text-center text-red-500">Falha ao carregar componentes. Verifique se a API está no ar.</div>;
        }
        
        if (components.length === 0) {
            return <div className="text-center text-gray-500">Nenhum componente encontrado.</div>;
        }

        return (
            <div className="space-y-4">
                {components.map(component => (
                    <div key={component.id} className="p-4 bg-white rounded-lg shadow border border-gray-200">
                        <h3 className="text-lg font-semibold text-gray-800">{component.nome}</h3>
                        <p className="mt-1 text-sm text-gray-600">{component.descricao}</p>
                        <div className="mt-3 flex items-center space-x-2">
                            {component.tags.map(tag => (
                                <span key={tag} className="px-2 py-0.5 text-xs font-medium bg-blue-100 text-blue-800 rounded-full">{tag}</span>
                            ))}
                        </div>
                    </div>
                ))}
            </div>
        );
    };

    return (
        <div>
            <h1 className="text-2xl font-bold text-gray-800 mb-6">Bibliotecas de Componentes</h1>
            {renderContent()}
        </div>
    );
};

export default LibraryPage;
