import type { ClientInput, ServerSnapshot } from "@bolasocial/shared";

export interface PredictionState {
  position: { x: number; y: number };
  velocity: { x: number; y: number };
  lastAckSeq: number;
  pending: ClientInput[];
}

export function createPredictionState(): PredictionState {
  return {
    position: { x: 400, y: 240 },
    velocity: { x: 0, y: 0 },
    lastAckSeq: 0,
    pending: []
  };
}

export function applyLocalInput(state: PredictionState, input: ClientInput, dt: number) {
  const speed = input.pressed.sprint ? 260 : 180;
  const direction = { x: 0, y: 0 };
  if (input.pressed.left) direction.x -= 1;
  if (input.pressed.right) direction.x += 1;
  if (input.pressed.up) direction.y -= 1;
  if (input.pressed.down) direction.y += 1;
  const magnitude = Math.hypot(direction.x, direction.y) || 1;
  state.velocity.x = (direction.x / magnitude) * speed;
  state.velocity.y = (direction.y / magnitude) * speed;
  state.position.x += state.velocity.x * dt;
  state.position.y += state.velocity.y * dt;
  state.pending.push(input);
}

export function reconcile(state: PredictionState, snapshot: ServerSnapshot) {
  const player = snapshot.players[0];
  if (!player) return;
  state.position.x = player.x;
  state.position.y = player.y;
  state.pending = state.pending.filter((input) => input.seq > snapshot.tick);
  state.lastAckSeq = snapshot.tick;
}
