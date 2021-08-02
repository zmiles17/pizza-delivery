import { ItemIngredient } from "./itemingredient";

export class Item {
    id?: number;
    name: string;
    price: number;
    imageUrl: string;
    itemIngredients: ItemIngredient[]
}
