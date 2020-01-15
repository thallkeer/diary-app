export const config = {
  baseApi: "https://localhost:44320/api/"
};

export const getHeader = () => {
  // return authorization header with jwt token
  let token = JSON.parse(localStorage.getItem("token"));

  if (token) {
    return { Authorization: "Bearer " + token };
  } else {
    return {};
  }
};
