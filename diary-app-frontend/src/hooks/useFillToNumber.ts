import { IListItem } from "../models";

export function useFillToNumber<T extends IListItem>(
  list: T[],
  fillTo: number,
  getEmptyItem: () => T
): T[] {
  let length = list.length;
  fillTo = length >= fillTo ? length + 1 : fillTo;
  for (let i = length; i < fillTo; i++) {
    let emptyItem = getEmptyItem();
    emptyItem.readonly = true;
    list.push(emptyItem);
  }

  list[length].readonly = false;
  return list;
}
