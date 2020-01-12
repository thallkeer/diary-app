import React, { useState } from "react";
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

export type EventInfo = {
  event?: IEvent;
  addEvent: (newEvent: IEvent, month: number, day: number) => void;
  ownerID: number;
};

interface IFormProps {
  show: boolean;
  handleClose: () => void;
  day: number;
  eventInfo: EventInfo;
}

interface IFormState {
  text: string;
  date: Date;
  submitSuccess?: boolean;
}

export const AddEventForm: React.FC<IFormProps> = ({
  show,
  handleClose,
  day,
  eventInfo
}) => {
  const getDateByDay = (day: number) => {
    const date: Date = new Date();
    date.setDate(day);
    return date;
  };

  console.log(eventInfo);

  const [formState, setFormState] = useState<IFormState | null>({
    text: eventInfo.event ? eventInfo.event.subject : "",
    date: getDateByDay(day)
  });

  const onChange = (e: React.FormEvent<HTMLInputElement>) => {
    e.persist();
    const { name, value } = e.target as HTMLInputElement;

    setFormState({
      ...formState,
      [name]: value
    });
  };

  const handleSubmit = async (
    e: React.FormEvent<HTMLFormElement>
  ): Promise<void> => {
    e.preventDefault();
    const submitSuccess: boolean = await submitForm();
    setFormState({ ...formState, submitSuccess });
    if (submitSuccess) {
      handleClose();
    }
  };

  const getDisplayDate = (): string => {
    return formState.date.toLocaleString("ru", {
      day: "numeric",
      month: "long",
      year: "numeric"
    });
  };

  const submitForm = async (): Promise<boolean> => {
    try {
      const event: IEvent = {
        id: 0,
        subject: formState.text,
        date: formState.date,
        ownerID: eventInfo.ownerID
      };

      const month: number = event.date.getMonth() + 1; //from 0 to 11
      const day: number = event.date.getDate();

      eventInfo.addEvent(event, month, day);
      return true;
    } catch (ex) {
      console.log(ex);
      return false;
    }
  };

  return (
    <Modal
      size="lg"
      className="add-event-form-dialog"
      show={show}
      onHide={handleClose}
      aria-labelledby="contained-modal-title-vcenter"
    >
      <Form id="add-event-form" onSubmit={handleSubmit} noValidate={true}>
        <Modal.Header closeButton>
          <FormControl
            autoFocus
            autoComplete={"off"}
            placeholder="Добавьте название"
            aria-label="Добавьте название"
            aria-describedby="basic-addon1"
            value={formState.text}
            name="text"
            onChange={onChange}
          />
        </Modal.Header>
        <Modal.Body>
          <FormGroup as={Row} style={{ marginBottom: "0" }}>
            <FormLabel column sm="1">
              <FontAwesomeIcon icon={faClock} />
            </FormLabel>
            <Col sm="11">
              <FormControl
                plaintext
                disabled
                readOnly
                defaultValue={getDisplayDate()}
              />
            </Col>
          </FormGroup>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Закрыть
          </Button>
          <Button variant="primary" type="submit" form="add-event-form">
            Сохранить
          </Button>
        </Modal.Footer>
      </Form>
      {formState.submitSuccess && (
        <div className="alert alert-info" role="alert">
          The form was successfully submitted!
        </div>
      )}
      {formState.submitSuccess === false && (
        <div className="alert alert-danger" role="alert">
          Sorry, an unexpected error has occurred
        </div>
      )}
      {formState.submitSuccess === false && (
        <div className="alert alert-danger" role="alert">
          Sorry, the form is invalid. Please review, adjust and try again
        </div>
      )}
    </Modal>
  );
};
