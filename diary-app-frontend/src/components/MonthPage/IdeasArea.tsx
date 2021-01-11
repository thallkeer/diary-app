import React from "react";
import { Row, Col } from "react-bootstrap";
import Loader from "../Loader";
import { useDispatch, useSelector } from "react-redux";
import { getIdeasArea, getIdeasList } from "../../selectors/page-selectors";
import { CommonList } from "../Lists/CommonList/CommonList";
import {
	loadIdeasArea,
	ideasAreaActions,
	IDEAS_LIST,
} from "../../context/reducers/pageArea/ideasArea-reducer";
import { IIdeasAreaState } from "../../models/states";
import { IIdeasArea } from "../../models/entities";
import { usePageArea } from "../../hooks/usePageArea";

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
								dispatch(ideasAreaActions.deleteListItem(itemId, IDEAS_LIST));
							},
							updateItem: (item) => {
								dispatch(
									ideasAreaActions.addOrUpdateListItem(item, IDEAS_LIST)
								);
							},
						}}
						className="mt-10 no-list-header"
					/>
				</Col>
			</Row>
		</>
	);
};

export default IdeasArea;
