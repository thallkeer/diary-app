import React from "react";
import { Row, Col } from "react-bootstrap";
import Loader from "../../Loader";
import { useDispatch, useSelector } from "react-redux";
import {
	getIdeasArea,
	getIdeasList,
} from "../../../store/pages/pages.selectors";
import { CommonList } from "../../Lists/CommonList/CommonList";
import { usePageArea } from "../../../hooks/usePageArea";
import { IIdeasArea } from "models/PageAreas/pageAreas";
import { IIdeasAreaState } from "store/pageAreas/ideas/ideasArea.reducer";
import {
	IDEAS_LIST,
	loadIdeasArea,
} from "store/pageAreas/ideas/ideasArea.actions";
import { commonListThunks } from "store/diaryLists/commonLists.actions";

const IdeasArea: React.FC = () => {
	const dispatch = useDispatch();
	const { area, isLoading } = usePageArea<IIdeasAreaState, IIdeasArea>(
		getIdeasArea,
		(dispatch, pageId) => {
			dispatch(loadIdeasArea(pageId));
		}
	);
	const ideasList = useSelector(getIdeasList);

	if (isLoading || !ideasList || !ideasList.list) return <Loader />;

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
								dispatch(commonListThunks.deleteListItem(itemId, IDEAS_LIST));
							},
							updateItem: (item) => {
								dispatch(
									commonListThunks.addOrUpdateListItem(item, IDEAS_LIST)
								);
							},
						}}
						className="mt-20 month-lists-header no-list-header"
					/>
				</Col>
			</Row>
		</>
	);
};

export default IdeasArea;
