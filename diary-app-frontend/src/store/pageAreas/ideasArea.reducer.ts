import { IIdeasArea } from "models/PageAreas/pageAreas";
import { ListWrapperUrls } from "models/types";
import { IMonthPage } from "models";
import { PageAreaComponent } from "./pageAreas.reducer";
import { combineReducers } from "redux";
import { INITIAL_LOADABLE_STATE } from "store/utilities/loading-reducer";
import { IPageAreaState } from "models/states";
import { commonListComponent, createCommonListReducer } from "store/diaryLists";

export const IDEAS_LIST: ListWrapperUrls = "ideasLists";

class IdeasAreaComponent extends PageAreaComponent<IMonthPage, IIdeasArea> {
	onAreaLoaded(pageArea: IIdeasArea, dispatch): void {
		dispatch(ideasListThunks.setList(pageArea.ideasList));
	}
}

export const ideasAreaComponent = new IdeasAreaComponent(
	"monthPage",
	"ideasArea"
);

export interface IIdeasAreaState extends IPageAreaState<IIdeasArea> {}

const initialState: IIdeasAreaState = {
	area: null,
	pageAreaName: "ideasArea",
	...INITIAL_LOADABLE_STATE,
};

export const ideasListThunks = commonListComponent.getThunks(IDEAS_LIST);

export const ideasAreaReducer = combineReducers({
	area: ideasAreaComponent.getReducer(initialState),
	ideasList: createCommonListReducer(IDEAS_LIST),
});
