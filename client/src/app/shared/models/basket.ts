export interface IBasketItem {
    quantity: number;
    imageFile: string;
    price: number;
    productId: string;
    productName: string;
}

export interface IBasket {
    userName: string;
    items: IBasketItem[];
    totalPrice: number;
}