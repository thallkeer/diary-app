import { INITIAL_LOADABLE_STATE } from "../../utilities/loading-reducer";
import { combineReducers } from "redux";
import { IDEAS_LIST } from "./ideasArea.actions";
import { createPageAreaReducer, IPageAreaState } from "../pageAreas.reducer";
import { IIdeasArea } from "models/PageAreas/pageAreas";
import { createCommonListReducer } from "store/diaryLists/commonLists.reducer";

export interface IIdeasAreaState extends IPageAreaState<IIdeasArea> {}

const initialState: IIdeasAreaState = {
	area: null,
	pageAreaName: "ideasArea",
	...INITIAL_LOADABLE_STATE,
};

export const ideasAreaReducer = combineReducers({
	area: createPageAreaReducer(initialState, initialState.pageAreaName),
	ideasList: createCommonListReducer(IDEAS_LIST),
});
