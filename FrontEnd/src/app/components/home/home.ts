import { Component, OnInit, signal } from '@angular/core';
import { CategoryService } from '../../services/category-service';
import { Categories } from '../Categories/categories/categories';
import { CommonModule } from '@angular/common';  // Add this import
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Category } from '../../models/Category';
import { Products } from "../Products/products/products";

@Component({
  selector: 'app-home',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    Products,
    Categories
],
  templateUrl: './home.html',
  styleUrl: './home.scss',
})
export class Home {


}
