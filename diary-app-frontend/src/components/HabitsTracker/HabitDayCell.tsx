import React, { useContext } from "react";
import { OverlayTrigger, Popover } from "react-bootstrap";
import { ContextMenu, MenuItem, ContextMenuTrigger } from "react-contextmenu";
import { useModal } from "../../hooks/useModal";
import { IHabitDay, IHabitTracker } from "../../models/entities";
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

	const handleAddNote = (noteText: string) => {
		let trackerDays = [...tracker.items];
		let dayIndex = trackerDays.findIndex((d) => d.number === day.number);
		if (trackerDays[dayIndex].note !== noteText) {
			trackerDays[dayIndex].note = noteText;
			updateHabitTracker({
				...tracker,
				items: trackerDays,
			});
		}
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
