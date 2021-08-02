import { Ingredient } from "./ingredient";
import { Store } from "./store";

export class Inventory {
    id?: number;
    storeId: number;
    ingredientId: number;
    quantity: number;
    store: Store;
    ingredient: Ingredient;
}
