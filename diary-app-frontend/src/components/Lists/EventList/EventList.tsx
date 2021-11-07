import React, { FC } from "react";
import { IEvent, IEventList } from "models";
import { EventInput, IEventItemActions } from "../Controls/EventInput";
import { ListWithItems, IListActions } from "../CommonList/CommonListComponent";

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

	const events = [...eventList.items].sort(
		(e1, e2) => e1.date.getTime() - e2.date.getTime()
	);

	const getItemText = (event: IEvent): string => {
		if (event.id === 0) return event.subject;

		return `${event.date.toLocaleString("ru", {
			day: "numeric",
			month: "numeric",
		})} ${event.subject}`;
	};

	return (
		<ListWithItems
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
