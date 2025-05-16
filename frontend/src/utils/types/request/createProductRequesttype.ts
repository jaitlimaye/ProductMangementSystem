export type createProductRequesttype = {
    name: string,
    description: string | null,
    price: string,
    categoryId:number | undefined,
    image: File | null,
  }