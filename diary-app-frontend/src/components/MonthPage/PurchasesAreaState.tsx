import React from "react";
import { IPurchasesArea } from "../../models";
import {
	purchasesAreaReducer,
	IPurchasesAreaState,
} from "../../context/reducers/pageArea/purchasesArea";
import {
	PurchasesAreaActions,
	purcshasesAreaActions,
} from "../../context/actions/purchasesArea-actions";
import { useMonthArea } from "./MonthAreaHOC";

interface IPurchasesAreaContext {
	purchasesAreaState: IPurchasesAreaState;
	dispatch: React.Dispatch<PurchasesAreaActions>;
}

const purchasesAreaContext = React.createContext<IPurchasesAreaContext>({
	purchasesAreaState: {
		area: null,
		loading: false,
	},
	dispatch: () => null,
});
const { Provider } = purchasesAreaContext;

const PurchasesAreaState: React.FC = ({ children }) => {
	const [state, dispatch] = useMonthArea<IPurchasesArea, PurchasesAreaActions>(
		"purchasesArea",
		purchasesAreaReducer,
		purcshasesAreaActions.setArea
	);

	return (
		<Provider
			value={{
				purchasesAreaState: state,
				dispatch,
			}}
		>
			{children}
		</Provider>
	);
};

export { purchasesAreaContext, PurchasesAreaState };
