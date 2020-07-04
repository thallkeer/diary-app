import { IPurchasesArea } from "../../../models";
import { PurchasesAreaActions } from "../../actions/purchasesArea-actions";
import { PageAreaState } from "../../../hooks/usePageArea";
import { pageAreaReducer } from "./pageArea";
import { getPurchasesLists } from "../../../selectors";

export interface IPurchasesAreaState extends PageAreaState<IPurchasesArea> {}

export const purchasesAreaReducer = (
	state: IPurchasesAreaState,
	action: PurchasesAreaActions
): IPurchasesAreaState => {
	const purchasesArea = state.area;
	const purchasesLists = getPurchasesLists(state);

	switch (action.type) {
		case "ADD_PURCHASES_LIST": {
			return {
				...state,
				area: {
					...purchasesArea,
					purchasesLists: [...purchasesLists, action.payload],
				},
			};
		}

		case "DELETE_PURCHASES_LIST": {
			return {
				...state,
				area: {
					...purchasesArea,
					purchasesLists: purchasesLists.filter(
						(pl) => pl.id !== action.payload
					),
				},
			};
		}

		default:
			return pageAreaReducer<IPurchasesArea, IPurchasesAreaState>(
				state,
				action
			);
	}
};
