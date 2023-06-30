# Engenheiro de Software .NET Pleno

Neste teste serão avaliados seus conhecimentos e a metodologia aplicada no desenvolvimento de aplicação .NET.

## O Desafio

O objetivo é criar um sistema de que permita o cadastro de usuários, login, logout, listagem de produtos, compra de produtos e cadastro de empresas e produtos.
## Requisitos - Backend
O backend deve fornecer uma API REST para suportar as funcionalidades do sistema de e-commerce. Os requisitos do backend são os seguintes:

- Cadastro de usuário: implementar endpoints para permitir o cadastro de novos usuários no sistema.
- Cadastro de empresa: implementar endpoints para permitir o cadastro de novas empresas no sistema.
- Login: implementar endpoints para autenticar os empresas no sistema e fornecer tokens de acesso.
- Logout: implementar endpoints para invalidar os tokens de acesso e realizar o logout dos empresas.
- Listagem de produtos: implementar endpoints para listar os produtos disponíveis no sistema:
    - Quando usuário, retornar todos os produtos;
    - Quando empresa, retornar somente os produtos da empresa;
- Cadastro de produtos: implementar endpoints para permitir o cadastro de novos produtos pelas empresas.
- Autenticação: implementar um mecanismo de autenticação seguro para proteger os endpoints sensíveis.

## [OPCIONAL] Requisitos - Frontend
O frontend deve ser responsável por fornecer uma interface para os empresas e usuários interagirem com o sistema de e-commerce. Os requisitos do frontend são os seguintes:

- Para Usuários
    - Cadastro: permitir que um novo usuário se cadastre no sistema, fornecendo informações como nome, email e senha.
    - Login: permitir que os usuários façam login no sistema utilizando suas credenciais (email e senha).
    - Logout: fornecer uma opção para que os usuários possam fazer logout do sistema.
    - Listagem de produtos: exibir uma lista de produtos disponíveis para compra.
    - Compra de produtos: permitir que os usuários selecionem e comprem produtos.    

- Para Empresas
    - Cadastro: permitir que um nova empresa se cadastre no sistema, fornecendo informações como nome, email e senha.
    - Login: permitir que os usuários façam login no sistema utilizando suas credenciais (email e senha).
    - Logout: fornecer uma opção para que os usuários possam fazer logout do sistema.
    - Listagem de produtos: exibir uma lista de produtos disponíveis para compra.
    - Cadastro de produtos: permitir que as empresas cadastrem os produtos.

## Especificações
- A utilização de qualquer biblioteca é permitida, desde que justificada posteriormente.
- O backend sistema deve ser desenvolvido utilizando a linguagem C# e o framework .NET 6 ou 7.
- O frontend pode ser desenvolvido em qualquer tecnologia, seja em tecnologia .NET (MVC, Razor, Blazor) ou javacript (VueJS, Angular, ReactJS, etc.)
- O banco de dados pode ser escolhido de acordo com a sua preferência, seja SQL Server, MySQL, PostgreSQL, MongoDB, etc.

## Extra
- Docker / Docker Compose: fornecer uma configuração do Docker para facilitar o ambiente de desenvolvimento e implantação do sistema.
- Cache: implementar um mecanismo de cache para otimizar o desempenho do sistema, como o cache de consulta de produtos.
- Processo de deploy: configurar o processo de deploy do código em serviços gratuitos da Azure, AWS, Heroku ou outros provedores de sua escolha.

## Observações
- Não se preocupe tanto com o Frontend, o foco é o Backend. Mas se seu foco é ser fullstack, você pode explorar isso desenvolvendo o front-end solicitado e vai ser um grande diferencial.
- Aplique os seus conhecimentos e boas práticas de OOP, DDD, SOLID e Clean Code.
- Não existe certo ou errado, o nosso objetivo é entender como você pensa e como você resolve problemas.

## Entrega
- Ao finalizar o desenvolvimento, submeta um pull request para este repositório com o seu código.
- Inclua um arquivo README.md com as instruções para executar o seu código.
- [OPCIONAL] Inclua um arquivo COMMENTS.md com suas considerações sobre o desafio, o que você faria diferente ou como poderíamos melhorar o desafio.
