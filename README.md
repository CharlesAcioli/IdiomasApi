# API CURSO DE IDIOMAS COM DDD

API REST desenvolvida em **.NET 8** utilizando princípios de **Arquitetura Limpa** e **Domain-Driven Design (DDD)** para gerenciamento de turmas e matrículas de alunos.

## Destaques da Arquitetura Aplicada (Defesa Técnica)

*   **Entidades Ricas (Domain)**: Classes `Aluno` e `Turma` com encapsulamento restrito (`private set`), protegendo o estado do sistema contra alterações externas diretas.
*   **Value Objects (Objetos de Valor)**: Isolamento das regras de validação e formatação complexas em objetos imutáveis (`Cpf`, `Email` validado via expressões regulares e `Endereco`).
*   **Raiz de Agregado (Aggregate Root)**: A entidade `Turma` gerencia e protege a sua própria lista de alunos através do método de comportamento `MatricularAluno()`, aplicando as travas de limite máximo de 5 alunos e CPF duplicado na sala.
*   **Refatoração de Código Limpo**: Modelagem semântica ajustada de `Numero` para `NumeroTurma` refletida com sucesso em todas as 5 camadas da aplicação.
*   **Camada de Infraestrutura Isolada**: Mapeamento fluente (`Fluent API`) com conversores de valor nativos do EF Core para salvar tipos complexos e Enums (`NivelTurma` e `Idioma`) de forma otimizada no banco de dados.

*   ## 🛠️ Tecnologias e Pacotes Utilizados (NuGet)

*   **.NET 8 SDK** (Ambiente de execução e compilação)
*   **Microsoft.EntityFrameworkCore.SqlServer** (Provedor oficial para persistência no SQL Server)
*   **Microsoft.EntityFrameworkCore.Design** (Suporte às ferramentas de Migrations em tempo de design)
*   **Microsoft.EntityFrameworkCore.InMemory** (Utilizado para o ambiente isolado de testes e homologação local)

## Como Rodar o Projeto

1. Certifique-se de ter o **.NET 8 SDK** instalado.
2. Restaure os pacotes e compile o projeto na raiz:
   ```GitBash
   dotnet build
   ```
3. Navegue até a camada de API e execute:
   ```GitBash
   cd IdiomasApi.WebAPI
   dotnet run
   ```
4. O servidor web iniciará em segundo plano. Abra o seu navegador e acesse o painel visual do Swagger utilizando o endereço e a porta **localhost** gerados e exibidos na tela do seu terminal (ex: `/swagger/index.html`).
5. 
## Status dos Testes Unitários

*   **Camada de Testes Estruturada**: O projeto de testes `IdiomasApi.Tests` foi devidamente criado e acoplado à arquitetura da solução. 
*   **Próximos Passos**: O foco principal deste desafio foi a blindagem das regras de negócio no Domínio e a entrega da API funcional com validação no Swagger. A estrutura de testes foi deixada pronta como boa prática de design, permitindo que a cobertura de testes automatizados seja implementada em uma próxima etapa do projeto.
