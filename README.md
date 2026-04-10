# 🌱 Verdantis (VITS - Visual Information Tracking System)

## 🎯 Definição do Projeto

### Objetivo do Projeto
O Verdantis é um sistema de rastreabilidade e visualização inteligente desenvolvido para resolver a falta de transparência e certificação sustentável na cadeia produtiva agrícola brasileira. O projeto visa fornecer uma plataforma integrada que conecta produtores, distribuidores e compradores, permitindo o registro, visualização e certificação digital de produtos agrícolas, combatendo os R$ 12 bilhões perdidos anualmente em exportações devido à ausência de rastreabilidade.

### Escopo

#### Escopo do MVP:
* Cadastro de produtores e compradores.
* Registro de lotes agrícolas no Oracle Database com hash de segurança no Hyperledger Fabric.
* Dashboard interativo utilizando Oracle APEX e Oracle Spatial.
* Integração com API de clima para apoio à decisão.
* Protótipo mobile em React Native para consulta de rastreabilidade via QR Code.
* API RESTful completa com endpoints de busca avançada, paginação, ordenação e filtros.
* Interface Web MVC com validações completas e layout responsivo.
* Implementação de HATEOAS para navegação hipermídia na API.

#### Fora do Escopo do MVP:
* Marketplace integrado entre produtores e compradores.
* Dashboards avançados de sustentabilidade (economia de água, emissões de carbono).
* Integração completa com dispositivos IoT para coleta automática de dados.

## 📋 Requisitos Funcionais e Não Funcionais

### Requisitos Funcionais:
* **RF01:** Cadastro de usuários (produtores, distribuidores, compradores).
* **RF02:** Cadastro e consulta de lotes agrícolas.
* **RF03:** Exibição de mapa interativo com Oracle Spatial.
* **RF04:** Geração de relatórios periódicos via APEX e Python.
* **RF05:** Integração com API de clima para dados de previsão e histórico.
* **RF06:** Emissão de QR Code para consulta de rastreabilidade.
* **RF07:** Endpoints de busca avançada com paginação, ordenação e filtros para todas as entidades.
* **RF08:** Interface web responsiva com validações de dados em tempo real.

### Requisitos Não Funcionais:
* **Transparência:** Cadeia produtiva visível ponta a ponta.
* **Performance:** Performance enterprise garantida pelo Oracle Database.
* **Escalabilidade:** Através de arquitetura em camadas e backend robusto.
* **Segurança:** Registros imutáveis via Hyperledger Fabric.
* **Usabilidade:** Acessível para usuários com diferentes níveis de informatização.
* **Disponibilidade:** Hospedagem no Oracle Cloud Free Tier.
* **Manutenibilidade:** Código limpo seguindo padrões SOLID e Clean Architecture.

## 🏗️ Desenho da Arquitetura

<img width="1267" height="839" alt="image" src="https://github.com/user-attachments/assets/f7fadb9e-c1fc-432f-a42b-021d29c2da64" />

### Clean Architecture
O projeto Verdantis segue os princípios da Clean Architecture para garantir separação de responsabilidades, baixo acoplamento e alta coesão entre os componentes do sistema. A arquitetura é organizada em camadas concêntricas, onde as dependências apontam sempre para o centro (domínio), garantindo que as regras de negócio permaneçam independentes de frameworks, UI e infraestrutura.

#### Camada de Apresentação
* **Estrutura de Pastas:**
    * `Vits.NET.Web` - Frontend web desenvolvido em Next.js/React.
    * `Vits.NET.Mobile` - Aplicativo mobile em React Native.
    * `Vits.NET.APEX` - Dashboards e interfaces no Oracle APEX.
* **Justificativa:** A camada de apresentação é dividida em três projetos distintos para atender diferentes perfis de usuários. O frontend web oferece interface completa para gestores e compradores, o mobile proporciona acesso simplificado para produtores em campo, e o Oracle APEX fornece prototipação rápida e dashboards analíticos. Esta separação garante que cada interface seja otimizada para seu contexto de uso sem comprometer a lógica de negócio subjacente.

#### Camada de Aplicação
* **Estrutura de Pastas:**
    * `Vits.NET.Web` - Frontend web desenvolvido em ASP.NET Core MVC com interface responsiva e validações completas.
    * `Vits.NET.API` - API RESTful desenvolvida com Minimal API/Web API incluindo endpoints de busca avançada e HATEOAS.
    * `Vits.NET.Mobile` - Aplicativo mobile em React Native.
    * `Vits.NET.APEX` - Dashboards e interfaces no Oracle APEX.
