import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { HomepageComponent } from './components/homepage/homepage.component';
import { ShopComponent } from './components/shop/shop.component';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { HttpClientModule } from '@angular/common/http';
import { FooterComponent } from './shared/components/footer/footer.component';
import { ProizvodComponent } from './components/proizvod/proizvod.component';
import { AboutComponent } from './components/about/about.component';
import { SignupComponent } from './user/signup/signup.component';
import { LoginComponent } from './user/login/login.component';
import { CartComponent } from './components/cart/cart.component';
import { ContactComponent } from './components/contact/contact.component';
import { EditItemDialogComponent } from './components/dialogs/edit-item-dialog/edit-item-dialog.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    AppComponent,
    HomepageComponent,
    NavbarComponent,
    ShopComponent,
    FooterComponent,
    ProizvodComponent,
    AboutComponent,
    SignupComponent,
    LoginComponent,
    CartComponent,
    ContactComponent,
    EditItemDialogComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ButtonsModule.forRoot(),
    NgbModule,
    CollapseModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
