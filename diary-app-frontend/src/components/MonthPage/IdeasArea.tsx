import React from "react";
import { IIdeasArea, IEventList } from "../../models";
import { Row, Col } from "react-bootstrap";
import { EventList } from "../Lists/EventList";
import Loader from "../Loader";
import usePageArea from "../../hooks/usePageArea";
import { useEvents } from "../../hooks/useLists";

const ListCol: React.FC<{ eventList: IEventList }> = ({ eventList }) => {
  const { dispatch, list } = useEvents(null, eventList);

  return (
    <Col md={12}>
      <EventList
        className="mt-10 no-list-header"
        fillToNumber={6}
        eventList={list}
        dispatch={dispatch}
      />
    </Col>
  );
};

export const IdeasArea: React.FC = () => {
  const { areaState, page } = usePageArea<IIdeasArea>("ideasArea");

  if (!page || !areaState || areaState.loading) return <Loader />;

  return (
    <>
      <h1>{areaState.area.header}</h1>
      <Row>
        <ListCol eventList={areaState.area.ideasList} />
      </Row>
    </>
  );
};
