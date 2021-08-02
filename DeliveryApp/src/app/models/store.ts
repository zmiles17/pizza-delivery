import { Inventory } from "./inventory";
import { Order } from "./order";

export class Store {
    id?: number;
    storeNumber: string;
    phone: string;
    address: string;
    city: string;
    state: string;
    zip: string;
    inventory: Inventory[];
    orders: Order[];
}
