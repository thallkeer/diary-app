import React, { useEffect } from "react";
import { IPageArea } from "../../models";
import Loader from "../Loader";
import usePageArea, { PageAreaResult } from "../../hooks/usePageArea";

interface MonthAreaProps<T extends IPageArea>
  extends React.HTMLAttributes<HTMLDivElement> {
  areaName: string;
  areaBody: (areaProps: PageAreaResult<T>) => JSX.Element;
}

export function MonthArea<T extends IPageArea>(props: MonthAreaProps<T>) {
  const { areaBody, areaName, className } = props;
  const pageAreaRes = usePageArea<T>(areaName);
  const { pageAreaState, page } = pageAreaRes;

  useEffect(() => {}, [areaName, areaBody]);

  if (!page || !pageAreaState || pageAreaState.loading) return <Loader />;

  const cn = `${className || ""} area-header`;

  return (
    <>
      <h1 className={cn}>{pageAreaState.area.header}</h1>
      {areaBody(pageAreaRes)}
    </>
  );
}
