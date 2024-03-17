import { TipProizvoda } from "./TipProizvoda"

export interface Proizvod{
    id: number
    naziv: string
    cena: number
    slika: string
    opis: string
    kolicina: number
    tip: TipProizvoda
}

export interface ProizvodPost{
    naziv: string
    cena: number
    slika: string
    opis: string
    kolicina: number
}