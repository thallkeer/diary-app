import { IUser } from "../models";

export const getHeader = () => {
  // return authorization header with jwt token
  let user: IUser = JSON.parse(localStorage.getItem("user"));
  return (
    (user && {
      "Access-Control-Allow-Origin": "*",
      // Accept: "application/json",
      Authorization: "Bearer " + user.token
    }) ||
    {}
  );
};

export const config = {
  baseApi: process.env.REACT_APP_API_URL + "/api/",
  headers: getHeader()
};
