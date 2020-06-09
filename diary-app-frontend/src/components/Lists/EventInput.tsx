import React, { FC, useState, useEffect } from "react";
import { CheckCircle } from "./CheckCircle";
import { ListItemInput, UrlInput } from "./ListItemInput";
import { IEvent } from "../../models";
import { ContextMenu, MenuItem, ContextMenuTrigger } from "react-contextmenu";

interface IEventInputProps {
  event: IEvent;
  updateEvent: (event: IEvent) => void;
  deleteEvent: (eventID: number) => void;
  getItemText: (event: IEvent) => string;
  readonly: boolean;
}

export const EventInput: FC<IEventInputProps> = ({
  updateEvent,
  deleteEvent,
  event,
  getItemText,
  readonly,
}) => {
  const handleDeleteClick = () => {
    deleteEvent(event.id);
  };

  const updateEventItem = (eventItem: IEvent) => {
    updateEvent(eventItem);
  };

  useEffect(() => {}, [event, updateEvent, deleteEvent, getItemText]);

  const eventInput = (
    <ListItemInput
      className="no-left-padding"
      item={event}
      updateItem={updateEventItem}
      readonly={readonly}
      getItemText={getItemText}
    />
  );

  if (event.id === 0) return eventInput;

  return (
    <>
      <ContextMenuTrigger id={`context-menu-${event.id}`}>
        {eventInput}
      </ContextMenuTrigger>
      <ContextMenu className="menu" id={`context-menu-${event.id}`}>
        <MenuItem onClick={handleDeleteClick} className="menuItem">
          Удалить запись
        </MenuItem>
      </ContextMenu>
    </>
  );
};
