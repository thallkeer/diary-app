/*@typescript-eslint/no-unused-vars*/
import React, { FC } from "react";
import { ListItemInput } from "./ListItemInput";
import {
  Thunks as eventThunks,
  EventThunks
} from "../../actions/events-actions";
import { IEvent, IEventList } from "../../models";
import { DeleteBtn } from "./DeleteBtn";
import ListHeaderInput from "./ListHeaderInput";
import { useEventList } from "../../hooks/useLists";

type EventListProps = {
  withDate?: boolean;
  readonly?: boolean;
  className?: string;
  fillToNumber?: number;
  eventList: IEventList;
  dispatch: (action: EventThunks) => void;
};

export const EventList: FC<EventListProps> = ({
  withDate = false,
  readonly = false,
  className,
  eventList,
  dispatch,
  fillToNumber
}) => {
  const { addOrUpdateEvent, deleteEvent, updateEventList } = eventThunks;
  const [title, setTitle, events] = useEventList(eventList, fillToNumber);

  const onDelete = (eventID: number) => {
    eventID !== 0 && dispatch(deleteEvent(eventID));
  };

  const onUpdate = (event: IEvent) => {
    dispatch(
      addOrUpdateEvent({
        ...event,
        ownerID: eventList.id
      })
    );
  };

  const handleBlur = () => {
    dispatch(
      updateEventList({
        ...eventList,
        title
      })
    );
  };

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
    <div className={`mt-40 ${className}`}>
      <h1 className="todo-list-header">
        <ListHeaderInput
          value={title}
          handleChange={(e: React.ChangeEvent<HTMLInputElement>) =>
            setTitle(e.target.value)
          }
          handleBlur={handleBlur}
        />
      </h1>
      <ul className="todos">
        {events.map((event: IEvent, i) => (
          <li key={event.id !== 0 ? event.id : i + 80} className="list-item">
            <ListItemInput
              item={event}
              updateItem={onUpdate}
              getItemText={readonly ? getItemText : null}
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
