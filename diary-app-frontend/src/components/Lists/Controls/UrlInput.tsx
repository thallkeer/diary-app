import React, { useRef } from "react";
import { useListItemInput } from "hooks/useInputs";
import { FC, useEffect } from "react";
import { ListItemInputPropsBase } from "./ListItemInput";

interface UrlInputProps extends ListItemInputPropsBase {
	endEdit: () => void;
}

const UrlInput: FC<UrlInputProps> = ({ item, updateItem, endEdit }) => {
	const inputRef = useRef(null);

	const validateAndUpdate = (value: string) => {
		if (value !== item.url) {
			updateItem({
				...item,
				url: value,
			});
		}
		endEdit();
	};

	const { inputText, setInputText, handleBlur, handleKeyPress } =
		useListItemInput({
			defaultValue: item.url,
			validateAndUpdate,
			onEmptyValue: endEdit,
		});

	useEffect(() => {
		if (inputRef !== null) inputRef.current.focus();
	}, [inputRef]);

	return (
		<input
			type="url"
			value={inputText}
			onChange={(e) => setInputText(e.target.value)}
			onKeyPress={handleKeyPress}
			onBlur={handleBlur}
			className="list-item-input"
			autoComplete={"off"}
			ref={inputRef}
		/>
	);
};

export { UrlInput };
