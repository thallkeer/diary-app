import React from "react";
import { IListItem } from "models";
import ListHeaderInput from "../Controls/ListHeaderInput";
import { DeleteBtn } from "../Controls/DeleteBtn";
import { ContextMenu, MenuItem, ContextMenuTrigger } from "react-contextmenu";
import { nanoid } from "@reduxjs/toolkit";

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
					<li key={item.id !== 0 ? item.id : `item-${i}`} className="list-item">
						{renderItem(item)}
					</li>
				))}
			</ul>
		</div>
	);
};

export const withItemContextMenu = (
	component: JSX.Element,
	itemId: number,
	onDelete: () => void,
	menuItems?: JSX.Element[]
) => {
	if (itemId === 0) return component;
	const uniqueId = nanoid(); //items can have equal ids
	return (
		<>
			<ContextMenuTrigger id={`context-menu-${uniqueId}`}>
				{component}
			</ContextMenuTrigger>
			<ContextMenu className="menu" id={`context-menu-${uniqueId}`}>
				{menuItems}
				<MenuItem onClick={onDelete} className="menuItem">
					Удалить запись
				</MenuItem>
			</ContextMenu>
		</>
	);
};
