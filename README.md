# TreinaMais - Plataforma de Cursos Online com IA

## 📋 Sobre o Projeto

O **TreinaMais** é uma plataforma completa de cursos online que integra inteligência artificial para automatizar processos de criação de conteúdo, moderação e análise de sentimentos. A aplicação permite o cadastro de cursos, geração automática de resumos via IA, moderação de conteúdo e análise de feedback dos usuários.

## 🚀 Tecnologias Utilizadas

### Backend (.NET 9)
- **ASP.NET Core Web API** - Framework principal
- **Entity Framework Core** - ORM com banco em memória
- **OpenAPI/Swagger** - Documentação da API
- **Integrações com IA:**
  - **OpenAI API** - Geração de texto e moderação de conteúdo
  - **Leonardo.AI** - Geração de imagens via IA
  - **Análise de Sentimentos** - Classificação automática de feedback

### Frontend (React + TypeScript)
- **React 19** - Biblioteca principal
- **TypeScript** - Tipagem estática
- **React Router DOM** - Roteamento
- **React Hook Form** - Gerenciamento de formulários
- **Axios** - Cliente HTTP
- **Tailwind CSS** - Framework de estilização
- **Vite** - Build tool e dev server

## 🏗️ Arquitetura do Projeto

### Backend (EducaDev.API)
```
├── Application/
│   ├── DTOs/                    # Objetos de transferência de dados
│   └── Services/                # Serviços de aplicação
├── Core/
│   ├── Entities/               # Entidades do domínio (Course, Review)
│   └── Enums/                  # Enumerações
├── Infrastructure/
│   ├── Integrations/           # Integrações com APIs externas
│   │   └── Services/          # Serviços de IA (OpenAI, Leonardo.AI)
│   └── Persistence/           # Camada de persistência
│       └── Repositories/      # Repositórios de dados
├── Controllers/               # Controladores da API
└── Models/                    # Modelos de entrada/saída
```

### Frontend (React)
```
├── components/                # Componentes reutilizáveis
│   ├── CourseForm/           # Formulário de cadastro de cursos
│   ├── CourseList/           # Listagem de cursos
│   ├── CourseDetail/         # Detalhes do curso
│   ├── CourseFeedbackForm/   # Formulário de feedback
│   └── ...
├── pages/                    # Páginas da aplicação
├── services/                 # Serviços de API
└── utils/                    # Utilitários
```

## 🎯 Funcionalidades Principais

### 1. **Gestão de Cursos**
- ✅ Cadastro de novos cursos
- ✅ Listagem de cursos disponíveis
- ✅ Visualização detalhada de cursos
- ✅ Edição e exclusão de cursos
- ✅ Upload de imagens com moderação automática

### 2. **Inteligência Artificial Integrada**
- 🤖 **Geração de Resumos**: Criação automática de resumos dos cursos via OpenAI
- 🖼️ **Geração de Imagens**: Criação de capas personalizadas via Leonardo.AI
- 🛡️ **Moderação de Conteúdo**: Verificação automática de texto e imagens
- 📊 **Análise de Sentimentos**: Classificação automática de feedback

### 3. **Sistema de Avaliações**
- ⭐ Sistema de notas (1-5 estrelas)
- 💬 Comentários e feedback dos usuários
- 🔍 Análise automática de sentimento das avaliações
- ✅ Moderação e aprovação de reviews

### 4. **Interface Moderna**
- 📱 Design responsivo
- 🎨 Interface intuitiva com Tailwind CSS
- ⚡ Carregamento otimizado com lazy loading
- 🔄 Estados de loading e tratamento de erros

## 🛠️ Como Executar o Projeto

### Pré-requisitos
- .NET 9 SDK
- Node.js 18+
- Chaves de API:
  - OpenAI API Key
  - Leonardo.AI API Key

### Backend
```bash
cd treinamais-backend/EducaDev.API
dotnet restore
dotnet run
```

### Frontend
```bash
cd treinamais-frontend
npm install
npm run dev
```

### Configuração das APIs
Configure as chaves de API nos arquivos de configuração:
- `appsettings.json` (Backend)
- `.env` (Frontend)

## 📡 Endpoints da API

### Cursos
- `GET /api/courses` - Listar todos os cursos
- `GET /api/courses/{id}` - Buscar curso por ID
- `POST /api/courses` - Criar novo curso
- `PUT /api/courses/{id}` - Atualizar curso
- `DELETE /api/courses/{id}` - Excluir curso
- `POST /api/courses/{id}/image` - Upload de imagem

### Avaliações
- `GET /api/courses/{courseId}/reviews` - Listar avaliações
- `POST /api/courses/{courseId}/reviews` - Criar avaliação
- `PATCH /api/courses/{courseId}/reviews/{id}/approve` - Aprovar avaliação
- `PATCH /api/courses/{courseId}/reviews/{id}/reject` - Rejeitar avaliação

## 🔧 Funcionalidades de IA

### OpenAI Integration
- **Geração de Texto**: Criação automática de resumos de cursos
- **Moderação**: Verificação de conteúdo inadequado em texto e imagens
- **Análise de Sentimentos**: Classificação automática de feedback

### Leonardo.AI Integration
- **Geração de Imagens**: Criação de capas personalizadas para cursos
- **Múltiplas Opções**: Geração de 3 imagens por solicitação
- **Estilos Personalizados**: Aplicação de estilos específicos

## 📊 Estrutura de Dados

### Course (Curso)
- ID, Nome, Instrutor, Valor
- Descrição detalhada e resumo gerado por IA
- Imagem (bytes) e URL da capa
- Timestamps de criação e atualização
- Lista de avaliações

### Review (Avaliação)
- ID, Course ID, Nota, Descrição
- Análise de sentimento (positivo/neutro/negativo)
- Score de sentimento (-1 a +1)
- Status de moderação
- Timestamps


## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.

---

**Desenvolvido usando .NET 9, React 19 e Inteligência Artificial**
