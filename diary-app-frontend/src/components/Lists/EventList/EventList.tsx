/*@typescript-eslint/no-unused-vars*/
import React, { FC, useContext } from "react";
import { IEvent } from "../../../models";
import Loader from "../../Loader";
import { getEmptyEvent, fillToNumber } from "../../../utils";
import { EventInput } from "../Controls/EventInput";
import { EventListContext } from "./EventListState";
import { CommonListComponent } from "../CommonList/CommonListComponent";

export const EventList: FC = () => {
	const { eventListState, listFunctions } = useContext(EventListContext);
	const { deleteListItem, addOrUpdateItem } = listFunctions;
	const { list, loading } = eventListState;
	const eventList = list;

	if (loading || !eventList) return <Loader />;

	const items = [...eventList.items].sort(
		(e1, e2) => e1.date.getTime() - e2.date.getTime()
	);

	const events = fillToNumber(items, 6, getEmptyEvent);

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
					updateEvent={addOrUpdateItem}
					deleteEvent={deleteListItem}
					getItemText={getItemText}
					readonly={true}
				/>
			)}
		/>
	);
};
