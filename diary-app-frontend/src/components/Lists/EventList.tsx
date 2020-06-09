/*@typescript-eslint/no-unused-vars*/
import React, { FC, useContext } from "react";
import { IEvent } from "../../models";
import ListHeaderInput from "./ListHeaderInput";
import { EventListContext } from "../../context";
import Loader from "../Loader";
import { getEmptyEvent, fillToNumber } from "../../utils";
import { EventInput } from "./EventInput";

interface EventListProps extends React.HtmlHTMLAttributes<HTMLDivElement> {
  withDate?: boolean;
  readonly?: boolean;
  renderHeader?: boolean;
}

export const EventList: FC<EventListProps> = ({
  withDate = false,
  readonly = false,
  renderHeader = true,
}) => {
  const {
    list,
    loading,
    deleteItem,
    updateListTitle,
    addOrUpdateItem,
  } = useContext(EventListContext);

  if (loading || !list) return <Loader />;

  const items = [...list.items].sort((e1, e2) => {
    return e1.date.getTime() - e2.date.getTime();
  });

  const events = fillToNumber(items, 6, getEmptyEvent);

  const getItemText = (event: IEvent): string => {
    if (!withDate || event.id === 0) return event.subject;

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
    <div className="mt-40">
      <h1 className="todo-list-header">
        <ListHeaderInput
          value={list.title}
          handleBlur={updateListTitle}
          readonly={!renderHeader}
        />
      </h1>
      <ul className="todos">
        {events.map((event: IEvent, i) => (
          <li key={event.id !== 0 ? event.id : i + 80} className="list-item">
            <EventInput
              event={event}
              updateEvent={addOrUpdateItem}
              deleteEvent={deleteItem}
              getItemText={readonly ? getItemText : null}
              readonly={readonly || event.readonly}
            />
          </li>
        ))}
      </ul>
    </div>
  );
};
