import { ActionsUnion, createAction } from "../../actions/action-helpers";
import { IDesireList } from "models/entities";
import {
	CommonListActions,
	commonListActions,
	commonListThunks,
} from "store/diaryLists/commonLists.actions";
import { BaseThunkType } from "store/state.types";

const SET_DESIRE_LISTS = "SET_DESIRE_LISTS";

const actions = {
	setLists: (desireLists: IDesireList[]) =>
		createAction(SET_DESIRE_LISTS, desireLists),
	...commonListActions,
};

export const desireListsThunks = {
	...commonListThunks,
	setDesireLists: (desireLists: IDesireList[]): ThunkType => async (
		dispatch
	) => {
		dispatch(actions.setLists(desireLists));
	},
};

export type DesireListsActions =
	| ActionsUnion<typeof actions>
	| CommonListActions;
type ThunkType = BaseThunkType<DesireListsActions>;
