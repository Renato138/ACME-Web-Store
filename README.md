# **ACME Web Store - Aplicação Web Store simples com MVC e API RESTful**

## **1. Apresentação**

Bem-vindo ao repositório do projeto **ACME web Store**. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo **Introdução ao Desenvolvimento ASP.NET Core**.
O objetivo principal desenvolver uma aplicação web básica usando conceitos do Módulo 1 (C#, ASP.NET Core MVC, SQL, EF Core, APIs REST) para gestão simplificada de produtos e categorias em um formato tipo e-commerce marketplace..

### **Autor(es)**
- **José Renato de Oliveira**

## **2. Proposta do Projeto**

O projeto consiste em:

- **Aplicação MVC:** Interface web para interação com a web store.
- **API RESTful:** Exposição dos recursos da web store para integração com outras aplicações ou desenvolvimento de front-ends alternativos.
- **Autenticação e Autorização:** Implementação de controle de acesso, diferenciando administradores e usuários comuns.
- **Acesso a Dados:** Implementação de acesso ao banco de dados através de ORM.

## **3. Tecnologias Utilizadas**

- **Linguagem de Programação:** C#
- **Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Entity Framework Core
- **Banco de Dados:** SQL Server
- **Autenticação e Autorização:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
- **Front-end:**
  - Razor Pages/Views
  - HTML/CSS para estilização básica
- **Documentação da API:** Swagger

## **4. Estrutura do Projeto**

A estrutura do projeto é organizada da seguinte forma:


- src/
  - Acme.Store.UI.Mvc/ - Interface da Aplicação em Modelo MVC
  - Acme.Store.Api/ - API RESTful
  - Acme.Store.Business/ - Modelo de Negócio
  - Acme.Store.Data/ - Modelo de Acesso a Dados e Configuração do EF Core
  - Acme.Store.Abstractions - Interfaces e classes básicas utilizadas pelo modelo de negócio. Estas interfaces e classes foram removidas da biblioteca de Modelo de Negócio (Acme.Store.Business) para facilitar o reaproveitamento de código e uso em outras bibliotecas e projetos.
  - Acme.Store.Auth - Modelo de autorização e autenticação. Possui as classes e DbContext do Identity, bem como todo código relativo compartilhado pela API e pela aplicação MVC.
- README.md - Arquivo de Documentação do Projeto
- FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
- .gitignore - Arquivo de Ignoração do Git

## **5. Funcionalidades**

- **Reposítórios de Dados:** Acesso centralizado aos dados por meio de reposítórios e serviços.
- **Validação de Dados:** Validação dos dados popr meios de serviços, permitindo a integridade dos dados e da regra de negócio.
- **Serviço de Notificação:** Mensagens de validação e regras são enviadas às camadas de Ui e API através do serviço de notificação.
- **Serviço de Validação e Tratamento de Imagens:** Verificação do formato e integridade do arquivo de imagem, redimensionamento da imagem para tamanho máximo padrão, conversão da imagem para string Base64.
  - **CRUD para Entidades do Negócio:** Permite criar, editar, visualizar e excluir produtos (incluindo imagem), vendedores e categprias de produto.

- **Autenticação e Autorização:** O modelo de Autenticação e Autorização está desenvolvido em uma dll a parte para ser compartilhada pela API e pela interace MVC.
- **API RESTful:** Exposição de endpoints para operações CRUD via API.
- **Documentação da API:** Documentação automática dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**

### **Pré-requisitos**

- .NET SDK 9.0 ou superior
- SQL Server / SQLite
- Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
- Git

## **7. Primeira execução**

### **Pré-requisitos**

### **Passos para Execução**

1. **Clone o Repositório:**
   - `git clone https://github.com/seu-usuario/nome-do-repositorio.git`
   - `cd nome-do-repositorio`

2. **Configuração do Banco de Dados:**
   - No arquivo `appsettings.json`, configure a string de conexão do SQL Server e do SQLite.
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos.
   - Quando executando em modo de Desenvolvimento o sistema se conectará com o banco SQLite, não sendo necessário uma instância do SQL Server.

3. **Executar a Aplicação MVC:**
   - `cd src/Blog.Mvc/`
   - `dotnet run`
   - Acesse a aplicação em: https://localhost:7049/

4. **Executar a API:** (em desenvolvimento)
   - `cd src/Blog.Api/`
   - `dotnet run`
   - Acesse a documentação da API em: http://localhost:5001/swagger

## **7. Instruções de Configuração**

Ao ser executado pela primeira vez em mode de desenvolvimento o sistema ira criar a base de dados utilizando o banco de dados SQLite. Nesta execução
ele irá criar:
 - O usuário administrador do sistema, que tem acesso irrestrito a todas as funcionalidades e não está associado com um vendedor.
   - Loging: admin@acme.com
   - Senha: Admin!138
  
 - Dois vendedores/usuários com algumas funcionalidades restritas. 
   - Nome: Papa-Leguas
   - E-mail/Loging: papaleguas@acme.com
   - Senha: PapaLeguas!138
  
   - Nome: Coiote
   - E-mail/Loging: coiote@acme.com
   - Senha: Coiote!138
  
 - Três categorias de produtos. 
  
 - Quatro produtos, dois para o primeiro vendedor e outros dois para o segundo. 
  
- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## **8. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

http://localhost:5001/swagger

## **9. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
