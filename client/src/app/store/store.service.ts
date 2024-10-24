import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  constructor(private http: HttpClient) { }
  
  baseUrl = 'http://localhost:8000/api/v1/';

  getProducts(){
    return this.http.get<IPagination<IProduct[]>>(this.baseUrl+'Catalog/GetAllProducts');
  }
}