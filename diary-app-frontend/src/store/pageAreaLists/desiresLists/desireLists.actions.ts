import { ActionsUnion, createAction } from "../../actions/action-helpers";
import { BaseThunkType } from "store/state.types";
import { ICommonList } from "models";
import { CommonListActions } from "store/diaryLists";

const SET_DESIRE_LISTS = "SET_DESIRE_LISTS";

const actions = {
	setLists: (desireLists: ICommonList[]) =>
		createAction(SET_DESIRE_LISTS, desireLists),
};

export const desireListsThunks = {
	setDesireLists: (desireLists: ICommonList[]): ThunkType => async (
		dispatch
	) => {
		dispatch(actions.setLists(desireLists));
	},
};

export type DesireListsActions =
	| ActionsUnion<typeof actions>
	| CommonListActions;
type ThunkType = BaseThunkType<DesireListsActions>;
