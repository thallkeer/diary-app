import React from "react";
import { IIdeasArea } from "../../models";
import { Row, Col } from "react-bootstrap";
import { EventList } from "../Lists/EventList";
import { useEvents } from "../../hooks/useLists";
import Loader from "../Loader";
import usePageArea from "../../hooks/usePageArea";

export const IdeasArea: React.FC = () => {
  const { areaState, page } = usePageArea<IIdeasArea>("ideasArea");
  const events = useEvents(page);

  if (!page || !areaState || areaState.loading || !events || events.loading)
    return <Loader />;

  return (
    <>
      <h1>{areaState.area.header}</h1>
      <Row>
        <Col md={12}>
          <EventList
            className="mt-10 no-list-header"
            fillToNumber={6}
            eventList={events.list}
            dispatch={events.dispatch}
          />
        </Col>
      </Row>
    </>
  );
};
