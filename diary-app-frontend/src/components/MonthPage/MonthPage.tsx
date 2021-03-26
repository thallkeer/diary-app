import React, { Suspense } from "react";
import { Container, Row, Col } from "react-bootstrap";
import Loader from "../Loader";
import { Link } from "react-router-dom";
import strelka from "../../images/right-arrow.png";
import { usePage } from "hooks/usePage";
import { getMonthPage, monthPageComponent } from "store/pages";
import { PurchasesArea } from "./PurchasesArea/PurchasesArea";
import { DesiresArea } from "./DesiresArea/DesiresArea";
import { IdeasArea } from "./IdeasArea/IdeasArea";
import { GoalsArea } from "./GoalsArea/GoalsArea";

const MonthPage: React.FC = () => {
	const monthPage = usePage(getMonthPage, monthPageComponent);

	if (!monthPage) return <Loader />;

	return (
		<Container fluid className="mt-20 second-page-container text-center">
			<ReturnToCalendarLink />
			<Suspense fallback={<Loader />}>
				<Row>
					<Col md={6}>
						<PurchasesArea />
						<DesiresArea />
					</Col>
					<Col md={6}>
						<IdeasArea />
						<GoalsArea />
					</Col>
				</Row>
			</Suspense>
		</Container>
	);
};

const ReturnToCalendarLink = () => (
	<Link
		className="month-name"
		style={{
			position: "absolute",
			left: "0",
			pointerEvents: "all",
			cursor: "pointer",
			zIndex: 10,
		}}
		to="/"
	>
		<img
			src={strelka}
			alt="return to calendar"
			className="mirrored-arrow"
			style={{ cursor: "pointer" }}
			width="30"
			height="30"
		/>
	</Link>
);

export { MonthPage };
