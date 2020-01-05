export function useFillToNumber<T>(
  list: T[],
  fillTo: number,
  getEmptyItem: () => T
): T[] {
  for (let i = list.length; i < fillTo; i++) list.push(getEmptyItem());
  return list;
}
