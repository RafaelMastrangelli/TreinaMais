import type { TabsProps } from "./Tabs.types"

const Tabs: React.FC<TabsProps> = ({ items, activeId, onClick, className = '' }) => {
  return (
    <div role="tablist" aria-label="Categorias" className={`flex flex-wrap items-center justify-center gap-6 ${className}`}>
      {items.map((item, idx) => {
        const active = item.id === activeId;
        return (
          <button
            key={item.id}
            role="tab"
            aria-selected={active}
            onClick={() => onClick(item.id, idx)}
            className={[
              'px-5 py-2.5 rounded-lg text-sm font-medium transition-colors',
              active
                ? 'bg-[#5B2DD1] text-white shadow-md'
                : 'bg-white text-gray-700 border border-gray-300 hover:border-gray-400',
            ].join(' ')}
          >
            {item.label}
          </button>
        )
      })}
    </div>
  )
}

export default Tabs
