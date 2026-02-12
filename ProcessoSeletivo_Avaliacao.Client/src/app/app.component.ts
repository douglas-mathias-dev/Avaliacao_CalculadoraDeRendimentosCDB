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
  valInicial: number = 100;
  prazoMaximo: number = 12;
  retornoBruto?: number;
  retornoLiquido?: number;

  constructor(private http: HttpClient) { }

  Calcular(form: NgForm) {
    console.log(this.valInicial, this.prazoMaximo);
    var url: string = `${this.apiUrl}?valorInicial=${this.valInicial}&prazo=${this.prazoMaximo}`;

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
          console.error('Erro na API:', err);
        }
      });
  }

}
interface ResultadoCdb {
  retornoBruto: number;
  retornoLiquido: number;
}
