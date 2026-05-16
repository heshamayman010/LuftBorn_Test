import { Component, signal } from '@angular/core';
import { CategoryService } from '../../../services/category-service';
import { Category } from '../../../models/Category';

@Component({
  selector: 'app-categories',
  imports: [],
  templateUrl: './categories.html',
  styleUrl: './categories.scss',
})
export class Categories {
categories = signal<Category[]>([]);
SecondCategories:Category[]=[]

selectedCategoryId:number=0;

constructor( private categoryServie:CategoryService){




}
  ngOnInit(): void {
this.GetAllCategoires()

}



GetAllCategoires(){
  this.categoryServie.getCategories().subscribe({
    next: (data) => {
    this.categories.set(data);
    this.SecondCategories=data
    },
    error: (err) => {
      console.error('Error loading categories:', err);
    }
  });

}

selectCategory(cateId:number){
this.categoryServie.setSelectedCategory(cateId);
}
}