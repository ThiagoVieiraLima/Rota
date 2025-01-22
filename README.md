# Projeto Rota

####################################################
#### Arquitetura do projeto
####################################################

- IDE Visual Studio 2022
- Utilizado o Net Core 3.1
- Padr�o de desenvolvimento DDD
- Entity Framework Sqlite
- Teste unit�rio com o xUnit (Execu��o dos testes feitos na pr�pria IDE VS 2022)
- Documenta��o e Swagger UI
- Valida��o de dados com o FluentValidation
- Padr�o de retorno HttpCode

####################################################
#### Acesso a interface swagger das APIs
####################################################

O Projeto possue uma p�gina Swagger para visualiza��o e teste das APIs

https://localhost:44327/swagger

####################################################
#### Carga Inicial de dados
####################################################

Neste projeto existe uma API chamada (/api/Trecho/CargaInicial) que faz uma carga inicial com uns trechos de viagens pr� definidos.

Id = 1, Origem = "GRU", Destino = "BRC", Valor = 10
Id = 2, Origem = "BRC", Destino = "SCL", Valor = 5 
Id = 3, Origem = "GRU", Destino = "CDG", Valor = 75
Id = 4, Origem = "GRU", Destino = "SCL", Valor = 20
Id = 6, Origem = "ORL", Destino = "CDG", Valor = 5 
Id = 7, Origem = "SCL", Destino = "ORL", Valor = 20
Id = 8, Origem = "CDG", Destino = "ORL", Valor = 13
Id = 9, Origem = "CDG", Destino = "GRU", Valor = 11.25 
Id = 10, Origem = "CDG", Destino = "SCL", Valor = 9.14 

####################################################
#### Defini��o do projeto
####################################################

# Rota de Viagem #
Escolha a rota de viagem mais barata independente da quantidade de conex�es.
Para isso precisamos inserir as rotas.
 
# API
## CRUD de cadastro de ROTAS ##
* Dever� construir um endpoint de CRUD as rotas dispon�veis:
```
Origem: GRU, Destino: BRC, Valor: 10
Origem: BRC, Destino: SCL, Valor: 5
Origem: GRU, Destino: CDG, Valor: 75
Origem: GRU, Destino: SCL, Valor: 20
Origem: GRU, Destino: ORL, Valor: 56
Origem: ORL, Destino: CDG, Valor: 5
Origem: SCL, Destino: ORL, Valor: 20
```
 
## Explicando ## 
Uma viajem de **GRU** para **CDG** existem as seguintes rotas:
 
1. GRU - BRC - SCL - ORL - CDG ao custo de $40
2. GRU - ORL - CDG ao custo de $61
3. GRU - CDG ao custo de $75
4. GRU - SCL - ORL - CDG ao custo de $45
 
O melhor pre�o � da rota **1**, apesar de mais conex�es, seu valor final � menor.
O resultado da consulta deve ser: **GRU - BRC - SCL - ORL - CDG ao custo de $40**.
 
Sendo assim, o endpoint de consulta dever� efetuar o calculo de melhor rota.
 
# API .net core
1- Cadastro: CRUD de Rotas
2- Consulta: Dever� ter 2 campos para consulta de rota: **Origem-Destino** e exibir o resultado da consulta chamando a API
- Interface Rest (Obrigat�rio)
    A interface Rest dever� suportar o CRUD de rotas:
    - Manipula��o de rotas, dados podendo ser persistidos em arquivo, bd local, etc...
    - Consulta de melhor rota entre dois pontos.
	- Documento swagger
	- Testes unit�rios
  Exemplo:
  ```
  Consulte a rota: GRU-CGD
  Resposta: GRU - BRC - SCL - ORL - CDG ao custo de $40
  Consulte a rota: BRC-SCL
  Resposta: BRC - SCL ao custo de $5
  ```
# FRONT-END - Angular - Diferencial
1- Tela para consumir a API (incluir/alterar/excluir)
2- Tela para consultar melhor rota 
*Pode ser tudo em uma tela
 
## Entreg�veis ##
* Envie apenas o c�digo fonte
* Prefer�ncia no github (zipado)
* Priorize/Estruturar sua aplica��o seguindo as boas pr�ticas de desenvolvimento
* Evite o uso de frameworks ou bibliotecas externas � linguagem

