import React, { FC } from "react";
import { ListItem } from "../../../models";
import ListHeaderInput from "../Controls/ListHeaderInput";
import { DeleteBtn } from "../Controls/DeleteBtn";
import { ContextMenu, MenuItem, ContextMenuTrigger } from "react-contextmenu";
import { getRandomId } from "../../../utils";

interface ICommonListProps extends React.HtmlHTMLAttributes<HTMLDivElement> {
	items: ListItem[];
	renderItem: (item: ListItem) => JSX.Element;
	listTitle: string;
	readonlyTitle: boolean;
	updateListTitle?: (title: string) => void;
	isDeletable: boolean;
	onDeleteList?: () => void;
}

export const CommonListComponent: FC<ICommonListProps> = ({
	listTitle,
	readonlyTitle,
	items,
	updateListTitle,
	renderItem,
	className,
	isDeletable,
	onDeleteList,
}) => {
	return (
		<div className={className}>
			<h1 className="todo-list-header">
				<ListHeaderInput
					value={listTitle}
					handleBlur={updateListTitle}
					readonly={readonlyTitle}
				/>
				{isDeletable && <DeleteBtn onDelete={onDeleteList} />}
			</h1>
			<ul className="todos">
				{items.map((item: ListItem, i) => (
					<li key={item.id !== 0 ? item.id : `item-${i}`} className="list-item">
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
