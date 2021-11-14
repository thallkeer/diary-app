import history from "components/history";
import Loader from "components/Loader";
import { IUserSettings, IPageAreaTransferSettings } from "models/entities";
import React, { useState, useEffect } from "react";
import {
	Button,
	Form,
	FormGroup,
	Container,
	Row,
	Card,
	Col,
	FormControl,
} from "react-bootstrap";
import { useSelector } from "react-redux";
import { getAppInfo } from "selectors/app-selectors";
import { userService } from "services/users";

export const prepareTransferData = (settings: IPageAreaTransferSettings) => {
	const {
		transferPurchasesArea,
		transferDesiresArea,
		transferGoalsArea,
		transferIdeasArea,
	} = settings;

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
			text: "Списки идей",
		},
		{
			name: "transferGoalsArea",
			checkedState: transferGoalsArea,
			text: "Трекеры привычек",
		},
	];
	return checkBoxes;
};

const useUserSettings = (userId: number) => {
	const [userTelegramId, setUserTelegramId] = useState("");
	const [settings, setSettings] = useState<IUserSettings>(null);

	const updateNotificationSettings = (propName: string, propValue: any) => {
		const { notificationSettings } = settings;
		setSettings({
			...settings,
			notificationSettings: {
				...notificationSettings,
				[propName]: propValue,
			},
		});
	};

	const updateTransferSettings = (propName: string, propValue: any) => {
		const { pageAreaTransferSettings } = settings;
		setSettings({
			...settings,
			pageAreaTransferSettings: {
				...pageAreaTransferSettings,
				[propName]: propValue,
			},
		});
	};

	useEffect(() => {
		let mounted = true;
		userService.getUserSettings(userId).then((resp) => {
			if (mounted) {
				setUserTelegramId(resp.user.telegramId);
				const userSettings = resp.settings || {
					id: 0,
					userId,
					notificationSettings: {
						id: 0,
						isActivated: false,
						notifyAt: "10:00:00",
						notifyDayBefore: false,
					},
					pageAreaTransferSettings: {
						id: 0,
					},
				};
				setSettings(userSettings);
			}
		});
		return () => {
			mounted = false;
		};
	}, [userId]);

	return {
		settings,
		updateNotificationSettings,
		updateTransferSettings,
		userTelegramId,
		setUserTelegramId,
	};
};

const UserSettings = () => {
	const { user } = useSelector(getAppInfo);
	const {
		settings,
		updateNotificationSettings,
		updateTransferSettings,
		userTelegramId,
		setUserTelegramId,
	} = useUserSettings(user.id);

	if (!settings) return <Loader />;

	const { isActivated, notifyAt, notifyDayBefore } =
		settings.notificationSettings;

	const transferSettings = settings.pageAreaTransferSettings;
	const checkBoxes = prepareTransferData(transferSettings);

	const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
		const { name } = e.currentTarget;

		updateTransferSettings(name, !transferSettings[name]);
	};

	const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
		event.preventDefault();
		await userService.updateUser({
			...user,
			telegramId: userTelegramId,
		});

		await userService.updateUserSettings(settings);
		history.push("/main");
	};

	const handleGoBackward = () => {
		history.push("/main");
	};

	const TransferSettingsCard = (
		<Card style={{ marginTop: "2em", marginBottom: "2em" }}>
			<Card.Header as="h6">
				Настройки автоматического переноса зон списков
			</Card.Header>
			<Card.Body>
				{checkBoxes.map((cb) => (
					<FormGroup as={Row} key={cb.name}>
						<Col sm={5}>
							<Form.Check
								// custom
								type="checkbox"
								name={cb.name}
								id={cb.name}
								label={cb.text}
								checked={transferSettings[cb.name]}
								onChange={handleChange}
							/>
						</Col>
					</FormGroup>
				))}
			</Card.Body>
		</Card>
	);

	const CustomCheckboxRow: React.FC<{
		propName: string;
		propValue: boolean;
		label: string;
		disabled?: boolean;
	}> = ({ propName, propValue, label, disabled = false }) => {
		return (
			<FormGroup as={Row}>
				<Col sm={5}>
					<Form.Check
						// custom
						type="checkbox"
						name={propName}
						id={propName}
						checked={!disabled && propValue}
						disabled={disabled}
						label={label}
						onChange={(e) =>
							updateNotificationSettings(propName, e.target.checked)
						}
					/>
				</Col>
			</FormGroup>
		);
	};
	return (
		<Container>
			<Form style={{ marginTop: "2em" }} onSubmit={handleSubmit}>
				<FormGroup as={Row} className="ml-2">
					<Form.Label column sm="3">
						Идентификатор Telegram
					</Form.Label>
					<Col sm="2">
						<FormControl
							autoFocus={true}
							type="text"
							minLength={9}
							maxLength={9}
							value={userTelegramId}
							size={"sm"}
							onChange={(e) => {
								const isInteger = /^[0-9]+$/;
								if (e.target.value === "" || isInteger.test(e.target.value))
									setUserTelegramId(e.target.value);
							}}
						/>
					</Col>
				</FormGroup>
				<Card style={{ marginTop: "2em" }}>
					<Card.Header as="h6">Настройки уведомлений</Card.Header>
					<Card.Body>
						<CustomCheckboxRow
							propName="isActivated"
							propValue={isActivated}
							label="Получать уведомления о предстоящих событиях"
						/>
						<CustomCheckboxRow
							propName="notifyDayBefore"
							propValue={notifyDayBefore}
							label="Уведомлять за день до события"
							disabled={!isActivated}
						/>
						<FormGroup as={Row}>
							<Form.Label column sm="3">
								Время получения уведомления
							</Form.Label>
							<Col sm={2}>
								<FormControl
									type="time"
									value={notifyAt}
									disabled={!isActivated || !notifyDayBefore}
									onChange={(e) => {
										updateNotificationSettings("notifyAt", e.target.value);
									}}
								/>
							</Col>
						</FormGroup>
					</Card.Body>
				</Card>
				{TransferSettingsCard}
				<Button variant="secondary" onClick={handleGoBackward}>
					Назад
				</Button>
				<Button
					variant="primary"
					type="submit"
					style={{ marginLeft: "0.5rem" }}
				>
					Сохранить
				</Button>
			</Form>
		</Container>
	);
};

export default UserSettings;
