
# Sistema de Agendamento de Salas - API REST

Este projeto é uma API REST desenvolvida com .NET MVC para gerenciar o agendamento de salas de reunião. Ele fornece endpoints para realizar operações CRUD (Criar, Ler, Atualizar e Excluir) em usuários, salas e agendamentos, além de oferecer autenticação via JWT.

## Funcionalidades

- **Gerenciamento de Salas**: Cadastro, listagem, edição e remoção de salas.
- **Gerenciamento de Usuários**: Cadastro, autenticação, listagem, edição e remoção de usuários.
- **Agendamento de Reuniões**: Criação, visualização, edição e cancelamento de agendamentos.
- **Autenticação JWT**: Autenticação segura para acessar os endpoints protegidos.
- **Documentação da API com Swagger**: Interface interativa para explorar e testar os endpoints.

## Tecnologias Utilizadas

- **Framework**: .NET MVC
- **Banco de Dados**: Entity Framework Core com suporte a SQLite/SQL Server
- **Autenticação**: JWT (JSON Web Token)
- **Documentação**: Swagger/OpenAPI
- **Middleware**: JwtCookieMiddleware
- **Outras**: 
  - Repositórios: `IAuthRepository`, `IClienteRepository`, `IAdministradorRepository`
  - Day.js para manipulação de datas (se necessário)

## Requisitos

- **SDK do .NET**: Versão 6 ou superior
- **Banco de Dados**: SQL Server (ou SQLite para desenvolvimento)
- **Ferramentas**: Visual Studio ou Visual Studio Code

## Configuração do Projeto

1. **Clone o repositório**:
   ```bash
   git clone https://github.com/seu-usuario/sistema-agendamento-salas.git
   cd sistema-agendamento-salas
   ```

2. **Configure o Banco de Dados**:
   - Altere a string de conexão no arquivo `appsettings.json` conforme necessário.
   - Execute as migrações para criar o banco de dados:
     ```bash
     dotnet ef database update
     ```

3. **Execute o Projeto**:
   ```bash
   dotnet run
   ```
   A API estará disponível em `http://localhost:5000` (ou a porta configurada).

4. **Acesse o Swagger**:
   Navegue até `http://localhost:5000/swagger` para acessar a documentação interativa da API.

## Estrutura do Projeto

- **Controllers**: Contém os controladores que gerenciam os endpoints da API.
- **Models**: Define as entidades do sistema, como Usuários, Salas e Agendamentos.
- **ViewModels/DTOs**: Estruturas usadas para comunicação entre cliente e servidor.
- **Repositories**: Implementação do padrão repositório para acesso ao banco de dados.
- **Middleware**: Customizações, como autenticação via JWT.

## Endpoints Principais

### Autenticação
- `POST /auth/login`: Realiza o login e retorna um token JWT.

### Usuários
- `GET /users`: Lista todos os usuários.
- `POST /users`: Cria um novo usuário.
- `PUT /users/{id}`: Atualiza os dados de um usuário.
- `DELETE /users/{id}`: Remove um usuário.

### Salas
- `GET /rooms`: Lista todas as salas.
- `POST /rooms`: Cria uma nova sala.
- `PUT /rooms/{id}`: Atualiza os dados de uma sala.
- `DELETE /rooms/{id}`: Remove uma sala.

### Agendamentos
- `GET /schedules`: Lista todos os agendamentos.
- `POST /schedules`: Cria um novo agendamento.
- `PUT /schedules/{id}`: Atualiza os dados de um agendamento.
- `DELETE /schedules/{id}`: Remove um agendamento.

## Contribuição

1. Faça um fork do repositório.
2. Crie uma branch para sua funcionalidade/feature:
   ```bash
   git checkout -b minha-feature
   ```
3. Faça o commit das suas alterações:
   ```bash
   git commit -m "Adiciona minha nova funcionalidade"
   ```
4. Envie para o repositório remoto:
   ```bash
   git push origin minha-feature
   ```
5. Abra um Pull Request.

## Licença

Este projeto está licenciado sob a Licença MIT. Consulte o arquivo LICENSE para mais informações.

## Contato

Para dúvidas ou sugestões, entre em contato:
- Nome: Eduardo
- Email: eduardo@exemplo.com
