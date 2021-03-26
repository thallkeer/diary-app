import { IEntity } from "models/entities";

export interface IListItem extends IEntity {
	subject: string;
	url: string;
	ownerId: number;
	readonly?: boolean;
}

export interface IList<T extends IEntity> extends IEntity {
	items: T[];
}

export interface IDiaryList<T extends IEntity> extends IList<T> {
	title: string;
}

export interface ITodo extends IListItem {
	done: boolean;
}

export interface IEvent extends IListItem {
	date: Date;
	location: string;
}

export interface IHabitDay extends IEntity {
	number: number;
	note: string;
	habitTrackerId: number;
}

export interface ITodoList extends IDiaryList<ITodo> {}
export interface IEventList extends IDiaryList<IEvent> {}
export interface ICommonList extends IDiaryList<IListItem> {}
export interface IHabitTracker extends IList<IHabitDay> {
	goalName: string;
	goalsAreaId: number;
}
