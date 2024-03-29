import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Proizvod } from 'src/app/models/Proizvod';
import { ProizvodService } from 'src/app/services/proizvod.service';
import { ManageProductDialogsComponent } from '../dialogs/manage-product-dialogs/manage-product-dialogs.component';
import { TipProizvodaService } from 'src/app/services/tipProizvoda.service';
import { TipProizvoda } from 'src/app/models/TipProizvoda';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {

  public filterCategory: any
  public Proizvodi: Proizvod[] = []
  public SviProizvodi: Proizvod[] = []
  public SviTipoviProizvoda: TipProizvoda[] = []

  constructor(private proizvodService: ProizvodService, private router: Router, private tipProizvodaService: TipProizvodaService) { }

  ngOnInit(): void {
    this.proizvodService.prikaziProizvode().subscribe(p => {
      this.Proizvodi = p
      this.filterCategory = p
      this.SviProizvodi = p
      console.log(this.Proizvodi)
    })

    this.tipProizvodaService.prikaziTipoveProizvoda().subscribe(tp => {
      this.SviTipoviProizvoda = tp
      console.log(this.SviTipoviProizvoda)
    })
  }

  filter(tip: string){
    this.filterCategory = this.Proizvodi
    .filter((a: any) => {
      if(a.tipProizvoda.naziv == tip || tip == ''){
        return a;
      }
    })
  }

  prikaziProizvode(){
    return this.filterCategory = this.SviProizvodi
  }

  sortirajRastuce(){
    return this.filterCategory.sort((a: { cena: number; },b: { cena: number; }) => a.cena - b.cena)
  }

  sortirajOpadajuce(){
    return this.filterCategory.sort((a: { cena: number; },b: { cena: number; }) => b.cena - a.cena)
  }
}