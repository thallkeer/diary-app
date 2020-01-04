/*@typescript-eslint/no-unused-vars*/
import React, { FC, useContext, useEffect } from "react";
import { FaTrash } from "react-icons/fa";
import { ListItemInput } from "./ListItemInput";
import { Store } from "../../context";
import { Thunks as eventThunks } from "../../actions/events-actions";
import "./style.css";
import Loader from "../Loader";
import { getEmptyEvent } from "../../utils";
import { IEventList } from "../../models";

interface IProps {
  withDate?: boolean;
  readonly?: boolean;
  style?: React.CSSProperties;
  eventList?: IEventList;
}

export const ImportanceList: FC<IProps> = ({
  withDate = false,
  readonly = false,
  style,
  eventList
}) => {
  const { dispatch } = useContext(Store).events;

  const onDelete = (eventID: number) => {
    dispatch(eventThunks.deleteEvent(eventID));
  };

  const events = eventList.items;
  ///TODO:
  const loading = false;
  // (eventList && eventList.items) ?? list.items.length === 0
  //   ? [getEmptyEvent()]
  //   : list.items;

  return (
    <div style={style || { marginTop: "40px" }}>
      <h1 className="todo-list-header">{eventList.title}</h1>
      {loading ? (
        <Loader />
      ) : (
        <ul className="todos">
          {events.map(event => (
            <li key={event.id} className="event">
              <ListItemInput
                itemId={event.id}
                itemText={
                  withDate
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
      )}
    </div>
  );
};
