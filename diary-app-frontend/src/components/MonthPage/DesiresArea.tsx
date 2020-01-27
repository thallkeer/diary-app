import React from "react";
import { Row, Col } from "react-bootstrap";
import { EventList } from "../Lists/EventList";
import { IDesiresArea, IEventList } from "../../models";
import { useEvents } from "../../hooks/useLists";
import Loader from "../Loader";
import usePageArea from "../../hooks/usePageArea";

const OneEventList: React.FC<{ eventList: IEventList }> = ({ eventList }) => {
  const { dispatch, list } = useEvents(null, eventList);

  return (
    <Col md={4} key={list.id}>
      <EventList
        fillToNumber={6}
        className="mt-10 month-lists-header no-list-header-border"
        eventList={list}
        dispatch={dispatch}
        readonly={false}
      />
    </Col>
  );
};

export const DesiresArea: React.FC = () => {
  const { areaState } = usePageArea<IDesiresArea>("desiresArea");

  if (!areaState || areaState.loading) return <Loader />;

  return (
    <>
      <h1 className="mt-40">{areaState.area.header}</h1>
      <Row>
        {areaState.area.desiresLists.map(dl => (
          <OneEventList key={dl.id} eventList={dl} />
        ))}
      </Row>
    </>
  );
};
