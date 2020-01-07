/*@typescript-eslint/no-unused-vars*/
import React, { FC, useContext } from "react";
import { FaTrash } from "react-icons/fa";
import { ListItemInput } from "./ListItemInput";
import { Thunks as eventThunks } from "../../actions/events-actions";
import Loader from "../Loader";
import { getEmptyEvent } from "../../utils";
import { IEvent } from "../../models";
import { useFillToNumber } from "../../hooks/useFillToNumber";
import { getEvents } from "../../selectors";
import { EventListContext } from "../../context";

interface IProps {
  withDate?: boolean;
  readonly?: boolean;
  className?: string;
  fillToNumber?: number;
}

export const EventList: FC<IProps> = ({
  withDate = false,
  readonly = false,
  className,
  fillToNumber
}) => {
  const { eventList, dispatch } = useContext(EventListContext);

  const onDelete = (eventID: number) => {
    dispatch(eventThunks.deleteEvent(eventID));
  };

  const events = useFillToNumber(
    [...getEvents(eventList)],
    fillToNumber,
    getEmptyEvent
  );

  if (eventList.loading) return <Loader />;

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
      <h1 className="todo-list-header">{eventList.list.title}</h1>
      <ul className="todos">
        {events.map((event: IEvent, i) => (
          <li key={event.id !== 0 ? event.id : i + 80} className="event">
            <ListItemInput
              item={event}
              readonlyMode={{ getItemText, readonly }}
            />
            {event.id !== 0 && (
              <span
                className="delete-event-btn"
                onClick={() => onDelete(event.id)}
              >
                <FaTrash />
              </span>
            )}
          </li>
        ))}
      </ul>
    </div>
  );
};
