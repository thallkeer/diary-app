import { prepareTransferData } from "components/Users/UserSettings";
import { IPageAreaTransferSettings } from "models/entities";
import React, { useState, useRef } from "react";
import { Button, Modal, Row, Form, FormGroup, Overlay } from "react-bootstrap";
import { useSelector } from "react-redux";
import { getMonthPage } from "store/pages";
import axios from "../../axios/axios";

interface IState {
	show: boolean;
	transferDataModel: IPageAreaTransferSettings;
	error: string;
}

type TransferDataRequestParams = {
	originalPageId: number;
	transferDataModel: IPageAreaTransferSettings;
};

export const TransferDataForm: React.FC<{
	show: boolean;
	onHide: () => void;
}> = ({ show, onHide }) => {
	const [state, setState] = useState<IState>({
		show,
		transferDataModel: {},
		error: null,
	});
	const target = useRef(null);

	const monthPage = useSelector(getMonthPage);

	const { transferDataModel } = state;

	const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
		e.preventDefault();
		const data: TransferDataRequestParams = {
			originalPageId: monthPage.id,
			transferDataModel,
		};

		axios
			.post("monthpage/transferData", data)
			.then((res) => {
				setState({ ...state, show: false });
				onHide();
			})
			.catch((err) => {
				setState({ ...state, error: "Error!!!" });
			});
	};

	const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
		const { name } = e.currentTarget;

		setState({
			...state,
			transferDataModel: {
				...transferDataModel,
				[name]: !transferDataModel[name],
			},
		});
	};

	const checkBoxes = prepareTransferData(transferDataModel);

	return (
		<Modal
			size="lg"
			className="transfer-data-form"
			show={state.show}
			onHide={onHide}
			aria-labelledby="contained-modal-title-vcenter"
		>
			<Form id="add-event-form" onSubmit={handleSubmit} noValidate={true}>
				<Modal.Header closeButton translate={"yes"}>
					<Modal.Title>Перенести на следующий месяц</Modal.Title>
				</Modal.Header>
				<Modal.Body>
					{checkBoxes.map((cb) => (
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
					{state.error && (
						<Overlay
							target={target.current}
							show={state.error !== null}
							placement="right"
						>
							{({
								placement,
								scheduleUpdate,
								arrowProps,
								outOfBoundaries,
								show: _show,
								...props
							}) => <div>{state.error}</div>}
						</Overlay>
					)}
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

// {...props}
// style={{
// 	backgroundColor: "rgba(255, 100, 100, 0.85)",
// 	padding: "2px 10px",
// 	color: "white",
// 	borderRadius: 3,
// 	...props.style,
// }}
