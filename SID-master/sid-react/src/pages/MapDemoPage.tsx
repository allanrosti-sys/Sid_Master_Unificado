import React, { useCallback } from 'react';
import ReactFlow, {
  MiniMap,
  Controls,
  Background,
  useNodesState,
  useEdgesState,
  addEdge,
  BackgroundVariant,
} from 'reactflow';
import type { Connection, Edge } from 'reactflow';
import 'reactflow/dist/style.css';

const initialNodes = [
  { id: '1', position: { x: 100, y: 100 }, data: { label: 'Nó Inicial' }, style: { borderColor: '#16a34a', borderWidth: 2 } },
  { id: '2', position: { x: 400, y: 100 }, data: { label: 'Nó Final' } },
];
const initialEdges = [{ id: 'e1-2', source: '1', target: '2', animated: true }];

const MapDemoPage: React.FC = () => {
    const [nodes, , onNodesChange] = useNodesState(initialNodes);
    const [edges, setEdges, onEdgesChange] = useEdgesState(initialEdges);

    const onConnect = useCallback(
        (params: Edge | Connection) => setEdges((eds) => addEdge(params, eds)),
        [setEdges],
    );

    return (
        <div className="h-full w-full bg-white rounded-lg shadow">
            <ReactFlow
                nodes={nodes}
                edges={edges}
                onNodesChange={onNodesChange}
                onEdgesChange={onEdgesChange}
                onConnect={onConnect}
                fitView
            >
                <Controls />
                <MiniMap />
                <Background variant={BackgroundVariant.Dots} gap={12} size={1} />
            </ReactFlow>
        </div>
    );
};

export default MapDemoPage;
