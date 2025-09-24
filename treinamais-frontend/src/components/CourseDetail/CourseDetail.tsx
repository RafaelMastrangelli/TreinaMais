import Layout from "../Layout";
import { TestimonialCard } from "../TestimonialCard/TestimonialCard";
import type { CourseDTO, ReviewDTO } from "../../services";

type CourseDetailsProps = {
  course: CourseDTO;
  reviews: ReviewDTO[];
}

const CourseDetails: React.FC<CourseDetailsProps> = ({ course, reviews }) => {
  return (
    <Layout>
      <h1 className="text-4xl font-extrabold text-center text-orange-500 mt-12 mb-12">Sobre o curso</h1>

      <p className="text-gray-700 text-center text-base/8 mb-8">
        {course.descricaoDetalhada}
      </p>

      <section>
        <h2 className="text-xl font-semibold uppercase text-center mb-8">O que eu vou aprender com o curso?</h2>
        <p className="text-gray-700 text-center text-base/8">
          {course.resumo || course.descricaoDetalhada}
        </p>
        <div className="text-center mt-4">
          <p className="text-sm text-gray-600">
            Instrutor: <span className="font-semibold">{course.instrutor}</span>
          </p>
        </div>
      </section>

      <section>
        <h2 className="text-xl font-semibold"></h2>
        <h1 className="text-4xl font-extrabold text-center text-orange-500 mt-12 mb-12">Reviews</h1>

        <div className="grid md:grid-cols-2 gap-4">
          {reviews.length > 0 ? (
            reviews.map((review, idx) => (
              <TestimonialCard
                key={idx}
                quote={review.descricao}
                rating={review.nota}
                authorName={review.authorName || 'Usuário Anônimo'}
                authorRole="Estudante"
                avatarUrl="/student-avatar.png"
                accent="orange"
              />
            ))
          ) : (
            <div className="col-span-2 text-center text-gray-500">
              <p>Nenhuma review encontrada para este curso.</p>
            </div>
          )}
        </div>
      </section>
    </Layout>
  )
}

export default CourseDetails
