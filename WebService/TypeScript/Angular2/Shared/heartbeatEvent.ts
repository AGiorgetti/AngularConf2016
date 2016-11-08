export interface IHeartbeatEvent {
    Id: string;
    Msg: string;
    Timestamp: string;
    ExtraElements?: { [index: string]: any};
}
