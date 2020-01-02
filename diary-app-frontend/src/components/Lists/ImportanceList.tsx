/*@typescript-eslint/no-unused-vars*/
import React, { FC, useContext, useEffect } from "react";
import { FaTrash } from "react-icons/fa";
import { ListItemInput } from "./ListItemInput";
import { Store } from "../../context";
import { Thunks as eventThunks } from "../../actions/events-actions";
import "./style.css";
import Loader from "../Loader";
import { getEmptyEvent } from "../../utils";

interface IProps {
  header: string;
  withDate?: boolean;
  readonly?: boolean;
  style?: React.CSSProperties;
}

export const ImportanceList: FC<IProps> = ({
  header,
  withDate = false,
  readonly = false,
  style
}) => {
  const { list, dispatch, loading } = useContext(Store).events;

  useEffect(() => {
    dispatch && dispatch(eventThunks.loadEvents(header));
  }, []);

  const onDelete = (eventID: number) => {
    dispatch(eventThunks.deleteEvent(eventID));
  };

  const events = list.items.length === 0 ? [getEmptyEvent()] : list.items;

  return (
    <div style={style || { marginTop: "40px" }}>
      <h1 className="todo-list-header">{header}</h1>
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
