"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var router_1 = require("@angular/router");
var http_1 = require("@angular/http");
var forms_1 = require("@angular/forms");
var app_component_1 = require("./app.component");
var navmenu_component_1 = require("./navmenu/navmenu.component");
var home_component_1 = require("./home/home.component");
var helloworld_component_1 = require("./helloworld.component");
var product_component_1 = require("./products/product.component");
var productlist_component_1 = require("./products/productlist.component");
var addproduct_component_1 = require("./products/addproduct.component");
var weather_component_1 = require("./weather/weather.component");
var album_component_1 = require("./albums/album.component");
var albumlist_component_1 = require("./albums/albumlist.component");
var addalbum_component_1 = require("./albums/addalbum.component");
var artistdropdown_component_1 = require("./albums/artistdropdown.component");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        imports: [platform_browser_1.BrowserModule, http_1.HttpModule, http_1.JsonpModule, forms_1.FormsModule, router_1.RouterModule.forRoot([
                { path: '', redirectTo: 'home', pathMatch: 'full' },
                { path: 'home', component: home_component_1.HomeComponent },
                { path: 'hello', component: helloworld_component_1.HelloWorldComponent },
                { path: 'weather', component: weather_component_1.WeatherComponent },
                { path: 'products/:id', component: product_component_1.ProductComponent },
                { path: 'products', component: productlist_component_1.ProductListComponent },
                { path: 'albums/:id', component: album_component_1.AlbumComponent },
                { path: 'albums', component: albumlist_component_1.AlbumListComponent },
                { path: '**', redirectTo: 'home' }
            ])],
        declarations: [app_component_1.AppComponent, home_component_1.HomeComponent, navmenu_component_1.NavMenuComponent, helloworld_component_1.HelloWorldComponent, weather_component_1.WeatherComponent,
            product_component_1.ProductComponent, productlist_component_1.ProductListComponent, addproduct_component_1.AddProductComponent, album_component_1.AlbumComponent, albumlist_component_1.AlbumListComponent, addalbum_component_1.AddAlbumComponent,
            artistdropdown_component_1.ArtistDropdownComponent],
        bootstrap: [app_component_1.AppComponent]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map