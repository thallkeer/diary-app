import React, { useContext } from "react";
import { Route, Redirect } from "react-router-dom";
import { AppNavbar } from "../Navbars/AppNavbar";
import { store } from "../../context/store";

export const PrivateRoute = ({ component: Component, path, ...rest }) => {
  const { user } = useContext(store).state;

  const routedComponent = (
    <Route
      {...rest}
      render={(props) =>
        user ? (
          <Component {...props} />
        ) : (
          <Redirect
            to={{ pathname: "/login", state: { from: props.location } }}
          />
        )
      }
    />
  );

  return (
    <>
      <AppNavbar isOnMonthPage={path === "/month"} />
      {routedComponent}
    </>
  );
};
