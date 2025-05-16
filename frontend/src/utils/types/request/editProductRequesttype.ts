export type editProductRequesttype = {
  productId: number,
    name: string,
    description: string | null,
    price: string,
    categoryId:number | undefined,
    image: File | null,
    currentImageUrl: string | null,
  }