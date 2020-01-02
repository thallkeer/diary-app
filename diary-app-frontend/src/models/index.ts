export interface IList<T extends ITodoItem | ILightEvent> {
  id: number;
  pageId: number;
  month: number;
  title: string;
  items: T[];
}

export interface IEventsByDay {
  day: number;
  event: ILightEvent;
}

export interface IListItem {
  id: number;
  subject: string;
  ownerID?: number;
}

export interface ITodoItem extends IListItem {
  done: boolean;
}

export interface ILightEvent extends IListItem {
  date: Date;
  fullDay?: boolean;
  description?: string;
}
