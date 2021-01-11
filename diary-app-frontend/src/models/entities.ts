export interface IList extends IEntity {
	pageId: number;
}

export interface IEntity {
	id: number;
}

export interface IListWithItems<TItem> extends IList {
	items: TItem[];
}

export interface IListItem extends IEntity {
	subject: string;
	url: string;
	ownerID?: number;
	readonly?: boolean;
}

export interface IDiaryList<T extends IListItem> extends IListWithItems<T> {
	title: string;
}

export interface ITodoList extends IDiaryList<ITodo> {}
export interface IEventList extends IDiaryList<IEvent> {}
export interface ICommonList extends IDiaryList<IListItem> {}

export interface ITodo extends IListItem {
	done: boolean;
}

export interface IEvent extends IListItem {
	date: Date;
}

export interface IHabitDay extends IEntity {
	number: number;
	note: string;
	habitTrackerId: number;
}

export interface IHabitTracker extends IListWithItems<IHabitDay> {
	goalName: string;
	goalsAreaId: number;
}

export interface IUser extends IEntity {
	username: string;
	password?: string;
	token?: string;
}

export interface IPage extends IEntity {}

export interface IPageArea extends IEntity {
	header: string;
}

export interface IImportantEventsArea extends IPageArea {
	importantEvents: IEventList;
}

export interface IImportantThingsArea extends IPageArea {
	importantThings: ITodoList;
}
export interface IPurchasesArea extends IPageArea {
	purchasesLists: IPurchaseList[];
}

export interface IDesiresArea extends IPageArea {
	desiresLists: IDesireList[];
}

export interface IIdeasArea extends IPageArea {
	ideasList: IIdeasList;
}

export interface IGoalsArea extends IPageArea {
	goalLists: IHabitTracker[];
}

export interface IDiaryListWrapper extends IEntity {
	areaOwnerId: number;
}

export interface IPurchaseList extends IDiaryListWrapper {
	list: ITodoList;
}

export interface IDesireList extends IDiaryListWrapper {
	list: ICommonList;
}

export interface IIdeasList extends IDiaryListWrapper {
	list: ICommonList;
}
