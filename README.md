# 📌 Sprint03 + Sprint04 - API de Saúde Bucal com Visão Computacional 🦷

## 👥 Integrantes do Grupo

* **Murillo Ferreira Ramos** - RM553315
* **Pedro Luiz Prado** - RM553874
* **William Kenzo Hayashi** - RM552659

## 🔄 Atualização Importante

Este projeto foi **atualizado na Sprint04** para atender aos novos requisitos propostos, incluindo:

* Middleware para tratamento global de exceções.
* Middleware de idempotência para evitar repetição de requisições.
* Rate Limiting com AspNetCoreRateLimit.
* Testes automatizados com xUnit e Moq.

## 📖 Visão Geral do Projeto

O objetivo central do projeto é desenvolver uma API em .NET para **redução de sinistros odontológicos** por meio de análise de dados e visão computacional. Além disso, foi integrada uma API externa da CDC (Centers for Disease Control and Prevention) para acesso a dados de saúde bucal de adultos nos EUA.

## 💡 Arquitetura Escolhida

* Arquitetura **monolítica** com divisão em camadas:

  * `Domain`: Entidades de negócio.
  * `Infrastructure`: Conexão com banco Oracle e repositórios.
  * `Presentation`: Controllers da API.
  * `Middleware`: Tratamento de erros e idempotência.

## 📊 CDC Open Data API

A API pública integrada é da **CDC Open Data**:

* Dataset: Saúde bucal de adultos nos Estados Unidos.
* Sistema de origem: BRFSS (Behavioral Risk Factor Surveillance System).
* Período dos dados: **2016 até 2020**.

### Informacoes fornecidas:

* Ano da coleta (`year`)
* Faixa etária (`category`)
* Indicador de saúde bucal (`indicator`)
* Valor percentual (`data_value`)
* Tamanho da amostra (`samplesize`)
* Quebra por: Raça, sexo, região

## ✅ Funcionalidades Implementadas

### 🧑 CRUD de Usuários:

* Criação, leitura, atualização e exclusão com Oracle + EF Core.

### 🔍 Integração com CDC API:

* Endpoint para listar dados dentários.
* Filtros por ano e faixa etária.
* Alertas para valores altos.
* Recomendador de frequência ao dentista baseado na idade.

### 🛡️ Middleware:

* `ExceptionMiddleware`: Trata erros inesperados e retorna JSON.
* `IdempotencyMiddleware`: Garante que requisições POST repetidas não sejam processadas mais de uma vez.

### 🚦 Rate Limiting:

* Limita a 5 requisições por minuto por IP.
* Implementado com `AspNetCoreRateLimit`.

### 🧪 Testes Automatizados:

* Testes unitários com xUnit e Moq para Controller e Service.

## 🚀 Como Executar a API

### Requisitos:

* .NET 8
* Banco de dados Oracle
* Visual Studio ou VS Code

### Banco de Dados:

Configure o `appsettings.json`:

```json
"ConnectionStrings": {
  "OracleConnection": "Data Source=oracle.fiap.com.br:1521/orcl; User ID=SEU_ID; Password=SUA_SENHA;"
}
```

Execute:

```bash
dotnet ef migrations add InicialNovaEstrutura
dotnet ef database update
dotnet run
```

## 🔄 Endpoints Importantes

### CDC

* `GET /api/cdc/dados-dentais`: Lista dados gerais
* `GET /api/cdc/comparar?year=2020&category=Adult`: Filtro
* `GET /api/cdc/alertas`: Alerta de valor alto
* `GET /api/cdc/recomendacao-dentista?idade=35`: Sugestão de frequência ao dentista

### NomeUsuario

* `POST /api/nomeusuarios`
* `GET /api/nomeusuarios`
* `GET /api/nomeusuarios/{id}`
* `PUT /api/nomeusuarios/{id}`
* `DELETE /api/nomeusuarios/{id}`

## 🧠 Boas Práticas Aplicadas

* Validação com DataAnnotations
* Padrão Repository
* Tratamento global de erros
* Idempotência
* Rate Limiting
* Documentação com Swagger
* Testes automatizados

## 📄 Entrega do Projeto

* Estrutura em camadas com organização limpa
* Swagger funcional
* Banco Oracle ativo
* Testes passando
* Middleware aplicado
* Projeto pronto para produção

---

Projeto atualizado com sucesso na Sprint04 com foco em robustez, segurança e manutenção da API! ✨
