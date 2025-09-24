import { render, screen } from '@testing-library/react';
import Header from './Header';

const HeaderDefaultIcon: React.FC = () => (
  <svg
    width="96"
    height="96"
    viewBox="0 0 96 96"
    fill="none"
    xmlns="http://www.w3.org/2000/svg"
    className="text-[#A881FF] opacity-90"
    aria-hidden
    focusable="false"
  >
    {/* Balão de mensagem */}
    <rect x="10" y="26" width="56" height="38" rx="10" stroke="currentColor" strokeWidth="6" />
    {/* Rabicho do balão */}
    <path d="M22 64 L22 78 L34 68" stroke="currentColor" strokeWidth="6" strokeLinecap="round" strokeLinejoin="round" />
    {/* Estrela no canto superior direito */}
    <polygon
      points="74,10 79,23 93,23 82,31 86,44 74,36 62,44 66,31 55,23 69,23"
      fill="currentColor"
      className="opacity-80"
    />
    {/* Linhas internas do balão (efeito de texto) */}
    <line x1="20" y1="40" x2="46" y2="40" stroke="currentColor" strokeWidth="4" strokeLinecap="round" />
    <line x1="20" y1="50" x2="54" y2="50" stroke="currentColor" strokeWidth="4" strokeLinecap="round" />
  </svg>
);


describe('Header', () => {
  it('renderiza o título e estrutura visual base', () => {
    render(<Header title="Cadastro de Feedback" icon={<HeaderDefaultIcon />} />);
    expect(screen.getByRole('banner', { name: 'Cadastro de Feedback' })).toBeInTheDocument();
    expect(screen.getByText('Cadastro de Feedback')).toBeInTheDocument();
  });
});