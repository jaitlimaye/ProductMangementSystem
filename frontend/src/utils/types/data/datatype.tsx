export type User = {
    id?: number,
    email: string,
    name?: string,
}

export type Category = {
    categoryId: number,
    name: string,
    description: string,
}

export type Attribute = {
    name: string,
}
export type Product = {
    productId: number,
    name: string,
    description?: string,
    price: number,
    categoryId?: number,
    attributes?: Attribute[],
    imageUrl?: string,
}

export type Role = {
    roleId: number,
    roleName: string,
}