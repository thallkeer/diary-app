/*@typescript-eslint/no-unused-vars*/
import React, { FC } from "react";
import { ListItemInput } from "./ListItemInput";
import {
  Thunks as eventThunks,
  EventThunks
} from "../../actions/events-actions";
import { getEmptyEvent } from "../../utils";
import { IEvent, IEventList } from "../../models";
import { useFillToNumber } from "../../hooks/useFillToNumber";
import { DeleteBtn } from "./DeleteBtn";

interface IProps {
  withDate?: boolean;
  readonly?: boolean;
  className?: string;
  fillToNumber?: number;
  eventList: IEventList;
  dispatch: (action: EventThunks) => void;
}

export const EventList: FC<IProps> = ({
  withDate = false,
  readonly = false,
  className,
  eventList,
  dispatch,
  fillToNumber
}) => {
  const onDelete = (eventID: number) => {
    dispatch(eventThunks.deleteEvent(eventID));
  };

  const onUpdate = (event: IEvent) => {
    dispatch(
      eventThunks.addOrUpdateEvent({
        ...event,
        ownerID: eventList.id
      })
    );
  };

  const events = useFillToNumber(
    [...eventList.items],
    fillToNumber,
    getEmptyEvent
  );

  const getItemText = (event: IEvent): string => {
    if (!withDate || event.id === 0) return event.subject;

    return (
      event.date.toLocaleString("ru", {
        day: "numeric",
        month: "numeric"
      }) +
      " " +
      event.subject
    );
  };

  return (
    <div style={{ marginTop: "40px" }} className={className}>
      <h1 className="todo-list-header">{eventList.title}</h1>
      <ul className="todos">
        {events.map((event: IEvent, i) => (
          <li key={event.id !== 0 ? event.id : i + 80} className="list-item">
            <ListItemInput
              item={event}
              updateItem={onUpdate}
              readonlyMode={readonly ? { getItemText } : null}
            />
            {event.id !== 0 && (
              <DeleteBtn onDelete={() => onDelete(event.id)} />
            )}
          </li>
        ))}
      </ul>
    </div>
  );
};
