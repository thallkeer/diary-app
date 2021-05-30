import React from "react";
import { Row } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { loadGoalsArea } from "store/pageAreas/goalsArea.reducer";
import { IHabitTracker } from "models";
import { useMonthPageArea } from "hooks/usePageArea";
import { setGoalLists, addGoalList } from "store/pageAreaLists/goalLists.slice";
import Loader from "components/Loader";
import { getGoalsArea, getGoalsLists } from "selectors/pages.selectors";
import { GoalList } from "./GoalList";
import { AddListBtn } from "components/AddListBtn";

const GoalsArea: React.FC = () => {
	const dispatch = useDispatch();
	const { area, status } = useMonthPageArea(
		getGoalsArea,
		loadGoalsArea,
		(area) => setGoalLists(area.goalLists)
	);
	const goalsLists = useSelector(getGoalsLists);

	if (status === "idle" || status === "loading" || !goalsLists)
		return <Loader />;

	const addGoalsList = () => {
		const tracker: IHabitTracker = {
			id: 0,
			goalName: "Цель на месяц",
			goalsAreaId: area.id,
			items: [],
		};
		dispatch(addGoalList(tracker));
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
