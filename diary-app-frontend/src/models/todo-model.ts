import IListItem from "./list-item-model";

export default interface ITodoItem extends IListItem {
  done: boolean;
}
