import type { ReviewDTO } from '../services/types';

/**
 * Calculates the average rating from an array of reviews
 * @param reviews Array of ReviewDTO objects
 * @returns Average rating rounded to 1 decimal place, or 0 if no reviews
 */
export function calculateAverageRating(reviews: ReviewDTO[]): number {
  if (!reviews || reviews.length === 0) {
    return 0;
  }

  const validReviews = reviews.filter(review =>
    review.nota && review.nota >= 1 && review.nota <= 5
  );

  if (validReviews.length === 0) {
    return 0;
  }

  const sum = validReviews.reduce((acc, review) => acc + review.nota, 0);
  const average = sum / validReviews.length;

  return Math.round(average * 10) / 10;
}