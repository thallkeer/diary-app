import axios from "axios/axios";
import { IPageArea } from "models/PageAreas/pageAreas";
import { IPage } from "models/Pages/pages";
import { PageAreaNames, PageNames } from "models/types";

export const pageAPI = {
	async getPage<TPage extends IPage>(
		pageName: PageNames,
		userId: number,
		year: number,
		month: number
	) {
		const query = `${pageName}/${userId}/${year}/${month}`;
		const res = await axios.get<TPage>(query);
		return res.data;
	},
	async getPageArea<TArea extends IPageArea>(
		areaName: PageAreaNames,
		pageName: string,
		pageID: number
	) {
		const res = await axios.get<TArea>(`${pageName}/${areaName}/${pageID}`);
		return res.data;
	},
};
