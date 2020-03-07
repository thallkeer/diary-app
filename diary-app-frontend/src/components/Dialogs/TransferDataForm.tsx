import React, { useState, useContext } from "react";
import { Button, Modal, Row, Form, FormGroup } from "react-bootstrap";
import axios from "../../axios/axios";
import { AppContext } from "../../context";

class TransferDataModel {
  transferGoalsArea: boolean;
  transferPurchasesArea: boolean;
  transferDesiresArea: boolean;
  transferIdeasArea: boolean;

  /**
   *
   */
  constructor() {
    this.transferDesiresArea = false;
    this.transferGoalsArea = false;
    this.transferIdeasArea = false;
    this.transferPurchasesArea = false;
  }
}

interface IState {
  show: boolean;
  transferDataModel: TransferDataModel;
}

type PageParams = {
  userId: number;
  year: number;
  month: number;
};

type TransferDataRequestParams = {
  pageParams: PageParams;
  transferDataModel: TransferDataModel;
};

export const TransferDataForm: React.FC<{
  show: boolean;
  onHide: () => void;
}> = ({ show, onHide }) => {
  const [state, setState] = useState<IState>({
    show,
    transferDataModel: new TransferDataModel()
  });

  const { year, month, user } = useContext(AppContext);

  const { transferDataModel } = state;

  const {
    transferDesiresArea,
    transferGoalsArea,
    transferIdeasArea,
    transferPurchasesArea
  } = transferDataModel;

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    let data: TransferDataRequestParams = {
      pageParams: {
        year,
        month,
        userId: user.id
      },
      transferDataModel
    };

    axios
      .post("monthpage/transferData", data)
      .then(res => setState({ ...state, show: false }))
      .catch(err => console.log(err));
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name } = e.currentTarget;

    setState({
      ...state,
      transferDataModel: {
        ...transferDataModel,
        [name]: !transferDataModel[name]
      }
    });
  };

  const checkBoxes = [
    {
      name: "transferPurchasesArea",
      checkedState: transferPurchasesArea,
      text: "Списки покупок"
    },
    {
      name: "transferDesiresArea",
      checkedState: transferDesiresArea,
      text: "Списки желаний"
    },
    {
      name: "transferIdeasArea",
      checkedState: transferIdeasArea,
      text: "Списки идеи"
    },
    {
      name: "transferGoalsArea",
      checkedState: transferGoalsArea,
      text: "Трекеры привычек"
    }
  ];

  return (
    <Modal
      size="lg"
      className="transfer-data-form"
      show={state.show}
      onHide={onHide}
      aria-labelledby="contained-modal-title-vcenter"
    >
      <Form id="add-event-form" onSubmit={handleSubmit} noValidate={true}>
        <Modal.Header closeButton>
          <Modal.Title>Перенести на следующий месяц</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          {checkBoxes.map(cb => (
            <FormGroup key={cb.name} as={Row} className="ml-2">
              <Form.Check
                custom
                type="checkbox"
                name={cb.name}
                id={cb.name}
                label={cb.text}
                onChange={handleChange}
              />
            </FormGroup>
          ))}
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={onHide}>
            Закрыть
          </Button>
          <Button variant="primary" type="submit" form="add-event-form">
            Перенести
          </Button>
        </Modal.Footer>
      </Form>
    </Modal>
  );
};
