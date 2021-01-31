import { AxiosError, AxiosResponse } from "axios";
import axios from "axios/axios";
import { IPageArea } from "models/PageAreas/pageAreas";
import { IPage } from "models/Pages/pages";
import { PageAreaNames, PageNames } from "models/types";

export const pageAPI = {
	async getOrCreatePage<TPage extends IPage>(
		pageName: PageNames,
		userId: number,
		year: number,
		month: number
	) {
		const query = `${pageName}/${userId}/${year}/${month}`;
		const res = await axios
			.get<TPage>(query)
			.then((r) => r)
			.catch(async (err: AxiosResponse) => {
				if (err.status === 404) {
					return await axios.post<TPage>(pageName, { userId, year, month });
				}

				throw err;
			});

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
