"use client";

import { useEffect, useState } from "react";

interface HudOverlayProps {
  matchId: string;
}

export default function HudOverlay({ matchId }: HudOverlayProps) {
  const [elapsed, setElapsed] = useState(0);

  useEffect(() => {
    const id = setInterval(() => setElapsed((prev) => prev + 1), 1000);
    return () => clearInterval(id);
  }, []);

  return (
    <div className="pointer-events-none absolute inset-0 flex flex-col justify-between p-4 text-white">
      <header className="flex items-center justify-between text-sm">
        <div>
          <p className="text-xs uppercase tracking-wide text-slate-300">Partida</p>
          <p className="font-semibold">#{matchId}</p>
        </div>
        <div className="flex flex-col items-center">
          <span className="text-xs text-slate-300">Tempo</span>
          <span className="font-mono text-lg">{formatTime(elapsed)}</span>
        </div>
        <div className="text-right">
          <p className="text-xs uppercase tracking-wide text-slate-300">Placar</p>
          <p className="font-semibold">Azul 0 - 0 Rosa</p>
        </div>
      </header>
      <footer className="grid grid-cols-2 gap-2 text-xs text-slate-300">
        <div className="rounded bg-slate-950/60 p-3">
          <p className="font-semibold text-white">Controles</p>
          <ul className="mt-1 space-y-1">
            <li>WASD → mover</li>
            <li>Z → chutar (segure para carregar)</li>
            <li>X → passar</li>
            <li>C → correr</li>
            <li>Espaço → trocar jogador</li>
            <li>V → carrinho</li>
          </ul>
        </div>
        <div className="rounded bg-slate-950/60 p-3">
          <p className="font-semibold text-white">Estado</p>
          <p>Stamina: 82%</p>
          <p>Latência: 34 ms</p>
          <p>Previsão ativa</p>
        </div>
      </footer>
    </div>
  );
}

function formatTime(seconds: number) {
  const mm = String(Math.floor(seconds / 60)).padStart(2, "0");
  const ss = String(seconds % 60).padStart(2, "0");
  return `${mm}:${ss}`;
}
