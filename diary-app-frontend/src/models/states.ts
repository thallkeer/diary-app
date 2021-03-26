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

export interface IListState<TList extends IList<TItem>, TItem extends IEntity> {
	list: TList;
}

export interface IPageState<T extends IPage> extends IStateWithLoading {
	page: T;
	pageName: PageUrls;
}

export interface IPageAreaState<T extends IPageArea> extends IStateWithLoading {
	area: T;
	pageAreaName: PageAreaUrls;
}

export interface ListsStateByName<T> {
	[listName: string]: T;
}
