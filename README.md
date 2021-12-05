# CadastroUsuarios
WebAPI para Cadastro de usuários.

Bem vindo !

    Configurando:

Para o funcionamento da WEB Api siga os passos:

1 - Abra o arquivo appsettings.json e em seguida altere a string de conexão em "DefaultConnection" para se conectar com o banco PostgreSQL
2 - Execute o comando "dotnet ef database update" para aplicar as migrations e assim o Entity construa as tabelas no banco de dados.
3 - Execute a aplicação, irá abrir no seu navegador a página do Swagger e você ja estará apto a realizar as chamadas aos endpoints.

----------------------------------------------------------------------------------------------------------------------------------------------------------
    Executando:

"Cadastro/cria-usuario" -> Insira os dados necessários para criar um usuário novo. Nota: o parâmetro "listaDeAcessosId" é opcional, e será utilizado quando for criado um acesso no endpoint "criar-perfil-acesso", quando criado , para se obter o Id do perfil utilize o "listar-perfis" e entao coloque no campo "listaDeAcessosId".

"Cadastro/listar-usuarios" -> Retorna uma lista de todos os usuários do sistema.

"Cadastro/listar-perfis" -> Retorna uma lista de todos os perfis do sistema.

"Cadastro/obter-por-nome" -> Retorna o usuário relativo ao nome informado. Observação: Esse endpoint para funcionar é necessário se autenticar no sistema no endpoint "Login/login" copiar o valor do parâmetro de retorno "access_token" e então clicar no cadeado posicionado no canto direito dos nomes dos endpoint e então digitar "Bearer <access_token>".

"Cadastro/remover-usuario" -> Apaga um usuário do sistema.

"Cadastro/atualizar-usuario" -> É necessário informar o Id do usuário a ser atualizado e em seguida como parâmetros opcionais de atualização o email ou a senha ou os dois.

"Cadastro/associar-perfil-usuario" -> Associa um perfil ao usuario informado.

"Cadastro/{id}" -> Retorna um usuário de acordo com o Id informado.

"Login/login" -> Realiza o login do usuario informando o email e a senha do usuario e retorna a chave de acesso , data e hora de expiração do token de acesso , informações relativas ao login e uma lista de permissões associadas ao usuario informado.

----------------------------------------------------------------------------------------------------------------------------------------------------------

Tecnologias:

.NET Core 5, EntityFramework, PostgreSQL, Dapper

Bibliotecas relevantes:

Microsoft.Identity
SwashBuckle







