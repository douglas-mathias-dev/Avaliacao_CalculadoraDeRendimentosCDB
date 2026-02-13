import { Component } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { LOCALE_ID } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  styleUrls: ['./app.component.css'],
  imports: [FormsModule, HttpClientModule, CommonModule],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' }
  ]
})

export class AppComponent {
  private apiUrl = 'https://localhost:7024/CalcularRetornoCDB';
  title = 'processoseletivo_avaliacao.client';
  valInicial: number = 0.01;
  prazoMaximo: number = 2;
  retornoBruto?: number;
  retornoLiquido?: number;

  errorMessage: string | null = null;
  constructor(private http: HttpClient) { }

  Calcular(form: NgForm) {
    this.errorMessage = null;

    const params = {
      valorInicial: this.valInicial,
      prazo: this.prazoMaximo
    };

    this.http
      .get<ResultadoCdb>(this.apiUrl, { params })
      .subscribe({
        next: (response) => {
          this.retornoBruto = response.retornoBruto;
          this.retornoLiquido = response.retornoLiquido;
        },
        error: (err) => {
          const detail = err?.error?.detail;
          this.errorMessage = detail ? `${detail}` : 'Erro desconhecido ao chamar a API.';
        }
      });
  }

  dismissError() {
    this.errorMessage = null;
  }
}
interface ResultadoCdb {
  retornoBruto: number;
  retornoLiquido: number;
}
