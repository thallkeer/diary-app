import React from "react";
import { Row, Col } from "react-bootstrap";
import Loader from "../../Loader";
import { useDispatch, useSelector } from "react-redux";
import {
	getDesireLists,
	getDesiresArea,
} from "../../../store/pages/pages.selectors";
import { CommonList } from "../../Lists/CommonList/CommonList";
import { IDesiresAreaState } from "store/pageAreas/desires/desiresArea.reducer";
import { IDesiresArea } from "models/PageAreas/pageAreas";
import { loadDesiresArea } from "store/pageAreas/desires/desiresArea.actions";
import { usePageArea } from "hooks/usePageArea";
import { desireListsThunks } from "store/pageAreaLists/desiresLists/desireLists.actions";
import {
	desireListsHandler,
	IDesireListState,
} from "store/pageAreaLists/desiresLists/desireLists.reducer";

const DesiresArea: React.FC = () => {
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
				{desireLists.map((dl) => (
					<DesireList key={dl.desireListId} desireListState={dl} />
				))}
			</Row>
		</>
	);
};

export default DesiresArea;
const DesireList: React.FC<{ desireListState: IDesireListState }> = ({
	desireListState,
}) => {
	const listName = desireListsHandler.getListName(desireListState.desireListId);
	const dispatch = useDispatch();

	return (
		<Col md={4}>
			<CommonList
				commonList={desireListState.listState.list}
				isDeletable={false}
				listItemActions={{
					deleteItem: (itemId) => {
						dispatch(desireListsThunks.deleteListItem(itemId, listName));
					},
					updateItem: (item) => {
						dispatch(desireListsThunks.addOrUpdateListItem(item, listName));
					},
				}}
				readonlyTitle={true}
				className="mt-10 month-lists-header no-list-header-border"
			/>
		</Col>
	);
};
