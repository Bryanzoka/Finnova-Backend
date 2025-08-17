
# Finnova

Finnova é uma plataforma de serviços financeiros digitais, desenvolvida com o objetivo de simular uma experiência bancária moderna e segura. FinnovaAPI é uma API RESTful desenvolvida em ASP.NET Core 9 e Entity Framework Core para gerenciamento de contas bancárias.
Inclui autenticação JWT, serviços especializados, envio de e-mail, hashing de senhas e rendimento automático de contas poupança via hosted service.


## Ferramentas utilizadas

**Back-end:**
- C# 13
- .NET/ ASP.NET Core 9
- Entity Framework Core (ORM) 8.0.2
- JWT Bearer (Json Web Token)
- BCrypt.Net-Next (Hash de senhas)
- MailKit (Envio de e-mails) 
- MySQL (Banco de dados)
- Swagger/Postman (para testes)

##  Funcionalidades do Projeto

-  **Cadastro e gerenciamento de clientes**

-  **Criação e gerenciamento de contas** (corrente e poupança) com regras distintas:
    - **Conta corrente:** taxa para saques (conforme regras do serviço)
    - **Conta poupança:** rendimento automático periódico e limite de saques mensais

  - **Operações bancárias seguras:** depósitos, saques e transferências

-  **Autenticação JWT e controle de rules**

- **Envio de e-mail com código de verificação usando MailKit**

-  **Hash seguro de senhas usando BCrypt.Net**

  - **Serviço hospedado (HostedService)** para aplicar rendimento periódico nas contas poupança

-  **Postman** utilizado para testar os endpoints durante o desenvolvimento

- **Swagger UI** Documentação interativa da API


## Arquitetura e Padrões de Design

A arquitetura do projeto segue o padrão em camadas, garantindo a clara separação de responsabilidades. A estrutura do código também adota princípios de Domain-Driven Design (DDD), como um modelo de domínio claro e o uso de abstrações para a persistência de dados.

- **Controllers**:  Responsáveis por receber as requisições HTTP, executar validações de entrada e orquestrar o fluxo de dados

- **Interfaces:** Definem contratos que as classes devem seguir e facilitam a injenção de dependências

- **Services:** Contêm a lógica de negócio da aplicação e serviços de infraestruturas

- **Repositories:** Responsáveis por acessar o banco de dados usando Entity Framework Core

- **Models:** Representam as entidades do domínio

- **DTOs:** São objetos usados para trafegar dados entre a API e o Frontend sem expor diretamente os Models
## Endpoints

A API é organizada por recursos principais, com endpoints intuitivos para as operações de cada domínio.

#### **Clientes**

- `GET api/clients`: Retorna uma lista de todos os clientes cadastrados. (Apenas para administração)

- `GET api/clients/me`: Retorna o perfil do cliente autenticado com base no token JWT

- `POST api/clients/search`: Busca um cliente pelo CPF. O método POST é usado para evitar expor dados sensíveis como o CPF na URL

- `POST api/clients/verify` Valida os dados de um novo cliente (CPF, email, telefone) e envia um código de verificação como parte da primeira etapa do cadastro

- `POST api/clients/register`: Realiza o cadastro de um novo cliente após a verificação de informações e validação do código enviado por email

- `PUT api/clients/update`: Atualiza os dados do cliente autenticado

- `DELETE api/clients`: Deleta a conta do cliente autenticado.

#### **Contas**

- `GET api/accounts`: Retorna uma lista de todas as contas existentes. (Apenas para administração)

- `GET api/accounts/{id}`: Retorna uma conta específica por id

- `GET api/accounts/search/{clientId}`: Lista todas as contas pertencentes a um cliente específico

- `POST api/accounts`: Cria uma nova conta bancária para o cliente autenticado

- `PATCH api/accounts/deposit`: Realiza um depósito de valor em uma conta específica

- `PATCH api/accounts/withdraw`: Realiza um saque de valor em uma conta específica

- `PATCH api/accounts/transfer`: Realiza uma transferência de valor entre duas contas

- `DELETE api/accounts/{id}`: Deleta uma conta bancária específica por id

#### **Autenticação**

- `POST api/auth/login`: Autentica um cliente com CPF e senha, retornando um token JWT temporário

## Instalação e Execução

#### 1. Clone o repositório

```bash
git clone https://github.com/Bryanzoka/Finnova-Backend.git
cd Finnova-Backend/src/FinnovaAPI
```

#### 2. Instalação de dependências

```bash
dotnet restore
```

#### 3. Configuração do Banco de dados

- Copiar o conteúdo do arquivo ``appsettings.example.json`` para um novo arquivo chamado ``appsettings.json``. No terminal, execute:

```bash
cp appsettings.example.json appsettings.json
```

Edite o ``appsettings.json`` configurando com base no seu banco de dados


```
    "ConnectionStrings": {
        "DefaultConnection" : "Server=localhost;database=your_database;user=your_user;password=your_password"
    },
```

#### 4. Chaves Secreta e Configuração de Email


Configure o JWT no ``appsettings.json`` para autenticação:

```bash
    "Jwt" : {
        "Key" : "your_secret_key_here",
        "Issuer" : "your_issuer",
        "Audience": "your_audience"
    },
```

Configure o serviço de emails a partir de EmailSettings para o envio de emails.

⚠️ Aviso: não utilize sua senha principal do email para o campo ``"Password"``, use uma senha de app gerada nas configurações de segurança, após ativar a verificação em duas etapas.

```bash
    "EmailSettings" : {
        "From" : "your_email",
        "Server" : "smtp.gmail.com",
        "Port" : 465,
        "Username" : "your_user_name",
        "Password" : "your_app_password_here"
    }
```

#### 5. Migrations e Execução

Execute as migrations no terminal para criar as tabelas no banco de dados:

```bash
dotnet ef database update
```

Execute o servidor local acessível com o seguinte comando: 

```bash
dotnet run
```

Acesse a documentação Swagger em: http://localhost:5260/swagger

ou teste com o Postman a partir dos endpoints demonstrados.
## Autenticação e Autorização JWT

Ao realizar um login ou cadastro o cliente receberá um token JWT que deve ser enviado nas requisições autenticadas: 

```
Authorization = Bearer your_json_web_token
```
## Autor

#### Bryan Sales

**Github**: https://github.com/Bryanzoka

**Linkedin**: www.linkedin.com/in/bryansalesz