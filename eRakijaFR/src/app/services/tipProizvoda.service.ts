import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError } from 'rxjs';
import { ProizvodPost } from '../models/Proizvod';
import { TipProizvoda, TipProizvodaPost } from '../models/TipProizvoda';
import { url } from 'environments/environments.dev';

@Injectable({
  providedIn: 'root'
})
export class TipProizvodaService {

  private proizvodSubject: BehaviorSubject<TipProizvodaPost[]> = new BehaviorSubject<TipProizvodaPost[]>([]);
  public proizvodiRes$ = this.proizvodSubject.asObservable();

  constructor(private http: HttpClient) { }

  prikaziTipoveProizvoda() {
    return this.http.get<TipProizvoda[]>(`${url}/TipProizvoda/PrikaziTipoveProizvoda`)
  }

  dodajTipProizvoda(tp: TipProizvodaPost): Observable<any> {
    console.log(tp.naziv)
    const httpOptions={
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    }
    return this.http.post<TipProizvodaPost>(`${url}/TipProizvoda/DodajTipoveProizvoda/`, JSON.stringify(tp), httpOptions)
      .pipe(
        catchError(error => {
          throw 'Error in dodajTipProizvoda method: ' + error;
        })
      );
  }

  izmeniTipProizvoda(tp: TipProizvodaPost, tipID:number): Observable<any> {
    console.log(tp.naziv)
    const httpOptions={
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    }
    return this.http.put<TipProizvodaPost>(`${url}/TipProizvoda/IzmeniTipProizvoda/${tipID}`, JSON.stringify(tp), httpOptions)
      .pipe(
        catchError(error => {
          throw 'Error in dodajTipProizvoda method: ' + error;
        })
      );
  }

  obrisiTipProizvoda(tipID: number): Observable<any> {
    const httpOptions={
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    }
    return this.http.delete<any>(`${url}/TipProizvoda/ObrisiTipProizvoda/${tipID}`, httpOptions)
      .pipe(
        catchError(error => {
          throw 'Error in obrisiTipProizvoda method: ' + error;
        })
      );
  }
}
