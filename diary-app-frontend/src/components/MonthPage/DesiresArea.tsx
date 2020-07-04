import React, { useContext } from "react";
import { Row, Col } from "react-bootstrap";
import { CommonListState } from "../Lists/CommonList/CommonListState";
import { CommonList } from "../Lists/CommonList/CommonList";
import { DesiresAreaState, desiresAreaContext } from "./DesiresAreaState";
import Loader from "../Loader";

const DesiresArea: React.FC = () => {
	const DesiresAreaComponent = () => {
		const { desiresAreaState } = useContext(desiresAreaContext);
		const { area, loading } = desiresAreaState;

		if (!area || loading) return <Loader />;

		return (
			<>
				<h1 className="mt-40 area-header">{area.header}</h1>
				<Row>
					{area.desiresLists.map((commonList) => (
						<Col md={4} key={commonList.id}>
							<CommonListState initList={commonList}>
								<CommonList
									readonlyTitle={true}
									className="mt-10 month-lists-header no-list-header-border"
								/>
							</CommonListState>
						</Col>
					))}
				</Row>
			</>
		);
	};

	return (
		<DesiresAreaState>
			<DesiresAreaComponent />
		</DesiresAreaState>
	);
};

export default DesiresArea;
