import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import BackgroundWaveIcon from '../assets/icons/BackgroundWaveIcon';
import CourseDetail from '../components/CourseDetail';
import Header from '../components/Header';
import SocialProof from '../components/SocialProof';
import { CoursesApi, ReviewsApi, type CourseDTO, type ReviewDTO } from '../services';
import { calculateAverageRating } from '../utils/rating';
import { formatCurrency } from '../utils/currency';

const CourseDetailPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [course, setCourse] = useState<CourseDTO | null>(null);
  const [reviews, setReviews] = useState<ReviewDTO[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (id) {
      const fetchCourseData = async () => {
        try {
          setLoading(true);
          const courseId = parseInt(id);
          
          const [courseData, reviewsData] = await Promise.all([
            CoursesApi.getById(courseId),
            ReviewsApi.listByCourse(courseId)
          ]);
          
          setCourse(courseData);
          setReviews(reviewsData);
        } catch (err) {
          setError('Erro ao carregar dados do curso');
          console.error('Error fetching course data:', err);
        } finally {
          setLoading(false);
        }
      };

      fetchCourseData();
    }
  }, [id]);

  if (loading) {
    return (
      <div className="flex justify-center items-center min-h-screen">
        <div className="text-2xl">Carregando...</div>
      </div>
    );
  }

  if (error || !course) {
    return (
      <div className="flex justify-center items-center min-h-screen">
        <div className="text-2xl text-red-500">{error || 'Curso não encontrado'}</div>
      </div>
    );
  }

  return (
    <>
      <Header
        title={<h1 className='text-5xl font-semibold leading-snug w-150 mb-8'>{course.nomeCurso}</h1>}
        icon={<BackgroundWaveIcon />}
      >
        <>
          <SocialProof
            rating={calculateAverageRating(reviews)}
            reviewsLabel={`(${course.quantidadeReview || reviews.length}+ Reviews)`}
            className="mb-4"
            size="md"
            avatars={[
              { src: "/student-avatar.png", alt: "Cliente 1", ringClassName: "outline-rose-300" },
              { src: "/student-avatar.png", alt: "Cliente 2", ringClassName: "outline-amber-300" },
              { src: "/student-avatar.png", alt: "Cliente 3", ringClassName: "outline-cyan-300" },
            ]}
          />
          <div className="flex items-baseline gap-3 mb-4">
            <h1 className="text-3xl font-bold text-white">{formatCurrency(course.valor)}</h1>
          </div>
          <div className="flex gap-3">
            <button 
              onClick={() => navigate('/')}
              className="bg-gray-200 text-gray-800 font-bold px-6 py-2 rounded hover:bg-gray-300"
            >
              Voltar ao Início
            </button>
            <button className="bg-orange-500 text-white font-bold px-6 py-2 rounded hover:bg-orange-600">
              Comprar Agora
            </button>
            <button 
              onClick={() => navigate('/feedback')}
              className="bg-blue-500 text-white font-bold px-6 py-2 rounded hover:bg-blue-600"
            >
              Adicionar Review
            </button>
          </div>
        </>
      </Header>
      <CourseDetail course={course} reviews={reviews} />
    </>
  )
};

export default CourseDetailPage;
