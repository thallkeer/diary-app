import React, { useRef, useEffect } from "react";
import {
  Button,
  Modal,
  FormControl,
  Row,
  FormGroup,
  Col,
} from "react-bootstrap";

export const AddDayNote: React.FC<{
  note: string;
  show: boolean;
  handleClose: (
    event?: React.MouseEvent<HTMLButtonElement, MouseEvent>
  ) => void;
  handleSubmit: (noteText: string) => void;
}> = ({ show, handleSubmit, handleClose, note }) => {
  const ref = useRef<HTMLInputElement>(null);

  useEffect(() => {
    if (show) ref.current.focus();
  }, [show]);

  const addNote = () => {
    handleSubmit(ref.current.value);
    handleClose();
  };

  return (
    <Modal
      show={show}
      onHide={handleClose}
      aria-labelledby="contained-modal-title-vcenter"
    >
      <Modal.Header closeButton>
        <Modal.Title>Заметка</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <FormGroup as={Row}>
          <Col sm="12">
            <FormControl
              autoFocus={true}
              autoComplete={"off"}
              aria-describedby="basic-addon1"
              ref={ref}
              type="text"
              defaultValue={note}
              style={{ outline: "none" }}
              required
            />
          </Col>
        </FormGroup>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Закрыть
        </Button>
        <Button variant="primary" onClick={addNote}>
          Сохранить
        </Button>
      </Modal.Footer>
    </Modal>
  );
};
