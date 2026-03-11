import React, { useState, useEffect } from 'react';

const SiemensBlocks: React.FC = () => {
  const [blocks, setBlocks] = useState<string[]>([]);
  const [selectedBlock, setSelectedBlock] = useState<string | null>(null);
  const [blockContent, setBlockContent] = useState<string | null>(null);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchBlocks = async () => {
      try {
        // TODO: Get the API URL from a configuration file.
        const response = await fetch('http://localhost:5000/api/siemens/blocks');
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const data: string[] = await response.json();
        setBlocks(data);
      } catch (error) {
        setError('Failed to fetch blocks.');
        console.error('There was a problem with the fetch operation:', error);
      }
    };

    fetchBlocks();
  }, []);

  useEffect(() => {
    if (selectedBlock) {
      const fetchBlockContent = async () => {
        try {
          // TODO: Get the API URL from a configuration file.
          const response = await fetch(`http://localhost:5000/api/siemens/blocks/${selectedBlock}`);
          if (!response.ok) {
            throw new Error('Network response was not ok');
          }
          const data = await response.text();
          setBlockContent(data);
        } catch (error) {
          setError(`Failed to fetch block content for ${selectedBlock}.`);
          console.error('There was a problem with the fetch operation:', error);
        }
      };

      fetchBlockContent();
    }
  }, [selectedBlock]);


  if (error) {
    return <div className="p-4 text-red-500">{error}</div>;
  }

  if (blocks.length === 0) {
    return <div className="p-4">Loading...</div>;
  }

  return (
    <div className="flex h-full">
        <div className="w-1/4 bg-gray-100 p-4 overflow-y-auto">
            <h2 className="text-xl font-bold mb-4">Siemens Blocks</h2>
            <ul>
                {blocks.map((blockName) => (
                    <li
                        key={blockName}
                        className="cursor-pointer hover:bg-gray-200 p-2 rounded"
                        onClick={() => setSelectedBlock(blockName)}
                    >
                        {blockName}
                    </li>
                ))}
            </ul>
        </div>
        <div className="w-3/4 p-4">
            {selectedBlock ? (
                <div>
                    <h2 className="text-2xl font-bold">{selectedBlock}</h2>
                    <pre className="bg-gray-200 p-4 rounded mt-4 overflow-auto">
                        {blockContent || 'Loading...'}
                    </pre>
                </div>
            ) : (
                <div className="text-center text-gray-500 pt-10">
                    Select a block to view its content.
                </div>
            )}
        </div>
    </div>
  );
};

export default SiemensBlocks;
