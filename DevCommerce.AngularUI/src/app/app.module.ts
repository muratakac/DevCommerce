import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { ProductViewComponent } from './product-view/product-view.component';
import { AppRoutingModule } from './/app-routing.module';
import { NavmenuComponent } from './navmenu/navmenu.component';
import { BasketComponent } from './basket/basket.component';


@NgModule({
  declarations: [
    AppComponent,
    ProductViewComponent,
    NavmenuComponent,
    BasketComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
