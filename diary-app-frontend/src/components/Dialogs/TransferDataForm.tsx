import React, { useState, useContext } from "react";
import {
  Button,
  Modal,
  FormControl,
  FormLabel,
  Row,
  Form,
  FormGroup,
  Col
} from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faClock } from "@fortawesome/free-regular-svg-icons";
import { IEvent } from "../../models";
import { getEmptyEvent } from "../../utils";
import { AppContext } from "../../context";

class TransferDataModel {
  transferGoalsArea: boolean;
  transferPurchasesArea: boolean;
  transferDesiresArea: boolean;
  transferIdeasArea: boolean;

  constructor() {}
}

interface IState {
  show: boolean;
  transferDataModel: TransferDataModel;
}

export const TransferDataForm: React.FC = () => {
  const [state, setState] = useState<IState>({
    show: true,
    transferDataModel: new TransferDataModel()
  });

  const handleSubmit = () => {};

  return (
    <Modal
      size="lg"
      className="transfer-data-form"
      show={state.show}
      onHide={() => setState({ ...state, show: false })}
      aria-labelledby="contained-modal-title-vcenter"
    >
      <Form id="add-event-form" onSubmit={handleSubmit} noValidate={true}>
        <Modal.Header closeButton>
          {/* <FormControl
            autoFocus={true}
            autoComplete={"off"}
            placeholder="Добавьте название"
            aria-label="Добавьте название"
            aria-describedby="basic-addon1"
            value={formState.item.subject}
            onChange={onChange}
          /> */}
        </Modal.Header>
        <Modal.Body>
          <FormGroup as={Row} style={{ marginBottom: "0" }}>
            <FormLabel column sm="1">
              <FontAwesomeIcon icon={faClock} />
            </FormLabel>
            {/* <Col sm="11">
              <FormControl
                plaintext
                disabled
                readOnly
                defaultValue={getDisplayDate()}
              />
            </Col> */}
          </FormGroup>
        </Modal.Body>
        <Modal.Footer>
          {/* <Button variant="secondary" onClick={handleClose}>
            Закрыть
          </Button> */}
          <Button variant="primary" type="submit" form="add-event-form">
            Сохранить
          </Button>
        </Modal.Footer>
      </Form>
    </Modal>
  );
};
