import { useState, useEffect, useContext } from "react";
import { IPageArea } from "../models";
import axios from "axios";
import { MonthPageContext } from "../context";

type AreaState<T extends IPageArea> = {
  loading: boolean;
  area: T;
};

export default function usePageArea<T extends IPageArea>(areaName: string) {
  const { page } = useContext(MonthPageContext);
  const [areaState, setAreaState] = useState<AreaState<T>>(null);

  useEffect(() => {
    if (page) {
      setAreaState({ ...areaState, loading: true });
      axios
        .get(`https://localhost:44320/api/monthpage/${areaName}/${page.id}`)
        .then(res => setAreaState({ area: res.data, loading: false }));
    }
  }, [page, areaName]);

  return { areaState, page };
}
