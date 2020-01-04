export interface IList<T extends ITodoItem | IEvent> {
  id: number;
  pageId: number;
  month: number;
  title: string;
  items: T[];
}

export interface ITodoList extends IList<ITodoItem> {}

export interface IEventList extends IList<IEvent> {}

export interface IEventsByDay {
  day: number;
  event: IEvent;
}

export interface IListItem {
  id: number;
  subject: string;
  ownerID?: number;
}

export interface ITodoItem extends IListItem {
  done: boolean;
}

export interface IEvent extends IListItem {
  date: Date;
  fullDay?: boolean;
  description?: string;
}

export interface IHabitsTracker {
  id: number;
  goalName: string;
  selectedDays: number[];
}

export interface IUser {
  id: string;
  userName: number;
}

export interface IPage {
  id: number;
  year: number;
  month: number;
  user?: IUser;
}

export interface IMainPage extends IPage {
  thingsTodo: ITodoList;
  importantEvents: IEventList;
}

export interface IMonthPage extends IPage {
  purchasesArea?: IPurchasesArea;
  desiresArea?: IDesiresArea;
  ideasArea?: IIdeasArea;
  goalsArea?: IGoalsArea;
}

export interface IPageArea {
  id: number;
  header: string;
}

export interface IPurchasesArea extends IPageArea {
  purchasesLists: ITodoList[];
}

export interface IDesiresArea extends IPageArea {
  desiresLists: IEventList[];
}

export interface IIdeasArea extends IPageArea {
  ideasList: IEventList;
}

export interface IGoalsArea extends IPageArea {
  goalsLists: IHabitsTracker[];
}
