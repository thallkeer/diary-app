import { IEntity } from "./entities";
import { IList } from "./Lists/lists";
import { IPageArea } from "./PageAreas/pageAreas";
import { IPage } from "./Pages/pages";
import { PageAreaUrls, PageUrls } from "./types";

export interface IStateWithLoading {
	error: false;
	isLoading: false;
	success: false;
}

export type LoadingStatus = "idle" | "loading" | "succeeded" | "failed";

export interface ILoadingState {
	status: LoadingStatus;
	error: string | null;
}

export interface IListState<TList extends IList<TItem>, TItem extends IEntity> {
	list: TList;
}

export interface IPageState<T extends IPage> extends ILoadingState {
	page: T;
	readonly pageName: PageUrls;
}

export type PageAreaState<TArea extends IPageArea> = ILoadingState & {
	area: TArea | null;
	readonly pageAreaName: PageAreaUrls;
};
