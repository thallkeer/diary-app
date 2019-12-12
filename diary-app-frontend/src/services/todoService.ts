import axios from "axios";
import { ITodoItem } from "../models/index";

const callApi: string = "https://localhost:44320/api/todo";

export const getTodos = async (): Promise<ITodoItem[]> => {
  const result = await axios.get<ITodoItem[]>(callApi).then(({ data }) => data);

  return new Promise(resolve => resolve(result));
};
