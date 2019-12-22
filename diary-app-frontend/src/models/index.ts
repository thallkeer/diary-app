export interface IEventList {
  id: number;
  pageId: number;
  month: number;
  title: string;
  items: ILightEvent[];
}

export interface ILightEvent {
  eventID: number;
  date: Date;
  fullDay?: boolean;
  subject: string;
  description?: string;
}

export interface IEventsByDay {
  day: number;
  event: ILightEvent;
}

export interface IListItem {
  id: number;
  text: string;
}

export interface ITodoItem extends IListItem {
  done: boolean;
}
