import React, { useState, useEffect } from 'react';

interface ClickUpTask {
  name: string;
  time_estimate: string;
}

const ClickUpTasks: React.FC = () => {
  const [tasks, setTasks] = useState<ClickUpTask[]>([]);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchTasks = async () => {
      try {
        // TODO: Get the API URL from a configuration file.
        const response = await fetch('http://localhost:5000/api/clickup/tasks');
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const data: ClickUpTask[] = await response.json();
        setTasks(data);
      } catch (error) {
        setError('Failed to fetch tasks.');
        console.error('There was a problem with the fetch operation:', error);
      }
    };

    fetchTasks();
  }, []);

  if (error) {
    return <div className="p-4 text-red-500">{error}</div>;
  }

  if (tasks.length === 0) {
    return <div className="p-4">Loading...</div>;
  }

  return (
    <div className="p-4">
      <h2 className="text-xl font-bold mb-4">ClickUp Tasks</h2>
      <ul>
        {tasks.map((task, index) => (
          <li key={index} className="border-b p-2">
            <span className="font-semibold">{task.name}</span> - <span>{task.time_estimate}</span>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ClickUpTasks;
