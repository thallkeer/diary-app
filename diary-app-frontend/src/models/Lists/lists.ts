import { IEntity } from "models/entities";

export interface IList extends IEntity {
	pageId: number;
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
	location: string;
}
