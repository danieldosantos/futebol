import Link from "next/link";

export default function HomePage() {
  return (
    <section className="mx-auto flex w-full max-w-5xl flex-col gap-8 px-4 py-16">
      <div className="flex flex-col gap-4">
        <h2 className="text-4xl font-extrabold tracking-tight text-white">
          O futebol social voltou
        </h2>
        <p className="max-w-2xl text-lg text-slate-300">
          Reviva o clássico Bola Social com partidas online em tempo real, matchmaking
          inteligente, ranking competitivo e economia interna baseada em moedas.
        </p>
        <div className="flex gap-3">
          <Link
            className="rounded-md bg-primary px-6 py-3 text-sm font-semibold text-primary-foreground shadow hover:bg-blue-500"
            href="/lobby"
          >
            Jogar agora
          </Link>
          <Link
            className="rounded-md border border-slate-700 px-6 py-3 text-sm font-semibold text-slate-200 hover:bg-slate-800/80"
            href="/docs/overview"
          >
            Ver documentação
          </Link>
        </div>
      </div>
      <div className="grid gap-6 md:grid-cols-3">
        {features.map((feature) => (
          <article key={feature.title} className="rounded-lg border border-slate-800 bg-slate-950/60 p-5 shadow-lg">
            <h3 className="text-lg font-semibold text-white">{feature.title}</h3>
            <p className="mt-2 text-sm text-slate-300">{feature.description}</p>
          </article>
        ))}
      </div>
    </section>
  );
}

const features = [
  {
    title: "Netcode autoritativo",
    description: "Prediction, reconciliação e interpolação com snapshots de 20Hz."
  },
  {
    title: "Social completo",
    description: "Login social, lista de amigos, chat de lobby e ranking entre amigos."
  },
  {
    title: "Economia e progressão",
    description: "Ganhe moedas, desbloqueie uniformes, escudos e treinos especiais."
  }
];
