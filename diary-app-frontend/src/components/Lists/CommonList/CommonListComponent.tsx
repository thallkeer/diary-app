import React from "react";
import { IListItem } from "models";
import ListHeaderInput from "../Controls/ListHeaderInput";
import { DeleteBtn } from "../Controls/DeleteBtn";
import { ContextMenu, MenuItem, ContextMenuTrigger } from "react-contextmenu";
import { getRandomId } from "../../../utils";

export interface IListActions {
	updateTitle?: (title: string) => void;
	deleteList?: () => void;
}

export interface IListOptions {
	readonlyTitle: boolean;
	isDeletable: boolean;
}

export type ListComponentProps<T extends IListItem> = {
	items: T[];
	className: string;
	listTitle: string;
	renderItem: (item: T) => JSX.Element;
};

export const CommonListComponent = <T extends IListItem>(
	props: IListActions & IListOptions & ListComponentProps<T>
) => {
	const {
		listTitle,
		readonlyTitle,
		items,
		updateTitle,
		deleteList,
		renderItem,
		className,
		isDeletable,
	} = props;

	return (
		<div className={className}>
			<h1 className="todo-list-header">
				<ListHeaderInput
					value={listTitle}
					handleBlur={updateTitle}
					readonly={readonlyTitle}
				/>
				{isDeletable && <DeleteBtn onDelete={deleteList} />}
			</h1>
			<ul className="todos">
				{items.map((item: T, i) => (
					<li
						key={item.id !== 0 ? item.id : `item-${i}`}
						className="list-item"
						style={{
							flex: "1",
							display: "flex",
							borderBottom: "1px solid black",
							justifyContent: "flex-start",
						}}
					>
						{renderItem(item)}
					</li>
				))}
			</ul>
		</div>
	);
};

export const withContextMenu = (
	component: JSX.Element,
	itemID: number,
	onDelete: () => void,
	menuItems?: JSX.Element[]
) => {
	if (itemID === 0) return component;
	const uniqueID = getRandomId(); //items can have equal ids
	return (
		<>
			<ContextMenuTrigger id={`context-menu-${uniqueID}`}>
				{component}
			</ContextMenuTrigger>
			<ContextMenu className="menu" id={`context-menu-${uniqueID}`}>
				{menuItems}
				<MenuItem onClick={onDelete} className="menuItem">
					Удалить запись
				</MenuItem>
			</ContextMenu>
		</>
	);
};
