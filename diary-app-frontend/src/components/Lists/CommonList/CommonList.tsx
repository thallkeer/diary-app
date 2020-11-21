import React, { useContext } from "react";
import Loader from "../../Loader";
import { getEmptyItem, fillToNumber } from "../../../utils";
import {
	CommonListComponent,
	withContextMenu,
} from "../CommonList/CommonListComponent";
import { ListItemInput } from "../Controls/ListItemInput";

interface ICommonListProps extends React.HtmlHTMLAttributes<HTMLDivElement> {
	readonlyTitle?: boolean;
}


// export const CommonList: React.FC<ICommonListProps> = ({
// 	className,
// 	readonlyTitle = false,
// }) => {
// 	const { listFunctions, commonListState } = useContext(CommonListContext);

// 	const { updateListTitle, addOrUpdateItem, deleteListItem } = listFunctions;

// 	const { list, loading } = commonListState;

// 	if (loading || !list) return <Loader />;

// 	const todos = fillToNumber([...list.items], 6, getEmptyItem);

// 	return (
// 		<CommonListComponent
// 			className={`mt-52 ${className}`}
// 			items={todos}
// 			listTitle={list.title}
// 			readonlyTitle={readonlyTitle}
// 			updateListTitle={updateListTitle}
// 			isDeletable={false}
// 			renderItem={(item) =>
// 				withContextMenu(
// 					<ListItemInput
// 						item={item}
// 						updateItem={addOrUpdateItem}
// 						readonly={item.readonly}
// 						className="no-left-padding"
// 					/>,
// 					item.id,
// 					() => deleteListItem(item.id)
// 				)
// 			}
// 		/>
// 	);
// };
