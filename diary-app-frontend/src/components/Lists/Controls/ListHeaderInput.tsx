import React, { useRef } from "react";

interface ListHeaderProps {
	value: string;
	handleBlur: (title: string) => void;
	readonly?: boolean;
}

export const ListHeaderInput: React.FC<ListHeaderProps> = ({
	value,
	handleBlur,
	readonly = false,
}) => {
	const handleKeyPress = (event: React.KeyboardEvent<HTMLInputElement>) => {
		if (event.key === "Enter") handleBlur(titleInput.current.value);
	};

	const onBlur = () => {
		if (!readonly && value !== titleInput.current.value)
			handleBlur(titleInput.current.value);
	};

	const titleInput = useRef(null);

	return (
		<input
			ref={titleInput}
			defaultValue={value}
			className="list-header-input"
			type="text"
			maxLength={50}
			onBlur={onBlur}
			onKeyPress={handleKeyPress}
			autoComplete="off"
			readOnly={readonly}
		/>
	);
};

export default ListHeaderInput;
