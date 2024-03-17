import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomepageComponent } from './components/homepage/homepage.component';
import { ShopComponent } from './components/shop/shop.component';
import { AboutComponent } from './components/about/about.component';
import { LoginComponent } from './user/login/login.component';
import { CartComponent } from './components/cart/cart.component';
import { ContactComponent } from './components/contact/contact.component';


const routes: Routes = [
  {path: "shop", component:ShopComponent},
  {path: "home", component:HomepageComponent},
  {path: "about", component:AboutComponent},
  {path: "login", component:LoginComponent},
  {path: "cart", component:CartComponent},
  {path: "contact", component:ContactComponent},
  {path: '', redirectTo: 'home', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
