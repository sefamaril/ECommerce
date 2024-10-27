import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Basket, IBasket, IBasketItem } from '../shared/models/basket';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = 'http://localhost:8001/api/v1'
  // http://localhost:8001/api/v1/Basket/CreateBasket
  constructor(private http: HttpClient) { }
  private basketSource = new BehaviorSubject<Basket | null>(null);
  basketSource$ = this.basketSource.asObservable();

  getBasket(username: string) {
    return this.http.get<IBasket>(this.baseUrl + '/Basket/GetBasket/sefa').subscribe({
      next: basket => this.basketSource.next(basket)
    });
  }

  setBasket(basket: IBasket) {
    return this.http.post<IBasket>(this.baseUrl + '/Basket/CreateBasket', basket).subscribe({
      next: basket => this.basketSource.next(basket),
      error: error => {
        console.error('Error creating basket:', error);
        // Burada hata mesajını daha detaylı gösterebilirsiniz
        if (error.error instanceof ErrorEvent) {
          // A client-side or network error occurred. Handle it accordingly.
          console.error('An error occurred:', error.error.message);
        } else {
          // The backend returned an unsuccessful response code.
          // The response body may contain clues as to what went wrong.
          console.error(
            `Backend returned code ${error.status}, ` +
            `body was: ${error.error}`);
        }
      }
    });
  }
  
  
  getCurrentBasket() {
    return this.basketSource.value;
  }

  addItemToBasket(item: IProduct, quantity = 1) {
    const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(item);
    const basket = this.getCurrentBasket() ?? this.createBasket();
    // now items can be added in the basket
    basket.items = this.addOrUpdateItem(basket.items, itemToAdd, quantity);
    this.setBasket(basket);
  }

  private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    //if we have the item in basket which matches the Id, then we can get here.
    const item = items.find(x => x.productId == itemToAdd.productId);
    if (item) {
      item.quantity += quantity;
    }
    else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    return items;
  }

  private createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem('basket_username', 'sefa'); //TODO: sefa can be replaced with LoggedIn User
    return basket;
  }

  private mapProductItemToBasketItem(item: IProduct): IBasketItem {
    return {
      productId: item.id,
      productName: item.name,
      price: item.price,
      imageFile: item.imageFile,
      quantity: 0
    }
  }

}