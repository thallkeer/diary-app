import { IIdeasArea } from "models/PageAreas/pageAreas";
import { ListWrapperUrls } from "models/types";
import { ActionsUnion } from "store/actions/action-helpers";
import { commonListThunks } from "store/diaryLists/commonLists.actions";
import { BaseThunkType } from "store/state.types";
import { getPageAreaActions, loadPageArea } from "../pageAreas.actions";

export const IDEAS_LIST: ListWrapperUrls = "ideasLists";

const actions = {
	...getPageAreaActions<IIdeasArea>("ideasArea"),
};

export const loadIdeasArea = (pageID: number): ThunkType => async (
	dispatch
) => {
	dispatch(
		loadPageArea<IIdeasArea>("ideasArea", "monthPage", pageID, (ideasArea) => {
			dispatch(commonListThunks.setList(ideasArea.ideasList.list, IDEAS_LIST));
		})
	);
};

export type IdeasAreaActions = ActionsUnion<typeof actions>;
type ThunkType = BaseThunkType<IdeasAreaActions>;
