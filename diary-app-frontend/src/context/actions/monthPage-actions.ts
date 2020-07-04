import { ActionsUnion, createAction } from "./action-helpers";
import {
	IDesiresArea,
	IIdeasArea,
	IGoalsArea,
	IPurchasesArea,
	IPageArea,
} from "../../models";
import axios from "../../axios/axios";
import { AxiosResponse } from "axios";

export const SET_DESIRES_AREA = "SET_DESIRES_AREA";
export const SET_IDEAS_AREA = "SET_IDEAS_AREA";
export const SET_GOALS_AREA = "SET_GOALS_AREA";
export const SET_PURCHASES_AREA = "SET_PURCHASES_AREA";

export const Actions = {
	setDesiresArea: (desiresArea: IDesiresArea) =>
		createAction(SET_DESIRES_AREA, desiresArea),
	setIdeasArea: (ideasArea: IIdeasArea) =>
		createAction(SET_DESIRES_AREA, ideasArea),
	setGoalsArea: (goalsArea: IGoalsArea) =>
		createAction(SET_GOALS_AREA, goalsArea),
	setPurchasesArea: (purchasesArea: IPurchasesArea) =>
		createAction(SET_PURCHASES_AREA, purchasesArea),
};

export async function getPageArea<T extends IPageArea>(
	areaName: string,
	pageID: number
): Promise<AxiosResponse<T>> {
	return axios.get(`monthpage/${areaName}/${pageID}`).catch((err) => {
		console.log(err);
		return null;
	});
}

export type MonthPageActions = ActionsUnion<typeof Actions>;
