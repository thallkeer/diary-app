import { ActionsUnion, createAction } from "./action-helpers";
import {
	IDesiresArea,
	IPageArea,
	IPurchasesArea,
	IIdeasArea,
	IGoalsArea,
} from "../../models";

const SET_LOADING = "SET_LOADING";
const SET_PAGE_AREA = "SET_PAGE_AREA";

export type AnyPageArea =
	| IPurchasesArea
	| IDesiresArea
	| IIdeasArea
	| IGoalsArea;

export const PageAreaBaseActions = {
	setLoading: (value: boolean) => createAction(SET_LOADING, value),
	setArea: (area: AnyPageArea) => createAction(SET_PAGE_AREA, area),
};

export function PageAreaBaseActionsGen<T extends IPageArea>() {
	const actions = {
		setLoading: (value: boolean) => createAction(SET_LOADING, value),
		setArea: (area: T) => createAction(SET_PAGE_AREA, area),
	};

	return actions;
}

export type PageAreaActions = ActionsUnion<typeof PageAreaBaseActions>;
