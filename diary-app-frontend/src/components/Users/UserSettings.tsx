import history from "components/history";
import Loader from "components/Loader";
import { IUserSettings, PageAreaTransferSettings } from "models/entities";
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

export const prepareTransferData = (settings: PageAreaTransferSettings) => {
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
					},
					pageAreaTransferSettings: {
						id: 0,
					},
				};
				setSettings(userSettings);
			}
		});
		return () => (mounted = false);
	}, [userId]);

	return { settings, setSettings, userTelegramId, setUserTelegramId };
};

export const UserSettings = () => {
	const { user } = useSelector(getAppInfo);
	const {
		settings,
		setSettings,
		userTelegramId,
		setUserTelegramId,
	} = useUserSettings(user.id);

	if (!settings) return <Loader />;

	const { isActivated } = settings.notificationSettings;
	const transferSettings = settings.pageAreaTransferSettings;
	const checkBoxes = prepareTransferData(transferSettings);

	const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
		const { name } = e.currentTarget;

		setSettings({
			...settings,
			pageAreaTransferSettings: {
				...transferSettings,
				[name]: !transferSettings[name],
			},
		});
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

	return (
		<Container>
			<Form style={{ marginTop: "2em" }} onSubmit={handleSubmit}>
				<FormGroup as={Row}>
					<Form.Label column sm="4">
						Идентификатор Telegram
					</Form.Label>
					<Col sm="8">
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
						<FormGroup as={Row} className="ml-2">
							<Form.Check
								custom
								type="checkbox"
								name="isActivated"
								id="isActivated"
								checked={isActivated}
								label="Получать уведомления о предстоящих событиях"
								onChange={(e) =>
									setSettings({
										...settings,
										notificationSettings: {
											...settings.notificationSettings,
											isActivated: e.target.checked,
										},
									})
								}
							/>
						</FormGroup>
					</Card.Body>
				</Card>
				<Card style={{ marginTop: "2em", marginBottom: "2em" }}>
					<Card.Header as="h6">
						Настройки автоматического переноса зон списков
					</Card.Header>
					<Card.Body>
						<FormGroup>
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
						</FormGroup>
					</Card.Body>
				</Card>
				<Button variant="primary" type="submit">
					Сохранить
				</Button>
			</Form>
		</Container>
	);
};
