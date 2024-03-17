import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit{

  public products : any = []
  totalPrice: number = 0
  totalQuantity: number = 0

  constructor(private cartService: CartService) { }
  ngOnInit(): void {
    this.cartService.getProducts().subscribe(res => this.products = res)

    this.cartService.totalPrice.subscribe(data => this.totalPrice = data)

    this.cartService.totalQuantity.subscribe(data => this.totalQuantity += data)

    this.cartService.computeCartTotals()
  }

  removeItem(item: any){
    this.cartService.removeCartItem(item)
  }

  povecajKolicinu(item: any){
    this.cartService.addToCart(item)
  }

  smanjiKolicinu(item: any){
    this.cartService.decreaseCartItem(item)
  }

  emptyCart(){
    this.cartService.removeAllCart()
  }
}
