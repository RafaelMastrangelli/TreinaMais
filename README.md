# 🎓 TreinaMais - Plataforma de Cursos Online com IA

<div align="center">
  <img src="treinamais-frontend/public/treinemais.png" alt="TreinaMais Logo" width="200"/>
  
  **Plataforma completa de cursos online com inteligência artificial integrada**
  
  [![.NET 9](https://img.shields.io/badge/.NET-9.0-purple.svg)](https://dotnet.microsoft.com/download/dotnet/9.0)
  [![React](https://img.shields.io/badge/React-19.1.1-blue.svg)](https://reactjs.org/)
  [![TypeScript](https://img.shields.io/badge/TypeScript-5.8.3-blue.svg)](https://www.typescriptlang.org/)
  [![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-3.4.17-38B2AC.svg)](https://tailwindcss.com/)
</div>

## 📋 Sobre o Projeto

O **TreinaMais** é uma plataforma completa de cursos online que integra inteligência artificial para automatizar processos de criação de conteúdo, moderação e análise de sentimentos. A aplicação permite o cadastro de cursos, geração automática de resumos via IA, moderação de conteúdo e análise de feedback dos usuários.

### ✨ Principais Características
- 🤖 **IA Integrada**: OpenAI + Leonardo.AI para geração de conteúdo
- 🔐 **Autenticação JWT**: Sistema seguro de login
- 📱 **Interface Responsiva**: Design moderno com Tailwind CSS
- 🛡️ **Moderação Automática**: Análise de sentimentos e moderação de conteúdo
- 📊 **Swagger/OpenAPI**: Documentação completa da API
- ⚡ **Performance**: Lazy loading e otimizações

## 🚀 Stack Tecnológica

### Backend (.NET 9)
- **ASP.NET Core Web API** - Framework principal
- **Entity Framework Core** - ORM com banco em memória
- **Swagger/OpenAPI** - Documentação interativa da API
- **JWT Authentication** - Autenticação segura
- **Integrações com IA:**
  - **OpenAI API** - Geração de texto, moderação e análise de sentimentos
  - **Leonardo.AI** - Geração de imagens via IA
  - **Análise de Sentimentos** - Classificação automática de feedback

### Frontend (React + TypeScript)
- **React 19** - Biblioteca principal com hooks
- **TypeScript** - Tipagem estática
- **React Router DOM** - Roteamento SPA
- **React Hook Form** - Gerenciamento de formulários
- **Axios** - Cliente HTTP com interceptors
- **Tailwind CSS** - Framework de estilização
- **Vite** - Build tool e dev server
- **Vitest** - Framework de testes

## 🏗️ Arquitetura do Projeto

### Backend (TreinaMais.API)
```
├── Application/
│   ├── DTOs/                    # Objetos de transferência de dados
│   └── Services/                # Serviços de aplicação
│       ├── AuthService.cs       # Autenticação JWT
│       ├── CourseApplicationService.cs  # Gestão de cursos
│       └── ReviewApplicationService.cs # Gestão de avaliações
├── Core/
│   ├── Entities/               # Entidades do domínio (Course, Review)
│   └── Enums/                  # Enumerações (ReviewStatus)
├── Infrastructure/
│   ├── Integrations/           # Integrações com APIs externas
│   │   ├── Services/          # Serviços de IA (OpenAI, Leonardo.AI)
│   │   ├── OpenAiService.cs   # Integração OpenAI
│   │   ├── LeonardoAiService.cs # Integração Leonardo.AI
│   │   └── SentimentAnalysisService.cs # Análise de sentimentos
│   └── Persistence/           # Camada de persistência
│       ├── Repositories/      # Repositórios de dados
│       └── TreinaMaisDbContext.cs # Contexto do EF Core
├── Controllers/               # Controladores da API
│   ├── AuthController.cs      # Autenticação
│   ├── CoursesController.cs   # Gestão de cursos
│   └── ReviewsController.cs   # Gestão de avaliações
└── Models/                    # Modelos de entrada/saída
```

### Frontend (React)
```
├── components/                # Componentes reutilizáveis
│   ├── CourseForm/           # Formulário de cadastro de cursos
│   ├── CourseList/           # Listagem de cursos
│   ├── CourseDetail/         # Detalhes do curso
│   ├── CourseFeedbackForm/   # Formulário de feedback
│   ├── Header/               # Cabeçalho da aplicação
│   ├── ProtectedRoute/       # Proteção de rotas
│   ├── SocialProof/          # Prova social
│   ├── Stars/                # Sistema de avaliação
│   └── Tabs/                 # Componente de abas
├── pages/                    # Páginas da aplicação
│   ├── Login.tsx             # Página de login
│   ├── CourseList.tsx        # Lista de cursos
│   ├── CourseDetail.tsx      # Detalhes do curso
│   ├── RegisterCourse.tsx    # Cadastro de curso
│   └── Feedback.tsx          # Página de feedback
├── contexts/                 # Contextos React
│   └── AuthContext.tsx       # Contexto de autenticação
├── services/                 # Serviços de API
│   ├── auth.ts              # Serviço de autenticação
│   ├── courses.ts           # Serviço de cursos
│   ├── reviews.ts           # Serviço de avaliações
│   └── config/              # Configurações da API
├── utils/                    # Utilitários
│   ├── currency.ts          # Formatação de moeda
│   └── rating.ts            # Cálculo de avaliações
└── assets/                   # Recursos estáticos
```

## 🎯 Funcionalidades Principais

### 1. **Sistema de Autenticação** 🔐
- ✅ Login com JWT (JSON Web Token)
- ✅ Proteção de rotas privadas
- ✅ Contexto global de autenticação
- ✅ Logout com limpeza de token
- ✅ Redirecionamento automático
- ✅ Credenciais: `admin@treinamais.com` / `admin`

### 2. **Gestão de Cursos** 📚
- ✅ Cadastro de novos cursos com validação
- ✅ Listagem de cursos disponíveis
- ✅ Visualização detalhada de cursos
- ✅ Edição e exclusão de cursos
- ✅ Upload de imagens com moderação automática
- ✅ Geração automática de resumos via IA
- ✅ Criação de capas personalizadas via Leonardo.AI

### 3. **Sistema de Avaliações e Feedback** ⭐
- ⭐ Sistema de notas (1-5 estrelas)
- 💬 Comentários e feedback dos usuários
- 🔍 Análise automática de sentimento das avaliações
- ✅ Moderação automática de conteúdo
- ✅ Aprovação/rejeição de reviews
- 📊 Classificação de sentimentos (positivo/neutro/negativo)
- 📈 Score de sentimento (-1 a +1)

### 4. **Inteligência Artificial Integrada** 🤖
- 🤖 **Geração de Resumos**: Criação automática de resumos dos cursos via OpenAI
- 🖼️ **Geração de Imagens**: Criação de capas personalizadas via Leonardo.AI
- 🛡️ **Moderação de Conteúdo**: Verificação automática de texto e imagens
- 📊 **Análise de Sentimentos**: Classificação automática de feedback
- 🔄 **Processamento Assíncrono**: Geração de conteúdo em background

### 5. **Interface Moderna e Responsiva** 🎨
- 📱 Design responsivo com Tailwind CSS
- 🎨 Interface intuitiva e moderna
- ⚡ Carregamento otimizado com lazy loading
- 🔄 Estados de loading e tratamento de erros
- 🎭 Componentes reutilizáveis
- 📊 Prova social com avaliações

### 6. **Documentação da API** 📖
- 📋 **Swagger UI**: Interface interativa para testar a API
- 🔐 **Autenticação JWT**: Suporte completo para Bearer Token
- 📝 **Documentação Automática**: Todos os endpoints documentados
- 🧪 **Testes Interativos**: Teste todos os endpoints diretamente no navegador

## 🛠️ Como Executar o Projeto

### Pré-requisitos
- .NET 9 SDK
- Node.js 18+
- Chaves de API:
- OpenAI API Key
- Leonardo.AI API Key

### 1. Backend (API)
```bash
cd treinamais-backend/TreinaMais.API
dotnet restore
dotnet run
```
**API disponível em:** `http://localhost:5056`  
**Swagger UI:** `http://localhost:5056` (raiz da aplicação)

### 2. Frontend (React)
```bash
cd treinamais-frontend
npm install
npm run dev
```
**Aplicação disponível em:** `http://localhost:5173`

### 3. Configuração das APIs

**Backend** (`appsettings.json`):
```json
{
  "OpenAi": {
    "ApiKey": "sua-chave-openai",
    "TextModel": "gpt-3.5-turbo",
    "TextUrl": "https://api.openai.com/v1/chat/completions",
    "ModerationUrl": "https://api.openai.com/v1/moderations"
  },
  "LeonardoAi": {
    "ApiKey": "sua-chave-leonardo",
    "DefaultModelId": "modelo-id",
    "DefaultStyleUUID": "style-uuid"
  },
  "Jwt": {
    "Key": "sua-chave-jwt-secreta-minimo-32-caracteres",
    "Issuer": "TreinaMais",
    "Audience": "TreinaMaisFrontend",
    "ExpiresMinutes": 120
  }
}
```

## 📡 API Endpoints

### 🔐 Autenticação
- `POST /api/auth/login` - Login do usuário

### 📚 Cursos
- `GET /api/courses` - Listar todos os cursos
- `GET /api/courses/{id}` - Buscar curso por ID
- `POST /api/courses` - Criar novo curso (🔒 autenticação)
- `PUT /api/courses/{id}` - Atualizar curso (🔒 autenticação)
- `DELETE /api/courses/{id}` - Excluir curso
- `POST /api/courses/{id}/image` - Upload de imagem

### ⭐ Avaliações
- `GET /api/courses/{courseId}/reviews` - Listar avaliações do curso
- `GET /api/courses/{courseId}/reviews/{id}` - Buscar avaliação específica
- `POST /api/courses/{courseId}/reviews` - Criar avaliação (🔒 autenticação)
- `PATCH /api/courses/{courseId}/reviews/{id}/approve` - Aprovar avaliação
- `PATCH /api/courses/{courseId}/reviews/{id}/reject` - Rejeitar avaliação
- `DELETE /api/courses/{courseId}/reviews/{id}` - Excluir avaliação (🔒 autenticação)

## 🔧 Funcionalidades de IA

### OpenAI Integration
- **Geração de Texto**: Criação automática de resumos de cursos
- **Moderação**: Verificação de conteúdo inadequado em texto e imagens
- **Análise de Sentimentos**: Classificação automática de feedback
- **Modelos**: GPT-3.5-turbo para geração de texto

### Leonardo.AI Integration
- **Geração de Imagens**: Criação de capas personalizadas para cursos
- **Múltiplas Opções**: Geração de 3 imagens por solicitação
- **Estilos Personalizados**: Aplicação de estilos específicos
- **Dimensões**: 1472x832 pixels por padrão

## 📊 Estrutura de Dados

### Course (Curso)
```csharp
public class Course
{
    public int Id { get; set; }
    public string NomeCurso { get; set; }
    public string Instrutor { get; set; }
    public decimal Valor { get; set; }
    public string DescricaoDetalhada { get; set; }
    public string? Resumo { get; set; }              // 🤖 Gerado por IA
    public byte[]? ImagemBytes { get; set; }         // 🖼️ Imagem moderada
    public string? CoverUrl { get; set; }            // 🎨 URL da capa
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? UpdatedAtUtc { get; set; }
    public ICollection<Review> Reviews { get; set; }
}
```

### Review (Avaliação)
```csharp
public class Review
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public double Nota { get; set; }
    public string Descricao { get; set; }
    public string? Sentimento { get; set; }          // "positive", "neutral", "negative"
    public double? SentimentScore { get; set; }     // -1 a +1
    public string? ModerationLabel { get; set; }    // "clean", "profanity", etc.
    public ReviewStatus Status { get; set; }         // Pending, Approved, Rejected
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? ModeratedAtUtc { get; set; }
}
```

### ReviewStatus (Enum)
```csharp
public enum ReviewStatus
{
    Pending = 0,    // ⏳ Aguardando moderação
    Approved = 1,   // ✅ Aprovado
    Rejected = 2    // ❌ Rejeitado
}
```

## 🎨 Interface e UX

### Páginas Principais
1. **🔐 Login** - Autenticação do usuário
2. **📚 Lista de Cursos** - Página inicial com todos os cursos
3. **👁️ Detalhes do Curso** - Visualização completa do curso
4. **📝 Cadastro de Curso** - Formulário para criar novos cursos
5. **💬 Feedback** - Sistema de avaliações

### Componentes Reutilizáveis
- **Header** - Cabeçalho com navegação e informações do usuário
- **CourseForm** - Formulário de cadastro/edição de cursos
- **CourseList** - Listagem de cursos com cards
- **CourseDetail** - Detalhes completos do curso
- **CourseFeedbackForm** - Formulário de avaliação
- **SocialProof** - Prova social com avaliações
- **Stars** - Sistema de avaliação visual
- **ProtectedRoute** - Proteção de rotas privadas

### Design System
- **🎨 Cores**: Azul (#5B2DD1) como cor principal
- **📝 Tipografia**: Fontes modernas e legíveis
- **📱 Layout**: Responsivo com Tailwind CSS
- **🧩 Componentes**: Reutilizáveis e modulares
- **⚡ Estados**: Loading, erro e sucesso bem definidos

## 🔒 Segurança

### Autenticação
- JWT (JSON Web Token) para autenticação
- Tokens com expiração configurável (120 minutos)
- Proteção de rotas sensíveis
- Validação de credenciais no backend

### Moderação de Conteúdo
- Verificação automática de texto via OpenAI
- Análise de sentimentos em tempo real
- Aprovação/rejeição automática de conteúdo
- Logs de moderação para auditoria

## 🚀 Deploy e Produção

### Backend
- Configuração de CORS para frontend
- Banco de dados em memória (desenvolvimento)
- Logs estruturados
- Documentação OpenAPI/Swagger

### Frontend
- Build otimizado com Vite
- Lazy loading de componentes
- Tratamento de erros global
- Estados de loading consistentes

## 📝 Scripts Disponíveis

### Backend
```bash
dotnet run                    # Executar a API
dotnet build                  # Compilar o projeto
dotnet test                   # Executar testes
```

### Frontend
```bash
npm run dev                   # Servidor de desenvolvimento
npm run build                 # Build para produção
npm run preview              # Preview do build
npm run test                  # Executar testes
npm run test:watch           # Testes em modo watch
npm run lint                  # Linting do código
```

## 🎯 Próximos Passos

- [ ] 🗄️ Implementar banco de dados persistente (SQL Server/PostgreSQL)
- [ ] 👥 Adicionar sistema de usuários completo
- [ ] 🔔 Implementar notificações em tempo real
- [ ] 🧪 Adicionar testes automatizados
- [ ] ⚡ Implementar cache para melhor performance
- [ ] 📊 Adicionar métricas e analytics
- [ ] 💳 Implementar sistema de pagamentos
- [ ] 🔍 Adicionar funcionalidades de busca avançada
- [ ] 🌐 Implementar internacionalização (i18n)
- [ ] 📱 Desenvolver aplicativo mobile

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.

---

<div align="center">
  <strong>Desenvolvido com ❤️ usando .NET 9, React 19 e Inteligência Artificial</strong>
  
  [![Made with .NET](https://img.shields.io/badge/Made%20with-.NET-purple.svg)](https://dotnet.microsoft.com/)
  [![Made with React](https://img.shields.io/badge/Made%20with-React-blue.svg)](https://reactjs.org/)
  [![Powered by AI](https://img.shields.io/badge/Powered%20by-AI-orange.svg)](https://openai.com/)
</div>