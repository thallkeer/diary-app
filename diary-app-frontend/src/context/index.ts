import {  IHabitsTracker, IPageAreaState } from "../models/index";

export interface IGoalsAreaContext {
	goalsArea: IPageAreaState;
	addOrUpdate: (tracker?: IHabitsTracker) => void;
	deleteTracker: (tracker: IHabitsTracker) => void;
}
