export interface Product {

    
}



export interface ProductAdd {
     nameAr :string
     nameEn  :string
     quantity :number
      price :number
      barCode  :string
      notesAr  :string
      notesEn  :string
     categoryId :number

    
}

export interface ProductUpdate {
     id:number
     nameAr :string
     nameEn  :string
     quantity :number
      price :number
      barCode  :string
      notesAr  :string
      notesEn  :string
     categoryId :number

    
}




export interface ProductReturn {
     id :number
     nameAr :string
     nameEn  :string
     quantity :number
      price :number
      barCode  :string
      notesAr  :string
      notesEn  :string
     categoryId :number
    categoryNameAr:string;
    categoryNameEn:string;
    
}




