import { Controls } from "@bolasocial/shared";

export type KeyState = Record<keyof typeof Controls, boolean>;

export function createKeyState(): KeyState {
  return {
    moveUp: false,
    moveDown: false,
    moveLeft: false,
    moveRight: false,
    pass: false,
    kick: false,
    sprint: false,
    swap: false,
    slide: false
  };
}

export function bindKeyboard(target: Window, state: KeyState) {
  const onKeyDown = (event: KeyboardEvent) => {
    for (const [action, key] of Object.entries(Controls)) {
      if (event.code === key) {
        state[action as keyof typeof Controls] = true;
      }
    }
  };
  const onKeyUp = (event: KeyboardEvent) => {
    for (const [action, key] of Object.entries(Controls)) {
      if (event.code === key) {
        state[action as keyof typeof Controls] = false;
      }
    }
  };
  target.addEventListener("keydown", onKeyDown);
  target.addEventListener("keyup", onKeyUp);
  return () => {
    target.removeEventListener("keydown", onKeyDown);
    target.removeEventListener("keyup", onKeyUp);
  };
}
