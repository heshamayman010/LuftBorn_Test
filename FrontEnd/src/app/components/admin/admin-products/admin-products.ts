import { Component, signal } from '@angular/core';
import { ProductService } from '../../../services/product-service';
import { Product, ProductAdd, ProductReturn, ProductUpdate } from '../../../models/Products';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-products',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-products.html',
  styleUrl: './admin-products.scss',
})
export class AdminProducts {

  products = signal<ProductReturn[]>([]);
  showModal = signal<boolean>(false);
  isEdit = signal<boolean>(false);
   showDeleteModal = signal<boolean>(false);
   productToDelete = signal<ProductReturn | null>(null);
  
  productForm :ProductUpdate= {
     id:0,
     nameAr :'',
     nameEn  :'',
     quantity :0,
      price :0,
      barCode  :'',
      notesAr  :'',
      notesEn  :'',
     categoryId :0,
  };
  
  constructor(private productService: ProductService) {}
  
  ngOnInit() {
    this.loadProducts();
  }
  
  loadProducts() {
    this.productService.getProducts().subscribe({
      next: (data) => this.products.set(data)
    });
  }
  
  openAddModal() {
    this.isEdit.set(false);
    this.resetForm();
    this.showModal.set(true);
  }
  
  openEditModal(product: ProductUpdate) {
    this.isEdit.set(true);
    this.productForm = { ...product };
    this.showModal.set(true);
  }
  
  save() {
    if (this.isEdit()) {
      this.productService.updateProduct(this.productForm).subscribe({
        next: () => {
          this.loadProducts();
          this.showModal.set(false);
        }
      });
    } else {
      this.productService.createProduct(this.productForm).subscribe({
        next: () => {
          this.loadProducts();
          this.showModal.set(false);
        }
      });
    }
  }
  
  
  resetForm() {
    this.productForm = {
     id:0,
     nameAr :'',
     nameEn  :'',
     quantity :0,
      price :0,
      barCode  :'',
      notesAr  :'',
      notesEn  :'',
     categoryId :0,
    };
  }
  
  closeModal() {
    this.showModal.set(false);
  }




// for the delete 
  openDeleteModal(product: ProductReturn) {
    this.productToDelete.set(product);
    this.showDeleteModal.set(true);
  }
  
  confirmDelete() {
    const product = this.productToDelete();
    if (product) {
      this.productService.deleteProduct(product.id).subscribe({
        next: () => {
          this.loadProducts();
          this.closeDeleteModal();
        },
        error: (err) => console.error('problem in deleteing the product:', err)
      });
    }
  }
  
  closeDeleteModal() {
    this.showDeleteModal.set(false);
    this.productToDelete.set(null);
  }



}
