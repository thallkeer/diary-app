import React from "react";
import { Row, Col } from "react-bootstrap";
import { useSelector } from "react-redux";
import { loadDesiresArea } from "store/pageAreas/desiresArea.reducer";
import { useMonthPageArea } from "hooks/usePageArea";
import {
	setDesiresLists,
	addOrUpdateItem,
	deleteItem,
} from "store/pageAreaLists/desireLists.slice";
import { ICommonListState } from "store/diaryLists";
import { useAppDispatch } from "hooks/hooks";
import { getDesireLists, getDesiresArea } from "selectors/pages.selectors";
import Loader from "components/Loader";
import { CommonList } from "components/Lists/CommonList/CommonList";

const DesiresArea: React.FC = () => {
	const { area, status } = useMonthPageArea(
		getDesiresArea,
		loadDesiresArea,
		(area) => {
			return setDesiresLists(area.desiresLists);
		}
	);
	const desireLists = useSelector(getDesireLists);

	if (status === "idle" || status === "loading") return <Loader />;

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
	const dispatch = useAppDispatch();

	return (
		<Col md={4}>
			<CommonList
				commonList={desireListState.list}
				isDeletable={false}
				listItemActions={{
					deleteItem: (itemId) => {
						dispatch(deleteItem(desireListState.list.id, itemId));
					},
					updateItem: (item) => {
						dispatch(addOrUpdateItem(desireListState.list.id, item));
					},
				}}
				readonlyTitle={true}
				className="mt-10 month-lists-header no-list-header-border"
			/>
		</Col>
	);
};

export { DesiresArea };
