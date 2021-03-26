import { AxiosInstance, AxiosResponse } from "axios";
import axios from "../axios/axios";
import { IPageArea } from "models/PageAreas/pageAreas";
import { IPage } from "models/Pages/pages";
import { PageAreaUrls } from "models/types";

export class PageService<TPage extends IPage> {
	protected apiUrl: string;
	protected axios: AxiosInstance;
	constructor(apiUrl: string) {
		this.apiUrl = apiUrl;
		this.axios = axios;
	}

	async getOrCreatePage(
		userId: number,
		year: number,
		month: number
	): Promise<TPage> {
		const query = `${this.apiUrl}/${userId}/${year}/${month}`;
		const res = await axios
			.get<TPage>(query)
			.catch(async (err: AxiosResponse) => {
				if (err && err.status === 404) {
					return await axios.post<TPage>(this.apiUrl, { userId, year, month });
				}

				throw err;
			});

		return res.data;
	}

	async getPageArea<TArea extends IPageArea>(
		areaName: PageAreaUrls,
		pageId: number
	): Promise<TArea> {
		const response = await axios.get<TArea>(
			`${this.apiUrl}/${areaName}/${pageId}`
		);
		return response.data;
	}
}
