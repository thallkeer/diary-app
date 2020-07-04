import { ActionsUnion, createAction } from "./action-helpers";
import axios from "../../axios/axios";
import { IUser, IPage } from "../../models";
import { AxiosError, AxiosResponse } from "axios";

export const START_LOAD_PAGE = "START_LOAD_PAGE";
export const FINISH_LOAD_PAGE = "FINISH_LOAD_PAGE";

const Actions = {
	startLoadPage: () => createAction(START_LOAD_PAGE),
	finishLoadPage: (page: IPage) => createAction(FINISH_LOAD_PAGE, page),
};

export const loadPage = async (
	pageName: string,
	user: IUser,
	year: number,
	month: number,
	dispatch: React.Dispatch<PageActions>
) => {
	const query = `${pageName}/${user.id}/${year}/${month}`;

	dispatch(Actions.startLoadPage());

	axios
		.get(query)
		.then((res: AxiosResponse<IPage>) => {
			dispatch(Actions.finishLoadPage(res.data));
		})
		.catch((err: AxiosError) => console.log(err));
};

export type PageActions = ActionsUnion<typeof Actions>;
