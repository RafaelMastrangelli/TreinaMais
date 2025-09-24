import type { HeaderProps } from "./Header.types";

const Header: React.FC<HeaderProps> = ({ title, className = '', icon, children }) => {
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
      {children}
    </div>
  );
};

export default Header