import React from "react";
import { Row, Col, Accordion } from "react-bootstrap";
import { useSelector } from "react-redux";
import { loadDesiresArea } from "store/pageAreas/desiresArea.reducer";
import { useMonthPageArea } from "hooks/usePageArea";
import {
	addOrUpdateItem,
	deleteItem,
} from "store/pageAreaLists/desireLists.slice";
import { ICommonListState } from "store/diaryLists";
import { useAppDispatch } from "hooks/hooks";
import { getDesireLists, getDesiresArea } from "selectors/pages.selectors";
import Loader from "components/Loader";
import { CommonList } from "components/Lists/CommonList/CommonList";

const DesiresArea: React.FC = () => {
	const { area, status } = useMonthPageArea(getDesiresArea, loadDesiresArea);
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
	const desireList = desireListState.list;

	return (
		<Col md={12} className="mt-20">
			<Accordion defaultActiveKey="0">
				<Accordion.Item eventKey={desireList.id.toString()}>
					<Accordion.Header>{desireList.title}</Accordion.Header>
					<Accordion.Body>
						<CommonList
							commonList={desireList}
							renderTitle={false}
							isDeletable={false}
							listItemActions={{
								deleteItem: (itemId) => {
									dispatch(deleteItem(desireList.id, itemId));
								},
								updateItem: (item) => {
									dispatch(addOrUpdateItem(desireList.id, item));
								},
							}}
							readonlyTitle={true}
							className="mt-1 month-lists-header no-list-header-border"
						/>
					</Accordion.Body>
				</Accordion.Item>
			</Accordion>
		</Col>
	);
};

export { DesiresArea };
