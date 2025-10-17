"use client";

import { useState } from "react";

export default function TeamPage() {
  const [teamName, setTeamName] = useState("Azul FC");
  const [primary, setPrimary] = useState("#2563eb");
  const [secondary, setSecondary] = useState("#fbbf24");

  return (
    <div className="mx-auto flex w-full max-w-3xl flex-col gap-6 px-4 py-12">
      <header>
        <h2 className="text-3xl font-bold text-white">Customizar time</h2>
        <p className="text-sm text-slate-300">
          Personalize uniformes, escudo e escalação. Alterações são sincronizadas com a API.
        </p>
      </header>
      <form className="grid gap-4 rounded-lg border border-slate-800 bg-slate-950/60 p-6 shadow-lg">
        <label className="flex flex-col gap-2 text-sm text-slate-200">
          Nome do time
          <input
            className="rounded-md border border-slate-700 bg-slate-900 px-3 py-2 text-white"
            value={teamName}
            onChange={(event) => setTeamName(event.target.value)}
          />
        </label>
        <div className="flex flex-wrap gap-4">
          <label className="flex flex-col gap-2 text-sm text-slate-200">
            Cor primária
            <input
              className="h-12 w-24 cursor-pointer rounded border border-slate-700"
              type="color"
              value={primary}
              onChange={(event) => setPrimary(event.target.value)}
            />
          </label>
          <label className="flex flex-col gap-2 text-sm text-slate-200">
            Cor secundária
            <input
              className="h-12 w-24 cursor-pointer rounded border border-slate-700"
              type="color"
              value={secondary}
              onChange={(event) => setSecondary(event.target.value)}
            />
          </label>
        </div>
        <button
          type="submit"
          className="rounded-md bg-primary px-4 py-2 text-sm font-semibold text-primary-foreground hover:bg-blue-500"
        >
          Salvar alterações
        </button>
      </form>
      <section className="rounded-lg border border-slate-800 bg-slate-950/50 p-4">
        <h3 className="text-lg font-semibold text-white">Prévia</h3>
        <div className="mt-3 flex gap-6">
          <div className="flex h-24 w-24 items-center justify-center rounded-lg border border-slate-700" style={{ background: primary }}>
            <span className="text-sm font-bold text-black">Home</span>
          </div>
          <div className="flex h-24 w-24 items-center justify-center rounded-lg border border-slate-700" style={{ background: secondary }}>
            <span className="text-sm font-bold text-black">Away</span>
          </div>
        </div>
      </section>
    </div>
  );
}
