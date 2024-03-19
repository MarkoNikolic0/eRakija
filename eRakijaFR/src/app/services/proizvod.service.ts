import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ErrorHandler, Injectable } from '@angular/core';
import { Proizvod, ProizvodPost } from '../models/Proizvod';
import { url } from 'environments/environments.dev';
import { BehaviorSubject, Observable, catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProizvodService {

  private proizvodSubject: BehaviorSubject<ProizvodPost[]> = new BehaviorSubject<ProizvodPost[]>([]);
  public proizvodiRes$ = this.proizvodSubject.asObservable();

  constructor(private http: HttpClient) { }

  prikaziProizvode() {
    return this.http.get<Proizvod[]>(`${url}/Proizvod/PrikaziProizvode`)
  }

  dodajProizvod(proizvod: ProizvodPost, tipID: number): Observable<any> {
    console.log(proizvod)
    const httpOptions={
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    }
    return this.http.post<any>(`${url}/Proizvod/DodajProizvod/${tipID}`, JSON.stringify(proizvod), httpOptions)
      .pipe(
        catchError(error => {
          throw 'Error in dodajProizvod method: ' + error;
        })
      );
  }

  izmeniProizvod(proizvod: ProizvodPost, proizvodID: number): Observable<any> {
    console.log(proizvod)
    const httpOptions={
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    }
    return this.http.put<any>(`${url}/Proizvod/IzmeniProizvod/${proizvodID}`, JSON.stringify(proizvod), httpOptions)
      .pipe(
        catchError(error => {
          throw 'Error in izmeniProizvod method: ' + error;
        })
      );
  }

  obrisiProizvod(proizvodID: number): Observable<any> {
    const httpOptions={
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    }
    return this.http.delete<any>(`${url}/Proizvod/ObrisiProizvod/${proizvodID}`, httpOptions)
      .pipe(
        catchError(error => {
          throw 'Error in obrisiProizvod method: ' + error;
        })
      );
  }
}
