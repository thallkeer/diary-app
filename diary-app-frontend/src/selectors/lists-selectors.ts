import { createSelector } from "reselect";
import { IEvent } from "models";
import { getImportantEventsList } from "./pages.selectors";
export const getEventsByDay = createSelector(
	[getImportantEventsList],
	(importantEvents) => {
		const eventsMap = new Map<number, IEvent[]>();

		const events = importantEvents.list?.items ?? [];

		if (events && events.length) {
			events.forEach((ev) => {
				const day = ev.date.getDate();
				if (!eventsMap.has(day)) eventsMap.set(day, []);
				eventsMap.get(day).push(ev);
			});
		}

		return eventsMap;
	}
);
