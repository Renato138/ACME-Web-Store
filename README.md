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

## **7. Instruções de Configuração**

1. **Clone o Repositório:**
   - `git clone https://github.com/seu-usuario/nome-do-repositorio.git`
   - `cd nome-do-repositorio`

2. **Configuração do Banco de Dados:**
   - No arquivo `appsettings.json`, configure a string de conexão do SQL Server e do SQLite.
   - Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos.
   - Quando executando em modo de Desenvolvimento o sistema se conectará com o banco SQLite, não sendo necessário uma instância do SQL Server.

3. **Executar a Aplicação MVC:**
   - `cd src/Acme.Store.UI.Mvc/`
   - `dotnet run`
   - Acesse a aplicação em: https://localhost:7049/

4. **Executar a API:**
   - `cd src/Acme.Store.Api/`
   - `dotnet run`
   - Acesse a documentação da API em: https://localhost:7268/swagger/

## **8. Primeira execução**
Em caso de primeira execução em modo desenvolvimento, delete a pasta Database contida tanto no projeto de interface de usuário (Acme.Store.UI.Mvc), quanto do projeto da API. Em modo de desenvolvimento estas base são distintas e contida em cada projeto.

### **Passos para Execução**

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

Voce pode utilizar qualquer dos usuário acima para logar na aplicação.
  
- **JWT para API:** As chaves de configuração do JWT estão no `appsettings.json`.
- **Migrações do Banco de Dados:** As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## **9. Documentação da API**

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

http://localhost:5001/swagger

## **10. Funcionalidades da aplicação**

As funcionalidades da aplicação são basicamente as mesmas, tanto para a API, quanto para a interface MVC:

1. **Produtos:**
   - A controller de Produtos está acessível apenas para usuários autenticados, entranto a lista de todos os produtos e lista por categoria é acessível para qualquer pessoa, mesmo que não esteja logada.
   - Usuários autenticados podem cadastror novos produtos, e o produto será atribuido ao usuário/vendedor que realizou o cadastro.
   - A alteração e exclusão do produto só é possível de ser feito pelo usuário/vendedor que realizou o cadastro do mesmo.
   - No entanto o usuário Administrador pode cadastrar novos produtos para qualquer usuário/vendedor, bem como fazer a alteração e exclusão de qualquer produto.
   - Não são permitidos nomes duplicados para os produtos, bem como estoque ou preço negativo.

2. **Categorias:**
   - A controller de Produtos está acessível apenas para usuários autenticados.
   - A inclusão, alteração e/ou exclusão das categorias pode ser feita por qualquer usuário/vendedor altenticado.
   - A exclusão, no entanto, só pode ser feita se não houver produtos associados à categoria.
   - Não são permitidos nomes duplicados para as categorias.

3. **Vendedores/Usuários:**
   - A controller de Vendedores está acessível apenas para usuários que possua a regra Admin.
   - A inclusão/registro de um novo vendedor/usuário pode ser feito por qualquer pessoa.
   - A alteração e exclusão dos vendedores/usuários só pode ser feita pelo usuário que possua a regra Admin.
   - A troca da senha está disponível após o usuário estiver logado no menu Setting/Sua conta e pode ser feita por qualquer usuário.
   - Não é permitido criar usuários administradores, este é criado automaticamente pelo sistema.

4. **Login:**
   - O login na API é feito pela controller Auth através do método POST: /Login. Deve ser fornecido o email e a senha do usuário. Se o login for bem sucedico será devolvida o token JWT para o que seja usada nas chamadas da API que precisam de autenticação.
   - O login na aplicação interface MVC é feito através do menu Login. O usuário deve fornecer seu email e senha e, caso queira acessar diretamente sem necessidade de fazer login na próxima vez que utilizar a aplicação, deve selecionar a opção "Lembrar-me?" na tela de Login.
   - Obs.: Apesar de na tela de login aparecer a opção "Esqueceu sua senha?", esta não está ativa, pois não foi pré-requisito funcional. Mas poderá está dsponível numa próxima versão.

## **11. Avaliação**

- Este projeto é parte de um curso acadêmico e não aceita contribuições externas. 
- Para feedbacks ou dúvidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
