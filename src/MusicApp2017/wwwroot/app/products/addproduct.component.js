"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var AddProductComponent = (function () {
    function AddProductComponent(http) {
        this.http = http;
        this.model = new Product();
        this.showForm = false;
    }
    AddProductComponent.prototype.onSubmit = function (form) {
        var _this = this;
        var headers = new http_1.Headers();
        headers.append('Content-Type', 'application/json');
        this.http.post('/api/products', JSON.stringify(this.model), { headers: headers }).subscribe(function (res) { return _this.postResponse = res.json(); });
        form.reset();
        this.showForm = !this.showForm;
    };
    AddProductComponent.prototype.toggleForm = function () {
        this.showForm = !this.showForm;
    };
    return AddProductComponent;
}());
AddProductComponent = __decorate([
    core_1.Component({
        selector: 'addproduct',
        templateUrl: './addproduct.component.html'
    }),
    __metadata("design:paramtypes", [http_1.Http])
], AddProductComponent);
exports.AddProductComponent = AddProductComponent;
var Product = (function () {
    function Product(productID, name, description, price, category, quantity, amountDue) {
        if (productID === void 0) { productID = 0; }
        if (name === void 0) { name = null; }
        if (description === void 0) { description = null; }
        if (price === void 0) { price = null; }
        if (category === void 0) { category = null; }
        if (quantity === void 0) { quantity = 0; }
        if (amountDue === void 0) { amountDue = 0; }
        this.productID = productID;
        this.name = name;
        this.description = description;
        this.price = price;
        this.category = category;
        this.quantity = quantity;
        this.amountDue = amountDue;
    }
    return Product;
}());
//# sourceMappingURL=addproduct.component.js.map