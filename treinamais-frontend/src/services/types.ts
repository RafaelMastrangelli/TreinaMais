export interface CourseDTO {
  id: number;
  nomeCurso: string;
  instrutor: string;
  valor: number;
  descricaoDetalhada: string;
  quantidadeReview: number;
  averageRating?: number;
  resumo?: string;
  imagemUrl?: string;
  imagemBytes?: string;
  coverUrl?: string;
  createdAtUtc?: string;
  updatedAtUtc?: string;
}

export interface CreateCourseRequest {
  nomeCurso: string;
  instrutor: string;
  valor: number;
  descricaoDetalhada: string;
  resumo?: string;
  imagemBytes?: string;
}

export interface ReviewDTO {
  id: number;
  courseId: number;
  nota: number;
  descricao: string;
  createdAtUtc?: string;
  authorName?: string;
}

export interface CreateReviewRequest {
  nota: number;
  descricao: string;
  authorName?: string;
}
