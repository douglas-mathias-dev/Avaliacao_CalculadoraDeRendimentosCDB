import { Component } from '@angular/core';
import { NgForm, FormsModule } from '@angular/forms';
import { InvestimentoService } from '../../services/investimento/investimento.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-investimento',
  templateUrl: './investimento.component.html',
  imports: [FormsModule, CommonModule],
  providers: [
    
  ]
})
export class InvestimentoComponent {
  
  title = 'processoseletivo_avaliacao.client';
  valInicial: number = 0.01;
  prazoMaximo: number = 2;
  retornoBruto?: number;
  retornoLiquido?: number;

  errorMessage: string | null = null;
  constructor(private readonly investimentoService: InvestimentoService) { }

  Calcular(form: NgForm) {
    this.errorMessage = null;
    this.investimentoService.getRetornoCDB(this.valInicial, this.prazoMaximo).subscribe({
      next: (response) => {
        this.retornoBruto = response.retornoBruto;
        this.retornoLiquido = response.retornoLiquido;
      },
      error: (err) => {
        this.errorMessage = err?.error?.detail ? `${err?.error?.detail}` : 'Erro desconhecido ao chamar a API.';
      }
    });
  }

  dismissError() {
    this.errorMessage = null;
  }
}
