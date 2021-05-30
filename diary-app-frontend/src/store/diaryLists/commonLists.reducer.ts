import { SliceCaseReducers } from "@reduxjs/toolkit";
import { ICommonList, IListItem } from "models";
import { IListState } from "models/states";
import { commonListService, listItemService } from "services/todosService";
import {
	createGenericListThunks,
	createGenericSlice,
	generateListSliceReducers,
} from "./lists.reducer";

export interface ICommonListState extends IListState<ICommonList, IListItem> {
	isDeletable: boolean;
	readonlyHeader: boolean;
}

export const commonListInitialState: ICommonListState = {
	list: null,
	isDeletable: false,
	readonlyHeader: true,
};

const baseReducers = generateListSliceReducers<
	ICommonListState,
	ICommonList,
	IListItem
>() as SliceCaseReducers<ICommonListState>;

type CommonListReducersType = typeof baseReducers;

export const createCommonListSlice = (listName: string) =>
	createGenericSlice<
		ICommonListState,
		ICommonList,
		IListItem,
		CommonListReducersType
	>({
		name: listName,
		initialState: commonListInitialState,
		reducers: {},
	});

export type CommonListSlice = ReturnType<typeof createCommonListSlice>;

export const createCommonListThunks = (slice: CommonListSlice) => {
	const baseThunks = createGenericListThunks<
		ICommonListState,
		ICommonList,
		IListItem
	>(slice.name, commonListService, listItemService);

	return {
		...baseThunks,
	};
};
