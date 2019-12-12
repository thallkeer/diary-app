import axios from "axios";
import { ILightEvent } from "../models/index";

const callApi: string = `https://localhost:44320/api/events/${new Date().getMonth() +
  1}`;

export const getEvents = async (): Promise<ILightEvent[]> => {
  const result = await axios
    .get<ILightEvent[]>(callApi)
    .then(({ data }) => data);

  return new Promise(resolve => resolve(result));
};
