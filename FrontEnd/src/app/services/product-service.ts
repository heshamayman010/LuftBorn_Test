import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductAdd, ProductReturn } from '../models/Products';
const env = environment as any;

@Injectable({
  providedIn: 'root',
})
export class ProductService {


  private productsAPIURL: string = env.productsAPIURL;
  constructor(private http: HttpClient) {
  }
  getProducts(): Observable<ProductReturn[]> {
    return this.http.get<ProductReturn[]>(`${this.productsAPIURL}/GetAll`);
  }

  searchProducts(searchString: string): Observable<ProductReturn[]> {
    return this.http.get<ProductReturn[]>(`${this.productsAPIURL}/search/${searchString}`);
  }

  getProductByProductID(productID: number): Observable<ProductReturn> {
    return this.http.get<ProductReturn>(`${this.productsAPIURL}/${productID}`);
  }
  

  updateProduct(product: ProductAdd): Observable<ProductReturn> {
    return this.http.put<ProductReturn>(`${this.productsAPIURL}/update`, product);
  }

  deleteProduct(productID: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.productsAPIURL}/HardDelete?id=${productID}`);
  }

      SoftdeleteProduct(productID: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.productsAPIURL}?id=${productID}`);
  }


  createProduct(newProductRequest: ProductAdd): Observable<ProductReturn> {
    return this.http.post<ProductReturn>(`${this.productsAPIURL}/add`, newProductRequest);
  }


  getProductByCategoryID(categoryId: number): Observable<ProductReturn[]> {
    return this.http.get<ProductReturn[]>(`${this.productsAPIURL}/GetAllProductsByCategoryId/${categoryId}`);
  }


}





