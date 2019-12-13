/*@typescript-eslint/no-unused-vars*/
import React, { FC, useContext, useReducer, useEffect } from "react";
import { ListItemInput } from "./ListItemInput";
import { AppContext } from "../contexts";
import "./TodoList/style.css";

interface IProps {
  header: string;
}

export const ImportanceList: FC<IProps> = ({ header }) => {
  const { events, loading, thunks, dispatch } = useContext(AppContext);

  useEffect(() => {
    dispatch && dispatch(thunks.loadEvents());
  }, []);

  return (
    <div style={{ marginTop: "40px" }}>
      <h1 className="todo-list-header">{header}</h1>
      {loading ? (
        <div>
          <h2>Loading...</h2>
        </div>
      ) : (
        <ul className="todos">
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
              />
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};
