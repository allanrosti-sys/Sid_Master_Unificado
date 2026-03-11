import React, { useState, useEffect } from 'react';

interface ComplexTablesData {
  classificacoes: string[];
  controlModules: string[];
  phases: string[];
}

const ComplexTables: React.FC = () => {
  const [tables, setTables] = useState<ComplexTablesData | null>(null);
  const [selectedTable, setSelectedTable] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchTables = async () => {
      try {
        // TODO: Get the API URL from a configuration file.
        const response = await fetch('http://localhost:5000/api/complex/tables');
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const data: ComplexTablesData = await response.json();
        setTables(data);
      } catch (error) {
        setError('Failed to fetch tables.');
        console.error('There was a problem with the fetch operation:', error);
      }
    };

    fetchTables();
  }, []);

  if (error) {
    return <div className="p-4 text-red-500">{error}</div>;
  }

  if (!tables) {
    return <div className="p-4">Loading...</div>;
  }

  const renderTableList = (title: string, tableNames: string[]) => (
    <div key={title}>
      <h3 className="text-lg font-semibold mt-4 mb-2">{title}</h3>
      <ul>
        {tableNames.map((tableName) => (
          <li
            key={tableName}
            className="cursor-pointer hover:bg-gray-200 p-2 rounded"
            onClick={() => setSelectedTable(tableName)}
          >
            {tableName}
          </li>
        ))}
      </ul>
    </div>
  );

  return (
    <div className="flex h-full">
      <div className="w-1/4 bg-gray-100 p-4 overflow-y-auto">
        <h2 className="text-xl font-bold mb-4">Complex Tables</h2>
        {renderTableList('Classificações', tables.classificacoes)}
        {renderTableList('Control Modules', tables.controlModules)}
        {renderTableList('Phases', tables.phases)}
      </div>
      <div className="w-3/4 p-4">
        {selectedTable ? (
          <div>
            <h2 className="text-2xl font-bold">{selectedTable}</h2>
            {/* TODO: Fetch and display table data here */}
          </div>
        ) : (
          <div className="text-center text-gray-500 pt-10">
            Select a table to view its content.
          </div>
        )}
      </div>
    </div>
  );
};

export default ComplexTables;
