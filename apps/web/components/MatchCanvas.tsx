"use client";

import { useEffect, useRef } from "react";
import Phaser from "phaser";
import { Controls } from "@bolasocial/shared";

interface MatchCanvasProps {
  matchId: string;
}

class DemoScene extends Phaser.Scene {
  private ball!: Phaser.GameObjects.Arc;
  private cursorKeys!: Phaser.Types.Input.Keyboard.CursorKeys;
  private speed = 200;

  constructor() {
    super("DemoScene");
  }

  preload() {}

  create() {
    this.ball = this.add.circle(400, 240, 14, 0xf8fafc);
    this.add.rectangle(400, 240, 740, 420, 0x1e293b, 0.3).setStrokeStyle(4, 0x38bdf8);
    this.cursorKeys = this.input.keyboard!.createCursorKeys();
    this.input.keyboard!.addKeys(Controls);
  }

  update(_: number, delta: number) {
    if (!this.cursorKeys) return;
    const velocity = new Phaser.Math.Vector2();
    if (this.cursorKeys.left.isDown) velocity.x -= 1;
    if (this.cursorKeys.right.isDown) velocity.x += 1;
    if (this.cursorKeys.up.isDown) velocity.y -= 1;
    if (this.cursorKeys.down.isDown) velocity.y += 1;
    velocity.normalize();
    this.ball.x += velocity.x * this.speed * (delta / 1000);
    this.ball.y += velocity.y * this.speed * (delta / 1000);
  }
}

export default function MatchCanvas({ matchId }: MatchCanvasProps) {
  const containerRef = useRef<HTMLDivElement | null>(null);
  const gameRef = useRef<Phaser.Game | null>(null);

  useEffect(() => {
    if (!containerRef.current || gameRef.current) return;
    gameRef.current = new Phaser.Game({
      type: Phaser.AUTO,
      width: 800,
      height: 480,
      backgroundColor: "#020617",
      parent: containerRef.current,
      scene: DemoScene,
      fps: { target: 60, forceSetTimeOut: true }
    });

    return () => {
      gameRef.current?.destroy(true);
      gameRef.current = null;
    };
  }, []);

  useEffect(() => {
    console.info(`Match ${matchId} ready for inputs.`);
  }, [matchId]);

  return <div ref={containerRef} className="flex flex-1 items-center justify-center" />;
}
