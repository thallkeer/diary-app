/*@typescript-eslint/no-unused-vars*/
import React, { FC, useContext } from "react";
import { FaTrash } from "react-icons/fa";
import { ListItemInput } from "./ListItemInput";
import { Store } from "../../context";
import { Thunks as eventThunks } from "../../actions/events-actions";
import Loader from "../Loader";
import { getEmptyEvent } from "../../utils";
import { IEventList } from "../../models";

interface IProps {
  withDate?: boolean;
  readonly?: boolean;
  className?: string;
  eventList: IEventList;
  fillToNumber?: number;
  loading?: boolean;
}

export const ImportanceList: FC<IProps> = ({
  withDate = false,
  readonly = false,
  className,
  eventList,
  fillToNumber,
  loading = false
}) => {
  const { dispatch } = useContext(Store).events;

  const onDelete = (eventID: number) => {
    dispatch(eventThunks.deleteEvent(eventID));
  };

  const events = eventList.items;

  if (fillToNumber && events.length < fillToNumber) {
    for (let i = events.length; i < fillToNumber; i++) {
      events.push(getEmptyEvent());
    }
  }

  return (
    <div style={{ marginTop: "40px" }} className={className}>
      {loading ? (
        <Loader />
      ) : (
        <>
          <h1 className="todo-list-header">{eventList.title}</h1>
          <ul className="todos">
            {events.map((event, i) => (
              <li key={event.id !== 0 ? event.id : i + 80} className="event">
                <ListItemInput
                  itemId={event.id}
                  itemText={
                    withDate && event.id !== 0
                      ? event.date.toLocaleString("ru", {
                          day: "numeric",
                          month: "numeric"
                        }) +
                        " " +
                        event.subject
                      : event.subject
                  }
                  readonly={readonly}
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
        </>
      )}
    </div>
  );
};
