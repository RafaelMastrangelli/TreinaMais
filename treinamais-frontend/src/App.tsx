import { Suspense, lazy } from 'react';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

const RegisterCourse = lazy(() => import('./pages/RegisterCourse'));
const Feedback = lazy(() => import('./pages/Feedback'));
const CourseDetail = lazy(() => import('./pages/CourseDetail'));
const CourseList = lazy(() => import('./pages/CourseList'));

const App: React.FC = () => {
  return (
    <BrowserRouter>
      <Suspense fallback={<div className="p-4 text-center">Carregando...</div>}>
        <Routes>
          <Route path="/" element={<CourseList />} />
          <Route path="/cadastro" element={<RegisterCourse />} />
          <Route path="/feedback" element={<Feedback />} />
          <Route path="/curso/:id" element={<CourseDetail />} />
        </Routes>
      </Suspense>
    </BrowserRouter>
  );
};

export default App
