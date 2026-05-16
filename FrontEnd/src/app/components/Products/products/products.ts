import { Component, effect, OnInit, signal, Signal } from '@angular/core';
import { ProductService } from '../../../services/product-service';
import { ProductReturn } from '../../../models/Products';
import { CategoryService } from '../../../services/category-service';

@Component({
  selector: 'app-products',
  imports: [],
  templateUrl: './products.html',
  styleUrl: './products.scss',
})
export class Products implements OnInit {

Products = signal<ProductReturn[]>([]);
constructor( private ProductServ:ProductService,private categoryService:CategoryService){
      effect(() => {
      const categoryId = this.categoryService.selectedCategoryId();
      if (categoryId > 0) {
        this.GetProductsByCategory(categoryId);
      } else {
        this.GetAllProducts();
      }
    });


}
  ngOnInit(): void {
this.GetAllProducts(); 

}


GetAllProducts(){

this.ProductServ.getProducts().subscribe({
  next:(data)=>{
    this.Products.set(data);
  },
  error:(er)=>{
    console.log(er)
  }
})
}


GetProductsByCategory(Catid:number){

this.ProductServ.getProductByCategoryID(Catid).subscribe({
  next:(data)=>{
    this.Products.set(data);
  },
  error:(er)=>{
    console.log(er)
  }
})


}
}
