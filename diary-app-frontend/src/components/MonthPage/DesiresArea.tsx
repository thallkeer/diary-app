import React from "react";
import { Row, Col } from "react-bootstrap";
import { EventList } from "../Lists/EventList";
import { IDesiresArea } from "../../models";
import { useEvents } from "../../hooks/useLists";
import Loader from "../Loader";
import usePageArea from "../../hooks/usePageArea";

export const DesiresArea: React.FC = () => {
  const [_, dispatch] = useEvents(null); //just for get a dispatch
  const { areaState } = usePageArea<IDesiresArea>("desiresArea");

  if (!areaState || areaState.loading) return <Loader />;

  return (
    <>
      <h1 className="mt-40">{areaState.area.header}</h1>
      <Row>
        {areaState.area.desiresLists.map(dl => (
          <Col md={4} key={dl.id}>
            <EventList
              fillToNumber={6}
              className="mt-10 month-lists-header"
              eventList={dl}
              dispatch={dispatch}
              readonly={false}
            />
          </Col>
        ))}
      </Row>
    </>
  );
};
