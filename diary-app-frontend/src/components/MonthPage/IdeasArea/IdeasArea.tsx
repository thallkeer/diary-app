import React from "react";
import { Row, Col } from "react-bootstrap";
import Loader from "../../Loader";
import { useSelector } from "react-redux";
import { getIdeasArea, getIdeasList } from "../../../selectors/pages.selectors";
import { CommonList } from "../../Lists/CommonList/CommonList";
import { useMonthPageArea } from "../../../hooks/usePageArea";
import {
	ideasListThunks,
	loadIdeasArea,
} from "store/pageAreas/ideasArea.reducer";
import { useAppDispatch } from "hooks/hooks";

const IdeasArea: React.FC = () => {
	const dispatch = useAppDispatch();
	const { area, status } = useMonthPageArea(
		getIdeasArea,
		loadIdeasArea,
		(area) => ideasListThunks.setList(area.ideasList)
	);
	const ideasList = useSelector(getIdeasList);

	if (status === "idle" || status === "loading" || !ideasList.list)
		return <Loader />;

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
								dispatch(ideasListThunks.deleteItemById(itemId));
							},
							updateItem: (item) => {
								dispatch(ideasListThunks.addOrUpdateItem(item));
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
