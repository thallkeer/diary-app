import React, { useState, useRef, useEffect } from "react";
import {
	Button,
	Modal,
	FormControl,
	FormLabel,
	Row,
	Form,
	FormGroup,
	Col,
} from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faClock } from "@fortawesome/free-regular-svg-icons";
import { IEvent } from "../../models";
import { getEmptyEvent } from "../../utils";
import { useSelector } from "react-redux";
import { getAppInfo } from "../../selectors/app-selectors";

interface IFormProps {
	show: boolean;
	handleClose: () => void;
	day: number;
	event?: IEvent;
	addEvent: (newEvent: IEvent) => void;
}

interface IFormState {
	item: IEvent;
	submitSuccess?: boolean;
}

export const AddEventForm: React.FC<IFormProps> = ({
	show,
	handleClose,
	day,
	addEvent,
	event,
}) => {
	const { year, month } = useSelector(getAppInfo);

	const initialState: IFormState = {
		item: event || {
			...getEmptyEvent(0),
			date: new Date(year, month - 1, day),
		},
	};

	const [formState, setFormState] = useState<IFormState | null>(initialState);

	const inputRef = useRef(null);

	useEffect(() => {
		inputRef.current.focus();
	}, []);

	const onChange = (e: React.ChangeEvent<HTMLInputElement>) => {
		e.persist();
		const { value } = e.target as HTMLInputElement;

		setFormState({
			...formState,
			item: {
				...formState.item,
				subject: value,
			},
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
		return formState.item.date.toLocaleString("ru", {
			day: "numeric",
			month: "long",
			year: "numeric",
		});
	};

	const submitForm = async (): Promise<boolean> => {
		try {
			addEvent(formState.item);
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
				<Modal.Header closeButton translate={"yes"}>
					<Modal.Title>
						{formState.item.id === 0
							? "Новое событие"
							: "Редактирование события"}
					</Modal.Title>
				</Modal.Header>
				<Modal.Body>
					<FormGroup as={Row}>
						<FormLabel column sm="2">
							Событие
						</FormLabel>
						<Col sm="10">
							<FormControl
								autoFocus={true}
								autoComplete={"off"}
								aria-describedby="basic-addon1"
								value={formState.item.subject}
								onChange={onChange}
								ref={inputRef}
								required
							/>
						</Col>
					</FormGroup>
					<FormGroup as={Row} className="mb-0">
						<FormLabel column sm="2">
							<FontAwesomeIcon icon={faClock} />
						</FormLabel>
						<Col sm="10">
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
