export function useFillToNumber<T>(
  list: T[],
  fillTo: number,
  getEmptyItem: () => T
): T[] {
  fillTo = list.length >= fillTo ? list.length + 1 : fillTo;
  for (let i = list.length; i < fillTo; i++) list.push(getEmptyItem());
  return list;
}
