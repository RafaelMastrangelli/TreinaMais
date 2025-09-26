import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Tabs from "../Tabs";
import Layout from "../Layout";
import Stars from "../Stars";
import type { CourseProps } from "./CourseList.types";

const CoursesList: React.FC<CourseProps> = ({ courses }) => {
  const [active, setActive] = useState('all');
  const navigate = useNavigate();

  return (
    <Layout>
      <h1 className="text-4xl font-extrabold text-center text-orange-500 mt-12 mb-12">Cursos Populares</h1>
      <div className="mb-12">
      <Tabs
        items={[
          { id: 'all', label: 'All Programme' },
          { id: 'uiux', label: 'Ui/Ux Design' },
          { id: 'prog', label: 'Program Design' },
        ]}
        activeId={active}
        onClick={(id) => setActive(id)}
      />
      </div>
      <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-6">
        {courses.map((course, idx) => (
          <div
            key={idx}
            className="rounded-3xl overflow-hidden bg-white shadow-xl ring-1 ring-gray-100 cursor-pointer"
            onClick={() => navigate(`/curso/${course.id}`)}
          >
            <div className="relative">
              <img 
                src={
                  course.coverUrl
                    || (course.imagemBytes ? `data:image/jpeg;base64,${course.imagemBytes}` : "/student-avatar.png")
                } 
                alt={course.nomeCurso}
                className="h-40 w-full object-cover"
                onError={(e) => { e.currentTarget.src = "/student-avatar.png"; }}
              />
              <div className="absolute left-4 -bottom-4">
                <div className="flex items-center gap-2 bg-white/95 backdrop-blur rounded-full px-3 py-1.5 shadow-md">
                  {course.averageRating ? (
                    <Stars rating={course.averageRating} className="text-yellow-400" />
                  ) : (
                    <span className="text-yellow-400 text-sm">⭐ Ver avaliação</span>
                  )}
                  <span className="text-gray-600 text-sm">({course.quantidadeReview} Reviews)</span>
                </div>
              </div>
            </div>
            <div className="p-6 pt-8 space-y-4 bg-gradient-to-b from-white to-purple-50/40">
              <div className="text-[11px] text-gray-400">
                {course.createdAtUtc ? new Date(course.createdAtUtc).toLocaleDateString('pt-BR') : 'Data não disponível'}
              </div>
              <h2 className="text-xl font-extrabold leading-snug text-[#5B2DD1]">
                {course.nomeCurso}
              </h2>
              <p className="text-sm text-gray-600">{course.resumo}</p>
              <div className="flex items-center justify-between pt-1">
                <div className="flex items-baseline gap-3">
                  <span className="text-2xl font-bold text-orange-500">R${course.valor}</span>
                </div>
                <button 
                  className="px-5 py-2 rounded-md bg-[#5B2DD1] text-white text-sm font-medium shadow-sm hover:bg-[#4B1FAF]"
                  onClick={(e) => {
                    e.stopPropagation();
                    navigate(`/curso/${course.id}`);
                  }}
                >
                  Comprar
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </Layout>
  )
}

export default CoursesList
