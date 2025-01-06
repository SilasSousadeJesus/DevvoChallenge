# DevvoChallenge

# Documentação de Execução do Aplicativo

## Requisitos

Antes de rodar o aplicativo, certifique-se de que as seguintes ferramentas estão instaladas em sua máquina:

- **SQL Server**: O aplicativo exige o SQL Server para rodar corretamente. Caso não tenha o SQL Server instalado, você pode baixá-lo [aqui](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads).
- **Visual Studio 2022**: A aplicação foi desenvolvida utilizando o Visual Studio 2022. Baixe e instale a versão mais recente [aqui](https://visualstudio.microsoft.com/pt-br/).

## Passo a Passo para Rodar a Aplicação

1. **Configurar Credenciais do Banco de Dados**
   Antes de rodar a aplicação, é necessário configurar suas credenciais de banco de dados. Para isso:
   - Abra o arquivo `appsettings.json` localizado na raiz do projeto.
   - Encontre a seção de configuração do banco de dados, que geralmente se parece com isso:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=NomeDoBanco;User Id=usuario;Password=senha;"
     }
     ```
   - Substitua `localhost`, `NomeDoBanco`, `usuario` e `senha` pelos dados de conexão corretos do seu banco de dados SQL Server.

2. **Verificar o Projeto de Inicialização**
   Abra o Visual Studio 2022 e certifique-se de que o projeto de inicialização está configurado corretamente. O projeto de inicialização correto é o `Devvo.RazoPage`.

   Para garantir que o projeto de inicialização esteja correto:
   - Clique com o botão direito sobre o projeto `Devvo.RazoPage` no **Solution Explorer**.
   - Selecione a opção **Set as Startup Project**.

3. **Rodar a Aplicação**
   Após configurar as credenciais do banco de dados e selecionar o projeto de inicialização correto, basta pressionar **Ctrl + F5** ou clicar em **Iniciar sem depuração** no Visual Studio para rodar a aplicação.

A aplicação será inicializada, e você poderá acessar as funcionalidades desenvolvidas.

