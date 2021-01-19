import { IDiaryListWrapper, IEntity } from "models/entities";
import { IListWithItems } from "./lists";

export interface IHabitDay extends IEntity {
	number: number;
	note: string;
	habitTrackerId: number;
}

export interface IHabitTracker
	extends IDiaryListWrapper,
		IListWithItems<IHabitDay> {
	goalName: string;
}
