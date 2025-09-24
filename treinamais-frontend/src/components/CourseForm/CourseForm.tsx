import React, { useMemo, useRef, useState } from "react";
import { useForm, Controller } from "react-hook-form";
import Layout from "../Layout";
import { centsToDisplay } from "../../utils/currency";

type FormValues = {
  name: string;
  instructor: string;
  priceCents: number;     // armazenado em centavos
  description: string;
  image: File | null;
};

type CourseFormProps = {
  defaultValues?: Partial<FormValues>;
  onSubmit: (data: FormValues) => void;
  isSubmitting?: boolean;
  className?: string;
};

function formatFileSize(bytes: number) {
  if (!bytes && bytes !== 0) return "";
  const units = ["B", "KB", "MB", "GB"];
  let i = 0;
  let v = bytes;
  while (v >= 1024 && i < units.length - 1) {
    v /= 1024;
    i++;
  }
  return `${v.toFixed(1)}${units[i]}`;
}


function displayToCents(input: string) {
  // mantém apenas dígitos; últimos 2 dígitos são os centavos
  const digits = (input || "").replace(/\D/g, "");
  const asNumber = Number(digits || "0");
  return asNumber;
}

const CourseForm: React.FC<CourseFormProps> = ({
  defaultValues,
  onSubmit,
  isSubmitting,
  className = "",
}) => {
  const {
    register,
    control,
    handleSubmit,
    setValue,
    watch,
    formState: { errors },
  } = useForm<FormValues>({
    defaultValues: {
      name: "",
      instructor: "",
      priceCents: 0,
      description: "",
      image: null,
      ...defaultValues,
    },
  });

  // Exibição amigável do preço
  const priceCents = watch("priceCents");
  const [priceDisplay, setPriceDisplay] = useState(centsToDisplay(priceCents));

  // Upload
  const image = watch("image");
  const inputFileRef = useRef<HTMLInputElement | null>(null);
  const dragging = useMemo(() => false, []); // placeholder para possível estado visual

  const onDropFiles = (files: FileList | null) => {
    if (!files || files.length === 0) return;
    const f = files[0];
    if (!f.type.startsWith("image/")) return; // somente imagem
    setValue("image", f, { shouldValidate: true });
  };

  const internalSubmit = handleSubmit((data) => {
    onSubmit({
      ...data,
      // garante coerência preço ao enviar
      priceCents: typeof data.priceCents === "number" ? data.priceCents : 0,
    });
  });

  return (
    <Layout>
      <h1 className="text-4xl font-extrabold text-center text-orange-500 mt-12 mb-12">Dados do produto</h1>

      <form
        onSubmit={internalSubmit}
        className={`w-full max-w-5xl mx-auto ${className}`}
      >
        {/* Linha do uploader + chip do arquivo */}
        <div className="grid grid-cols-1 md:grid-cols-[1fr,auto] gap-6 items-start">
          {/* Dropzone */}
          <Controller
            control={control}
            name="image"
            rules={{
              validate: (v) => {
                if (v && !v.type.startsWith("image/")) return "Apenas imagens são permitidas";
                if (v && v.size > 8 * 1024 * 1024) return "Arquivo deve ter no máximo 8MB";
                return true;
              },
            }}
            render={({ field: _ }) => (
              <div
                onDragOver={(e) => e.preventDefault()}
                onDrop={(e) => {
                  e.preventDefault();
                  onDropFiles(e.dataTransfer.files);
                }}
                onClick={() => inputFileRef.current?.click()}
                className={[
                  "rounded-xl border-2 border-dashed p-8 cursor-pointer",
                  "bg-white hover:bg-slate-50 transition-colors",
                  dragging ? "border-violet-400" : "border-slate-300",
                  "min-h-[180px] flex items-center justify-center text-center",
                ].join(" ")}
                role="button"
                aria-label="Clique ou arraste para adicionar uma imagem"
              >
                <div>
                  {/* Ícone (nuvem upload) */}
                  <svg
                    viewBox="0 0 24 24"
                    className="mx-auto h-10 w-10 text-slate-400 mb-3"
                    fill="none"
                    stroke="currentColor"
                    strokeWidth="1.5"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      d="M7 16a4 4 0 01.88-7.906A5 5 0 0119 9a3 3 0 01.176 5.995M12 12v9m0-9l-3 3m3-3l3 3"
                    />
                  </svg>
                  <p className="text-violet-600 font-medium">
                    Clique para adicionar uma imagem (opcional)
                  </p>
                  <p className="text-xs text-slate-500 mt-1">
                    PNG, JPG até 8MB - Se não enviada, será gerada automaticamente
                  </p>
                </div>

                <input
                  ref={inputFileRef}
                  type="file"
                  accept="image/*"
                  className="hidden"
                  onChange={(e) => {
                    onDropFiles(e.target.files);
                  }}
                />
              </div>
            )}
          />

          {/* Chip do arquivo selecionado */}
          {image ? (
            <div className="bg-slate-50 border border-slate-200 rounded-xl p-4 w-full md:w-[320px]">
              <div className="flex items-center gap-3">
                <div className="h-9 w-9 rounded-lg bg-violet-600 text-white flex items-center justify-center shrink-0">
                  <svg
                    viewBox="0 0 24 24"
                    className="h-5 w-5"
                    fill="none"
                    stroke="currentColor"
                    strokeWidth="1.8"
                  >
                    <path d="M4 7a2 2 0 012-2h5l2 2h5a2 2 0 012 2v8a2 2 0 01-2 2H6a2 2 0 01-2-2V7z" />
                  </svg>
                </div>
                <div className="flex-1 min-w-0">
                  <div className="text-sm text-slate-700 truncate">{image.name}</div>
                  <div className="text-xs text-slate-500">{formatFileSize(image.size)}</div>
                </div>
                <button
                  type="button"
                  onClick={() => setValue("image", null, { shouldValidate: true })}
                  className="text-slate-400 hover:text-slate-600"
                  aria-label="Remover arquivo"
                >
                  ×
                </button>
              </div>
            </div>
          ) : (
            <div className="w-full md:w-[320px]" />
          )}
        </div>

        {errors.image && (
          <p className="text-sm text-rose-600 mt-2">{String(errors.image.message)}</p>
        )}

        {/* Campos */}
        <div className="mt-8 grid grid-cols-1 gap-6">
          {/* Nome do curso */}
          <div>
            <label className="block text-slate-800 font-semibold mb-2">
              Nome do curso
            </label>
            <input
              {...register("name", { required: "Informe o nome do curso" })}
              placeholder="Nome"
              className="w-full rounded-lg bg-slate-50 border border-slate-200 px-4 py-3 outline-none focus:ring-2 focus:ring-violet-500"
            />
            {errors.name && (
              <p className="text-sm text-rose-600 mt-1">{errors.name.message}</p>
            )}
          </div>

          {/* Linha: Instrutor | Valor */}
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            <div>
              <label className="block text-slate-800 font-semibold mb-2">
                Instrutor
              </label>
              <input
                {...register("instructor", { required: "Informe o instrutor" })}
                placeholder="Instrutor"
                className="w-full rounded-lg bg-slate-50 border border-slate-200 px-4 py-3 outline-none focus:ring-2 focus:ring-violet-500"
              />
              {errors.instructor && (
                <p className="text-sm text-rose-600 mt-1">
                  {errors.instructor.message}
                </p>
              )}
            </div>

            <div>
              <label className="block text-slate-800 font-semibold mb-2">
                Valor
              </label>

              {/* Controlador para formatar R$ em tempo real sem libs externas */}
              <Controller
                control={control}
                name="priceCents"
                rules={{
                  validate: (v) =>
                    v >= 0 || "O valor não pode ser negativo",
                }}
                render={({ field: _ }) => (
                  <input
                    inputMode="numeric"
                    value={priceDisplay}
                    onChange={(e) => {
                      const cents = displayToCents(e.target.value);
                      setValue("priceCents", cents, { shouldValidate: true });
                      setPriceDisplay(centsToDisplay(cents));
                    }}
                    onBlur={() => setPriceDisplay(centsToDisplay(watch("priceCents")))}
                    placeholder="R$ 00,00"
                    className="w-full rounded-lg bg-slate-50 border border-slate-200 px-4 py-3 outline-none focus:ring-2 focus:ring-violet-500"
                  />
                )}
              />
              {errors.priceCents && (
                <p className="text-sm text-rose-600 mt-1">
                  {errors.priceCents.message as string}
                </p>
              )}
            </div>
          </div>

          {/* Descrição */}
          <div>
            <label className="block text-slate-800 font-semibold mb-2">
              Descrição
            </label>
            <textarea
              {...register("description", { required: "Descreva o curso" })}
              placeholder="Descrição detalhada"
              rows={6}
              className="w-full rounded-lg bg-slate-50 border border-slate-200 px-4 py-3 outline-none focus:ring-2 focus:ring-violet-500"
            />
            {errors.description && (
              <p className="text-sm text-rose-600 mt-1">
                {errors.description.message}
              </p>
            )}
          </div>
        </div>

        {/* Ações */}
        <div className="mt-8 w-full flex items-center justify-end gap-3">
          <div className="flex w-full justify-between gap-3">
            <button
              type="button"
              className="px-5 py-2 rounded-md border border-[#5B2DD1] text-[#5B2DD1] text-sm font-medium shadow-sm hover:bg-[#4B1FAF] hover:text-white"
              onClick={() => {
                setValue("name", "");
                setValue("instructor", "");
                setValue("priceCents", 0);
                setValue("description", "");
                setValue("image", null);
                setPriceDisplay(centsToDisplay(0));
              }}
            >
              Limpar
            </button>
            <button
              type="submit"
              className="px-5 py-2 rounded-md bg-[#5B2DD1] text-white text-sm font-medium shadow-sm hover:bg-[#4B1FAF]"
              disabled={isSubmitting}
            >
              {isSubmitting ? "Salvando..." : "Salvar curso"}

            </button>
          </div>
        </div>
      </form>
    </Layout>
  );
};

export default CourseForm
