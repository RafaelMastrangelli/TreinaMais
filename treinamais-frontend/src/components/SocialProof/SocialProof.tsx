import Stars from "../Stars";
import { SIZE_MAP } from "./SocialProof.constants";
import type { SocialProofProps } from "./SocialProof.types";

const SocialProof: React.FC<SocialProofProps> = ({
  avatars,
  rating,
  reviewsLabel = "(10k+ Reviews)",
  size = "md",
  className = "",
  onClick,
}) => {
  const sizeClass = SIZE_MAP[size];

  return (
    <div
      className={[
        "flex items-center rounded-full text-white",
        sizeClass.pad,
        sizeClass.gap,
        onClick ? "cursor-pointer transition hover:brightness-110 active:brightness-95" : "",
        className,
      ].join(" ")}
      onClick={onClick}
      aria-label={`Avaliação ${rating} de 5`}
    >
      <div className="flex items-center">
        <div className="flex -space-x-3">
          {avatars.slice(0, 5).map((a, i) => (
            <div
              key={`${a.src}-${i}`}
              className={[
                "relative rounded-full",
                sizeClass.avatar,
                "ring-2 ring-white",
                a.ringClassName ?? "",
              ].join(" ")}
              style={{ outlineWidth: "2px", outlineStyle: a.ringClassName ? "solid" : undefined }}
            >
              <img
                src={a.src}
                alt={a.alt ?? `Avatar ${i + 1}`}
                className="h-full w-full rounded-full object-cover"
              />
            </div>
          ))}
        </div>
      </div>
      <div>
        <div className="flex items-center gap-1 text-white" aria-hidden>
          <Stars rating={rating} className={sizeClass.star} />
        </div>
        <span className={`${sizeClass.text} text-white/90`}>{reviewsLabel}</span>
      </div>
    </div>
  );
};

export default SocialProof
