import React, { ChangeEvent, FocusEvent, useEffect } from "react";
import {
	ListWithItems,
	IListOptions,
	IListActions,
	WithItemContextMenu,
} from "../CommonList/CommonListComponent";
import { ICommonList, IListItem } from "models";
import { fillToNumber, getEmptyItem } from "../../../utils";
import { useUrlInput } from "hooks/useInputs";
import { useState } from "react";
import { useRef } from "react";

export interface IListItemActions {
	updateItem?: (item: IListItem) => void;
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
	renderTitle,
	readonlyTitle = false,
	isDeletable = false,
}) => {
	const listItems = fillToNumber(
		commonList.items,
		commonList.items.length,
		() => getEmptyItem(commonList.id)
	);

	return (
		<ListWithItems
			className={`mt-52 ${className}`}
			items={listItems}
			listTitle={commonList.title}
			readonlyTitle={readonlyTitle}
			renderTitle={renderTitle}
			updateTitle={updateTitle}
			isDeletable={isDeletable}
			deleteList={deleteList}
			renderItem={(item) => (
				<ResizableTextAreaWithUrlEdit
					item={item}
					listItemActions={listItemActions}
				/>
			)}
		/>
	);
};

type ResizableTextAreaProps = {
	value: string;
	rows: number;
	minRows: number;
	maxRows: number;
	updateItem: (text: string) => void;
};

const ResizableTextarea: React.FC<ResizableTextAreaProps> = (props) => {
	const [state, setState] = useState<ResizableTextAreaProps>(props);
	const inputEl = useRef<HTMLTextAreaElement>(null);

	useEffect(() => {
		recalculateHeight(inputEl.current);
	}, [inputEl.current]);

	const handleBlur = (event: FocusEvent<HTMLTextAreaElement>) => {
		state.updateItem(state.value);
	};

	const handleChange = (event: ChangeEvent<HTMLTextAreaElement>) => {
		recalculateHeight(event.target);
	};

	const recalculateHeight = (textArea: HTMLTextAreaElement) => {
		const textareaLineHeight = 24;
		const { minRows, maxRows } = state;

		const previousRows = textArea.rows;
		textArea.rows = minRows; // reset number of rows in textarea

		const currentRows = ~~(textArea.scrollHeight / textareaLineHeight);

		if (currentRows === previousRows) {
			textArea.rows = currentRows;
		}

		if (currentRows >= maxRows) {
			textArea.rows = maxRows;
			textArea.scrollTop = textArea.scrollHeight;
		}

		setState({
			...state,
			value: textArea.value,
			rows: currentRows < maxRows ? currentRows : maxRows,
		});
	};

	return (
		<textarea
			ref={inputEl}
			rows={state.rows}
			value={state.value}
			className={"textarea"}
			onChange={handleChange}
			onBlur={handleBlur}
		/>
	);
};

export const WithUrlEdit: React.FC<{
	item: IListItem;
	listItemActions: IListItemActions;
	inputElement: JSX.Element;
}> = ({ item, listItemActions, inputElement }) => {
	const { editUrlMode, urlInput, menuItem } = useUrlInput({
		item,
		updateItem: listItemActions.updateItem,
	});

	const input = editUrlMode ? urlInput : inputElement;
	return (
		<WithItemContextMenu
			itemId={item.id}
			onDelete={() => listItemActions.deleteItem(item.id)}
			menuItems={[menuItem]}
		>
			{input}
		</WithItemContextMenu>
	);
};

const ResizableTextAreaWithUrlEdit: React.FC<{
	item: IListItem;
	listItemActions: IListItemActions;
}> = ({ item, listItemActions }) => {
	return (
		<WithUrlEdit
			item={item}
			listItemActions={listItemActions}
			inputElement={
				<ResizableTextarea
					value={item.subject}
					rows={1}
					minRows={1}
					maxRows={10}
					updateItem={(txt) => {
						if (txt && txt.length && item.subject !== txt) {
							listItemActions!.updateItem({
								...item,
								subject: txt,
							});
						}
					}}
				/>
			}
		/>
	);
};
