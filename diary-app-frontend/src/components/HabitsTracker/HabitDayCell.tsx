import React from "react";
import { OverlayTrigger, Popover } from "react-bootstrap";
import { ContextMenu, MenuItem, ContextMenuTrigger } from "react-contextmenu";
import { useModal } from "../../hooks/useModal";
import { IHabitDay, IHabitTracker } from "models";
import { AddDayNote } from "../Dialogs/AddDayNote";

interface IHabitDayProps {
	tracker: IHabitTracker;
	updateHabitTracker: (tracker: IHabitTracker) => void;
	day: IHabitDay;
	isSelected: boolean;
	onDayClick: (
		e: React.MouseEvent<HTMLElement, MouseEvent>,
		day: IHabitDay
	) => void;
}

export const HabitDayCell: React.FC<IHabitDayProps> = ({
	tracker,
	updateHabitTracker,
	day,
	isSelected,
	onDayClick,
}) => {
	const { isShowing, toggle } = useModal();
	const { number, note } = day;

	const dayComponent = (
		<div id={`day-${tracker.id}-${number}`} onClick={(e) => onDayClick(e, day)}>
			{number}
		</div>
	);

	const handleAddNote = (noteText: string) => {
		const trackerDays = [...tracker.items];
		const dayIndex = trackerDays.findIndex((d) => d.number === day.number);
		if (trackerDays[dayIndex].note !== noteText) {
			trackerDays[dayIndex].note = noteText;
			updateHabitTracker({
				...tracker,
				items: trackerDays,
			});
		}
	};

	if (!isSelected) return dayComponent;

	const overlayComponent = (
		<Popover id="popover-basic">
			<Popover.Title as="h3">Заметка</Popover.Title>
			<Popover.Content>{day.note}</Popover.Content>
		</Popover>
	);

	return (
		<>
			<ContextMenuTrigger id={`day-context-menu-${number}`}>
				<OverlayTrigger
					key={number}
					trigger={["hover", "focus"]}
					delay={{ show: 500, hide: 400 }}
					placement="top"
					overlay={overlayComponent}
				>
					{dayComponent}
				</OverlayTrigger>
			</ContextMenuTrigger>
			<ContextMenu className="menu" id={`day-context-menu-${number}`}>
				<MenuItem onClick={toggle} className="menuItem">
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
