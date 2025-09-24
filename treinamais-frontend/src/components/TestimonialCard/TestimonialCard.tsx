import { ACCENT_MAP } from "./TestimonialCard.constants";
import type { TestimonialCardProps } from "./TestimonialCard.types";

function Star({ filled, half = false, className = "" }: { filled: boolean; half?: boolean; className?: string }) {
  const path = "M12 2.5l2.962 5.997 6.621.963-4.791 4.666 1.13 6.591L12 17.97 6.078 20.717l1.13-6.591-4.79-4.666 6.62-.963L12 2.5z";
  if (half) {
    return (
      <span className={`relative inline-block ${className}`} aria-label="Meia estrela" role="img">
        <svg viewBox="0 0 24 24" className="absolute inset-0" fill="none" stroke="currentColor" strokeWidth={2} aria-hidden>
          <path d={path} />
        </svg>
        <span className="absolute inset-0 overflow-hidden" style={{ width: "50%" }} aria-hidden>
          <svg viewBox="0 0 24 24" className="absolute inset-0" fill="currentColor"><path d={path} /></svg>
        </span>
        <svg viewBox="0 0 24 24" className="opacity-0"><path d={path} /></svg>
      </span>
    );
  }
  return filled ? (
    <svg viewBox="0 0 24 24" className={className} role="img" aria-label="Estrela cheia" fill="currentColor">
      <path d={path} />
    </svg>
  ) : (
    <svg viewBox="0 0 24 24" className={className} role="img" aria-label="Estrela vazia" fill="none" stroke="currentColor" strokeWidth={2}>
      <path d={path} />
    </svg>
  );
}

function getStarTypes(value = 5) {
  const items: Array<"full" | "half" | "empty"> = [];
  for (let i = 1; i <= 5; i++) {
    if (value >= i) items.push("full");
    else if (value >= i - 0.5) items.push("half");
    else items.push("empty");
  }
  return items;
}

export const TestimonialCard: React.FC<TestimonialCardProps> = ({
  quote,
  rating = 5,
  authorName,
  authorRole,
  avatarUrl,
  className = "",
  accent = "orange",
}) => {
  const a = ACCENT_MAP[accent];

  return (
    <figure
      className={[
        "max-w-3xl rounded-2xl p-6 md:p-8",
        "border-2 border-dashed",
        a.border,
        "shadow-sm",
        className,
      ].join(" ")}
    >
      <div className={`flex items-center gap-1 ${a.star} mb-4`} aria-label={`Avaliação ${rating} de 5`}>
        {getStarTypes(rating).map((t, i) => (
          <Star key={i} filled={t === "full"} half={t === "half"} className="h-5 w-5" />
        ))}
      </div>

      <blockquote className="text-slate-700 text-lg md:text-xl leading-8 md:leading-9 italic mb-6">
        <span className="select-none mr-1">“</span>
        {quote}
        <span className="select-none ml-1">”</span>
      </blockquote>

      <figcaption className="flex items-center gap-3">
        <img
          src={avatarUrl}
          alt={`Foto de ${authorName}`}
          className="h-12 w-12 rounded-full object-cover ring-2 ring-white shadow-sm"
        />
        <div className="leading-tight">
          <div className={`font-semibold ${a.name}`}>{authorName}</div>
          <div className="text-slate-500 text-sm">{authorRole}</div>
        </div>
      </figcaption>
    </figure>
  );
};
