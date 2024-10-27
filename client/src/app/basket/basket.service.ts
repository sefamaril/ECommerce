import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Basket, IBasket, IBasketItem, IBasketTotal } from '../shared/models/basket';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = 'http://localhost:8001/api/v1';

  constructor(private http: HttpClient) { }

  private basketSource = new BehaviorSubject<Basket | null>(null);
  basketSource$ = this.basketSource.asObservable();

  private basketTotal = new BehaviorSubject<IBasketTotal | null>(null);
  basketTotal$ = this.basketTotal.asObservable();

  //#region Basket Operations

  getBasket(username: string) {
    return this.http.get<IBasket>(this.baseUrl + '/Basket/GetBasket/sefa').subscribe({
      next: basket => {
        this.basketSource.next(basket);
        this.calculateBasketTotal();
      }
    });
  }

  setBasket(basket: IBasket) {
    return this.http.post<IBasket>(this.baseUrl + '/Basket/CreateBasket', basket).subscribe({
      next: basket => {
        this.basketSource.next(basket);
        this.calculateBasketTotal();
      }
    });
  }

  getCurrentBasket() {
    return this.basketSource.value;
  }

  addItemToBasket(item: IProduct, quantity = 1) {
    const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(item);
    const basket = this.getCurrentBasket() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
    this.setBasket(basket);
  }

  incrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasket();
    if (!basket) return;
    const foundItemIndex = basket.items.findIndex((x) => x.productId === item.productId);
    basket.items[foundItemIndex].quantity++;
    this.setBasket(basket);
  }

  removeItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentBasket();
    if (!basket) return;
    if (basket.items.some((x) => x.productId === item.productId)) {
      basket.items = basket.items.filter((x) => x.productId !== item.productId);
      if (basket.items.length > 0) {
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket.userName);
      }
    }
  }

  deleteBasket(userName: string) {
    return this.http.delete(this.baseUrl + "/Basket/DeleteBasket/" + userName).subscribe({
      next: (response) => {
        this.basketSource.next(null);
        this.basketTotal.next(null);
        localStorage.removeItem('basket_username');
      },
      error: (err) => {
        console.log('Error occured while deleting basket');
        console.log(err);
      }
    });
  }

  decrementItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasket();
    if (!basket) return;
    const foundItemIndex = basket.items.findIndex((x) => x.productId === item.productId);
    if (basket.items[foundItemIndex].quantity > 1) {
      basket.items[foundItemIndex].quantity--;
      this.setBasket(basket);
    } else {
      this.removeItemFromBasket(item);
    }
  }

  //#endregion Basket Operations

  //#region Helper Methods

  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const item = items.find(x => x.productId == itemToAdd.productId);
    if (item) {
      item.quantity += quantity;
    } else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    return items;
  }

  private createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem('basket_username', 'sefa');
    return basket;
  }

  private mapProductItemToBasketItem(item: IProduct): IBasketItem {
    return {
      productId: item.id,
      productName: item.name,
      price: item.price,
      imageFile: item.imageFile,
      quantity: 0
    };
  }

  private calculateBasketTotal() {
    const basket = this.getCurrentBasket();
    if (!basket) return;
    const total = basket.items.reduce((x, y) => (y.price * y.quantity) + x, 0);
    this.basketTotal.next({ total });
  }

  //#endregion Helper Methods
}