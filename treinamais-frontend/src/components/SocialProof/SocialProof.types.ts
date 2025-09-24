type Avatar = {
  src: string;
  alt?: string;
  ringClassName?: string;
};

type Size = "sm" | "md" | "lg";

export type SocialProofProps = {
  avatars: Avatar[];
  rating: number;
  reviewsLabel?: string;
  size?: Size;
  className?: string;
  onClick?: () => void;
}
