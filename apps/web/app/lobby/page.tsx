"use client";

import { useState } from "react";

const modes = [
  { id: "1v1", label: "Duelo 1x1" },
  { id: "5v5", label: "Clássico 5x5" }
];

export default function LobbyPage() {
  const [mode, setMode] = useState("1v1");
  const [queued, setQueued] = useState(false);

  return (
    <div className="mx-auto flex w-full max-w-3xl flex-col gap-6 px-4 py-12">
      <header className="flex flex-col gap-2">
        <h2 className="text-3xl font-bold text-white">Lobby</h2>
        <p className="text-sm text-slate-300">
          Entre na fila para encontrar jogadores ou bots. O matchmaking utiliza Redis e MMR simplificado.
        </p>
      </header>
      <section className="rounded-lg border border-slate-800 bg-slate-950/50 p-6 shadow-lg">
        <div className="flex flex-col gap-4">
          <label className="flex flex-col gap-2 text-sm text-slate-200">
            Modo de jogo
            <select
              className="rounded-md border border-slate-700 bg-slate-900 px-3 py-2 text-white"
              value={mode}
              onChange={(event) => setMode(event.target.value)}
            >
              {modes.map((option) => (
                <option key={option.id} value={option.id}>
                  {option.label}
                </option>
              ))}
            </select>
          </label>
          <button
            className="rounded-md bg-primary px-4 py-2 text-sm font-semibold text-primary-foreground hover:bg-blue-500"
            onClick={() => setQueued((previous) => !previous)}
          >
            {queued ? "Sair da fila" : "Entrar na fila"}
          </button>
          <div className="rounded-md border border-dashed border-slate-700 p-4 text-sm text-slate-300">
            {queued
              ? "Buscando adversários... latência média: 35 ms. Previsão: partida em ~15 segundos."
              : "Nenhuma busca ativa. Clique em 'Entrar na fila' para começar."}
          </div>
        </div>
      </section>
      <section className="rounded-lg border border-slate-800 bg-slate-950/50 p-6 shadow">
        <h3 className="text-lg font-semibold text-white">Chat do Lobby</h3>
        <p className="mt-2 text-sm text-slate-400">
          O chat em tempo real será sincronizado via SignalR. Utilize-o para combinar partidas com amigos.
        </p>
      </section>
    </div>
  );
}
