# 🏢 ApiEmpresas - Gestão de Empresas e Funcionários

Este projeto é uma Web API desenvolvida em **ASP.NET Core** para o gerenciamento de empresas e colaboradores, utilizando **Entity Framework Core** com suporte a **MySQL** via Docker.

## 🚀 Como Executar o Projeto

1.  **Pré-requisitos:** Possuir o Docker instalado desktop (recomendado)
2.  **Execução via Docker:** Na pasta raiz do projeto, onde está o arquivo `docker-compose.yml`, execute:
    ```bash
    docker-compose up -d --build
    ```
3.  **Acesso:** A API utiliza o **Swagger UI** para documentação. Com o Docker, acesse: `http://localhost:5101/swagger`.



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
**Estudante:** [SEU NOME]  
**RU:** [SEU RU]

## 🛠 Lotes para consulta Empresa 
```json

[
  {
    "nome": "Empresa de Teste Final",
    "cnpj": "12345678000199",
    "setor": "TI",
    "endereco": "Rua Teste, 123",
    "dataFundacao": "2025-01-01T00:00:00"
  },
  {
    "nome": "Tech Soluções Digitais",
    "cnpj": "12345678000190",
    "setor": "Tecnologia",
    "endereco": "Rua da Inovação, 100 - SP",
    "dataFundacao": "2020-05-15T00:00:00"
  },
  {
    "nome": "Logística Expressa",
    "cnpj": "98765432000188",
    "setor": "Transporte",
    "endereco": "Av. Industrial, 500 - PR",
    "dataFundacao": "2018-10-20T00:00:00"
  }
]
```
## 🛠 Lotes para consulta Funcionario

```json
[
  {
    "nome": "João Silva",
    "cargo": "Analista de Sistemas",
    "departamento": "TI",
    "salario": 4500.00,
    "dataAdmissao": "2025-12-21T00:00:00",
    "empresaId": 1
  },
  {
    "nome": "Maria Oliveira",
    "cargo": "Desenvolvedora Full Stack",
    "departamento": "Desenvolvimento",
    "salario": 6800.50,
    "dataAdmissao": "2025-12-21T00:00:00",
    "empresaId": 1
  },
  {
    "nome": "Pedro Santos",
    "cargo": "Especialista em QA",
    "departamento": "Qualidade",
    "salario": 5200.00,
    "dataAdmissao": "2025-12-20T00:00:00",
    "empresaId": 1
  }
]
```