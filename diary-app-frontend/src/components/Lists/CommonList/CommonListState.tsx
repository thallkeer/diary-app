import React, { useReducer, useEffect, createContext } from "react";
import { ICommonList, IListState, IListItem } from "../../../models";
import { IListContext } from "../../../context";
import { CommonListActions } from "../../../context/actions/commonList-actions";
import { commonListReducer } from "../../../context/reducers/list/commonLists";
import { useListActions } from "../../../context/actions/list-actions";

export interface ICommonListState extends IListState<ICommonList, IListItem> {}

export interface ICommonListContext extends IListContext {
	commonListState: ICommonListState;
	dispatch: React.Dispatch<CommonListActions>;
}

const CommonListContext = createContext<ICommonListContext>({
	commonListState: {
		list: null,
		loading: false,
	},
	listFunctions: null,
	dispatch: () => null,
});

const {
	addOrUpdateListItem,
	deleteListItem,
	loadListByPageID,
	setList,
	updateList,
} = useListActions("commonList/", "Item");

const CommonListState: React.FC<{
	initList: ICommonList;
}> = ({ initList, children }) => {
	const [state, dispatch] = useReducer(commonListReducer, {
		list: initList,
		loading: false,
	});
	const commonList = state.list;

	useEffect(() => {
		if (initList && initList !== commonList) {
			setList(initList, dispatch);
		}
	}, [initList, commonList]);

	return (
		<CommonListContext.Provider
			value={{
				commonListState: state,
				dispatch,
				listFunctions: {
					addOrUpdateItem: (item) =>
						addOrUpdateListItem({ ...item, ownerID: commonList.id }, dispatch),
					deleteListItem: (itemID) =>
						itemID !== 0 && deleteListItem(itemID, dispatch),
					loadByPageID: (pageID) => loadListByPageID(pageID, dispatch),
					setList: (list) => setList(list, dispatch),
					updateListTitle: (title) =>
						updateList({ ...commonList, title }, dispatch),
				},
			}}
		>
			{children}
		</CommonListContext.Provider>
	);
};

export { CommonListContext, CommonListState };
