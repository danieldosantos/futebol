"use client";

const rooms = [
  { id: "dev-room", players: 2, bots: 0, latency: 32 },
  { id: "training-room", players: 1, bots: 4, latency: 48 }
];

export default function GameMasterPage() {
  return (
    <div className="mx-auto flex w-full max-w-4xl flex-col gap-6 px-4 py-12">
      <header>
        <h2 className="text-3xl font-bold text-white">Painel Game Master</h2>
        <p className="text-sm text-slate-300">
          Monitore salas em tempo real, injete bots ou encerre partidas problemáticas.
        </p>
      </header>
      <div className="rounded-lg border border-slate-800 bg-slate-950/50 shadow">
        <table className="w-full table-fixed">
          <thead className="bg-slate-900 text-left text-xs uppercase tracking-wider text-slate-400">
            <tr>
              <th className="px-4 py-3">Sala</th>
              <th className="px-4 py-3">Jogadores</th>
              <th className="px-4 py-3">Bots</th>
              <th className="px-4 py-3">Latência</th>
              <th className="px-4 py-3">Ações</th>
            </tr>
          </thead>
          <tbody>
            {rooms.map((room) => (
              <tr key={room.id} className="border-t border-slate-800 text-sm text-slate-200">
                <td className="px-4 py-3 font-mono text-white">{room.id}</td>
                <td className="px-4 py-3">{room.players}</td>
                <td className="px-4 py-3">{room.bots}</td>
                <td className="px-4 py-3">{room.latency} ms</td>
                <td className="px-4 py-3">
                  <div className="flex gap-2">
                    <button className="rounded border border-slate-700 px-3 py-1 text-xs text-slate-200 hover:bg-slate-800">
                      Encerrar
                    </button>
                    <button className="rounded border border-slate-700 px-3 py-1 text-xs text-slate-200 hover:bg-slate-800">
                      Injetar bot
                    </button>
                  </div>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
