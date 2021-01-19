import { IEvent, IEventList } from "models";
import { ActionsUnion } from "../actions/action-helpers";
import {
	createDiaryListActions,
	getDiaryListActions,
} from "./diaryLists.actions";
import { getListItemActions } from "./lists.actions";

const actions = {
	...createDiaryListActions<IEventList, IEvent>(),
};

export const eventThunks = {
	...getDiaryListActions<IEventList, IEvent>("eventLists"),
	...getListItemActions<IEvent>("events"),
};

export type EventListActions = ActionsUnion<typeof actions>;
