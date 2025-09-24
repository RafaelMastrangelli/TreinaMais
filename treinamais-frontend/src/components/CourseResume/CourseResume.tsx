import type { CourseResumeProps } from "./CourseResume.types"
import Stars from "../Stars"

const CourseResume: React.FC<CourseResumeProps> = ({ title, description, reviews, date, imageUrl, rating = 0 }) => {
  return (
    < div className="rounded-3xl bg-white shadow-xl ring-1 ring-gray-100 overflow-hidden" >
      <div className="grid md:grid-cols-[320px_1fr]">
        {/* Capa/ilustração */}
        {imageUrl ? (
          <img
            src={imageUrl}
            alt={title}
            className="h-52 md:h-56 w-full object-cover"
          />
        ) : (
          <div className="bg-[#12B3AE] h-52 md:h-56" />
        )}

        {/* Conteúdo */}
        <div className="p-6 md:p-8">
          <div className="text-[12px] text-gray-400 mb-2">{date}</div>

          <h2 className="text-2xl font-extrabold text-[#5B2DD1] leading-snug">
            {title}
          </h2>

          <p className="text-gray-600 text-sm max-w-2xl mt-2">
            {description}
          </p>
          <div className="mt-5">
            <div className="inline-flex items-center gap-3 bg-white/95 backdrop-blur rounded-full px-4 py-2 shadow-md">
              <Stars rating={rating} className="text-yellow-400" />
              <span className="text-gray-600 text-sm">+ {reviews} Reviews</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}

export default CourseResume