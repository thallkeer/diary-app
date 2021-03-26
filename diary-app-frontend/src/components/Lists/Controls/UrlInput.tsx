import React from "react";
import { useListItemInput } from "hooks/useInputs";
import { FC, useEffect } from "react";
import { ListItemInputPropsBase } from "./ListItemInput";

interface UrlInputProps extends ListItemInputPropsBase {
	endEdit: () => void;
}

const UrlInput: FC<UrlInputProps> = ({ item, updateItem, endEdit }) => {
	const validateAndUpdate = (value: string) => {
		if (value !== item.url) {
			item.url = value;
			updateItem(item);
		}
		endEdit();
	};

	const { inputRef, handleBlur, handleKeyPress } = useListItemInput({
		validateAndUpdate,
		onEmptyValue: endEdit,
	});

	useEffect(() => {
		if (inputRef !== null) inputRef.current.focus();
	}, [inputRef]);

	return (
		<input
			type="url"
			defaultValue={item.url}
			onKeyPress={handleKeyPress}
			onBlur={handleBlur}
			className="list-item-input"
			autoComplete={"off"}
			ref={inputRef}
		/>
	);
};

export { UrlInput };
