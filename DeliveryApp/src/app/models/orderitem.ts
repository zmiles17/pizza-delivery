import { Item } from "./item";
import { Order } from "./order";

export class OrderItem {
    orderId?: number;
    itemId: number;
    quantity: number;
    order: Order;
    item: Item;

}
