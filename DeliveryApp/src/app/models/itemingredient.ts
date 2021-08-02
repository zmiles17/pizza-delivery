import { Ingredient } from "./ingredient";
import { Item } from "./item";

export class ItemIngredient {
    itemId: number;
    ingredientId: number;
    quantity: number;
    item: Item;
    ingredient: Ingredient;
}
