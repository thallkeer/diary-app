import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Row, Col } from "react-bootstrap";
// import { loadImportantEventsArea } from "../../context/reducers/pageArea/importantEventsArea-reducer";
import {
	// getImportantEventsArea,
	getImportantThingsArea,
	getLoading,
	getMainPage,
} from "../../selectors/page-selectors";
import Loader from "../Loader";

const ImportantEventsArea: React.FC = () => {
	const dispatch = useDispatch();
	const mainPage = useSelector(getMainPage);
	// const { area, isLoading } = useSelector(getImportantEventsArea);

	useEffect(() => {
		// if (mainPage !== null) dispatch(loadImportantEventsArea(mainPage.id));
	}, [mainPage]);

	// if (isLoading || !area) return <Loader />;

	return (
		<>
			<h1 className="area-header">
				{/* {area.header} */}
				</h1>
			<Row>
				<Col md={12}>
					{/* <TodoListState readonlyHeader={true} isDeletable={false}>
							<TodoList className="mt-10 no-list-header" />
						</TodoListState> */}
				</Col>
			</Row>
		</>
	);
};

export default ImportantEventsArea;
