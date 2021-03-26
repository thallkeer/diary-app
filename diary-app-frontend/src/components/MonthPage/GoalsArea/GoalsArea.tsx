import React from "react";
import { Row } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { goalsListsThunks } from "../../../store/pageAreaLists/goalsLists/goalsLists.actions";
import { useMonthPageArea } from "../../../hooks/usePageArea";
import {
	getGoalsArea,
	getGoalsLists,
} from "../../../store/pages/pages.selectors";
import { AddListBtn } from "../../AddListBtn";
import Loader from "../../Loader";
import { GoalList } from "./GoalList";
import { goalsAreaComponent } from "store/pageAreas/goalsArea.reducer";
import { IHabitTracker } from "models";

const GoalsArea: React.FC = () => {
	const dispatch = useDispatch();
	const { area, isLoading } = useMonthPageArea(
		getGoalsArea,
		goalsAreaComponent
	);
	const goalsLists = useSelector(getGoalsLists);

	if (isLoading || !goalsLists) return <Loader />;

	const addGoalsList = () => {
		const tracker: IHabitTracker = {
			id: 0,
			goalName: "Цель на месяц",
			goalsAreaId: area.id,
			items: [],
		};
		dispatch(goalsListsThunks.addGoalsList(tracker));
	};

	return (
		<>
			<h1 className="mt-40 area-header">{area.header}</h1>
			{goalsLists.map((gl, i) => (
				<GoalList key={gl.list.id} reversed={i % 2 === 0} goalList={gl} />
			))}
			<Row className="mt-20">
				<AddListBtn onClick={addGoalsList} />
			</Row>
		</>
	);
};

export { GoalsArea };
