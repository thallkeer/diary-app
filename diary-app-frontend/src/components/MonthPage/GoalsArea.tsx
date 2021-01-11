import React from "react";
import { Row } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { loadGoalsArea } from "../../context/reducers/pageArea/goalsArea-reducer";
import { goalsListsActions } from "../../context/reducers/pageAreaLists/goalLists-reducer";
import { usePageArea } from "../../hooks/usePageArea";
import { IGoalsArea, IHabitTracker } from "../../models/entities";
import { IGoalsAreaState } from "../../models/states";
import { getGoalsArea, getGoalsLists } from "../../selectors/page-selectors";
import { AddListBtn } from "../AddListBtn";
import Loader from "../Loader";
import { TrackerRow } from "./TrackerRow";

const GoalsArea: React.FC = () => {
	const dispatch = useDispatch();
	const { area, isLoading, monthPage } = usePageArea<
		IGoalsAreaState,
		IGoalsArea
	>(getGoalsArea, (dispatch, pageId) => {
		dispatch(loadGoalsArea(pageId));
	});
	const goalsLists = useSelector(getGoalsLists);

	if (isLoading || !area || !goalsLists) return <Loader />;

	const addHabitsTracker = (habitTracker?: IHabitTracker) => {
		if (!habitTracker)
			habitTracker = {
				id: 0,
				goalName: "Цель на месяц",
				goalsAreaId: area.id,
				items: [],
				pageId: monthPage.id,
			};
		dispatch(goalsListsActions.addOrUpdateTracker(habitTracker));
	};

	return (
		<>
			<h1 className="mt-40 area-header">{area.header}</h1>
			{goalsLists.map((gl, i) => (
				<TrackerRow
					key={gl.list.id}
					index={i}
					tracker={gl.list}
					updateHabitTracker={addHabitsTracker}
					deleteHabitTracker={(tr) => {
						dispatch(goalsListsActions.deleteTracker(tr.id));
					}}
				/>
			))}
			<Row className="mt-20">
				<AddListBtn onClick={() => addHabitsTracker()} />
			</Row>
		</>
	);
};

export default GoalsArea;
