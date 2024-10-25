import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IProduct } from '../shared/models/product';
import { IBrand } from '../shared/models/brand';
import { IType } from '../shared/models/type';
import { StoreParams } from '../shared/models/storeParams';

@Injectable({
  providedIn: 'root'
})
export class StoreService {

  constructor(private http: HttpClient) { }

  baseUrl = 'http://localhost:8000/api/v1/';

  getProducts(storeParams: StoreParams) {
    let params = new HttpParams();
    if (storeParams.brandId) {
      params = params.append('brandId', storeParams.brandId);
    }
    if (storeParams.typeId) {
      params = params.append('typeId', storeParams.typeId);
    }
    return this.http.get<IPagination<IProduct[]>>(this.baseUrl + 'Catalog/GetAllProducts', { params });
  }
  getBrands() {
    return this.http.get<IBrand[]>(this.baseUrl + 'Catalog/GetAllBrands');
  }
  getTypes() {
    return this.http.get<IType[]>(this.baseUrl + 'Catalog/GetAllTypes');
  }
}