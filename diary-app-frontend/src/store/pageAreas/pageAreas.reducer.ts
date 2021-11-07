import { IPage } from "models";
import { IPageArea } from "models/PageAreas/pageAreas";
import { PageAreaState } from "models/states";
import { PageAreaUrls, PageUrls } from "models/types";
import { PageService } from "services/pageService";
import { INITIAL_LOADABLESTATE } from "store/utilities/loading-reducer";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { toast } from "react-toastify";

export const createPageAreaSlice = <
	TPage extends IPage,
	TArea extends IPageArea
>(
	pageAreaUrl: PageAreaUrls,
	pageUrl: PageUrls,
	onAreaLoaded: (area: TArea) => any
) => {
	const initialState: PageAreaState<TArea> = {
		area: null,
		pageAreaName: pageAreaUrl,
		...INITIAL_LOADABLESTATE,
	};

	const loadPageAreaThunk = createAsyncThunk(
		`${pageAreaUrl}/loadPageArea`,
		async (pageId: number, thunkApi) => {
			const response = await new PageService<TPage>(pageUrl).getPageArea<TArea>(
				pageAreaUrl,
				pageId
			);
			if (onAreaLoaded) thunkApi.dispatch(onAreaLoaded(response));
			return response;
		}
	);

	const slice = createSlice({
		name: pageAreaUrl,
		initialState,
		reducers: {},
		extraReducers: (builder) => {
			builder.addCase(loadPageAreaThunk.pending, (state, action) => {
				state.status = "loading";
			});
			builder.addCase(loadPageAreaThunk.fulfilled, (state, { payload }) => {
				return { ...state, area: payload, status: "succeeded" };
			});
			builder.addCase(loadPageAreaThunk.rejected, (state, action) => {
				state.status = "failed";
				toast.error(action.error.message);
				state.error = action.error.message;
			});
		},
	});

	return { slice, loadPageArea: loadPageAreaThunk };
};
