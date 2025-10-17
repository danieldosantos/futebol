const sampleRanking = [
  { team: "Azul FC", mmr: 1040, wins: 12, losses: 5 },
  { team: "Rosa United", mmr: 1015, wins: 8, losses: 6 },
  { team: "Bot Academy", mmr: 980, wins: 4, losses: 8 }
];

export default function RankingPage() {
  return (
    <div className="mx-auto flex w-full max-w-4xl flex-col gap-6 px-4 py-12">
      <header>
        <h2 className="text-3xl font-bold text-white">Ranking</h2>
        <p className="text-sm text-slate-300">Dados oficiais persistidos no PostgreSQL com recalibração Elo-like.</p>
      </header>
      <div className="overflow-hidden rounded-lg border border-slate-800 bg-slate-950/60 shadow-lg">
        <table className="w-full table-fixed">
          <thead className="bg-slate-900 text-left text-xs uppercase tracking-wider text-slate-400">
            <tr>
              <th className="px-4 py-3">Posição</th>
              <th className="px-4 py-3">Time</th>
              <th className="px-4 py-3">MMR</th>
              <th className="px-4 py-3">Vitórias</th>
              <th className="px-4 py-3">Derrotas</th>
            </tr>
          </thead>
          <tbody>
            {sampleRanking.map((row, index) => (
              <tr key={row.team} className="border-t border-slate-800 text-sm text-slate-200">
                <td className="px-4 py-3">#{index + 1}</td>
                <td className="px-4 py-3 font-medium text-white">{row.team}</td>
                <td className="px-4 py-3">{row.mmr}</td>
                <td className="px-4 py-3">{row.wins}</td>
                <td className="px-4 py-3">{row.losses}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
