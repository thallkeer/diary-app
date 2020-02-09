import axios from "axios";
import { IUser } from "../models";
import { config } from "../helpers/config";
import history from "../components/history";

const { baseApi } = config;

interface ILoginResponse {
  id: number;
  userName: string;
  token: string;
}

export const logoff = () => {
  localStorage.removeItem("user");
  history.push("/");
};

export const login = (user: IUser) => {
  return axios.post<ILoginResponse>(`${baseApi}users/authenticate`, user);
};

export const register = (user: IUser) => {
  return axios.post<ILoginResponse>(`${baseApi}users/register`, user);
};
