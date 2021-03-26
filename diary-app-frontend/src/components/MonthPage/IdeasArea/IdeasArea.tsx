import React from "react";
import { Row, Col } from "react-bootstrap";
import Loader from "../../Loader";
import { useDispatch, useSelector } from "react-redux";
import {
	getIdeasArea,
	getIdeasList,
} from "../../../store/pages/pages.selectors";
import { CommonList } from "../../Lists/CommonList/CommonList";
import { useMonthPageArea } from "../../../hooks/usePageArea";
import {
	ideasAreaComponent,
	ideasListThunks,
} from "store/pageAreas/ideasArea.reducer";

const IdeasArea: React.FC = () => {
	const dispatch = useDispatch();
	const { area, isLoading } = useMonthPageArea(
		getIdeasArea,
		ideasAreaComponent
	);
	const ideasList = useSelector(getIdeasList);

	if (isLoading || !ideasList.list) return <Loader />;

	return (
		<>
			<h1 className="area-header">{area.header}</h1>
			<Row>
				<Col md={12}>
					<CommonList
						commonList={ideasList.list}
						isDeletable={false}
						readonlyTitle={true}
						listItemActions={{
							deleteItem: (itemId) => {
								dispatch(ideasListThunks.deleteListItem(itemId));
							},
							updateItem: (item) => {
								dispatch(ideasListThunks.addOrUpdateListItem(item));
							},
						}}
						className="mt-20 month-lists-header no-list-header"
					/>
				</Col>
			</Row>
		</>
	);
};

export { IdeasArea };
