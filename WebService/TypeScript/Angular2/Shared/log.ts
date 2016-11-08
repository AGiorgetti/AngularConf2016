export interface ILogMessage {
    Id: string;
    SourceId: string;
    Exception: string;
    Level: string;
    Msg: string;
    Timestamp: string;
    Type: string;
}

export class LogMessageTypes {
    static Log: string = "log";
    static Hb: string = "hb";
}