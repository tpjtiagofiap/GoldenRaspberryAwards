# Golden Raspberry Awards API

Este repositório contém uma API RESTful desenvolvida em **.NET 8** para gerenciar a lista de indicados e vencedores da categoria "Pior Filme" do **Golden Raspberry Awards**. O objetivo principal da API é fornecer informações sobre o produtor com o maior intervalo entre dois prêmios consecutivos e o produtor que obteve dois prêmios no menor intervalo de tempo.

## 🚀 Especificação do Projeto

### Funcionalidades

1. **Ler arquivo CSV e inserir dados em banco de dados**  
   - Ao iniciar a aplicação, os dados do arquivo CSV fornecido são automaticamente lidos e inseridos em uma base de dados em memória.
   
2. **Consulta de intervalo entre prêmios**  
   - Endpoint que retorna o produtor com o maior intervalo entre dois prêmios consecutivos e o produtor que obteve dois prêmios mais rápido.  
   - Exemplo de retorno:
     ```json
     {
       "min": [
         {
           "producer": "Producer 1",
           "interval": 1,
           "previousWin": 2008,
           "followingWin": 2009
         }
       ],
       "max": [
         {
           "producer": "Producer 2",
           "interval": 99,
           "previousWin": 1900,
           "followingWin": 1999
         }
       ]
     }
     ```

### Requisitos Não Funcionais

- **Nível de Maturidade**: A API atende ao **nível 2 de maturidade de Richardson**.  
- **Testes**: Inclui somente **testes de integração**, garantindo que os dados obtidos estão de acordo com os fornecidos.  
- **Banco de Dados**: Base de dados em memória usando **H2**, eliminando a necessidade de instalação externa.  

---

## 🛠️ Tecnologias Utilizadas

- **Linguagem**: C#  
- **Framework**: .NET 8  
- **Banco de Dados**: H2 (em memória)  
- **Testes**: xUnit  

---

## 📋 Instruções de Uso

### 1. Pré-requisitos

- Instale o **.NET 8 SDK**.
- Opcional: Ferramenta de linha de comando para testes `dotnet`.

### 2. Clonando o Repositório

```bash
git clone https://github.com/seu-usuario/seu-repositorio.git
cd seu-repositorio
