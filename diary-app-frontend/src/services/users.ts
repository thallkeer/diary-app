import axios from "axios";
import { IUser } from "../models";
import { config } from "../helpers/config";

const { baseApi, getHeader } = config;
const headers = getHeader();

interface ILoginResponse {
  id: string;
  userName: string;
  token: string;
}

export const login = (user: IUser) => {
  return axios.post<ILoginResponse>(`${baseApi}users/authenticate`, user, {
    headers
  });
};

export const register = (user: IUser) => {
  return axios.post(`${baseApi}users/register`, user, { headers });
};
