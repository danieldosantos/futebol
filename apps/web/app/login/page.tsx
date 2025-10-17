"use client";

import { useState } from "react";

export default function LoginPage() {
  const [email, setEmail] = useState("azul@bolasocial.gg");
  const [password, setPassword] = useState("Bola@123");

  return (
    <div className="mx-auto flex w-full max-w-md flex-col gap-6 px-4 py-12">
      <header>
        <h2 className="text-3xl font-bold text-white">Entrar</h2>
        <p className="text-sm text-slate-300">Use as credenciais seed para testar rapidamente.</p>
      </header>
      <form className="flex flex-col gap-4">
        <label className="flex flex-col gap-2 text-sm text-slate-200">
          Email
          <input
            className="rounded-md border border-slate-700 bg-slate-900 px-3 py-2 text-white"
            value={email}
            onChange={(event) => setEmail(event.target.value)}
          />
        </label>
        <label className="flex flex-col gap-2 text-sm text-slate-200">
          Senha
          <input
            className="rounded-md border border-slate-700 bg-slate-900 px-3 py-2 text-white"
            type="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
          />
        </label>
        <button
          type="submit"
          className="rounded-md bg-primary px-4 py-2 text-sm font-semibold text-primary-foreground hover:bg-blue-500"
        >
          Entrar
        </button>
        <button
          type="button"
          className="rounded-md border border-slate-700 px-4 py-2 text-sm text-slate-200 hover:bg-slate-800"
        >
          Entrar com Google
        </button>
      </form>
    </div>
  );
}
