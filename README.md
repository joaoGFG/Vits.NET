# 🌱 Verdantis (VITS - Visual Information Tracking System)

## 🎯 Definição do Projeto

### Objetivo do Projeto
O Verdantis é um sistema de rastreabilidade e visualização inteligente desenvolvido para resolver a falta de transparência e certificação sustentável na cadeia produtiva agrícola brasileira. O projeto visa fornecer uma plataforma integrada que conecta produtores, distribuidores e compradores, permitindo o registro, visualização e certificação digital de produtos agrícolas, combatendo os R$ 12 bilhões perdidos anualmente em exportações devido à ausência de rastreabilidade[cite: 10, 20].

### Escopo

#### Escopo do MVP:
* Cadastro de produtores e compradores.
* Registro de lotes agrícolas no Oracle Database com hash de segurança no Hyperledger Fabric.
* Dashboard interativo utilizando Oracle APEX e Oracle Spatial.
* Integração com API de clima para apoio à decisão.
* Protótipo mobile em React Native para consulta de rastreabilidade via QR Code.

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

### Requisitos Não Funcionais:
* **Transparência:** Cadeia produtiva visível ponta a ponta.
* **Performance:** Performance enterprise garantida pelo Oracle Database.
* **Escalabilidade:** Através de arquitetura em camadas e backend robusto.
* **Segurança:** Registros imutáveis via Hyperledger Fabric.
* **Usabilidade:** Acessível para usuários com diferentes níveis de informatização.
* **Disponibilidade:** Hospedagem no Oracle Cloud Free Tier.

## 🏗️ Desenho da Arquitetura

<img width="1267" height="839" alt="image" src="https://github.com/user-attachments/assets/f7fadb9e-c1fc-432f-a42b-021d29c2da64" />

### Clean Architecture
O projeto Verdantis segue os princípios da Clean Architecture para garantir separação de responsabilidades, baixo acoplamento e alta coesão entre os componentes do sistema. A arquitetura é organizada em camadas concêntricas, onde as dependências apontam sempre para o centro (domínio), garantindo que as regras de negócio permaneçam independentes de frameworks, UI e infraestrutura.

### Camadas da Aplicação

#### Camada de Apresentação
* **Estrutura de Pastas:**
    * `Vits.NET.Web` - Frontend web desenvolvido em Next.js/React.
    * `Vits.NET.Mobile` - Aplicativo mobile em React Native.
    * `Vits.NET.APEX` - Dashboards e interfaces no Oracle APEX.
* **Justificativa:** A camada de apresentação é dividida em três projetos distintos para atender diferentes perfis de usuários. O frontend web oferece interface completa para gestores e compradores, o mobile proporciona acesso simplificado para produtores em campo, e o Oracle APEX fornece prototipação rápida e dashboards analíticos. Esta separação garante que cada interface seja otimizada para seu contexto de uso sem comprometer a lógica de negócio subjacente.

#### Camada de Aplicação
* **Estrutura:**
    * `Vits.NET.Application` - Serviços de aplicação e casos de uso.
* **Responsabilidades:**
    * Implementação dos casos de uso do sistema.
    * Orquestração de fluxos entre domínio e infraestrutura.
    * Definição e implementação de DTOs (Data Transfer Objects).
    * Manipulação de erros e retorno de respostas apropriadas.
    * Validação de entrada de dados.
* **Serviços Principais:**
    * Serviço de cadastro de produtores e compradores.
    * Serviço de registro e consulta de lotes agrícolas.
    * Serviço de geração de relatórios periódicos.
    * Serviço de emissão de QR Code para rastreabilidade.
    * Serviço de integração com API de clima.

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

---
### Tecnologias do Projeto

| Camada | Tecnologia | Função |
|---|---|---|
| **Blockchain** | Hyperledger Fabric | Registro imutável de lotes agrícolas. |
| **Dados** | Oracle Database | Armazenar propriedades, lotes e transações. |
| **Geolocalização** | Oracle Spatial | Mapear propriedades e calcular áreas. |
| **Frontend Web** | Next.js + React | Interface moderna e responsiva. |
| **Mobile** | React Native | App para produtores e compradores. |
| **Backend** | Java (Spring Boot) + .NET 9 | API RESTful robusta e escalável. |
| **Scripts** | Python (oracledb, Pandas) | Integração e geração de relatórios. |
| **Dashboards** | Oracle APEX | Prototipação rápida e BI. |

---
### Equipe
**GreenCore Team:**
* Caio Lucas Silva Gomes (RM-560077) 
* João Gabriel Fuchs Grecco (RM-559863) 
* Madjer Henrique Almeida Finamor (RM-560716) 
