import React from "react";
import { Row } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { goalsListsThunks } from "../../../store/pageAreaLists/goalsLists/goalsLists.actions";
import { usePageArea } from "../../../hooks/usePageArea";
import {
	getGoalsArea,
	getGoalsLists,
} from "../../../store/pages/pages.selectors";
import { AddListBtn } from "../../AddListBtn";
import Loader from "../../Loader";
import { GoalList } from "./GoalList";
import { IGoalsArea } from "models/PageAreas/pageAreas";
import { IGoalsAreaState } from "store/pageAreas/goals/goalsArea.reducer";
import { IHabitTracker } from "models";
import { loadGoalsArea } from "store/pageAreas/goals/goalsArea.actions";

const GoalsArea: React.FC = () => {
	const dispatch = useDispatch();
	const { area, isLoading, monthPage } = usePageArea<
		IGoalsAreaState,
		IGoalsArea
	>(getGoalsArea, (disp, pageId) => {
		disp(loadGoalsArea(pageId));
	});
	const goalsLists = useSelector(getGoalsLists);

	if (isLoading || !area || !goalsLists) return <Loader />;

	const addGoalsList = () => {
		const tracker: IHabitTracker = {
			id: 0,
			goalName: "Цель на месяц",
			areaOwnerId: area.id,
			items: [],
			pageId: monthPage.id,
		};
		dispatch(
			goalsListsThunks.addGoalsList({
				id: 0,
				areaOwnerId: area.id,
				list: tracker,
			})
		);
	};

	return (
		<>
			<h1 className="mt-40 area-header">{area.header}</h1>
			{goalsLists.map((gl, i) => (
				<GoalList key={gl.goalListId} reversed={i % 2 === 0} goalList={gl} />
			))}
			<Row className="mt-20">
				<AddListBtn onClick={addGoalsList} />
			</Row>
		</>
	);
};

export default GoalsArea;
