# Feedback - Avaliação Geral

## Front End
### Navegação
  * Pontos positivos:
    - Possui views e rotas definidas usando ASP.NET Core MVC
    - Implementação com Razor Pages/Views
 
### Design
    - Será avaliado na entrega final
 
### Funcionalidade
  * Pontos positivos:
    - CRUD completo para produtos com suporte a imagens
    - Serviço de validação e tratamento de imagens implementado
    - Serviço de notificação para feedback ao usuário
    - CRUD para vendedores e categorias de produto

  * Pontos negativos:
    - API RESTful ainda em desenvolvimento
    - Autenticação e autorização ainda não implementadas
 
## Back End
### Arquitetura
  * Pontos positivos:
    - Arquitetura em camadas bem definida (UI.Mvc, Api, Business e Data)
    - Implementação de repositórios para acesso centralizado aos dados
    - Serviços de validação e notificação bem estruturados

  * Pontos negativos:
    - Confusão de responsabilidades:
        - Camada Data possui serviços de negócios
        - Camada Data possui tratamento de imagem (deveria ser responsabilidade da aplicação)
 
### Funcionalidade
  * Pontos positivos:
    - Implementação de repositórios e serviços
    - Validação de dados através de serviços
    - Tratamento de imagens com validação e redimensionamento
    - Suporte a múltiplos bancos de dados (SQL Server e SQLite)

  * Pontos negativos:
    - Serviços na camada errada
    - API RESTful ainda não finalizada
    - Autenticação JWT pendente de implementação
    - Não existe relação entre usuário da aplicação e vendedor
 
### Modelagem
  * Pontos positivos:
    - Modelagem de dados bem estruturada
    - Uso apropriado do Entity Framework Core
    - Implementação de repositórios para acesso aos dados

  * Pontos negativos:
    - Nenhum ponto negativo significativo identificado na modelagem
 
## Projeto
### Organização
  * Pontos positivos:
    - Projeto bem organizado com pasta src
    - Solução (.sln) na raiz do projeto
    - Separação clara em projetos: UI.Mvc, Api, Business e Data
    - Documentação organizada em pasta docs
    - Imagens organizadas em pasta imgs

  * Pontos negativos:
    - Pasta "scr" com grafia incorreta (deveria ser "src")
 
### Documentação
  * Pontos positivos:
    - README.md muito bem detalhado
    - Documentação clara das funcionalidades implementadas e em desenvolvimento
    - Instruções detalhadas de instalação e configuração
    - Suporte planejado para Swagger na API

  * Pontos negativos:
    - Documentação da API ainda não disponível (em desenvolvimento)
 
### Instalação
  * Pontos positivos:
    - Suporte a múltiplos bancos (SQL Server e SQLite)
    - SQLite configurado para ambiente de desenvolvimento
    - Seed de dados automático
    - Migrations configuradas

  * Pontos negativos:
    - Algumas funcionalidades importantes ainda em desenvolvimento podem impactar a experiência inicial