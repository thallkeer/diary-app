import { useState, useEffect, useContext } from "react";
import { IPageArea, IMonthPage } from "../models";
import { MonthPageContext } from "../context";
import axios from "../axios/axios";

export type AreaState<T extends IPageArea> = {
  loading: boolean;
  area: T;
};

export type PageAreaResult<T extends IPageArea> = {
  pageAreaState: AreaState<T>;
  page?: IMonthPage;
};

export default function usePageArea<T extends IPageArea>(
  areaName: string
): PageAreaResult<T> {
  const { page } = useContext(MonthPageContext);
  const [areaState, setAreaState] = useState<AreaState<T>>(null);

  useEffect(() => {
    if (page) {
      setAreaState((prevState) => {
        return { ...prevState, loading: true };
      });
      axios
        .get(`monthpage/${areaName}/${page.id}`)
        .then((res) =>
          setAreaState((prevState) => {
            return { ...prevState, area: res.data, loading: false };
          })
        )
        .catch((err) => console.log(err));
    }
  }, [page, areaName]);

  return { pageAreaState: areaState, page };
}
