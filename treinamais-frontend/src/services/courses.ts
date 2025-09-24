import api from "./config/client";
import { toApiError } from "./config/errors";
import { fileToBase64Bytes, centsToDecimal } from "./config/helpers";
import type { CourseDTO, CreateCourseRequest } from "./types";

export const CoursesApi = {
  /** GET /api/courses */
  async list(signal?: AbortSignal): Promise<CourseDTO[]> {
    try {
      const { data } = await api.get<CourseDTO[]>("/api/courses", { signal });
      return data;
    } catch (err) {
      throw toApiError(err);
    }
  },

  /** GET /api/courses/{id} */
  async getById(id: number, signal?: AbortSignal): Promise<CourseDTO> {
    try {
      const { data } = await api.get<CourseDTO>(`/api/courses/${id}`, { signal });
      return data;
    } catch (err) {
      throw toApiError(err);
    }
  },

  /**
   * POST /api/courses
   * Aceita:
   * - valorDecimal (ex.: 299.90) OU priceCents (ex.: 29990)
   * - imageFile opcional (File) -> será convertido para base64 como imagemBytes
   */
  async create(input: {
    nomeCurso: string;
    instrutor: string;
    descricaoDetalhada: string;
    resumo?: string;
    valorDecimal?: number;
    priceCents?: number;
    imageFile?: File;
  }): Promise<CourseDTO> {
    try {
      const valorEmDecimal = centsToDecimal(input.priceCents ?? 0);

      const imagemBytes = input.imageFile ? 
        await fileToBase64Bytes(input.imageFile) :
        undefined;

      const payload: CreateCourseRequest = {
        nomeCurso: input.nomeCurso,
        instrutor: input.instrutor,
        valor: valorEmDecimal,
        descricaoDetalhada: input.descricaoDetalhada,
        resumo: '',
        imagemBytes: imagemBytes
      };

      const { data } = await api.post<CourseDTO>("/api/courses", payload);

      return data;
    } catch (err) {
      throw toApiError(err);
    }
  },
};
