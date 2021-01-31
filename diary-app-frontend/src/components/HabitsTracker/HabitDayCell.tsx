import React from "react";
import { OverlayTrigger, Popover } from "react-bootstrap";
import { ContextMenu, MenuItem, ContextMenuTrigger } from "react-contextmenu";
import { useModal } from "../../hooks/useModal";
import { IHabitDay, IHabitTracker } from "models";
import { AddDayNote } from "../Dialogs/AddDayNote";

interface IHabitDayProps {
	tracker: IHabitTracker;
	day: IHabitDay;
	isMarked: boolean;
	updateHabitTracker: (tracker: IHabitTracker) => void;
	className: string;
}

export const HabitDayCell: React.FC<IHabitDayProps> = ({
	tracker,
	day,
	updateHabitTracker,
	isMarked,
	className,
}) => {
	const { isShowing, toggle } = useModal();
	const { number, note } = day;

	const dayComponent = <div id={`day-${tracker.id}-${number}`}>{number}</div>;

	if (!isMarked) return dayComponent;

	const handleAddNote = (noteText: string) => {
		if (note !== noteText) {
			updateHabitTracker({
				...tracker,
				items: tracker.items.map((d) =>
					d.id === day.id ? { ...d, note: noteText } : d
				),
			});
		}
	};

	const overlayComponent = (
		<Popover id="popover-basic">
			<Popover.Title as="h3">Заметка</Popover.Title>
			<Popover.Content>{day.note}</Popover.Content>
		</Popover>
	);

	const withOverlay = (
		<OverlayTrigger
			key={number}
			trigger={["hover", "focus"]}
			delay={{ show: 500, hide: 400 }}
			placement="top"
			overlay={overlayComponent}
		>
			{dayComponent}
		</OverlayTrigger>
	);
	//TODO: fix problem with context menu and clickable day cell
	return note.length ? withOverlay : dayComponent;

	const menuItemText = `${note.length ? "Изменить" : "Добавить"} заметку`;

	return (
		<>
			<ContextMenuTrigger id={`day-context-menu-${number}`}>
				{note.length ? withOverlay : dayComponent}
			</ContextMenuTrigger>
			<ContextMenu className="menu" id={`day-context-menu-${number}`}>
				<MenuItem
					onClick={() => {
						console.log("TOGGLING ", number);
						toggle();
					}}
					className="menuItem"
				>
					{menuItemText}
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