* **Justificativa:** A camada de apresentação é dividida em projetos distintos para atender diferentes perfis de usuários. O frontend web oferece interface completa para gestores e compradores, o mobile proporciona acesso simplificado para produtores em campo, e o Oracle APEX fornece prototipação rápida e dashboards analíticos. Esta separação garante que cada interface seja otimizada para seu contexto de uso sem comprometer a lógica de negócio subjacente.

#### Implementações da Sprint 2 - Camada Web

##### ASP.NET Core MVC - Views e Layouts
* **Rotas Padrão e Personalizadas:**
    * Configuração de rotas padrão para todas as páginas da aplicação seguindo convenções MVC.
    * Implementação de rotas personalizadas para operações específicas e URLs amigáveis.
    * Utilização de Route Constraints para validação e segurança.

* **Layout Principal:**
    * Layout responsivo implementado com Bootstrap 5 customizado.
    * Cabeçalho com navegação intuitiva e menu responsivo para dispositivos móveis.
    * Rodapé institucional com informações da equipe GreenCore Team.
    * Sistema de breadcrumb para melhor navegação e contexto do usuário.
    * Design mobile-first garantindo usabilidade em todos os dispositivos.

* **Views e ViewModels:**
    * Desenvolvimento de views para todas as principais funcionalidades (Index, Create, Edit, Details, Delete).
    * Criação de ViewModels específicas para transferência de dados entre apresentação e lógica de negócio.
    * Implementação de Data Annotations para validações client-side e server-side.
    * Mensagens de erro personalizadas e em português para melhor experiência do usuário.
    * Validações customizadas para regras de negócio específicas do domínio agrícola.

##### API RESTful - Minimal API / Web API

* **Endpoints de Busca Avançada:**
    * Implementação de rotas `/search` para cada entidade de domínio (Produtores, Compradores, Propriedades, Lotes, Rastreabilidade).
    * Suporte a paginação através dos parâmetros `pageNumber` e `pageSize`.
    * Ordenação dinâmica através dos parâmetros `sortBy` e `sortOrder`.
    * Filtros específicos por entidade para consultas precisas.
    * Metadata de paginação retornada nos headers da resposta HTTP.

* **HATEOAS (Hypermedia as the Engine of Application State):**
    * Implementação completa de HATEOAS em todos os endpoints da API.
    * Cada resposta inclui links hipermídia para recursos relacionados.
    * Links para operações disponíveis: self, collection, create, update, delete.
    * Links para entidades relacionadas facilitando navegação entre recursos.
    * Utilização de métodos HTTP apropriados (GET, POST, PUT, DELETE) nos links.

* **Controllers e Operações CRUD:**
    * Implementação completa de operações Create, Read, Update, Delete para todas as entidades.
    * Validação automática de ModelState com retorno de erros padronizados.
    * Tratamento centralizado de exceções através de middleware customizado.
    * Respostas HTTP padronizadas seguindo boas práticas REST (200, 201, 204, 400, 404, 500).
    * Logging estruturado de todas as operações para auditoria e debugging.
    * Aplicação de padrões de projeto: Repository Pattern, Unit of Work, Dependency Injection.
    * Validações de regras de negócio específicas do domínio agrícola.
    * Sanitização de inputs para prevenção de ataques (XSS, SQL Injection).

---

#### 🚀 Implementações da Sprint 3 - Monitoramento, Observabilidade e Testes

##### Monitoramento e Observabilidade
* **Health Checks:** Implementação de endpoints para monitoramento contínuo da saúde da aplicação e da disponibilidade do banco de dados (Oracle). Visite `https://localhost:<porta>/health` para validar o status.
* **Logging Estruturado:** Configuração do pacote **Serilog** substituindo o logger padrão. Registro de eventos (Information, Warning, Error), injeção de correlação de requests e arquivamento em rotatividade diária (`logs/log-<data>.txt`).
* **Tracing e Métricas (OpenTelemetry):** Rastreamento distribuído de requisições HTTP e métricas do ASP.NET Core configurados para diagnosticar gargalos de performance, mapeando requisições e exportando telemetria para o console (ambiente local).

##### Qualidade de Software: Testes Automatizados (Padrão AAA)
* **Testes Unitários:** Criação do projeto `Verdantis.Tests.Unit` utilizando o framework **xUnit** para validação das regras de negócio (Camada de Aplicação). Dependências isoladas com a biblioteca de mocks **Moq**, garantindo testes previsíveis seguindo o padrão Arrange-Act-Assert.
* **Testes de Integração:** Criação do projeto `Verdantis.Tests.Integration` para testar fluxos reais da API (Controller -> Repositories -> Banco na Memória/Mock HTTP) utilizando o pacote `WebApplicationFactory`, cobrindo requisições HTTP, validação de rotas e Health Checks.

