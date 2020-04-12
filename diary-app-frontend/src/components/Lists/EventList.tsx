/*@typescript-eslint/no-unused-vars*/
import React, { FC, useContext } from "react";
import { ListItemInput } from "./ListItemInput";
import { IEvent } from "../../models";
import { DeleteBtn } from "./DeleteBtn";
import ListHeaderInput from "./ListHeaderInput";
import { EventListContext } from "../../context";
import Loader from "../Loader";
import { getEmptyEvent, fillToNumber } from "../../utils";

type EventListProps = {
  withDate?: boolean;
  readonly?: boolean;
  className?: string;
  renderHeader?: boolean;
};

export const EventList: FC<EventListProps> = ({
  withDate = false,
  readonly = false,
  renderHeader = true,
  className,
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
    <div className={`mt-40 ${className}`}>
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
            <ListItemInput
              style={{ paddingLeft: "5px" }}
              item={event}
              updateItem={addOrUpdateItem}
              getItemText={readonly ? getItemText : null}
              readonly={readonly || event.readonly}
            />
            {event.id !== 0 && (
              <DeleteBtn onDelete={() => deleteItem(event.id)} />
            )}
          </li>
        ))}
      </ul>
    </div>
  );
};
