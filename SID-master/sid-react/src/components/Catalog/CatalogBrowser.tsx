import React, { useState, useEffect, useMemo } from 'react';

interface ComponentParameter {
  name: string;
  type: string;
  description: string;
}

interface Component {
  name: string;
  description: string;
  category: string;
  parameters: ComponentParameter[];
  example: string;
  dependencies: string[];
}

const CatalogBrowser: React.FC = () => {
  const [catalog, setCatalog] = useState<Component[]>([]);
  const [selectedComponent, setSelectedComponent] = useState<Component | null>(null);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedCategory, setSelectedCategory] = useState<string>('All');
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchCatalog = async () => {
      try {
        // TODO: Get the API URL from a configuration file.
        const response = await fetch('http://localhost:5000/api/catalog/components');
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const data: Component[] = await response.json();
        setCatalog(data);
      } catch (error) {
        setError('Failed to fetch component catalog.');
        console.error('There was a problem with the fetch operation:', error);
      }
    };

    fetchCatalog();
  }, []);

  const categories = useMemo(() => ['All', ...new Set(catalog.map(c => c.category))], [catalog]);

  const filteredCatalog = useMemo(() => {
    return catalog.filter(component => {
      const matchesCategory = selectedCategory === 'All' || component.category === selectedCategory;
      const matchesSearch = component.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
                            component.description.toLowerCase().includes(searchTerm.toLowerCase());
      return matchesCategory && matchesSearch;
    });
  }, [catalog, searchTerm, selectedCategory]);

  if (error) {
    return <div className="p-4 text-red-500">{error}</div>;
  }

  if (catalog.length === 0) {
    return <div className="p-4">Loading...</div>;
  }

  return (
    <div className="flex h-full">
      <div className="w-1/3 bg-gray-100 p-4 overflow-y-auto">
        <h2 className="text-xl font-bold mb-4">Component Catalog</h2>
        <input
          type="text"
          placeholder="Search..."
          className="w-full p-2 mb-4 border rounded"
          onChange={e => setSearchTerm(e.target.value)}
        />
        <select
          className="w-full p-2 mb-4 border rounded"
          onChange={e => setSelectedCategory(e.target.value)}
        >
          {categories.map(category => (
            <option key={category} value={category}>{category}</option>
          ))}
        </select>
        <ul>
          {filteredCatalog.map((component) => (
            <li
              key={component.name}
              className="cursor-pointer hover:bg-gray-200 p-2 rounded"
              onClick={() => setSelectedComponent(component)}
            >
              {component.name}
            </li>
          ))}
        </ul>
      </div>
      <div className="w-2/3 p-4">
        {selectedComponent ? (
          <div>
            <h2 className="text-2xl font-bold mb-2">{selectedComponent.name}</h2>
            <p className="text-gray-600 mb-4">{selectedComponent.category}</p>
            <p className="mb-4">{selectedComponent.description}</p>
            
            <h3 className="text-lg font-semibold mt-4 mb-2">Parameters</h3>
            <ul>
              {selectedComponent.parameters.map(param => (
                <li key={param.name} className="border-b py-2">
                  <span className="font-semibold">{param.name}</span> ({param.type}): {param.description}
                </li>
              ))}
            </ul>

            <h3 className="text-lg font-semibold mt-4 mb-2">Example</h3>
            <pre className="bg-gray-200 p-4 rounded mt-2 overflow-auto">
              <code>{selectedComponent.example}</code>
            </pre>

            {selectedComponent.dependencies.length > 0 && (
                <>
                    <h3 className="text-lg font-semibold mt-4 mb-2">Dependencies</h3>
                    <ul>
                        {selectedComponent.dependencies.map(dep => (
                            <li key={dep} className="border-b py-2">{dep}</li>
                        ))}
                    </ul>
                </>
            )}

          </div>
        ) : (
          <div className="text-center text-gray-500 pt-10">
            Select a component to view its details.
          </div>
        )}
      </div>
    </div>
  );
};

export default CatalogBrowser;
