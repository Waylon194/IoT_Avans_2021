import { Datagram } from '../models/Datagram'

export interface SmartMeterMeasurement{
    DatagramDate: Date,
    DatagramValues: Datagram
}