import React, { FC } from "react";
import { ListItemInput } from "./ListItemInput";
import { IEvent } from "models";
import { WithItemContextMenu } from "../CommonList/CommonListComponent";

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

	const eventInput = (
		<ListItemInput
			className="no-left-padding"
			item={event}
			updateItem={updateEvent}
			readonly={readonly}
			getItemText={getItemText}
		/>
	);

	return (
		<WithItemContextMenu
			component={eventInput}
			itemId={event.id}
			onDelete={handleDeleteClick}
		/>
	);
};
