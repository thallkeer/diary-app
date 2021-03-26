import React from "react";
import { ListItemInputPropsBase } from "components/Lists/Controls/ListItemInput";
import { UrlInput } from "components/Lists/Controls/UrlInput";
import { useEffect, useRef, useState } from "react";
import { MenuItem } from "react-contextmenu";

const useListItemInput = (props: {
	validateAndUpdate: (text: string) => void;
	onEmptyValue?: () => void;
}) => {
	const { validateAndUpdate, onEmptyValue } = props;
	const inputRef = useRef<HTMLInputElement>(null);

	useEffect(() => {}, [validateAndUpdate, inputRef]);

	const handleBlur = () => {
		const { value } = inputRef.current as HTMLInputElement;
		if (!value || !value.length) {
			if (onEmptyValue) onEmptyValue();
			return;
		}
		validateAndUpdate(value);
	};

	const handleKeyPress = (event: React.KeyboardEvent) => {
		if (event.key === "Enter") handleBlur();
	};

	return { inputRef, handleBlur, handleKeyPress };
};

const useUrlInput = (urlInputProps: ListItemInputPropsBase) => {
	const [editUrlMode, setEditUrlMode] = useState(false);
	const { item, updateItem } = urlInputProps;

	const handleAddHyperlinkClick = () => {
		setEditUrlMode(true);
	};

	const urlInput = (
		<UrlInput
			item={item}
			updateItem={updateItem}
			endEdit={() => setEditUrlMode(false)}
		/>
	);

	const menuItem = (
		<MenuItem
			key="menu-item-add"
			onClick={handleAddHyperlinkClick}
			className="menuItem"
		>
			Добавить/Изменить гиперссылку
		</MenuItem>
	);

	return {
		editUrlMode,
		urlInput,
		menuItem,
	};
};

export { useListItemInput, useUrlInput };
