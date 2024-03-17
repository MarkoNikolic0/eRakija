import { Component, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ProizvodPost } from 'src/app/models/Proizvod';
import { TipProizvoda, TipProizvodaPost } from 'src/app/models/TipProizvoda';
import { ProizvodService } from 'src/app/services/proizvod.service';
import { TipProizvodaService } from 'src/app/services/tipProizvoda.service';


@Component({
  selector: 'app-manage-product-type-dialogs',
  templateUrl: './manage-product-type-dialogs.component.html',
  styleUrls: ['./manage-product-type-dialogs.component.scss']
})
export class ManageProductTypeDialogsComponent {
  modalRef?: BsModalRef;

  constructor(private modalService: BsModalService, private proizvodService: ProizvodService, private tipProizvodaService: TipProizvodaService) { }

  openModal(template: TemplateRef<void>) {
    this.modalRef = this.modalService.show(template);
  }

  addProductType(naziv: string) {
    if (naziv !== "" && naziv!== null) {
      let noviProizvod: TipProizvodaPost = {
        naziv: naziv
      }
      this.tipProizvodaService.dodajTipProizvoda(noviProizvod).subscribe(res => {
        console.log(res)
      },
      error => {console.error(error)}
      )
    }
  }

  EditProductType(naziv: string, tipId: string) {
    if (naziv !== "" && naziv!== null) {
      let noviProizvod: TipProizvodaPost = {
        naziv: naziv
      }
      this.tipProizvodaService.izmeniTipProizvoda(noviProizvod, Number(tipId)).subscribe(res => {
        console.log(res)
      },
      error => {console.error(error)}
      )
    }
  }

  deleteProduct(tipId: string) {
    if (tipId !== "" && tipId !== null) {
      this.tipProizvodaService.obrisiTipProizvoda(Number(tipId)).subscribe(res => {
        console.log(res)
      },
      error => {console.error(error)}
      )
    }
  }

  closeModal(){
    this.modalService.hide()
  }
}
