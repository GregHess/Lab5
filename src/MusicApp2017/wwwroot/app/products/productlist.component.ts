import { Component } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'productlist',
    templateUrl: './productlist.component.html'
})
export class ProductListComponent {
    public products: Product[];

    constructor(http: Http) {
        http.get('/api/products').subscribe(result => {
            this.products = result.json();
           
        });
    }
}

interface Product {
    productID: number;
    name: string;
    description: string;
    price: number;
    category: string;
    quantity: number;
    amountDue: number
}