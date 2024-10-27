import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { IBasketItem } from '../shared/models/basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {
  constructor(public basketService: BasketService) { }

  removeBasketItem(item: IBasketItem) {
    this.basketService.removeItemFromBasket(item);
  }

  incrementItem(item: IBasketItem) {
    this.basketService.incrementItemQuantity(item);
  }

  decrementItem(item: IBasketItem) {
    this.basketService.decrementItemQuantity(item);
  }
}