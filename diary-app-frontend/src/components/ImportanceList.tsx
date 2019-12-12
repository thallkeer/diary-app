import React, { FC, useContext, useReducer, useEffect } from "react";
import { ListItemInput } from "./ListItemInput";
import { AppContext } from "../contexts/app-state";
import { eventsReducer } from "../contexts/events";
import { EventActions } from "../actions/events-actions";

interface IProps {
  header: string;
}

export const ImportanceList: FC<IProps> = ({ header }) => {
  const context = useContext(AppContext);
  const { events, loading, thunks, loadEvents } = context;

  useEffect(() => {
    loadEvents(context);
  }, []);

  const updateEvent = (itemId: number, itemText: string) => {
    console.log(itemId);
    // const updatedEvents = events.map(ev =>
    //   ev.eventID == itemId ? { ...ev, subject: itemText } : ev
    // );

    thunks.updateEvent(itemId, itemText);
  };

  return (
    <div>
      <h1 className="todo-list-header">{header}</h1>
      {loading ? (
        <div>
          <h2>Loading...</h2>
        </div>
      ) : (
        <ul className="todo-list-list">
          {events.map(event => (
            <li key={event.eventID}>
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
                updateItem={updateEvent}
              />
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};
