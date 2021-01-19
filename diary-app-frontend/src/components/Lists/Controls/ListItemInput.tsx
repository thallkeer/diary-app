import React, { FC, useEffect, useRef } from "react";
import { IListItem } from "models";

interface ListItemInputPropsBase
	extends React.HTMLAttributes<HTMLInputElement> {
	updateItem?: (item: IListItem) => void;
	item: IListItem;
}

interface ListItemInputProps extends ListItemInputPropsBase {
	getItemText?: (item: IListItem) => string;
	readonly?: boolean;
}

interface useListItemInputProps {
	validateAndUpdate: (text: string) => void;
}

function useListItemInput(props: useListItemInputProps) {
	const { validateAndUpdate } = props;
	const inputRef = useRef<HTMLInputElement>(null);

	useEffect(() => {}, [validateAndUpdate, inputRef]);

	const handleBlur = () => {
		const { value } = inputRef.current as HTMLInputElement;
		if (!value && !value.length) return;
		validateAndUpdate(value);
	};

	const handleKeyPress = (event: React.KeyboardEvent) => {
		if (event.key === "Enter") handleBlur();
	};

	return { inputRef, handleBlur, handleKeyPress };
}

export const UrlInput: FC<ListItemInputPropsBase & { endEdit: () => void }> = ({
	item,
	updateItem,
	endEdit,
}) => {
	const validateAndUpdate = (value: string) => {
		if (value !== item.url) {
			item.url = value;
			updateItem(item);
		}
		endEdit();
	};

	const { inputRef, handleBlur, handleKeyPress } = useListItemInput({
		validateAndUpdate,
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

export const ListItemInput: FC<ListItemInputProps> = ({
	updateItem,
	item,
	getItemText = null,
	readonly = false,
	className,
}) => {
	const validateAndUpdate = (value: string) => {
		if (value !== item.subject) {
			item.subject = value;
			updateItem(item);
		}
	};

	const { inputRef, handleBlur, handleKeyPress } = useListItemInput({
		validateAndUpdate,
	});

	const inputValue = getItemText ? getItemText(item) : item.subject;

	const inputControl = (
		<input
			type="text"
			maxLength={200}
			defaultValue={inputValue}
			readOnly={readonly || (getItemText ? true : false)}
			onBlur={readonly ? null : handleBlur}
			onKeyPress={handleKeyPress}
			className={`${className || ""} list-item-input`}
			autoComplete={"off"}
			ref={inputRef}
		/>
	);

	if (item.url && item.url.length) {
		const url = item.url.includes("http") ? item.url : `https:/${item.url}`;
		return (
			<a
				className="item-url"
				href={url}
				target="_blank"
				rel="noopener noreferrer"
			>
				{inputControl}
			</a>
		);
	}
	return inputControl;
};
