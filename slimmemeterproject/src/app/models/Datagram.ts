import { Telegram } from '../models/Telegram';
import { SObject } from '../models/SObject';

export interface Datagram {
    Telegram: Telegram,
    Signature: String,
    CarCharger: SObject,
    SolarPanel: SObject,
}