import React, { useState } from 'react';
import templates from '../../data/template-catalog.json';

interface Template {
  name: string;
  description: string;
  components: string[];
}

const ProjectWizard: React.FC = () => {
  const [step, setStep] = useState(1);
  const [selectedTemplate, setSelectedTemplate] = useState<Template | null>(null);
  const [projectName, setProjectName] = useState('');
  const [projectDescription, setProjectDescription] = useState('');

  const handleNext = () => setStep(step + 1);
  const handleBack = () => setStep(step - 1);

  const handleGenerate = () => {
    const project = {
      name: projectName,
      description: projectDescription,
      template: selectedTemplate?.name,
      components: selectedTemplate?.components,
      createdAt: new Date().toISOString(),
    };
    
    // Em um produto final, isso seria enviado para a API do backend.
    // No MVP, registramos no console e avançamos para uma tela de sucesso.
    console.log('Generated project:', project);
    setStep(step + 1);
  };
  
  switch (step) {
    case 1: // Welcome
      return (
        <div className="p-8 text-center">
          <h1 className="text-3xl font-bold mb-4">Criar Novo Projeto</h1>
          <p className="mb-8">Bem-vindo ao assistente de criação de projetos. Vamos começar!</p>
          <button onClick={handleNext} className="bg-blue-600 text-white px-6 py-2 rounded">
            Iniciar
          </button>
        </div>
      );

    case 2: // Select Template
      return (
        <div className="p-8">
          <h2 className="text-2xl font-bold mb-4">Passo 1: Selecione um Template</h2>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            {templates.map(template => (
              <div
                key={template.name}
                className={`p-4 border rounded cursor-pointer ${selectedTemplate?.name === template.name ? 'border-blue-600' : ''}`}
                onClick={() => setSelectedTemplate(template)}
              >
                <h3 className="text-xl font-semibold">{template.name}</h3>
                <p>{template.description}</p>
              </div>
            ))}
          </div>
          <div className="mt-8 flex justify-between">
            <button onClick={handleBack} className="bg-gray-300 px-6 py-2 rounded">
              Voltar
            </button>
            <button onClick={handleNext} disabled={!selectedTemplate} className="bg-blue-600 text-white px-6 py-2 rounded disabled:bg-gray-400">
              Próximo
            </button>
          </div>
        </div>
      );

    case 3: // Project Details
      return (
        <div className="p-8">
          <h2 className="text-2xl font-bold mb-4">Passo 2: Detalhes do Projeto</h2>
          <div className="mb-4">
            <label className="block mb-2">Nome do Projeto</label>
            <input
              type="text"
              value={projectName}
              onChange={e => setProjectName(e.target.value)}
              className="w-full p-2 border rounded"
            />
          </div>
          <div className="mb-4">
            <label className="block mb-2">Descrição do Projeto</label>
            <textarea
              value={projectDescription}
              onChange={e => setProjectDescription(e.target.value)}
              className="w-full p-2 border rounded"
            />
          </div>
          <div className="mt-8 flex justify-between">
            <button onClick={handleBack} className="bg-gray-300 px-6 py-2 rounded">
              Voltar
            </button>
            <button onClick={handleNext} disabled={!projectName} className="bg-blue-600 text-white px-6 py-2 rounded disabled:bg-gray-400">
              Próximo
            </button>
          </div>
        </div>
      );

    case 4: // Review and Generate
      return (
        <div className="p-8">
          <h2 className="text-2xl font-bold mb-4">Passo 3: Revisão</h2>
          <p><strong>Nome:</strong> {projectName}</p>
          <p><strong>Descrição:</strong> {projectDescription}</p>
          <p><strong>Template:</strong> {selectedTemplate?.name}</p>
          <div className="mt-8 flex justify-between">
            <button onClick={handleBack} className="bg-gray-300 px-6 py-2 rounded">
              Voltar
            </button>
            <button onClick={handleGenerate} className="bg-green-600 text-white px-6 py-2 rounded">
              Gerar Projeto
            </button>
          </div>
        </div>
      );
    
    case 5: // Success
        return (
            <div className="p-8 text-center">
                <h1 className="text-3xl font-bold mb-4 text-green-600">Projeto Gerado com Sucesso!</h1>
                <p>O seu novo projeto foi gerado. Verifique o console para ver os detalhes.</p>
                <button onClick={() => setStep(1)} className="bg-blue-600 text-white px-6 py-2 rounded mt-8">
                    Criar Outro Projeto
                </button>
            </div>
        );

    default:
      return null;
  }
};

export default ProjectWizard;
