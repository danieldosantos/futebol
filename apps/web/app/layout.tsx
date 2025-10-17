import "../styles/globals.css";
import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "BolaSocial",
  description: "Futebol social online com partidas em tempo real"
};

export default function RootLayout({ children }: { children: React.ReactNode }) {
  return (
    <html lang="pt-BR">
      <body>
        <main className="min-h-screen flex flex-col">
          <header className="border-b border-slate-700 bg-slate-950/80 backdrop-blur">
            <div className="mx-auto flex w-full max-w-6xl items-center justify-between p-4">
              <div>
                <h1 className="text-2xl font-bold text-primary-foreground">BolaSocial</h1>
                <p className="text-sm text-slate-300">
                  Entre em campo com o lendário futebol social do Orkut.
                </p>
              </div>
              <nav className="flex gap-4 text-sm text-slate-300">
                <a href="/" className="hover:text-white">
                  Início
                </a>
                <a href="/lobby" className="hover:text-white">
                  Lobby
                </a>
                <a href="/ranking" className="hover:text-white">
                  Ranking
                </a>
                <a href="/team" className="hover:text-white">
                  Meu Time
                </a>
              </nav>
            </div>
          </header>
          <div className="flex-1">{children}</div>
          <footer className="border-t border-slate-800 bg-slate-950/80 p-4 text-center text-xs text-slate-400">
            © {new Date().getFullYear()} BolaSocial. Todos os direitos reservados.
          </footer>
        </main>
      </body>
    </html>
  );
}
