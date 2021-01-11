import React from "react";
import { Row, Col } from "react-bootstrap";
import Loader from "../Loader";
import { useDispatch, useSelector } from "react-redux";
import { getDesireLists, getDesiresArea } from "../../selectors/page-selectors";
import { CommonList } from "../Lists/CommonList/CommonList";
import { loadDesiresArea } from "../../context/reducers/pageArea/desiresArea-reducer";
import {
	desireListsActions,
	getDesireListName,
} from "../../context/reducers/pageAreaLists/desireLists-reducer";
import { usePageArea } from "../../hooks/usePageArea";
import { IDesiresArea } from "../../models/entities";
import { IDesiresAreaState } from "../../models/states";

const DesiresArea: React.FC = () => {
	const dispatch = useDispatch();
	const { area, isLoading } = usePageArea<IDesiresAreaState, IDesiresArea>(
		getDesiresArea,
		(dispatch, pageId) => {
			dispatch(loadDesiresArea(pageId));
		}
	);
	const desireLists = useSelector(getDesireLists);

	if (isLoading) return <Loader />;

	return (
		<>
			<h1 className="mt-40 area-header">{area.header}</h1>
			<Row>
				{desireLists.map((commonList) => (
					<Col md={4} key={commonList.desireListId}>
						<CommonList
							commonList={commonList.listState.list}
							isDeletable={false}
							listItemActions={{
								deleteItem: (itemId) => {
									dispatch(
										desireListsActions.deleteListItem(
											itemId,
											getDesireListName(commonList.desireListId)
										)
									);
								},
								updateItem: (item) => {
									dispatch(
										desireListsActions.addOrUpdateListItem(
											item,
											getDesireListName(commonList.desireListId)
										)
									);
								},
							}}
							readonlyTitle={true}
							className="mt-10 month-lists-header no-list-header-border"
						/>
					</Col>
				))}
			</Row>
		</>
	);
};

export default DesiresArea;
