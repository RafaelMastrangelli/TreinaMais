# Swagger Setup - TreinaMais API

## Configuração Concluída

O Swagger foi configurado com sucesso na API do TreinaMais. Aqui estão os detalhes da implementação:

### 📦 Pacotes Adicionados
- `Swashbuckle.AspNetCore` (versão 7.0.0)

### ⚙️ Configurações Implementadas

#### 1. Geração de Documentação XML
- Habilitada a geração de arquivos XML para documentação
- Configurado para incluir comentários XML no Swagger

#### 2. Configuração do Swagger no Program.cs
- **Título**: TreinaMais API
- **Versão**: v1
- **Descrição**: API para gerenciamento de cursos e avaliações do TreinaMais
- **Autenticação JWT**: Configurada para usar Bearer Token
- **Interface**: Swagger UI disponível na raiz da aplicação

#### 3. Documentação dos Controllers
Todos os controllers foram documentados com:
- Comentários XML detalhados
- Descrições de parâmetros
- Códigos de resposta HTTP
- Tipos de retorno

### 🚀 Como Usar

#### 1. Executar a API
```bash
cd treinamais-backend/TreinaMais.API
dotnet run
```

#### 2. Acessar o Swagger
- **URL**: `https://localhost:5001` (ou a porta configurada)
- **Swagger UI**: Interface interativa para testar a API
- **OpenAPI JSON**: `https://localhost:5001/swagger/v1/swagger.json`

### 🔐 Autenticação

Para testar endpoints que requerem autenticação:

1. **Fazer Login**: Use o endpoint `POST /api/auth/login`
2. **Copiar o Token**: Copie o token retornado na resposta
3. **Autorizar no Swagger**: 
   - Clique no botão "Authorize" no Swagger UI
   - Cole o token no formato: `Bearer {seu-token}`
   - Clique em "Authorize"

### 📋 Endpoints Disponíveis

#### AuthController
- `POST /api/auth/login` - Realizar login

#### CoursesController
- `GET /api/courses` - Listar todos os cursos
- `GET /api/courses/{id}` - Obter curso por ID
- `POST /api/courses` - Criar curso (requer autenticação)
- `PUT /api/courses/{id}` - Atualizar curso (requer autenticação)
- `DELETE /api/courses/{id}` - Remover curso
- `POST /api/courses/{id}/image` - Upload de imagem

#### ReviewsController
- `GET /api/courses/{courseId}/reviews` - Listar avaliações de um curso
- `GET /api/courses/{courseId}/reviews/{id}` - Obter avaliação por ID
- `POST /api/courses/{courseId}/reviews` - Criar avaliação (requer autenticação)
- `DELETE /api/courses/{courseId}/reviews/{id}` - Remover avaliação (requer autenticação)
- `PATCH /api/courses/{courseId}/reviews/{id}/approve` - Aprovar avaliação
- `PATCH /api/courses/{courseId}/reviews/{id}/reject` - Rejeitar avaliação

### 🎯 Funcionalidades do Swagger

- **Interface Interativa**: Teste todos os endpoints diretamente no navegador
- **Documentação Automática**: Todos os endpoints são documentados automaticamente
- **Autenticação Integrada**: Suporte completo para JWT Bearer Token
- **Exemplos de Request/Response**: Modelos de dados com exemplos
- **Validação**: Validação automática de parâmetros e tipos de dados

### 📝 Próximos Passos

1. **Executar a API** e acessar o Swagger
2. **Testar os endpoints** usando a interface do Swagger
3. **Configurar autenticação** para testar endpoints protegidos
4. **Personalizar** a documentação conforme necessário

### 🔧 Personalização Adicional

Se precisar personalizar ainda mais o Swagger, você pode:

- Adicionar mais metadados na configuração do `SwaggerGen`
- Personalizar o tema do Swagger UI
- Adicionar exemplos customizados para os modelos
- Configurar diferentes versões da API


