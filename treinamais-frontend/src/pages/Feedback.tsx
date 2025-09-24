import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import CourseFeedbackForm from '../components/CourseFeedbackForm';
import Header from '../components/Header';
import BackgroundWaveIcon from '../assets/icons/BackgroundWaveIcon';
import { CoursesApi, type CourseDTO } from '../services';

const Feedback: React.FC = () => {
  const navigate = useNavigate();
  const [courses, setCourses] = useState<CourseDTO[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchCourses = async () => {
      try {
        const data = await CoursesApi.list();
        setCourses(data);
      } catch (err) {
        console.error('Error fetching courses:', err);
      } finally {
        setLoading(false);
      }
    };

    fetchCourses();
  }, []);

  return (
    <>
      <Header
        title={<h1 className='text-5xl font-semibold leading-snug w-100'>Cadastro de Feedback</h1>}
        icon={<BackgroundWaveIcon />}
      />
      <CourseFeedbackForm 
        courses={courses} 
        loading={loading}
        onSuccess={() => navigate('/')}
      />
    </>
  );
};

export default Feedback;
