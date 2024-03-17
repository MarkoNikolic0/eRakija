import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/services/cart.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit{

  constructor(private cartService: CartService) { }

  public brojProizvoda : number = 0

  ngOnInit(): void {
    this.cartService.totalQuantity
    .subscribe(res => {
      this.brojProizvoda =+ res
    })
  }

  isCollapsed = true;

  toggleCollapse() {
    this.isCollapsed = !this.isCollapsed;
  }
}
