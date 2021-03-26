import { IMonthPage } from "models";
import { IPurchasesArea } from "models/PageAreas/pageAreas";
import { purchaseListsThunks } from "store/pageAreaLists/purchasesLists/purchaseLists.actions";
import { IPageAreaState } from "models/states";
import { combineReducers } from "redux";
import { purchaseListsReducer } from "store/pageAreaLists/purchasesLists/purchaseLists.reducer";
import { INITIAL_LOADABLE_STATE } from "store/utilities/loading-reducer";
import { PageAreaComponent } from "./pageAreas.reducer";

class PurchasesAreaComponent extends PageAreaComponent<
	IMonthPage,
	IPurchasesArea
> {
	onAreaLoaded(pageArea: IPurchasesArea, dispatch): void {
		dispatch(purchaseListsThunks.setPurchaseLists(pageArea.purchasesLists));
	}
}

export const purchasesAreaComponent = new PurchasesAreaComponent(
	"monthPage",
	"purchasesArea"
);

export interface IPurchasesAreaState extends IPageAreaState<IPurchasesArea> {}

const initialState: IPurchasesAreaState = {
	area: null,
	pageAreaName: "purchasesArea",
	...INITIAL_LOADABLE_STATE,
};

export const purchasesAreaReducer = combineReducers({
	area: purchasesAreaComponent.getReducer(initialState),
	purchaseLists: purchaseListsReducer,
});
