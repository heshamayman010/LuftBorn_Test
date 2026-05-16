import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Category } from '../../../models/Category';
import { CategoryService } from '../../../services/category-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-categories',
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-categories.html',
  styleUrl: './admin-categories.scss',
})
export class AdminCategories {




  categories = signal<Category[]>([]);
  showModal = signal<boolean>(false);
  isEdit = signal<boolean>(false);
  showDeleteModal = signal<boolean>(false);
  categoryToDelete = signal<Category | null>(null);
  
  categoryForm = {
    id: 0,
    nameAr: '',
    nameEn: ''
  };
  
  constructor(
    private categoryService: CategoryService,
  ) {}
  
  ngOnInit() {
    this.loadCategories();
  }
  
  loadCategories() {
    this.categoryService.getCategories().subscribe({
      next: (data) => this.categories.set(data),
      error: (err) => console.error('error in the load categories:', err)
    });
  }
  
  openAddModal() {
    this.isEdit.set(false);
    this.resetForm();
    this.showModal.set(true);
  }
  
  openEditModal(category: Category) {
    this.isEdit.set(true);
    this.categoryForm = { ...category };
    this.showModal.set(true);
  }
  
  save() {
    if (this.isEdit()) {
      this.categoryService.updateCategory(this.categoryForm).subscribe({
        next: () => {
          this.loadCategories();
          this.closeModal();
        },
        error: (err) => console.error('Error updating category:', err)
      });
    } else {
      this.categoryService.createCategory(this.categoryForm).subscribe({
        next: () => {
          this.loadCategories();
          this.closeModal();
        },
        error: (err) => console.error('Error creating category:', err)
      });
    }
  }
  
  resetForm() {
    this.categoryForm = {
      id: 0,
      nameAr: '',
      nameEn: ''
    };
  }
  
  closeModal() {
    this.showModal.set(false);
    this.resetForm();
  }
  
  
  openDeleteModal(category: Category) {
    this.categoryToDelete.set(category);
    this.showDeleteModal.set(true);
  }
  
  confirmDelete() {
    const category = this.categoryToDelete();
    if (category) {
      this.categoryService.deleteCategory(category.id).subscribe({
        next: () => {
          this.loadCategories();
          this.closeDeleteModal();
        },
        error: (err) => console.error('Error deleting category:', err)
      });
    }
  }
  
  closeDeleteModal() {
    this.showDeleteModal.set(false);
    this.categoryToDelete.set(null);
  }
}

