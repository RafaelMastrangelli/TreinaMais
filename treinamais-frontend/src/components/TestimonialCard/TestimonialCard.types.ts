export type Accent = "orange" | "amber" | "rose" | "violet" | "blue" | "emerald";

export type TestimonialCardProps = {
  quote: string;
  rating?: number;
  authorName: string;
  authorRole: string;
  avatarUrl: string;
  className?: string;
  accent?: Accent;
}
