import { usePage } from "hooks/usePage";
import React, { Suspense, lazy, FC } from "react";
import { Container, Row, Col } from "react-bootstrap";

import { getMainPage, mainPageComponent } from "store/pages";

import Loader from "../Loader";

const ImportantThings = lazy(() => import("./ImportantThings"));
const ImportantEvents = lazy(() => import("./ImportantEvents"));
const Calendar = lazy(() => import("../Calendar/Calendar"));

const MainPage: FC = () => {
	const { page, status } = usePage(getMainPage, mainPageComponent);

	if (status !== "succeeded" || !page) return <Loader />;

	return (
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
	);
};

export default MainPage;
