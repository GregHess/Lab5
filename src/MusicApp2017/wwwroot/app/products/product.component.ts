import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { ActivatedRoute } from '@angular/router'

@Component({
    selector: 'product',
    templateUrl: './product.component.html'
})
export class ProductComponent {
    public product: Product;

    constructor(http: Http, route: ActivatedRoute) {
        var id = route.snapshot.params['id'];
        http.get('/api/products/' + id).subscribe(result => {
            this.product = result.json();
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