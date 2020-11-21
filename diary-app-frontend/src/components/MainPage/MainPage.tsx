import React, { Suspense, lazy, FC, useEffect } from "react";
import { Container, Row, Col } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { getAppInfo } from "../../selectors/app-selectors";
import { loadMainPage } from "../../context/reducers/page/mainPage-reducer";
import Loader from "../Loader";

const ImportantThings = lazy(() => import("./ImportantThings"));
const ImportantEvents = lazy(() => import("./ImportantEvents"));
// const Calendar = lazy(() => import("../Calendar/Calendar"));

const MainPage: FC = () => {
	const { user, year, month } = useSelector(getAppInfo);

	const dispatch = useDispatch();

	useEffect(() => {
		dispatch(loadMainPage(user, year, month));
	}, [user, year, month]);

	return (
		<Container fluid className="mt-20">
			<Row>
				<Suspense fallback={<Loader />}>
					<Col md="3" className="text-center">
						<ImportantThings />
						<ImportantEvents />
					</Col>
					<Col md="9">{/* <Calendar /> */}</Col>
				</Suspense>
			</Row>
		</Container>
	);
};

export default MainPage;
