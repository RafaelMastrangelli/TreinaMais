type TabItem = { id: string; label: string }

export type TabsProps = {
  items: TabItem[];
  activeId: string;
  onClick: (id: string, index: number) => void;
  className?: string;
}
