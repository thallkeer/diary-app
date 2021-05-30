import {
	createSlice,
	createAsyncThunk,
	AsyncThunk,
	Slice,
} from "@reduxjs/toolkit";
import { IUser } from "models/entities";
import { IPage } from "models/Pages/pages";
import { IPageState } from "models/states";
import { PageUrls } from "models/types";
import { PageService } from "services/pageService";

interface IPageInfo {
	user: IUser;
	year: number;
	month: number;
}

const createPageAsyncThunk = <TPage extends IPage>(
	pageName: string,
	pageService: PageService<TPage>
) =>
	createAsyncThunk(`${pageName}/loadPage`, async (pageInfo: IPageInfo) => {
		const { user, year, month } = pageInfo;
		const response = await pageService.getOrCreatePage(user.id, year, month);
		return response;
	});

export class PageComponent<
	TPage extends IPage,
	TState extends IPageState<TPage>
> {
	private pageUrl: PageUrls;
	pageService: PageService<TPage>;
	private loadPageThunk: AsyncThunk<TPage, IPageInfo, {}>;
	private pageSlice: Slice<TState, {}, PageUrls>;

	constructor(pageUrl: PageUrls, initialState: TState) {
		this.pageUrl = pageUrl;
		this.pageService = new PageService<TPage>(pageUrl);
		this.loadPageThunk = createPageAsyncThunk(this.pageUrl, this.pageService);
		this.pageSlice = createSlice({
			name: this.pageUrl,
			initialState,
			reducers: {},
			extraReducers: (builder) => {
				builder.addCase(this.loadPageThunk.pending, (state, action) => {
					state.status = "loading";
				});
				builder.addCase(this.loadPageThunk.fulfilled, (state, { payload }) => {
					return { ...state, page: payload, status: "succeeded" };
				});
				builder.addCase(this.loadPageThunk.rejected, (state, action) => {
					state.status = "failed";
					state.error = action.error.message;
				});
			},
		});
	}

	public get loadPage() {
		return this.loadPageThunk;
	}

	public get slice() {
		return this.pageSlice;
	}
}
