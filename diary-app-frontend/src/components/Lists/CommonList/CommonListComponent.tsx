import React from "react";
import { IListItem } from "models";
import ListHeaderInput from "../Controls/ListHeaderInput";
import { DeleteBtn } from "../Controls/DeleteBtn";
import { ContextMenu, MenuItem, ContextMenuTrigger } from "react-contextmenu";
import { nanoid } from "@reduxjs/toolkit";

export type IListActions = {
	updateTitle?: (title: string) => void;
	deleteList?: () => void;
};

export type IListOptions = {
	readonlyTitle: boolean;
	renderTitle?: boolean;
	isDeletable: boolean;
};

export type ListComponentProps<T extends IListItem> = {
	items: T[];
	className: string;
	listTitle: string;
	renderItem: (item: T) => JSX.Element;
};

export const ListWithItems = <T extends IListItem>(
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
		renderTitle = true,
	} = props;

	return (
		<div className={className}>
			{renderTitle && (
				<h1 className="list-with-items-header">
					<ListHeaderInput
						value={listTitle}
						handleBlur={updateTitle}
						readonly={readonlyTitle}
					/>
					{isDeletable && <DeleteBtn onDelete={deleteList} />}
				</h1>
			)}
			<ul className="todos">
				{items.map((item: T, i) => (
					<li
						key={item.id === 0 ? `item-${i}` : item.id}
						className={`list-item`}
					>
						{renderItem(item)}
					</li>
				))}
			</ul>
		</div>
	);
};

export const WithItemContextMenu: React.FC<{
	itemId: number;
	onDelete: () => void;
	menuItems?: JSX.Element[];
}> = ({ itemId, onDelete, menuItems, children }) => {
	if (itemId === 0) return <>{children}</>;
	const uniqueId = nanoid(); //items can have equal ids
	return (
		<>
			<ContextMenuTrigger id={`context-menu-${uniqueId}`}>
				{children}
			</ContextMenuTrigger>
			<ContextMenu
				className="menu"
				id={`context-menu-${uniqueId}`}
				style={{ zIndex: 10 }}
			>
				{menuItems}
				<MenuItem onClick={onDelete} className="menuItem">
					Удалить запись
				</MenuItem>
			</ContextMenu>
		</>
	);
};
