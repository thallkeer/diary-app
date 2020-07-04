import React from "react";
import { IIdeasArea } from "../../models";
import {
	ideasAreaReducer,
	IIdeasAreaState,
} from "../../context/reducers/pageArea/ideasArea";
import {
	IdeasAreaActions,
	ideasAreaActions,
} from "../../context/actions/ideasArea-actions";
import { useMonthArea } from "./MonthAreaHOC";

interface IIdeasAreaContext {
	ideasAreaState: IIdeasAreaState;
	dispatch: React.Dispatch<IdeasAreaActions>;
}

const ideasAreaContext = React.createContext<IIdeasAreaContext>({
	ideasAreaState: {
		area: {
			id: 0,
			header: "",
			ideasList: null,
		},
		loading: false,
	},
	dispatch: () => null,
});
const { Provider } = ideasAreaContext;

const IdeasAreaState: React.FC = ({ children }) => {
	const [state, dispatch] = useMonthArea<IIdeasArea, IdeasAreaActions>(
		"ideasArea",
		ideasAreaReducer,
		ideasAreaActions.setArea
	);

	return (
		<Provider
			value={{
				dispatch,
				ideasAreaState: state,
			}}
		>
			{children}
		</Provider>
	);
};

export { ideasAreaContext, IdeasAreaState };
