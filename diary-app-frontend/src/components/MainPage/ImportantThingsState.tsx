import React from "react";
// import { IImportantThingsArea } from "../../models";
// import {
// 	importantThingsAreaReducer,
// 	IImportantThingsAreaState,
// } from "../../context/reducers/pageArea/importantThingsArea";
// import {
// 	ImportantThingsAreaActions,
// 	importantThingsAreaActions,
// } from "../../context/actions/importantThingsArea-actions";

// interface IImportantThingsAreaContext {
// 	importantThingsAreaState: IImportantThingsAreaState;
// 	dispatch: React.Dispatch<ImportantThingsAreaActions>;
// }

// const importantThingsAreaContext = React.createContext<
// 	IImportantThingsAreaContext
// >({
// 	importantThingsAreaState: {
// 		area: {
// 			id: 0,
// 			header: "",
// 			importantThings: null,
// 		},
// 		loading: false,
// 	},
// 	dispatch: () => null,
// });
// const { Provider } = importantThingsAreaContext;

// const ImportantThingsAreaState: React.FC = ({ children }) => {
// 	const [state, dispatch] = useMonthArea<
// 		IImportantThingsArea,
// 		ImportantThingsAreaActions
// 	>(
// 		"importantThingsArea",
// 		importantThingsAreaReducer,
// 		importantThingsAreaActions.setArea
// 	);

// 	return (
// 		<Provider
// 			value={{
// 				dispatch,
// 				importantThingsAreaState: state,
// 			}}
// 		>
// 			{children}
// 		</Provider>
// 	);
// };

// export { importantThingsAreaContext, ImportantThingsAreaState };
