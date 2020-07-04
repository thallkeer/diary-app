import { MonthPageActions } from "../../actions/monthPage-actions";
import { IMonthPage } from "../../../models";

export const monthPageReducer = (
	state: IMonthPage,
	action: MonthPageActions
): IMonthPage => {
	switch (action) {
		default:
			return state;
	}
};
