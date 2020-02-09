import { useState, useEffect, useContext } from "react";
import { IPageArea } from "../models";
import { MonthPageContext } from "../context";
import axios from "../axios/axios";

type AreaState<T extends IPageArea> = {
  loading: boolean;
  area: T;
};

export default function usePageArea<T extends IPageArea>(areaName: string) {
  const { page } = useContext(MonthPageContext);
  const [areaState, setAreaState] = useState<AreaState<T>>(null);

  useEffect(() => {
    if (page) {
      setAreaState(prevState => {
        return { ...prevState, loading: true };
      });
      axios
        .get(`monthpage/${areaName}/${page.id}`)
        .then(res => setAreaState({ area: res.data, loading: false }))
        .catch(err => console.log(err));
    }
  }, [page, areaName]);

  return { areaState, setAreaState, page };
}
