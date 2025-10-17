import { z } from "zod";

export const InputSchema = z.object({
  matchId: z.string(),
  playerId: z.string(),
  seq: z.number().int().nonnegative(),
  ts: z.number().int(),
  pressed: z.object({
    up: z.boolean(),
    down: z.boolean(),
    left: z.boolean(),
    right: z.boolean(),
    pass: z.boolean(),
    kick: z.boolean(),
    sprint: z.boolean(),
    swap: z.boolean(),
    slide: z.boolean().optional().default(false)
  }),
  charge: z.object({
    kick: z.number().min(0).max(1)
  })
});

export type ClientInput = z.infer<typeof InputSchema>;

export const SnapshotSchema = z.object({
  tick: z.number().int().nonnegative(),
  ball: z.object({ x: z.number(), y: z.number(), vx: z.number(), vy: z.number() }),
  players: z.array(
    z.object({
      id: z.string(),
      x: z.number(),
      y: z.number(),
      vx: z.number(),
      vy: z.number(),
      dir: z.number(),
      st: z.string(),
      stamina: z.number(),
      hasBall: z.boolean()
    })
  ),
  score: z.object({ home: z.number(), away: z.number() }),
  time: z.object({ elapsedMs: z.number(), half: z.number() }),
  events: z.array(z.record(z.any()))
});

export type ServerSnapshot = z.infer<typeof SnapshotSchema>;

export const MatchEventKinds = {
  Pass: "pass",
  Goal: "goal",
  Steal: "steal",
  Out: "out"
} as const;

export type MatchEventKind = (typeof MatchEventKinds)[keyof typeof MatchEventKinds];

export interface MatchSummaryDto {
  id: string;
  mode: "1v1" | "5v5";
  startedAt: string;
  finishedAt: string | null;
  homeTeamId: string | null;
  awayTeamId: string | null;
  resultJson: string;
  replayRef?: string | null;
}

export interface RankingDto {
  teamId: string;
  mmr: number;
  wins: number;
  losses: number;
  draws: number;
  streak: number;
}

export interface TeamDto {
  id: string;
  name: string;
  kitPrimary: string;
  kitSecondary: string;
  logoUrl: string;
}

export const Controls = {
  moveUp: "KeyW",
  moveDown: "KeyS",
  moveLeft: "KeyA",
  moveRight: "KeyD",
  pass: "KeyX",
  kick: "KeyZ",
  sprint: "KeyC",
  swap: "Space",
  slide: "KeyV"
} as const;

export type ControlAction = keyof typeof Controls;

export const SnapshotRateHz = 20;
export const TickRateHz = 60;
export const InterpolationBufferMs = 100;
