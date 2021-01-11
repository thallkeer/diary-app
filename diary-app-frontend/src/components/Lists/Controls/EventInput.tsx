import React, { FC, useEffect } from "react";
import { ListItemInput } from "./ListItemInput";
import { IEvent } from "../../../models/entities";
import { withContextMenu } from "../CommonList/CommonListComponent";

export interface IEventItemActions {
	updateEvent: (event: IEvent) => void;
	deleteEvent: (eventID: number) => void;
	getItemText?: (event: IEvent) => string;
}

interface IEventInputProps extends IEventItemActions {
	event: IEvent;
	readonly: boolean;
}

export const EventInput: FC<IEventInputProps> = ({
	updateEvent,
	deleteEvent,
	event,
	getItemText,
	readonly,
}) => {
	const handleDeleteClick = () => {
		deleteEvent(event.id);
	};

	const updateEventItem = (eventItem: IEvent) => {
		updateEvent(eventItem);
	};

	useEffect(() => {}, [event, updateEvent, deleteEvent, getItemText]);

	const eventInput = (
		<ListItemInput
			className="no-left-padding"
			item={event}
			updateItem={updateEventItem}
			readonly={readonly}
			getItemText={getItemText}
		/>
	);

	if (event.id === 0) return eventInput;

	return withContextMenu(eventInput, event.id, handleDeleteClick);
};
