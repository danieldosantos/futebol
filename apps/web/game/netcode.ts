import { ClientInput, InputSchema, ServerSnapshot } from "@bolasocial/shared";
import * as signalR from "@microsoft/signalr";

export type SnapshotHandler = (snapshot: ServerSnapshot) => void;

interface NetcodeOptions {
  socketFactory: () => signalR.HubConnection;
  onSnapshot: SnapshotHandler;
}

export class NetcodeClient {
  private readonly connection: signalR.HubConnection;
  private readonly pendingInputs: ClientInput[] = [];
  private lastSeq = 0;

  constructor(private readonly options: NetcodeOptions) {
    this.connection = options.socketFactory();
    this.connection.on("snapshot", (payload: ServerSnapshot) => {
      options.onSnapshot(payload);
    });
  }

  async connect(matchId: string) {
    if (this.connection.state !== signalR.HubConnectionState.Connected) {
      await this.connection.start();
      await this.connection.invoke("JoinMatch", matchId);
    }
  }

  async sendInput(input: ClientInput) {
    const parsed = InputSchema.parse(input);
    this.pendingInputs.push(parsed);
    this.lastSeq = Math.max(this.lastSeq, parsed.seq);
    await this.connection.invoke("Input", parsed.matchId, parsed);
  }

  ack(seq: number) {
    while (this.pendingInputs.length > 0 && this.pendingInputs[0]!.seq <= seq) {
      this.pendingInputs.shift();
    }
  }
}
