import React from "react";
import ReactFlow, { Background, Controls } from "reactflow";
import "reactflow/dist/style.css";

const initialNodes = [
  {
    id: "1",
    type: "input",
    data: { label: "Início (PLC)" },
    position: { x: 250, y: 5 },
    style: {
      background: "#fff",
      border: "1px solid #94a3b8",
      width: 180,
      borderRadius: "8px",
      padding: "10px",
      boxShadow: "0 4px 6px -1px rgb(0 0 0 / 0.1)",
    },
  },
  {
    id: "2",
    data: { label: "Bloco Motor A" },
    position: { x: 100, y: 150 },
    style: {
      background: "#eff6ff",
      border: "1px solid #3b82f6",
      width: 180,
      borderRadius: "8px",
      padding: "10px",
      color: "#1e3a8a",
    },
  },
  {
    id: "3",
    data: { label: "Bloco Válvula B" },
    position: { x: 400, y: 150 },
    style: {
      background: "#fdf4ff",
      border: "1px solid #a855f7",
      width: 180,
      borderRadius: "8px",
      padding: "10px",
      color: "#6b21a8",
    },
  },
];

const initialEdges = [
  { id: "e1-2", source: "1", target: "2", animated: true },
  { id: "e1-3", source: "1", target: "3", animated: true },
];

export default function MapDemo() {
  return (
    <div className="h-[calc(100vh-160px)] border border-slate-300 rounded-xl shadow-inner bg-white overflow-hidden">
      <div className="bg-slate-100 p-2 border-b border-slate-200 text-xs text-slate-500 flex justify-between">
        <span>Visualizador de lógica (demo ReactFlow)</span>
        <span>Use o scroll para zoom</span>
      </div>
      <ReactFlow nodes={initialNodes} edges={initialEdges} fitView>
        <Background color="#94a3b8" gap={20} size={1} />
        <Controls />
      </ReactFlow>
    </div>
  );
}

