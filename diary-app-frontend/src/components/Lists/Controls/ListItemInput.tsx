import React, { FC, useEffect, useRef } from "react";
import { IListItem } from "models";
import { OverlayTrigger, Tooltip } from "react-bootstrap";

interface ListItemInputPropsBase
	extends React.HTMLAttributes<HTMLInputElement> {
	updateItem?: (item: IListItem) => void;
	item: IListItem;
}

interface ListItemInputProps extends ListItemInputPropsBase {
	getItemText?: (item: IListItem) => string;
	readonly?: boolean;
}

function useListItemInput(props: {
	validateAndUpdate: (text: string) => void;
	onEmptyValue?: () => void;
}) {
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

	return withOverlayAndAnchor(inputControl, item);
};

const withOverlayAndAnchor = (component: JSX.Element, item: IListItem) =>
	withOverlay(withAnchorLink(component, item), item);

const withAnchorLink = (component: JSX.Element, item: IListItem) => {
	if (item.url && item.url.length) {
		const url = item.url.includes("http") ? item.url : `https:/${item.url}`;
		return (
			<span className="item-url">
				<a href={url} target="_blank" rel="noopener noreferrer">
					{component}
				</a>
			</span>
		);
	}
	return component;
};

const withOverlay = (component: JSX.Element, item: IListItem) => {
	if (item.id === 0 || !item.subject) return component;
	return (
		<OverlayTrigger
			key={item.id}
			delay={{ show: 400, hide: 400 }}
			trigger={["hover", "focus"]}
			placement="bottom"
			overlay={
				<Tooltip id={`list-item-input-tooltip-${item.id}`}>
					{item.subject}
				</Tooltip>
			}
		>
			{component}
		</OverlayTrigger>
	);
};
