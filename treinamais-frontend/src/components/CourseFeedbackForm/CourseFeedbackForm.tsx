import { useState } from 'react';
import { useForm } from 'react-hook-form'
import { useNavigate } from 'react-router-dom';
import CourseResume from '../CourseResume/CourseResume';
import Layout from '../Layout';
import { ReviewsApi, type CourseDTO, type ReviewDTO } from '../../services';
import { calculateAverageRating } from '../../utils/rating';

type FeedbackFormValues = {
  courseId: number;
  rating: number;
  description: string;
  authorName: string;
}

type CourseFeedbackFormProps = {
  courses: CourseDTO[];
  loading: boolean;
  onSuccess: () => void;
}

const CourseFeedbackForm: React.FC<CourseFeedbackFormProps> = ({ courses, loading, onSuccess }) => {
  const [selectedCourse, setSelectedCourse] = useState<CourseDTO | null>(null);
  const [courseReviews, setCourseReviews] = useState<ReviewDTO[]>([]);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();
  
  const { register, handleSubmit, setValue, watch, reset } = useForm<FeedbackFormValues>({
    defaultValues: {
      courseId: 0,
      rating: 0,
      description: '',
      authorName: '',
    },
  })

  const rating = watch('rating');

  const fetchCourseReviews = async (courseId: number) => {
    try {
      const reviews = await ReviewsApi.listByCourse(courseId);
      setCourseReviews(reviews);
    } catch (err) {
      console.error('Error fetching course reviews:', err);
      setCourseReviews([]);
    }
  };

  const onSubmit = async (data: FeedbackFormValues) => {
    if (!selectedCourse) {
      setError('Por favor, selecione um curso.');
      return;
    }
    
    try {
      setSubmitting(true);
      setError(null);
      
      await ReviewsApi.create(selectedCourse.id, {
        nota: data.rating,
        descricao: data.description,
        authorName: data.authorName || 'Usuário Anônimo'
      });
      
      reset();
      setSelectedCourse(null);
      onSuccess();
    } catch (err) {
      setError('Erro ao enviar feedback. Tente novamente.');
      console.error('Error submitting review:', err);
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <Layout>
      <h1 className="text-4xl font-extrabold text-center text-blue-500 mt-12 mb-12">Review do produto</h1>
      {selectedCourse && (
        <div className='mb-12'>
          <CourseResume
            title={selectedCourse.nomeCurso}
            description={selectedCourse.descricaoDetalhada}
            reviews={selectedCourse.quantidadeReview?.toString() || '0'}
            date={selectedCourse.createdAtUtc ? new Date(selectedCourse.createdAtUtc).toLocaleDateString('pt-BR') : ''}
            imageUrl={selectedCourse.coverUrl || selectedCourse.imagemUrl}
            rating={calculateAverageRating(courseReviews)}
          />
        </div>
      )}
      {error && (
        <div className="mb-4 p-4 bg-red-100 border border-red-400 text-red-700 rounded">
          {error}
        </div>
      )}
      
      <form onSubmit={handleSubmit(onSubmit)} className="mx-auto w-full">
        <div className="mb-6">
          <label className="block text-2xl font-bold text-gray-700 mb-2">Selecionar Curso</label>
          {loading ? (
            <div>Carregando cursos...</div>
          ) : (
            <select 
              className="w-full p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
              value={selectedCourse?.id || ''}
              onChange={(e) => {
                const course = courses.find(c => c.id === parseInt(e.target.value));
                setSelectedCourse(course || null);
                setValue('courseId', parseInt(e.target.value));
                if (course) {
                  fetchCourseReviews(course.id);
                } else {
                  setCourseReviews([]);
                }
              }}
              required
            >
              <option value="">Selecione um curso...</option>
              {courses.map(course => (
                <option key={course.id} value={course.id}>
                  {course.nomeCurso} - {course.instrutor}
                </option>
              ))}
            </select>
          )}
        </div>
        
        <div className="mb-6">
          <label className="block text-2xl font-bold text-gray-700 mb-2">Seu Nome</label>
          <input
            {...register('authorName')}
            type="text"
            placeholder="Seu nome (opcional)"
            className="w-full p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>
        
        <div className="grid grid-cols-1 md:grid-cols-2 mb-12">
          <div>
            <label className="block text-2xl font-bold text-gray-700 mb-1">Classificação</label>
            <div className="flex items-center gap-1">
              {[1, 2, 3, 4, 5].map(n => (
                <button
                  key={n}
                  type="button"
                  aria-label={`Definir ${n} estrelas`}
                  onClick={() => setValue('rating', n)}
                  className={`text-4xl border-gray-300 ${n <= rating ? 'text-yellow-500' : 'text-gray-300'}`}
                >
                  ★
                </button>
              ))}
            </div>
          </div>
        </div>
        <div className="mb-12">
          <label className="block text-2xl font-bold text-gray-700 mb-1">Descrição</label>
          <textarea
            {...register('description')}
            placeholder="Descrição detalhada"
            className="w-full min-h-[120px] rounded-sm bg-gray-100 p-4 text-gray-600 focus:outline-none focus:ring-2 focus:ring-blue-500"
          />
        </div>
        <div className="flex justify-between gap-3">
          <button 
            type="button" 
            onClick={() => navigate('/')}
            className="px-5 py-2 rounded-md border border-blue-600 text-blue-600 text-sm font-medium shadow-sm hover:bg-blue-600 hover:text-white"
          >
            Voltar
          </button>
          <button 
            type="submit" 
            disabled={!selectedCourse || submitting}
            className="px-5 py-2 rounded-md bg-blue-600 text-white text-sm font-medium shadow-sm hover:bg-blue-700 disabled:opacity-50 disabled:cursor-not-allowed"
          >
            {submitting ? 'Enviando...' : 'Salvar'}
          </button>
        </div>
      </form>
    </Layout>
  );
};

export default CourseFeedbackForm
