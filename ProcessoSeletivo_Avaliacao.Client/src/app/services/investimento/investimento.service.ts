import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RetornoCDB } from '../../models/retorno-cdb';

@Injectable({
  providedIn: 'root'
})
export class InvestimentoService {
  private readonly apiUrl = 'https://localhost:7024/CalcularRetornoCDB';
  constructor(private readonly http: HttpClient) { }
  public errorMessage?: string;

  getRetornoCDB(valorInicial: number, prazo: number) {

    const params = {
      valorInicial: valorInicial,
      prazo: prazo
    };

    return this.http.get<RetornoCDB>(this.apiUrl, { params });;
  }
}
