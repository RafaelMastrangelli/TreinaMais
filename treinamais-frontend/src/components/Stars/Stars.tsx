import type { StarsProps } from "./Stars.types";

const Stars: React.FC<StarsProps> = ({
  className,
  rating,
  size = 'md'
}) => {
  const items: Array<"full" | "half" | "empty"> = [];

  const sizeClasses = {
    sm: 'w-4 h-4',
    md: 'w-5 h-5',
    lg: 'w-6 h-6'
  };

  const sizeClass = sizeClasses[size];

  const renderSVGStar = (type: "full" | "half" | "empty") => {
    const path =
      "M12 2.5l2.962 5.997 6.621.963-4.791 4.666 1.13 6.591L12 17.97 6.078 20.717l1.13-6.591-4.79-4.666 6.62-.963L12 2.5z";
  
    if (type === "full") {
      return (
        <svg viewBox="0 0 24 24" className={`${sizeClass} ${className}`} role="img" aria-label="Estrela cheia" fill="currentColor">
          <path d={path} />
        </svg>
      );
    }
  
    if (type === "empty") {
      return (
        <svg viewBox="0 0 24 24" className={`${sizeClass} ${className}`} role="img" aria-label="Estrela vazia" fill="none" stroke="currentColor" strokeWidth={2}>
          <path d={path} />
        </svg>
      );
    }
  
    return (
      <span className={`relative inline-block ${sizeClass} ${className}`} aria-label="Meia estrela" role="img">
        <svg viewBox="0 0 24 24" className="absolute inset-0 w-full h-full" fill="none" stroke="currentColor" strokeWidth={2} aria-hidden>
          <path d={path} />
        </svg>
        <span className="absolute inset-0 overflow-hidden" style={{ width: "50%" }} aria-hidden>
          <svg viewBox="0 0 24 24" className="absolute inset-0 w-full h-full" fill="currentColor">
            <path d={path} />
          </svg>
        </span>
        <svg viewBox="0 0 24 24" className="opacity-0 w-full h-full">
          <path d={path} />
        </svg>
      </span>
    );
  }

  function renderStars(value: number) {
    for (let i = 1; i <= 5; i++) {
      if (value >= i) items.push("full");
      else if (value >= i - 0.5) items.push("half");
      else items.push("empty");
    }
    return items;
  }

  return renderStars(rating).map((typeStart) => renderSVGStar(typeStart))
}

export default Stars
