export default interface ILightEvent {
  eventID: number;
  date: Date;
  fullDay?: boolean;
  subject: string;
  description?: string;
}
