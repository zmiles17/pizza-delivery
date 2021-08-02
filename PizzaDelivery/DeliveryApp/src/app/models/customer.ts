import { Order } from "./order";

export class Customer {
    id?: number;
    name: string;
    phone: string;
    address: string;
    city: string;
    state: string;
    zip: string;
    orders: Order[];
}
