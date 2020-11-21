import React, { useContext } from "react";
// import { Row, Col } from "react-bootstrap";
// import { ideasAreaContext, IdeasAreaState } from "./IdeasAreaState";
// import { CommonList } from "../Lists/CommonList/CommonList";
// import { CommonListState } from "../Lists/CommonList/CommonListState";
// import Loader from "../Loader";

// const IdeasArea: React.FC = () => {
// 	const IdeasAreaComponent = () => {
// 		const { ideasAreaState } = useContext(ideasAreaContext);
// 		const { area, loading } = ideasAreaState;

// 		if (!area || loading) return <Loader />;

// 		return (
// 			<>
// 				<h1 className="area-header">{area.header}</h1>
// 				<Row>
// 					<Col md={12}>
// 						<CommonListState initList={area.ideasList}>
// 							<CommonList
// 								className="mt-10 no-list-header"
// 								readonlyTitle={true}
// 							/>
// 						</CommonListState>
// 					</Col>
// 				</Row>
// 			</>
// 		);
// 	};

// 	return (
// 		<IdeasAreaState>
// 			<IdeasAreaComponent />
// 		</IdeasAreaState>
// 	);
// };

// export default IdeasArea;
