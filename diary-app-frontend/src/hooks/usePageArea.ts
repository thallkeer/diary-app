import { useState, useEffect, useContext } from "react";
import { IPageArea } from "../models";
import axios from "axios";
import { MonthPageContext } from "../context";
import { config } from "../helpers/config";

type AreaState<T extends IPageArea> = {
  loading: boolean;
  area: T;
};

export default function usePageArea<T extends IPageArea>(areaName: string) {
  const { page } = useContext(MonthPageContext);
  const [areaState, setAreaState] = useState<AreaState<T>>(null);

  const { headers } = config;

  useEffect(() => {
    if (page) {
      setAreaState({ ...areaState, loading: true });
      axios
        .get(`${config.baseApi}monthpage/${areaName}/${page.id}`, {
          headers
        })
        .then(res => {
          console.log(`${areaName}---${res.data}`);
          setAreaState({ area: res.data, loading: false });
        })
        .catch(err => console.log(err));
    }
  }, [page, areaName]);

  return { areaState, setAreaState, page };
}
