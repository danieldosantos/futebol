import MatchCanvas from "../../../components/MatchCanvas";
import HudOverlay from "../../../components/HudOverlay";
import { Suspense } from "react";

interface MatchPageProps {
  params: { id: string };
}

export default function MatchPage({ params }: MatchPageProps) {
  return (
    <div className="flex h-full flex-1 flex-col">
      <div className="border-b border-slate-800 bg-slate-950/80 p-4">
        <h2 className="text-xl font-semibold text-white">Partida #{params.id}</h2>
        <p className="text-xs text-slate-400">
          Engine autoritativa em modo demonstração. O cliente aplica prediction e sincroniza com snapshots fictícios.
        </p>
      </div>
      <div className="relative flex flex-1">
        <Suspense fallback={<div className="flex flex-1 items-center justify-center">Carregando...</div>}>
          <MatchCanvas matchId={params.id} />
          <HudOverlay matchId={params.id} />
        </Suspense>
      </div>
    </div>
  );
}
