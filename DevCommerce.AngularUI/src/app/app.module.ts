import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { ProductViewComponent } from './product-view/product-view.component';
import { AppRoutingModule } from './/app-routing.module';
import { NavmenuComponent } from './navmenu/navmenu.component';
import { BasketComponent } from './basket/basket.component';
import { HomeComponent } from './home/home.component';
import { SliderComponent } from './slider/slider.component';
import { HeroesComponent } from './heroes/heroes.component';


@NgModule({
  declarations: [
    AppComponent,
    ProductViewComponent,
    NavmenuComponent,
    BasketComponent,
    HomeComponent,
    SliderComponent,
    HeroesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
