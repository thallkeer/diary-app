import axios from "axios";
import { IUser } from "../models";
import { config } from "../helpers/config";
import history from "../components/history";

const { baseApi, headers } = config;

interface ILoginResponse {
  id: string;
  userName: string;
  token: string;
}

export const logoff = () => {
  localStorage.removeItem("user");
  history.push("/");
};

export const login = (user: IUser) => {
  return axios.post<ILoginResponse>(`${baseApi}users/authenticate`, user, {
    headers
  });
};

export const register = (user: IUser) => {
  return axios.post(`${baseApi}users/register`, user, { headers });
};