##### 🧪 Como executar os testes automatizados
No seu terminal (na pasta raiz da solução), execute:
* Para rodar **todos** os testes: `dotnet test`
* Para rodar apenas os testes **Unitários**: `dotnet test Verdantis.Tests.Unit/Verdantis.Tests.Unit.csproj`
* Para rodar apenas os testes de **Integração**: `dotnet test Verdantis.Tests.Integration/Verdantis.Tests.Integration.csproj`

---

#### Camada de Domínio
* **Estrutura:**
    * `Vits.NET.Domain` - Entidades, interfaces de repositório e regras de negócio.
* **Entidades Principais:**
    * `Produtor` - Representa produtores agrícolas cadastrados.
    * `Comprador` - Representa compradores e distribuidores.
    * `Propriedade` - Representa propriedades rurais com dados geográficos.
    * `Lote` - Representa lotes agrícolas com informações de plantio e colheita.
    * `Rastreabilidade` - Representa histórico de eventos da cadeia produtiva.
* **Interfaces de Repositório:**
    * `IProdutorRepository` - Acesso a dados de produtores.
    * `ICompradorRepository` - Acesso a dados de compradores.
    * `IPropriedadeRepository` - Acesso a dados de propriedades rurais.
    * `ILoteRepository` - Acesso a dados de lotes agrícolas.
    * `IRastreabilidadeRepository` - Acesso a dados de rastreabilidade.
* **Regras de Negócio:**
    * Validação de dados cadastrais de produtores.
    * Cálculo automático de área de propriedades com Oracle Spatial.
    * Validação de ciclo de vida de lotes agrícolas.
    * Geração de hash de segurança para registro blockchain.
    * Validação de certificações e comprovação de origem.

#### Camada de Infraestrutura
* **Estrutura:**
    * `Vits.NET.Infrastructure` - Implementação de repositórios, acesso a dados e integrações.
* **Responsabilidades:**
    * Mapeamento de entidades com Entity Framework Core.
    * Implementação de repositórios concretos com métodos CRUD.
    * Configuração e aplicação de migrações de banco de dados.
    * Integração com Oracle Database.
    * Integração com Hyperledger Fabric para blockchain.
    * Integração com APIs externas (clima, geolocalização).
    * Implementação de clientes HTTP para consumo de serviços.
* **Tecnologias Utilizadas:**
    * Entity Framework Core para ORM.
    * Oracle Database para armazenamento de dados.
    * Hyperledger Fabric para registros imutáveis.
    * Oracle Spatial para processamento geográfico.
    * Python (oracledb, Pandas) para scripts de integração e automação.

**Serviços Principais:**
    * Serviço de cadastro de produtores e compradores.
    * Serviço de registro e consulta de lotes agrícolas.
    * Serviço de geração de relatórios periódicos.
    * Serviço de emissão de QR Code para rastreabilidade.
    * Serviço de integração com API de clima.
    * Serviços de busca avançada com filtros, paginação e ordenação.

---
## 🛠️ Tecnologias do Projeto

| Camada | Tecnologia | Função |
|---|---|---|
| **Blockchain** | Hyperledger Fabric | Registro imutável de lotes agrícolas. |
| **Dados** | Oracle Database | Armazenar propriedades, lotes e transações. |
| **Geolocalização** | Oracle Spatial | Mapear propriedades e calcular áreas. |
| **Frontend Web** | ASP.NET Core MVC + Bootstrap 5 | Interface web responsiva e moderna. |
| **API** | ASP.NET Core Web API / Minimal API | API RESTful com HATEOAS e busca avançada. |
| **Mobile** | React Native | App para produtores e compradores. |
| **Backend** | .NET 9 | Framework principal para toda aplicação. |
| **Scripts** | Python (oracledb, Pandas) | Integração e geração de relatórios. |
| **Dashboards** | Oracle APEX | Prototipação rápida e BI. |
| **Monitoramento/Qualidade** | XUnit, Moq, OpenTelemetry, Serilog | Observabilidade de ponta a ponta e testes automatizados de comportamento e integração. |

---
### Equipe
**GreenCore Team:**
* Caio Lucas Silva Gomes (RM-560077) 
* João Gabriel Fuchs Grecco (RM-559863) 
* Madjer Henrique Almeida Finamor (RM-560716) 
