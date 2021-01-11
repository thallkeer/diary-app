import {
	IDesireListState,
	IDesireListsState,
	ListsStateByName,
} from "../../../models/states";
import { IDesireList } from "../../../models/entities";
import { ActionsUnion, createAction } from "../../actions/action-helpers";
import { BaseThunkType } from "../../store";
import { ListWrapperUrls } from "../../../models/types";
import { getDesireListByName } from "../../../selectors/page-selectors";
import {
	commonListActionCreators,
	commonListActions,
	CommonListActions,
	commonListInitialState,
	commonListReducer,
	createCommonListReducer,
} from "../list/commonLists";

const SET_DESIRE_LISTS = "SET_DESIRE_LISTS";
const DESIRE_LIST: ListWrapperUrls = "desireLists";
const DESIRE_LIST_PATTERN = DESIRE_LIST + "_";

export const getDesireListName = (listId: number) =>
	DESIRE_LIST_PATTERN + listId;

const initialState: IDesireListsState = {
	byName: {},
};

type CommonListReducerType = typeof commonListReducer;
const reducers: Map<string, CommonListReducerType> = new Map<
	string,
	CommonListReducerType
>();

export const desireListsReducer = (
	state = initialState,
	action: DesireListsActions
): IDesireListsState => {
	switch (action.type) {
		case "SET_DESIRE_LISTS":
			reducers.clear();
			const newState: ListsStateByName<IDesireListState> = {};

			action.payload.forEach((dl) => {
				newState[getDesireListName(dl.id)] = createDesireListState(dl);
			});
			return {
				byName: newState,
			};

		default:
			return oneListReducer(state, action);
	}
};

const oneListReducer = (
	state: IDesireListsState,
	action: CommonListActions
): IDesireListsState => {
	let listKey = action.subjectName;
	const reducer = reducers.get(listKey);
	if (!reducer) return state;
	const desireList = getDesireListByName(listKey)(state);
	const newTodoListState = reducer(desireList.listState, action);
	return {
		...state,
		byName: {
			...state.byName,
			[listKey]: {
				...desireList,
				listState: newTodoListState,
			},
		},
	};
};

const actions = {
	setLists: (desireLists: IDesireList[]) =>
		createAction(SET_DESIRE_LISTS, desireLists),
	...commonListActions,
};

export const desireListsActions = {
	...commonListActionCreators,
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

const createDesireListState = (desireList: IDesireList) => {
	const listName = getDesireListName(desireList.id);
	const listReducer = createCommonListReducer(listName);
	reducers.set(listName, listReducer);
	const plistState: IDesireListState = {
		desireListId: desireList.id,
		desireAreaId: desireList.areaOwnerId,
		listState: {
			...commonListInitialState,
			list: desireList.list,
			listName: "commonList_" + desireList.list.id,
		},
	};
	return plistState;
};
