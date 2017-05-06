import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Http, Headers } from '@angular/http';

@Component({
    selector: 'addproduct',
    templateUrl: './addproduct.component.html'

})

export class AddProductComponent {

    model: Product = new Product();
    postResponse: Object;
    showForm = false;

    constructor(private http: Http) {

    }

    onSubmit(form: NgForm) {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        this.http.post('/api/products', JSON.stringify(this.model), { headers: headers }).subscribe(res => this.postResponse = res.json());
        form.reset();
        this.showForm = !this.showForm;
    }

    toggleForm() {
        this.showForm = !this.showForm;
    }

}

class Product {
    constructor(private productID: number = 0,
        public name: string = null,
        public description: string = null,
        public price: number = null,
        public category: string = null,
        public quantity: number = 0,
        private amountDue: number = 0
    ) { }
}