import type { Accent } from "./TestimonialCard.types";

export const ACCENT_MAP: Record<Accent, { border: string; star: string; name: string }> = {
  orange: { border: "border-orange-400", star: "text-orange-500", name: "text-sky-700" },
  amber:  { border: "border-amber-400",  star: "text-amber-500",  name: "text-sky-700" },
  rose:   { border: "border-rose-400",   star: "text-rose-500",   name: "text-sky-700" },
  violet: { border: "border-violet-400", star: "text-violet-500", name: "text-sky-700" },
  blue:   { border: "border-blue-400",   star: "text-blue-500",   name: "text-blue-700" },
  emerald:{ border: "border-emerald-400",star: "text-emerald-500",name: "text-sky-700" },
}
