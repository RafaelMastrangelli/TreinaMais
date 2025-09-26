import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const Login: React.FC = () => {
  const navigate = useNavigate();
  const { login } = useAuth();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    setLoading(true);
    try {
      await login(email, password);
      navigate('/');
    } catch (err: any) {
      setError(err?.message ?? 'Falha no login');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div 
      className="min-h-screen flex items-center justify-center p-8 relative background-optimized"
      style={{
        backgroundImage: 'url(/treinemais.png)'
      }}
    >
      {/* Overlay escuro para melhorar legibilidade */}
      <div className="absolute inset-0 bg-black bg-opacity-20"></div>
      
      <div className="w-full max-w-md relative z-10">
        {/* Logo/Header */}
        <div className="text-center mb-8">
          <h1 className="text-4xl font-bold text-white mb-2 drop-shadow-lg">Treina+</h1>
          <p className="text-white text-xl font-bold drop-shadow-2xl bg-blue-700 bg-opacity-80 px-4 py-2 rounded-lg inline-block">
            Sua plataforma de aprendizado
          </p>
        </div>

        {/* Login Form */}
        <form onSubmit={onSubmit} className="bg-white shadow-xl rounded-2xl px-8 pt-8 pb-8 border border-blue-200">
          <h2 className="text-2xl font-semibold mb-6 text-center text-blue-800">Entrar</h2>
          
          {error && (
            <div className="mb-4 bg-red-50 border border-red-200 text-red-600 text-sm rounded-lg px-4 py-3">
              {error}
            </div>
          )}
          
          <div className="mb-4">
            <label className="block text-sm font-medium mb-2 text-blue-700">Email</label>
            <input 
              type="email"
              value={email} 
              onChange={(e) => setEmail(e.target.value)} 
              className="w-full border-2 border-blue-200 rounded-lg px-4 py-3 focus:border-blue-500 focus:outline-none transition-colors" 
              placeholder="Digite seu email"
              required
            />
          </div>
          
          <div className="mb-6">
            <label className="block text-sm font-medium mb-2 text-blue-700">Senha</label>
            <div className="relative">
              <input 
                value={password} 
                onChange={(e) => setPassword(e.target.value)} 
                type={showPassword ? "text" : "password"} 
                className="w-full border-2 border-blue-200 rounded-lg px-4 py-3 pr-12 focus:border-blue-500 focus:outline-none transition-colors" 
                placeholder="Digite sua senha"
              />
              <button
                type="button"
                onClick={() => setShowPassword(!showPassword)}
                className="absolute right-3 top-1/2 transform -translate-y-1/2 text-blue-500 hover:text-blue-700 focus:outline-none"
              >
                {showPassword ? (
                  <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M13.875 18.825A10.05 10.05 0 0112 19c-4.478 0-8.268-2.943-9.543-7a9.97 9.97 0 011.563-3.029m5.858.908a3 3 0 114.243 4.243M9.878 9.878l4.242 4.242M9.878 9.878L3 3m6.878 6.878L21 21" />
                  </svg>
                ) : (
                  <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z" />
                  </svg>
                )}
              </button>
            </div>
          </div>
          
          <button 
            type="submit" 
            disabled={loading} 
            className="w-full bg-gradient-to-r from-blue-600 to-blue-700 hover:from-blue-700 hover:to-blue-800 text-white rounded-lg py-3 font-medium transition-all duration-200 disabled:opacity-50 disabled:cursor-not-allowed shadow-lg hover:shadow-xl"
          >
            {loading ? 'Entrando...' : 'Entrar'}
          </button>
        </form>

        {/* Footer */}
        <div className="text-center mt-6">
          <p className="text-blue-200 text-sm drop-shadow-md">
            Demo: usuário <span className="font-semibold text-white">admin</span> / senha <span className="font-semibold text-white">admin</span>
          </p>
        </div>
      </div>
    </div>
  );
};

export default Login;


