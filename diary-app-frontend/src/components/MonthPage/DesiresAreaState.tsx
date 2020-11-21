import React from "react";
// import { IDesiresArea } from "../../models";
// import {
// 	desiresAreaReducer,
// 	IDesiresAreaState,
// } from "../../context/reducers/pageArea/desiresArea";
// import {
// 	DesiresAreaActions,
// 	desiresAreaActions,
// } from "../../context/actions/desiresArea-actions";

// interface IDesiresAreaContext {
// 	desiresAreaState: IDesiresAreaState;
// 	dispatch: React.Dispatch<DesiresAreaActions>;
// }

// const initialState: IDesiresAreaState = {
// 	area: {
// 		id: 0,
// 		header: "",
// 		desiresLists: [],
// 	},
// 	loading: false,
// };

// const desiresAreaContext = React.createContext<IDesiresAreaContext>({
// 	desiresAreaState: initialState,
// 	dispatch: () => null,
// });
// const { Provider } = desiresAreaContext;

// const DesiresAreaState: React.FC = ({ children }) => {
// 	const [state, dispatch] = useMonthArea<IDesiresArea, DesiresAreaActions>(
// 		"desiresArea",
// 		desiresAreaReducer,
// 		desiresAreaActions.setArea
// 	);

// 	return (
// 		<Provider
// 			value={{
// 				dispatch,
// 				desiresAreaState: state,
// 			}}
// 		>
// 			{children}
// 		</Provider>
// 	);
// };

// export { desiresAreaContext, DesiresAreaState };
