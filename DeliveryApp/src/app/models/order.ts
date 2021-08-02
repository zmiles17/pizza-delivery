import { Customer } from "./customer";
import { OrderItem } from "./orderitem";
import { Store } from "./store";

export class Order {
    id?: number;
    guid?: string;
    customerId?: number;
    storeId?: number;
    timeIn?: Date;
    timeOut?: Date;
    deliveryTime?: Date;
    timeInOven?: Date;
    orderStatus?: number;
    customer?: Customer;
    store?: Store;
    orderItems?: OrderItem[];
}
