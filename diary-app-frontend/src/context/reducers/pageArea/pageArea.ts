import { AnyPageArea, PageAreaActions } from "../../actions/pageArea-actions";
import { PageAreaState } from "../../../hooks/usePageArea";
import { IPageArea } from "../../../models";

export interface IPageAreaState extends PageAreaState<AnyPageArea> {}

export function pageAreaReducer<
	T extends IPageArea,
	U extends PageAreaState<T>
>(state: U, action: PageAreaActions): U {
	switch (action.type) {
		case "SET_LOADING":
			return { ...state, loading: action.payload };

		case "SET_PAGE_AREA":
			return { ...state, area: action.payload };

		default:
			return state;
	}
}
