import React from "react";
import { FaTrash } from "react-icons/fa";

export const DeleteBtn: React.FC<{ onDelete: () => void }> = ({ onDelete }) => {
	return (
		<span className="delete-list-item-btn" onClick={onDelete}>
			<FaTrash />
		</span>
	);
};
