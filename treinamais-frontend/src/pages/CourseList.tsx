// import WaveIcons from '../assets/icons/waves-icon.png'
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import BackgroundWaveIcon from '../assets/icons/BackgroundWaveIcon';
import CoursesList from '../components/CourseList';
import Header from '../components/Header';
import SocialProof from '../components/SocialProof';
import { CoursesApi, type CourseDTO } from '../services';

const CourseListPage: React.FC = () => {
  const [courses, setCourses] = useState<CourseDTO[]>()
  const navigate = useNavigate()

  useEffect(() => {
    CoursesApi.list()
      .then((data) => setCourses(data))
  }, [setCourses])

  return (
    <>
      <Header
        title={<h1 className='text-5xl font-semibold leading-snug w-150 mb-8'>Aprimore suas habilidades com os cursos online da Treina+.</h1>}
        icon={""}
      >
        <div className="flex flex-col items-start gap-4">
          <SocialProof
            rating={4}
            reviewsLabel="(10k+ Reviews)"
            size="md"
            avatars={[
              { src: "/student-avatar.png", alt: "Cliente 1", ringClassName: "outline-rose-300" },
              { src: "/student-avatar.png", alt: "Cliente 2", ringClassName: "outline-amber-300" },
              { src: "/student-avatar.png", alt: "Cliente 3", ringClassName: "outline-cyan-300" },
            ]}
          />
          <button 
            onClick={() => navigate('/cadastro')}
            className="bg-blue-500 hover:bg-blue-600 text-white font-bold px-6 py-2 rounded-lg shadow-lg transition-colors duration-200"
          >
            + Adicionar Novo Curso
          </button>
        </div>
      </Header>
      {courses ? (
        <CoursesList courses={courses} />
      ) : 'Carregando...'}
    </>
  )
};

export default CourseListPage;
