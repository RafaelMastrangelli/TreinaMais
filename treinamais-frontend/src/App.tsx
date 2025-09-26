import { Suspense, lazy } from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider, useAuth } from './contexts/AuthContext';
import ProtectedRoute from './components/ProtectedRoute';

const RegisterCourse = lazy(() => import('./pages/RegisterCourse'));
const Feedback = lazy(() => import('./pages/Feedback'));
const CourseDetail = lazy(() => import('./pages/CourseDetail'));
const CourseList = lazy(() => import('./pages/CourseList'));
const Login = lazy(() => import('./pages/Login'));

const AppRoutes: React.FC = () => {
  const { isAuthenticated, isLoading } = useAuth();

  if (isLoading) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-center">
          <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-[#5B2DD1] mx-auto"></div>
          <p className="mt-4 text-gray-600">Carregando...</p>
        </div>
      </div>
    );
  }

  return (
    <Routes>
      <Route 
        path="/" 
        element={isAuthenticated ? <CourseList /> : <Navigate to="/login" replace />} 
      />
      <Route path="/login" element={<Login />} />
      <Route 
        path="/cadastro" 
        element={
          <ProtectedRoute>
            <RegisterCourse />
          </ProtectedRoute>
        } 
      />
      <Route 
        path="/feedback" 
        element={
          <ProtectedRoute>
            <Feedback />
          </ProtectedRoute>
        } 
      />
      <Route 
        path="/curso/:id" 
        element={
          <ProtectedRoute>
            <CourseDetail />
          </ProtectedRoute>
        } 
      />
    </Routes>
  );
};

const App: React.FC = () => {
  return (
    <BrowserRouter>
      <AuthProvider>
        <Suspense fallback={<div className="p-4 text-center">Carregando...</div>}>
          <AppRoutes />
        </Suspense>
      </AuthProvider>
    </BrowserRouter>
  );
};

export default App
