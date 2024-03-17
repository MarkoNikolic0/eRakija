import { Component, Input, OnInit } from '@angular/core';
import { Proizvod } from 'src/app/models/Proizvod';
import { CartService } from 'src/app/services/cart.service';
import { ProizvodService } from 'src/app/services/proizvod.service';

@Component({
  selector: 'app-proizvod',
  templateUrl: './proizvod.component.html',
  styleUrls: ['./proizvod.component.scss']
})
export class ProizvodComponent {
  @Input() proizvod?: Proizvod;

  constructor(private cartService : CartService) { }

  addtocart(item: any){
    this.cartService.addToCart(item)
  }
}
