
# Avaliação - Calculadora de rendimentos de CDB

Projeto para avaliação em processo seletivo executado conforme requisitos propostos.

## Ferramentas e bibliotecas utilizadas

* Visual Studio 2022
* .NET 8.0
* Angular Framework (Versão 19.1.4)
* Nunit
* SonarQube Cloud

## Estrutura do projeto `Avaliacao_CalculadoraDeRendimentosCDB`

```bash
\---Avaliacao_CalculadoraDeRendimentosCDB-master
    |   ProcessoSeletivo_Avaliacao.sln # Solução do projeto, contendo os projetos Client, Server e Tests
    |
    +---ProcessoSeletivo_Avaliacao.Client # Projeto Angular, responsável pela interface do usuário
    |   \---src
    |       \---app
    |           \---components
    |           |   \---investimento
    |           |           investimento.component.html # Template HTML para a interface (UI) de cálculo de rendimentos
    |           |           investimento.component.ts
    |           |
    |           +---models
    |           |       retorno-cdb.ts // Modelo de dados para o retorno da API de calculo de rendimentos
    |           |
    |           \---services
    |               \---investimento
    |                       investimento.service.ts # Serviço Angular para comunicação com a API de cálculo de rendimentos
    |
    +---ProcessoSeletivo_Avaliacao.Server # Projeto ASP.NET Core, responsável pela API de cálculo de rendimentos
    |   \---Controllers
    |   |       InvestimentoController.cs
    |   |
    |   +---Models
    |   |       Investimento.cs
    |   |
    |   \---Services
    |           InvestimentoService.cs # Serviço para cálculo de rendimentos, implementando a lógica de negócio
    |
    \---ProcessoSeletivo_Avaliacao.Tests # Projeto de testes unitários utilizando NUnit
            InvestimentoTest.cs
``` 

## Guia de execução dos projetos via visual studio

1. Clone o repositório para sua máquina local utilizando o comando:

    ```bash
    git clone https://github.com/douglas-mathias-dev/Avaliacao_CalculadoraDeRendimentosCDB.git
    ```

2. Abra e Execute a solução `ProcessoSeletivo_Avaliacao.sln` no Visual Studio 2022 ou compativel a solução.

3. O projeto Client (Angular) estará disponível em [`http://localhost:56524/`](http://localhost:56524) e a API Server (ASP.NET Core) estará em [`https://localhost:7024/`](https://localhost:7024/).

4. Para executar os testes unitários, utilize o Test Explorer do Visual Studio para rodar os testes contidos no projeto `ProcessoSeletivo_Avaliacao.Tests`.

## Guia de execução dos projetos via linha de comando

1. Clone o repositório para sua máquina local utilizando o comando:
    ```bash
    git clone
    ```
2. Execute as instruções de execução para cada projeto conforme descrito nos arquivos README.md específicos de cada projeto:

    * Para o projeto Angular (Client), siga as instruções em [`ProcessoSeletivo_Avaliacao.Client/README.md`](ProcessoSeletivo_Avaliacao.Client/README.md)
    * Para o projeto ASP.NET Core (Server), siga as instruções em [`ProcessoSeletivo_Avaliacao.Server/README.md`](ProcessoSeletivo_Avaliacao.Server/README.md)
    * Para os testes unitários, siga as instruções em [`ProcessoSeletivo_Avaliacao.Tests/README.md`](ProcessoSeletivo_Avaliacao.Tests/README.md)