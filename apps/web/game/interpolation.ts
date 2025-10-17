import type { ServerSnapshot } from "@bolasocial/shared";

interface HistoryEntry {
  tick: number;
  snapshot: ServerSnapshot;
}

export class Interpolator {
  private readonly buffer: HistoryEntry[] = [];

  constructor(private readonly targetDelayMs: number) {}

  push(snapshot: ServerSnapshot) {
    this.buffer.push({ tick: snapshot.tick, snapshot });
    if (this.buffer.length > 60) {
      this.buffer.shift();
    }
  }

  sample(now: number) {
    const targetTime = now - this.targetDelayMs;
    const pair = findSnapshots(this.buffer, targetTime);
    if (!pair) return undefined;
    const [older, newer, alpha] = pair;
    return lerpSnapshot(older.snapshot, newer.snapshot, alpha);
  }
}

function findSnapshots(buffer: HistoryEntry[], target: number): [HistoryEntry, HistoryEntry, number] | undefined {
  if (buffer.length < 2) return undefined;
  for (let i = buffer.length - 1; i >= 1; i -= 1) {
    const newer = buffer[i]!;
    const older = buffer[i - 1]!;
    if (newer.snapshot.time.elapsedMs >= target) {
      const span = Math.max(newer.snapshot.time.elapsedMs - older.snapshot.time.elapsedMs, 1);
      const alpha = (target - older.snapshot.time.elapsedMs) / span;
      return [older, newer, alpha];
    }
  }
  return undefined;
}

function lerpSnapshot(a: ServerSnapshot, b: ServerSnapshot, alpha: number): ServerSnapshot {
  return {
    ...b,
    ball: {
      x: lerp(a.ball.x, b.ball.x, alpha),
      y: lerp(a.ball.y, b.ball.y, alpha),
      vx: lerp(a.ball.vx, b.ball.vx, alpha),
      vy: lerp(a.ball.vy, b.ball.vy, alpha)
    },
    players: b.players.map((player, index) => {
      const other = a.players[index] ?? player;
      return {
        ...player,
        x: lerp(other.x, player.x, alpha),
        y: lerp(other.y, player.y, alpha)
      };
    })
  };
}

const lerp = (a: number, b: number, t: number) => a + (b - a) * t;
