import React, { useState, useRef } from "react";
import { Button, Modal, Row, Form, FormGroup, Overlay } from "react-bootstrap";
import { useSelector } from "react-redux";
import axios from "../../axios/axios";
import { getAppInfo } from "../../selectors/app-selectors";

class TransferDataModel {
	public transferGoalsArea: boolean;
	public transferPurchasesArea: boolean;
	public transferDesiresArea: boolean;
	public transferIdeasArea: boolean;

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
	error: string;
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
		transferDataModel: new TransferDataModel(),
		error: null,
	});
	const target = useRef(null);

	const { year, month, user } = useSelector(getAppInfo);

	const { transferDataModel } = state;

	const {
		transferDesiresArea,
		transferGoalsArea,
		transferIdeasArea,
		transferPurchasesArea,
	} = transferDataModel;

	const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
		e.preventDefault();
		const data: TransferDataRequestParams = {
			pageParams: {
				year,
				month,
				userId: user.id,
			},
			transferDataModel,
		};

		axios
			.post("monthpage/transferData", data)
			.then((res) => {
				setState({ ...state, show: false });
				onHide();
			})
			.catch((err) => {
				console.log(err);
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

	const checkBoxes = [
		{
			name: "transferPurchasesArea",
			checkedState: transferPurchasesArea,
			text: "Списки покупок",
		},
		{
			name: "transferDesiresArea",
			checkedState: transferDesiresArea,
			text: "Списки желаний",
		},
		{
			name: "transferIdeasArea",
			checkedState: transferIdeasArea,
			text: "Списки идеи",
		},
		{
			name: "transferGoalsArea",
			checkedState: transferGoalsArea,
			text: "Трекеры привычек",
		},
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
