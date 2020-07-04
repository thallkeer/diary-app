import React, { Suspense, lazy } from "react";
import { Container, Row, Col } from "react-bootstrap";
import { MainPageState } from "./MainPageState";
import Loader from "../Loader";

const ImportantThings = lazy(() => import("./ImportantThings"));
const ImportantEvents = lazy(() => import("./ImportantEvents"));
const Calendar = lazy(() => import("../Calendar/Calendar"));

const MainPage: React.SFC = () => {
	return (
		<MainPageState>
			<Container fluid className="mt-20">
				<Row>
					<Suspense fallback={<Loader />}>
						<Col md="3" className="text-center">
							<ImportantThings />
							<ImportantEvents />
						</Col>
						<Col md="9">
							<Calendar />
						</Col>
					</Suspense>
				</Row>
			</Container>
		</MainPageState>
	);
};

export default MainPage;
