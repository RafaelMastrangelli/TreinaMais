import api from "./config/client";
import { toApiError } from "./config/errors";
import type { ReviewDTO, CreateReviewRequest } from "./types";

export const ReviewsApi = {
  /** GET /api/courses/{courseId}/reviews */
  async listByCourse(courseId: number, signal?: AbortSignal): Promise<ReviewDTO[]> {
    try {
      const { data } = await api.get<ReviewDTO[]>(
        `/api/courses/${courseId}/reviews`,
        { signal }
      );
      return data;
    } catch (err) {
      throw toApiError(err);
    }
  },

  /** POST /api/courses/{courseId}/reviews */
  async create(courseId: number, input: CreateReviewRequest): Promise<ReviewDTO> {
    try {
      const { data } = await api.post<ReviewDTO>(`/api/courses/${courseId}/reviews`, input);

      return data;
    } catch (err) {
      throw toApiError(err);
    }
  },
};
