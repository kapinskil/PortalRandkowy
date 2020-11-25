import { NodeCompatibleEventEmitter } from 'rxjs/internal/observable/fromEvent';

export interface Message {
    id: number;
    senderId: number;
    senderUserName: string;
    senderPhotoUrl: string;

    recipientId: number;
    recipientUserName: string;
    recipientPhotoUtl: string;
    content: string;
    IsRead: boolean;
    dateRead: Date;
    dateSend: Date;
}
