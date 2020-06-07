import React, { useContext } from "react";
import { IHabitsTracker, HabitDay } from "../../models";
import { GoalsAreaContext } from "../../context";
import { OverlayTrigger, Popover } from "react-bootstrap";
import { ContextMenu, MenuItem, ContextMenuTrigger } from "react-contextmenu";
import { useModal } from "../../hooks/useModal";
import { AddDayNote } from "../Dialogs/AddDayNote";

interface IHabitDayProps {
  tracker: IHabitsTracker;
  day: HabitDay;
  isSelected: boolean;
  onDayClick: (
    e: React.MouseEvent<HTMLElement, MouseEvent>,
    day: HabitDay
  ) => void;
}

export const HabitDayCell: React.FC<IHabitDayProps> = ({
  tracker,
  day,
  isSelected,
  onDayClick,
}) => {
  const { addOrUpdate } = useContext(GoalsAreaContext);
  const { isShowing, toggle } = useModal();

  const handleAddNote = (noteText: string) => {
    let trackerDays = [...tracker.selectedDays];
    let dayIndex = trackerDays.findIndex((d) => d.number === day.number);
    if (trackerDays[dayIndex].note !== noteText) {
      trackerDays[dayIndex].note = noteText;
      addOrUpdate({
        ...tracker,
        selectedDays: trackerDays,
      });
    }
  };

  const showAddNote = () => {
    toggle();
  };

  const { number, note } = day;
  let divID = `day-${tracker.id}-${number}`;
  const dayComponent = (
    <div id={divID} onClick={(e) => onDayClick(e, day)}>
      {number}
    </div>
  );

  if (!isSelected) return dayComponent;

  const overlayComponent = (
    <Popover id="popover-basic">
      <Popover.Title as="h3">Заметка</Popover.Title>
      <Popover.Content>{day.note}</Popover.Content>
    </Popover>
  );

  return (
    <>
      <ContextMenuTrigger id={`context-menu-${number}`}>
        <OverlayTrigger
          key={number}
          trigger={["hover", "focus"]}
          delay={{ show: 1000, hide: 400 }}
          placement="top"
          overlay={overlayComponent}
        >
          {dayComponent}
        </OverlayTrigger>
      </ContextMenuTrigger>
      <ContextMenu className="menu" id={`context-menu-${number}`}>
        <MenuItem onClick={showAddNote} className="menuItem">
          Добавить/Изменить заметку
        </MenuItem>
      </ContextMenu>
      {isShowing && (
        <AddDayNote
          show={isShowing}
          note={note}
          handleSubmit={handleAddNote}
          handleClose={toggle}
        />
      )}
    </>
  );
};
