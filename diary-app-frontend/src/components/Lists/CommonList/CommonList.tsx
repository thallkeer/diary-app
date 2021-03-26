import React from "react";
import {
	CommonListComponent,
	IListOptions,
	IListActions,
	withItemContextMenu,
} from "../CommonList/CommonListComponent";
import { ListItemInput } from "../Controls/ListItemInput";
import { ICommonList, IListItem } from "models";
import { fillToNumber, getEmptyItem } from "../../../utils";
import { useUrlInput } from "hooks/useInputs";

export interface IListItemActions {
	updateItem: (item: IListItem) => void;
	deleteItem: (itemId: number) => void;
}

interface ICommonListProps extends IListActions, IListOptions {
	commonList: ICommonList;
	className?: string;
	listItemActions: IListItemActions;
}

export const CommonList: React.FC<ICommonListProps> = ({
	className,
	commonList,
	deleteList,
	updateTitle,
	listItemActions,
	readonlyTitle = false,
	isDeletable = false,
}) => {
	const listItems = fillToNumber([...commonList.items], 6, () =>
		getEmptyItem(commonList.id)
	);

	return (
		<CommonListComponent
			className={`mt-52 ${className}`}
			items={listItems}
			listTitle={commonList.title}
			readonlyTitle={readonlyTitle}
			updateTitle={updateTitle}
			isDeletable={isDeletable}
			deleteList={deleteList}
			renderItem={(item) => (
				<ListItemInputWithUrlEdit
					item={item}
					listItemActions={listItemActions}
				/>
			)}
		/>
	);
};

const ListItemInputWithUrlEdit: React.FC<{
	item: IListItem;
	listItemActions: IListItemActions;
}> = ({ item, listItemActions }) => {
	const { editUrlMode, urlInput, menuItem } = useUrlInput({
		item,
		updateItem: listItemActions.updateItem,
	});

	const input = editUrlMode ? (
		urlInput
	) : (
		<ListItemInput
			item={item}
			updateItem={listItemActions.updateItem}
			readonly={item.readonly}
			className="no-left-padding"
		/>
	);

	return withItemContextMenu(
		input,
		item.id,
		() => listItemActions.deleteItem(item.id),
		[menuItem]
	);
};
