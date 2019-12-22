/*@typescript-eslint/no-unused-vars*/
import React, { FC, useContext, useEffect } from "react";
import { FaTrash } from "react-icons/fa";
import { ListItemInput } from "./ListItemInput";
import { AppContext } from "../contexts";
import "./TodoList/style.css";
import { getEvents } from "../selectors/events";

interface IProps {
  header: string;
  style?: React.CSSProperties;
}

export const ImportanceList: FC<IProps> = ({ header, style }) => {
  const { eventList, loading, thunks, dispatch } = useContext(AppContext);

  style = style || { marginTop: "40px" };

  useEffect(() => {
    dispatch && dispatch(thunks.loadEvents(header));
  }, []);

  const onDelete = (eventID: number) => {
    dispatch(thunks.deleteEvent(eventID));
  };

  return (
    <div style={style}>
      <h1 className="todo-list-header">{header}</h1>
      {loading ? (
        <div>
          <h2>Loading...</h2>
        </div>
      ) : (
        <ul className="todos">
          {eventList.items.map(event => (
            <li key={event.eventID} className="event">
              <ListItemInput
                itemId={event.eventID}
                itemText={
                  event.date.toLocaleString("ru", {
                    day: "numeric",
                    month: "numeric"
                  }) +
                  " " +
                  event.subject
                }
                readonly={true}
              />
              <span
                className="delete-event-btn"
                onClick={() => onDelete(event.eventID)}
              >
                <FaTrash />
              </span>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};
