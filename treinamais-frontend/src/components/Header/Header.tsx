import type { HeaderProps } from "./Header.types";
import { useAuth } from '../../contexts/AuthContext';

const Header: React.FC<HeaderProps> = ({ title, className = '', icon, children }) => {
  const { logout, user } = useAuth();
  const textSpacing = children ? 'mb-8' : ''
  
  return (
    <div
      className={
        `relative w-full rounded-b-2xl bg-[#5B2DD1] text-white pt-32 pb-24 pl-12 overflow-hidden ${className}`
      }
    >
      {typeof title !== 'string' ? title : <h1 className={["text-5xl font-semibold leading-snug max-w-[70%]", textSpacing].join(" ")}>{title}</h1>}
      <div className="absolute right-5 md:right-6 top-1/2 -translate-y-1/2 select-none">
        {icon}
      </div>
      
      {/* User info and logout button */}
      <div className="absolute top-4 right-4 flex items-center gap-4">
        {user && (
          <div className="text-sm">
            <span className="text-white/80">Olá, {user.name}</span>
          </div>
        )}
        <button
          onClick={logout}
          className="bg-white/20 hover:bg-white/30 text-white px-3 py-1 rounded-lg text-sm transition-colors"
        >
          Sair
        </button>
      </div>
      
      {children}
    </div>
  );
};

export default Header