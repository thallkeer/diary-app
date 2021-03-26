import React from "react";
import { Row, Col } from "react-bootstrap";
import Loader from "../../Loader";
import { useDispatch, useSelector } from "react-redux";
import {
	getDesireLists,
	getDesiresArea,
} from "../../../store/pages/pages.selectors";
import { CommonList } from "../../Lists/CommonList/CommonList";
import { desiresAreaComponent } from "store/pageAreas/desiresArea.reducer";
import { useMonthPageArea } from "hooks/usePageArea";
import { desireListsHandler } from "store/pageAreaLists/desiresLists/desireLists.reducer";
import { commonListComponent, ICommonListState } from "store/diaryLists";

const DesiresArea: React.FC = () => {
	const { area, isLoading } = useMonthPageArea(
		getDesiresArea,
		desiresAreaComponent
	);
	const desireLists = useSelector(getDesireLists);
	if (isLoading) return <Loader />;

	return (
		<>
			<h1 className="mt-40 area-header">{area.header}</h1>
			<Row>
				{desireLists.map((dl) => (
					<DesireList key={dl.list.id} desireListState={dl} />
				))}
			</Row>
		</>
	);
};

const DesireList: React.FC<{ desireListState: ICommonListState }> = ({
	desireListState,
}) => {
	const listName = desireListsHandler.getListName(desireListState.list.id);
	const commonListThunks = commonListComponent.getThunks(listName);
	const dispatch = useDispatch();

	return (
		<Col md={4}>
			<CommonList
				commonList={desireListState.list}
				isDeletable={false}
				listItemActions={{
					deleteItem: (itemId) => {
						dispatch(commonListThunks.deleteListItem(itemId));
					},
					updateItem: (item) => {
						dispatch(commonListThunks.addOrUpdateListItem(item));
					},
				}}
				readonlyTitle={true}
				className="mt-10 month-lists-header no-list-header-border"
			/>
		</Col>
	);
};

export { DesiresArea };
