export interface IEntity {
	id: number;
}

export interface IUser extends IEntity {
	username: string;
	password?: string;
	token?: string;
	telegramId?: string;
}

export interface IPageAreaTransferSettings {
	id?: number;
	transferGoalsArea?: boolean;
	transferPurchasesArea?: boolean;
	transferDesiresArea?: boolean;
	transferIdeasArea?: boolean;
}

export interface INotificationsSettings {
	id: number;
	isActivated: boolean;
	notifyDayBefore: boolean;
	notifyAt: string;
}

export interface IUserSettings {
	id: number;
	userId: number;
	pageAreaTransferSettings: IPageAreaTransferSettings;
	notificationSettings: INotificationsSettings;
}
