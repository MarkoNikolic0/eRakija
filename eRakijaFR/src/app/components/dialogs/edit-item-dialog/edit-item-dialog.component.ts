import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, Inject, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Proizvod, ProizvodPost } from 'src/app/models/Proizvod';
import { TipProizvoda } from 'src/app/models/TipProizvoda';
import { ProizvodService } from 'src/app/services/proizvod.service';

@Component({
  selector: 'app-edit-item-dialog',
  templateUrl: './edit-item-dialog.component.html',
  styleUrls: ['./edit-item-dialog.component.scss']
})
export class EditItemDialogComponent {

  modalRef?: BsModalRef;

  constructor(private modalService: BsModalService, private proizvodService: ProizvodService) { }

  openModal(template: TemplateRef<void>) {
    this.modalRef = this.modalService.show(template);
  }

  addProizvod(naziv: string, cena: string, slika: string, opis: string, kolicina: string, tipId: string) {
    if (naziv !== "" && naziv!== null && cena !== null) {
      let noviProizvod: ProizvodPost = {
        naziv: naziv,
        cena: Number(cena),
        slika: slika,
        opis: opis,
        kolicina: Number(kolicina)
      }
      this.proizvodService.dodajProizvod(noviProizvod, Number(tipId)).subscribe(res => {
        console.log(res)
      },
      error => {console.error(error)}
      )
    }
  }

  EditProizvod(naziv: string, cena: string, slika: string, opis: string, kolicina: string, proizvodId: string) {
    if (naziv !== "" && naziv!== null && cena !== null) {
      let noviProizvod: ProizvodPost = {
        naziv: naziv,
        cena: Number(cena),
        slika: slika,
        opis: opis,
        kolicina: Number(kolicina)
      }
      this.proizvodService.izmeniProizvod(noviProizvod, Number(proizvodId)).subscribe(res => {
        console.log(res)
      },
      error => {console.error(error)}
      )
    }
  }

  deleteProizvod(proizvodId: string) {
    if (proizvodId !== "" && proizvodId !== null) {
      this.proizvodService.obrisiProizvod(Number(proizvodId)).subscribe(res => {
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
