import React, { FC } from "react";
import { IListItem } from "models";
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import { useListItemInput } from "hooks/useInputs";

export interface ListItemInputPropsBase
	extends React.HTMLAttributes<HTMLInputElement> {
	updateItem?: (item: IListItem) => void;
	item: IListItem;
}

interface ListItemInputProps extends ListItemInputPropsBase {
	getItemText?: (item: IListItem) => string;
	readonly?: boolean;
}

const ListItemInput: FC<ListItemInputProps> = (props: ListItemInputProps) => {
	const {
		updateItem,
		item,
		getItemText = null,
		readonly = false,
		className,
	} = props;

	const validateAndUpdate = (value: string) => {
		if (value !== item.subject) {
			updateItem({
				...item,
				subject: value,
			});
		}
	};
	const inputValue = getItemText ? getItemText(item) : item.subject;

	const { inputText, setInputText, handleBlur, handleKeyPress } =
		useListItemInput({
			validateAndUpdate,
			defaultValue: inputValue,
		});

	const inputControl = (
		<input
			type="text"
			maxLength={200}
			value={inputText}
			readOnly={readonly || (getItemText ? true : false)}
			onBlur={readonly ? null : handleBlur}
			onKeyPress={handleKeyPress}
			className={`${className || ""} list-item-input`}
			autoComplete={"off"}
			onChange={(e) => setInputText(e.target.value)}
		/>
	);

	return withOverlayAndAnchor(inputControl, item);
};

const withOverlayAndAnchor = (component: JSX.Element, item: IListItem) =>
	withOverlay(withAnchorLink(component, item), item);

const withAnchorLink = (component: JSX.Element, item: IListItem) => {
	if (item.url && item.url.length) {
		const url = item.url.startsWith("http") ? item.url : `https:/${item.url}`;
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

export { ListItemInput };
