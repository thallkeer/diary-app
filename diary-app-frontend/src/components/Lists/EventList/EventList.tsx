import React, { FC } from "react";
import { IEvent, IEventList } from "../../../models";
import { getEmptyEvent, fillToNumber } from "../../../utils";
import { EventInput, IEventItemActions } from "../Controls/EventInput";
import {
	CommonListComponent,
	IListActions,
} from "../CommonList/CommonListComponent";

export interface IEventListProps extends IListActions {
	eventList: IEventList;
	eventItemActions: IEventItemActions;
	className: string;
}

export const EventList: FC<IEventListProps> = ({
	eventList,
	eventItemActions,
}) => {
	const { updateEvent, deleteEvent } = eventItemActions;

	const items = [...eventList.items].sort(
		(e1, e2) => e1.date.getTime() - e2.date.getTime()
	);

	const events = fillToNumber(items, 6, () => getEmptyEvent(eventList.id));

	const getItemText = (event: IEvent): string => {
		if (event.id === 0) return event.subject;

		return (
			event.date.toLocaleString("ru", {
				day: "numeric",
				month: "numeric",
			}) +
			" " +
			event.subject
		);
	};

	return (
		<CommonListComponent
			className={"mt-40"}
			items={events}
			listTitle={eventList.title}
			isDeletable={false}
			readonlyTitle={true}
			renderItem={(event: IEvent) => (
				<EventInput
					event={event}
					updateEvent={updateEvent}
					deleteEvent={deleteEvent}
					getItemText={getItemText}
					readonly={true}
				/>
			)}
		/>
	);
};
