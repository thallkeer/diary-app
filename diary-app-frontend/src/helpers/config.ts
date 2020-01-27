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
  baseApi: "https://localhost:44320/api/",
  headers: getHeader()
};
