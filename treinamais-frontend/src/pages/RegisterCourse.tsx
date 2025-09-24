import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import BackgroundWaveIcon from '../assets/icons/BackgroundWaveIcon';
import CourseForm from '../components/CourseForm';
import Header from '../components/Header';
import { CoursesApi } from '../services';

const RegisterCourse: React.FC = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (data: any) => {
    try {
      setLoading(true);
      setError(null);
      
      // Map form data to API format
      const courseData = {
        nomeCurso: data.name,
        instrutor: data.instructor,
        descricaoDetalhada: data.description,
        priceCents: data.priceCents,
        ...(data.image && { imageFile: data.image })
      };
      
      await CoursesApi.create(courseData);
      
      // Redirect to course list after successful creation
      navigate('/');
    } catch (err) {
      setError('Erro ao cadastrar curso. Tente novamente.');
      console.error('Error creating course:', err);
    } finally {
      setLoading(false);
    }
  };

  return (
    <>
      <Header
        title={<h1 className='text-5xl font-semibold leading-snug w-100'>Cadastro de Produtos</h1>}
        icon={<BackgroundWaveIcon />}
      />
      {error && (
        <div className="max-w-2xl mx-auto mt-4 p-4 bg-red-100 border border-red-400 text-red-700 rounded">
          {error}
        </div>
      )}
      <CourseForm
        onSubmit={handleSubmit}
        isSubmitting={loading}
      />
      {loading && (
        <div className="max-w-2xl mx-auto mt-4 text-center">
          <div className="text-lg">Cadastrando curso...</div>
        </div>
      )}
    </>
  )
};

export default RegisterCourse;
