import React from "react";
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
import ILightEvent from "../../models/event-light-model";
import "./style.css";

interface IFormProps {
  show: boolean;
  onHide: () => void;
  day: number;
  addEvent?: (subject: string, date: Date) => void;
}

interface IFormState {
  text: string;
  date: Date;
  submitSuccess?: boolean;
}

class AddEventForm extends React.Component<IFormProps, IFormState> {
  constructor(props: IFormProps) {
    super(props);
    let date = new Date();
    date.setDate(this.props.day);

    this.state = {
      text: "",
      date: date
    };
  }

  onChange = e => {
    e.preventDefault();
    const element = e.target as HTMLInputElement;
    this.setState({
      text: element.value
    });
  };

  handleSubmit = async (e: React.FormEvent<HTMLFormElement>): Promise<void> => {
    e.preventDefault();
    const submitSuccess: boolean = await this.submitForm();
    this.setState({ submitSuccess });
    if (submitSuccess) this.props.onHide();
  };

  getDisplayDate = (): string => {
    return this.state.date.toLocaleString("ru", {
      day: "numeric",
      month: "long",
      year: "numeric"
    });
  };

  async submitForm(): Promise<boolean> {
    try {
      const event: ILightEvent = {
        eventID: 0,
        subject: this.state.text,
        date: this.state.date
      };

      const month: number = event.date.getMonth() + 1; //from 0 to 11
      const day: number = event.date.getDate();

      const response = await fetch(
        `https://localhost:44320/api/events/${month}/day/${day}`,
        {
          method: "post",
          headers: new Headers({
            "Content-Type": "application/json",
            Accept: "application/json"
          }),
          body: JSON.stringify(event)
        }
      );
      return response.ok;
    } catch (ex) {
      return false;
    }
  }

  render() {
    return (
      <Form id="add-event-form" onSubmit={this.handleSubmit} noValidate={true}>
        <Modal
          {...this.props}
          style={{ fontFamily: `'Google Sans',Roboto,Arial,sans-serif` }}
          show={this.props.show}
          aria-labelledby="contained-modal-title-vcenter"
        >
          <Modal.Header closeButton>
            <FormControl
              autoFocus
              autoComplete={"off"}
              placeholder="Добавьте название"
              aria-label="Добавьте название"
              aria-describedby="basic-addon1"
              value={this.state.text}
              onChange={this.onChange}
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
                  defaultValue={this.getDisplayDate()}
                />
              </Col>
            </FormGroup>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="secondary" onClick={this.props.onHide}>
              Закрыть
            </Button>
            <Button variant="primary" type="submit" form="add-event-form">
              Сохранить
            </Button>
          </Modal.Footer>
          {/* {this.state.submitSuccess && (
            <div className="alert alert-info" role="alert">
              The form was successfully submitted!
            </div>
          )}
          {this.state.submitSuccess === false && (
            <div className="alert alert-danger" role="alert">
              Sorry, an unexpected error has occurred
            </div>
          )}
          {this.state.submitSuccess === false && (
            <div className="alert alert-danger" role="alert">
              Sorry, the form is invalid. Please review, adjust and try again
            </div>
          )} */}
        </Modal>
      </Form>
    );
  }
}

export default AddEventForm;
