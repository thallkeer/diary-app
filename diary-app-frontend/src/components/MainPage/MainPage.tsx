import Calendar from "components/Calendar/Calendar";
import Loader from "components/Loader";
import { usePage } from "hooks/usePage";
import React, { FC } from "react";
import { Container, Row, Col } from "react-bootstrap";

import { getMainPage, mainPageComponent } from "store/pages";
import ImportantEventsArea from "./ImportantEvents";
import ImportantThingsArea from "./ImportantThings";

const MainPage: FC = () => {
	const { page, status } = usePage(getMainPage, mainPageComponent);

	if (status !== "succeeded" || !page) return <Loader />;

	return (
		<Container fluid className="mt-20">
			<Row>
				<Col md="3" className="text-center">
					<ImportantThingsArea />
					<ImportantEventsArea />
				</Col>
				<Col md="9">
					<Calendar />
				</Col>
			</Row>
		</Container>
	);
};

export default MainPage;
