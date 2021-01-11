import React from "react";
import { Col, Row } from "react-bootstrap";
import { IHabitTracker } from "../../models/entities";
import { HabitTracker } from "../HabitsTracker/HabitTracker";
import { DeleteBtn } from "../Lists/Controls/DeleteBtn";
import ListHeaderInput from "../Lists/Controls/ListHeaderInput";

export const TrackerRow: React.FC<{
	tracker: IHabitTracker;
	updateHabitTracker: (tracker: IHabitTracker) => void;
	deleteHabitTracker: (tracker: IHabitTracker) => void;
	index: number;
}> = ({ index, tracker, updateHabitTracker, deleteHabitTracker }) => {
	const handleBlur = (title: string) => {
		updateHabitTracker({ ...tracker, goalName: title });
	};

	let components = [
		<Col key={index + 1} md={4}>
			<h3 className="tracker-header" style={{ marginTop: "15px" }}>
				<ListHeaderInput value={tracker.goalName} handleBlur={handleBlur} />
				<DeleteBtn onDelete={() => deleteHabitTracker(tracker)} />
			</h3>
		</Col>,
		<Col key={index + 2} md={8}>
			<HabitTracker tracker={tracker} updateHabitTracker={updateHabitTracker} />
		</Col>,
	];

	return (
		<Row className="mt-20">
			{index % 2 === 0 ? components : components.reverse()}
		</Row>
	);
};
