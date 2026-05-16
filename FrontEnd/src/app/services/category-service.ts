import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/Category';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
selectedCategoryId = signal<number>(0);
  setSelectedCategory(categoryId: number) {
    this.selectedCategoryId.set(categoryId);
  }


  private categoryAPIURL: string = environment.CategoryApiUrl;
  constructor(private http: HttpClient) {
  }
  getCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(`${this.categoryAPIURL}/GetAll`);
  }


  getCategoryBytID(catId: number): Observable<Category> {
    return this.http.get<Category>(`${this.categoryAPIURL}/${catId}`);
  }
  

  updateCategory(cat: Category): Observable<Category> {
    return this.http.put<Category>(`${this.categoryAPIURL}/update`, cat);
  }

  deleteCategory(cateId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.categoryAPIURL}/HardDelete?id=${cateId}`);
  }
    SoftdeleteCategory(cateId: number): Observable<boolean> {
    return this.http.delete<boolean>(`${this.categoryAPIURL}?id=${cateId}`);
  }


  createCategory(newCategory: Category): Observable<Category> {
    return this.http.post<Category>(`${this.categoryAPIURL}/add`, newCategory);
  }








}
