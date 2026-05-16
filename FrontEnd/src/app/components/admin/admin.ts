import { Component, signal } from '@angular/core';
import { AdminCategories } from './admin-categories/admin-categories';
import { AdminProducts } from './admin-products/admin-products';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  imports: [AdminCategories,AdminProducts],
  templateUrl: './admin.html',
  styleUrl: './admin.scss',
})
export class Admin {
constructor(private router:Router){}

  selectedTab = signal<'products' | 'categories'>('products');

goToHome(){

      this.router.navigate(['/home']);

}
}
