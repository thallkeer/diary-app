import { ActionsUnion } from "../../actions/action-helpers";
import { IIdeasArea } from "../../../models/entities";
import {
	createNamedWrapperPageAreaReducer,
	getPageAreaActions,
	loadPageArea,
	PageAreaActions,
} from "./pageArea-reducer";
import { BaseThunkType } from "../../store";
import { INITIAL_LOADABLE_STATE } from "../utilities/loading-reducer";
import { IIdeasAreaState } from "../../../models/states";
import { combineReducers } from "redux";
import {
	commonListActionCreators,
	createCommonListReducer,
} from "../list/commonLists";
import { ListWrapperUrls } from "../../../models/types";

const initialState: IIdeasAreaState = {
	area: null,
	pageAreaName: "ideasArea",
	...INITIAL_LOADABLE_STATE,
};

export const IDEAS_LIST: ListWrapperUrls = "ideasLists";

export const ideasAreaReducer = combineReducers({
	area: createNamedWrapperPageAreaReducer(
		initialState,
		initialState.pageAreaName
	),
	ideasList: createCommonListReducer(IDEAS_LIST),
});

export const loadIdeasArea = (pageID: number): ThunkType => async (
	dispatch
) => {
	dispatch(
		loadPageArea<IIdeasArea>(
			initialState.pageAreaName,
			"monthPage",
			pageID,
			(ideasArea) => {
				dispatch(
					commonListActionCreators.setList(ideasArea.ideasList.list, IDEAS_LIST)
				);
			}
		)
	);
};

const actions = {
	...getPageAreaActions<IIdeasArea>(initialState.pageAreaName),
};

export const ideasAreaActions = {
	...commonListActionCreators,
};

export type IdeasAreaActions = ActionsUnion<typeof actions> | PageAreaActions;
type ThunkType = BaseThunkType<IdeasAreaActions>;
