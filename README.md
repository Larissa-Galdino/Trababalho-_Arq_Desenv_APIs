# 🏢 Api Empresas e Funcionários

Este projeto é uma Web API desenvolvida em **ASP.NET Core** para o gerenciamento de empresas e funcionarios, utilizando **Entity Framework Core** com suporte a **MySQL** via Docker.

## 🚀 Como Executar o Projeto

1.  **Pré-requisitos:** Possuir o Docker instalado desktop
2.  **Execução via Docker:** Na pasta raiz do projeto, onde está o arquivo `docker-compose.yml`, execute:
    ```bash
    docker-compose up -d --build
    ```
3.  **Acesso:** A API utiliza o **Swagger UI** para documentação. Com o Docker, acesse: `http://localhost:5101/swagger`.
5.  **Acesso Banco de Dados pelo terminal** Senha : root
    ```bash
      docker exec -it mysql_db mysql -u root
      USE trabalhoapi;
      SHOW TABLES;
      SELECT * FROM Empresas
    ```
## 🛠 Funcionalidades e Endpoints

A API segue o padrão REST e permite operações de CRUD nas entidades `Empresa` e `Funcionario`:

* **GET /api/Empresa** e **/api/Funcionario**: Lista todos os registros cadastrados.
* **GET /api/Empresa/{id}**: Busca um registro específico pelo ID.
* **POST /api/Empresa** e **/api/Funcionario**: Permite cadastro flexível.
    * **Envio Único:** Envie um objeto JSON `{...}`.
    * **Envio em Lote:** Envie uma lista JSON `[{...}, {...}]`.
* **PUT /api/{Controller}/{id}**: Atualiza os dados de um registro existente.
* **DELETE /api/{Controller}/{id}**: Remove um registro (com exclusão em cascata de funcionários ao deletar uma empresa).

## ⚠️ Tratamento de Erros e Validação

O projeto implementa validações rigorosas e tratamento de exceções conforme o enunciado:
- **Data Annotations:** Valida campos obrigatórios, formato de CNPJ (Regex) e valores salariais positivos diretamente nas Models.
- **Tratamento de Erros:** Operações críticas são protegidas com blocos `try-catch` para retornar mensagens amigáveis e detalhes técnicos (InnerException), evitando o erro 500 genérico.
- **Resiliência:** O sistema aguarda 10 segundos para o banco de dados inicializar no Docker antes de realizar as migrações automáticas.

## 📂 Estrutura do Projeto
- `/Controllers`: Gerenciamento dos endpoints e rotas da API.
- `/Services`: Camada de lógica de negócio e regras de persistência.
- `/DTOs`: Objetos de transferência de dados para entradas seguras.
- `/Models`: Entidades do banco de dados com regras de validação.
- `/Data`: Contexto do banco de dados (AppDbContext) e configurações de relacionamento.
- `Dockerfile` e `docker-compose.yml`: Configurações de containerização e ambiente.

---
**Estudante:** [LARISSA GALDINO]  
**RU:** [4845990]

## 🛠 Lotes para consulta Empresa 
```json

[
  {
    "nome": "CineMundo Produções",
    "cnpj": "10203040000199",
    "setor": "Entretenimento",
    "endereco": "Av. das Artes, 50 - RJ",
    "dataFundacao": "2015-08-20T00:00:00"
  },
  {
    "nome": "Som Livre Estúdios",
    "cnpj": "20304050000188",
    "setor": "Música",
    "endereco": "Rua da Harmonia, 12 - MG",
    "dataFundacao": "2010-03-12T00:00:00"
  },
  {
    "nome": "GameOn Software",
    "cnpj": "30405060000177",
    "setor": "Jogos Digitais",
    "endereco": "Parque Tecnológico, Sala 4 - SC",
    "dataFundacao": "2022-01-10T00:00:00"
  },
  {
    "nome": "Agência Click Mídia",
    "cnpj": "40506070000166",
    "setor": "Publicidade",
    "endereco": "Av. Paulista, 1500 - SP",
    "dataFundacao": "2019-11-30T00:00:00"
  },
  {
    "nome": "Portal News 24h",
    "cnpj": "50607080000155",
    "setor": "Jornalismo",
    "endereco": "Esplanada Sul, Bloco C - DF",
    "dataFundacao": "2012-05-22T00:00:00"
  },
  {
    "nome": "Teatro Luz do Sol",
    "cnpj": "60708090000144",
    "setor": "Cultura",
    "endereco": "Rua das Flores, 88 - PR",
    "dataFundacao": "1998-07-04T00:00:00"
  },
  {
    "nome": "StreamBox Filmes",
    "cnpj": "70809010000133",
    "setor": "Streaming",
    "endereco": "Edf. Digital, andar 10 - SP",
    "dataFundacao": "2021-02-14T00:00:00"
  },
  {
    "nome": "Rádio FM Total",
    "cnpj": "80901020000122",
    "setor": "Comunicação",
    "endereco": "Torre Alta, s/n - ES",
    "dataFundacao": "2005-09-18T00:00:00"
  },
  {
    "nome": "Editora Paginas",
    "cnpj": "90102030000111",
    "setor": "Literatura",
    "endereco": "Largo do Paço, 05 - BA",
    "dataFundacao": "2014-12-01T00:00:00"
  },
  {
    "nome": "Eventos VIP Brasil",
    "cnpj": "01203040000100",
    "setor": "Eventos",
    "endereco": "Rua do Ouro, 777 - GO",
    "dataFundacao": "2017-04-10T00:00:00"
  }
]
```
## 🛠 Lotes para consulta Funcionario

```json
[
  {
    "nome": "Ricardo Almeida",
    "cargo": "Mestre de Obras",
    "departamento": "Construção",
    "salario": 4500.00,
    "dataAdmissao": "2023-05-10T08:00:00",
    "empresaId": 1
  },
  {
    "nome": "Fernanda Souza",
    "cargo": "Estilista Senior",
    "departamento": "Criação",
    "salario": 7200.50,
    "dataAdmissao": "2022-03-15T09:00:00",
    "empresaId": 1
  },
  {
    "nome": "Dr. João Mendes",
    "cargo": "Dentista",
    "departamento": "Clínico",
    "salario": 12500.00,
    "dataAdmissao": "2021-10-20T08:00:00",
    "empresaId": 10
  },
  {
    "nome": "Marcos Oliveira",
    "cargo": "Padeiro",
    "departamento": "Produção",
    "salario": 2800.00,
    "dataAdmissao": "2024-01-05T04:00:00",
    "empresaId": 8
  },
  {
    "nome": "Dra. Helena Castro",
    "cargo": "Advogada Civil",
    "departamento": "Jurídico",
    "salario": 9500.00,
    "dataAdmissao": "2020-06-12T09:00:00",
    "empresaId": 5
  }
]
```

