import React from "react";
import { Col, Row } from "react-bootstrap";

export const AddListBtn: React.FC<{ onClick: () => void }> = ({ onClick }) => {
	return (
		<Row className="justify-content-center">
			<Col md={3}>
				<div className="btn mt-10 more-btn" onClick={onClick}>
					Ещё
				</div>
			</Col>
		</Row>
	);
};
