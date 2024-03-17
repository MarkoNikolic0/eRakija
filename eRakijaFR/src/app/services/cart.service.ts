import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { Proizvod } from '../models/Proizvod';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  public cartItemList: any = []
  public productList = new BehaviorSubject<any>([])

  totalPrice: Subject<number> = new Subject<number>()
  totalQuantity: Subject<number> = new Subject<number>()

  constructor() { }

  getProducts() {
    return this.productList.asObservable()
  }

  setProduct(product: any) {
    this.cartItemList.push(...product)
    this.productList.next(product)
    this.computeCartTotals()
  }

  addToCart(product: any) {
    let alreadyExistsInCart: boolean = false
    let existingCartItem: any = undefined

    if(this.cartItemList.length > 0)
    {
      for(let tempCartItem of this.cartItemList){
        if(tempCartItem.id === product.id)
        {
          existingCartItem = tempCartItem
          break
        }
      }
      alreadyExistsInCart = (existingCartItem != undefined)
    }
    if(alreadyExistsInCart){
      existingCartItem.kolicina++
    }
    else{
      this.cartItemList.push(product)
      this.productList.next(this.cartItemList)
      console.log(this.cartItemList)
    }

    this.computeCartTotals()
  }

  computeCartTotals() {
    let totalPriceValue: number = 0
    let totalQuantityValue: number = 0

    for(let currItem of this.cartItemList){
      totalPriceValue += currItem.kolicina * currItem.cena
      totalQuantityValue += currItem.kolicina
    }

    this.totalPrice.next(totalPriceValue)
    this.totalQuantity.next(totalQuantityValue)
  }

  decreaseCartItem(product: any) {
    let alreadyExistsInCart: boolean = false
    let existingCartItem: any = undefined

    if(this.cartItemList.length > 0)
    {
      for(let tempCartItem of this.cartItemList){
        if(tempCartItem.id === product.id)
        {
          existingCartItem = tempCartItem
          break
        }
      }
      alreadyExistsInCart = (existingCartItem != undefined)
    }
    if(alreadyExistsInCart){
      if(existingCartItem.kolicina === 1)
      {
        this.cartItemList.splice(existingCartItem, 1)
      }
      else if(existingCartItem.kolicina > 1){
        existingCartItem.kolicina--
      }
    }
    this.computeCartTotals()
  }

  removeCartItem(product: any){
    this.cartItemList.map((a: any, index: any) => {
      if (product.id === a.id) {
        this.cartItemList.splice(index, 1)
      }
    })
    this.productList.next(this.cartItemList)

    this.computeCartTotals()
  }

  removeAllCart() {
    this.cartItemList = []
    this.productList.next(this.cartItemList)

    this.computeCartTotals()
  }
}


