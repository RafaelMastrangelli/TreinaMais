# Credenciais de Login

Para testar o sistema de autenticação, use as seguintes credenciais:

## Usuário Administrador
- **Email:** admin@treinamais.com
- **Senha:** admin

## Como funciona

1. **Primeira visita:** A aplicação redireciona automaticamente para a tela de login
2. **Após login:** Você é redirecionado para a página inicial (lista de cursos)
3. **Proteção de rotas:** Todas as páginas (exceto login) requerem autenticação
4. **Logout:** Clique no botão "Sair" no canto superior direito do header

## Funcionalidades implementadas

- ✅ Tela de login como primeira página
- ✅ Proteção de todas as rotas
- ✅ Contexto de autenticação global
- ✅ Logout com limpeza de token
- ✅ Redirecionamento automático
- ✅ Informações do usuário no header
- ✅ Validação de credenciais via API

## Estrutura de autenticação

- **AuthContext:** Gerencia o estado global de autenticação
- **ProtectedRoute:** Componente que protege rotas privadas
- **Header:** Mostra informações do usuário e botão de logout
- **API:** Endpoint `/api/auth/login` para autenticação
