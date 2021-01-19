import React, { Suspense, lazy } from "react";
import { Container, Row, Col } from "react-bootstrap";
import Loader from "../Loader";
import { Link } from "react-router-dom";
import strelka from "../../images/right-arrow.png";
import { loadMonthPage } from "store/pages/monthPages.actions";
import { usePage } from "hooks/usePage";
import { getMonthPage } from "store/pages";
const PurchasesArea = lazy(() => import("./PurchasesArea/PurchasesArea"));
const DesiresArea = lazy(() => import("./DesiresArea/DesiresArea"));
const IdeasArea = lazy(() => import("./IdeasArea/IdeasArea"));
const GoalsArea = lazy(() => import("./GoalsArea/GoalsArea"));

const MonthPage: React.FC = () => {
	const monthPage = usePage(getMonthPage, loadMonthPage);

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

export default MonthPage;
