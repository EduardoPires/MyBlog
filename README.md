# **MyBlog - Aplica��o de Blog Simples com MVC e API RESTful**

## **1. Apresenta��o**

Bem-vindo ao reposit�rio do projeto **MyBlog**. Este projeto � uma entrega do MBA DevXpert Full Stack .NET e � referente ao m�dulo **Introdu��o ao Desenvolvimento ASP.NET Core**.
O objetivo principal desenvolver uma aplica��o de blog que permite aos usu�rios criar, editar, visualizar e excluir posts e coment�rios, tanto atrav�s de uma interface web utilizando MVC quanto atrav�s de uma API RESTful.
Descreva livremente mais detalhes do seu projeto aqui.

### **Autor(es)**
- Jonatas Cruz


## **2. Proposta do Projeto**

O projeto consiste em:

- **Aplica��o MVC:** Interface web para intera��o com o blog.
- **API RESTful:** Exposi��o dos recursos do blog para integra��o com outras aplica��es ou desenvolvimento de front-ends alternativos.
- **Autentica��o e Autoriza��o:** Implementa��o de controle de acesso, diferenciando administradores e usu�rios comuns.
- **Acesso a Dados:** Implementa��o de acesso ao banco de dados atrav�s de ORM.

## **3. Tecnologias Utilizadas**

- **Linguagem de Programa��o:** C#
- **Frameworks:**
  - ASP.NET Core MVC
  - ASP.NET Core Web API
  - Entity Framework Core
- **Banco de Dados:** SQL Server
- **Autentica��o e Autoriza��o:**
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autentica��o na API
- **Front-end:**
  - Razor Pages/Views
  - HTML/CSS para estiliza��o b�sica
- **Documenta��o da API:** Swagger

## **4. Estrutura do Projeto**

A estrutura do projeto � organizada da seguinte forma:

- src/
  - MyBlog.Application: projeto de aplica��o contendo regras de neg�cio.
  - MyBlog.Domain: camada central contendo as entidades, modelos de dom�nio da aplica��o e interfaces.
  - MyBlog.Infra.Data: projeto de infraestrutura e persist�ncia de dados usando EF Core e SQL Server.
  - MyBlog.Infra.Identity: projeto contendo entidades relacionadas ao ASPNET Core Identity.
  - MyBlog.IoC: configura��o de inje��o de depend�ncia da aplica��o.
  - MyBlog.Web.Mvc: interface web da aplica��o, utilizando o padr�o MVC.
  - MyBlog.Web.Api: API RESTful da aplica��o.
	
- README.md: Arquivo de Documenta��o do Projeto
- FEEDBACK.md: Arquivo para Consolida��o dos Feedbacks
- .gitignore: Arquivo de Ignora��o do Git
- .gitattributes: Atributos do Git
- .editorconfig: Prefer�ncias de Estilo de C�digo

## **5. Funcionalidades Implementadas**

- **CRUD para Posts e Coment�rios:** Permite criar, editar, visualizar e excluir posts e coment�rios.
- **Autentica��o e Autoriza��o:** Diferencia��o entre usu�rios comuns e administradores.
- **API RESTful:** Exposi��o de endpoints para opera��es CRUD via API.
- **Documenta��o da API:** Documenta��o autom�tica dos endpoints da API utilizando Swagger.

## **6. Como Executar o Projeto**

### **Pr�-requisitos**

- .NET SDK 8.0 ou superior
- SQL Server
- Visual Studio 2022 ou superior (ou qualquer IDE de sua prefer�ncia)
- Git

### **Passos para Execu��o**

1. **Clone o Reposit�rio:**
   - `git clone https://github.com/jonataspc/MyBlog.git`
   - `cd MyBlog`

2. **Configura��o do Banco de Dados:**
   - No arquivo `\src\MyBlog.Web.Mvc\appsettings.json`, configure a string de conex�o do SQL Server.
   - Rode o projeto para que a configura��o do Seed crie o banco e popule com os dados b�sicos

3. **Executar a Aplica��o MVC:**
   - `cd src\MyBlog.Web.Mvc`
   - `dotnet run`
   - Acesse a aplica��o em: https://localhost:7160

4. **Executar a API:**
   - `cd src\MyBlog.Web.Api`
   - `dotnet run`
   - Acesse a documenta��o da API em: https://localhost:7161/swagger <!-- TODO: Porta da API -->

## **7. Instru��es de Configura��o**

- **JWT para API:** As chaves de configura��o do JWT est�o no `\src\MyBlog.Web.Mvc\appsettings.json`.
- **Migra��es do Banco de Dados:** As migra��es s�o gerenciadas pelo Entity Framework Core. N�o � necess�rio aplicar devido a configura��o do Seed de dados.

## **8. Documenta��o da API**

A documenta��o da API est� dispon�vel atrav�s do Swagger. Ap�s iniciar a API, acesse a documenta��o em:

https://localhost:7161/swagger <!-- TODO: Porta da API -->

## **9. Avalia��o**

- Este projeto � parte de um curso acad�mico e n�o aceita contribui��es externas. 
- Para feedbacks ou d�vidas utilize o recurso de Issues
- O arquivo `FEEDBACK.md` � um resumo das avalia��es do instrutor e dever� ser modificado apenas por ele.