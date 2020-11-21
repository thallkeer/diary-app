import { useEffect, Reducer, useReducer } from "react";
import { IPage, IPageArea } from "../models";

// function usePageArea<T extends IPageArea>(
// 	page: IPage,
// 	pageName: string,
// 	areaName: string
// ): PageAreaState<T> {
// 	const [state, setState] = useState<PageAreaState<T>>({
// 		loading: false,
// 		area: null,
// 	});

// 	useEffect(() => {
// 		let isUnmounting = false;
// 		const fetchPageArea = async () => {
// 			if (!isUnmounting) {
// 				setState((prevState) => {
// 					return { ...prevState, loading: true };
// 				});
// 				const pageArea = await getPageArea<T>(pageName, areaName, page.id);
// 				setState((prevState) => {
// 					return { ...prevState, area: pageArea.data, loading: false };
// 				});
// 			}
// 		};

// 		fetchPageArea();

// 		return () => (isUnmounting = true);
// 	}, [page, areaName]);

// 	return state;
// }
