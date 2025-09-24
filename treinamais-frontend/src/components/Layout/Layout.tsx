const Layout: React.FC<{ children: React.ReactNode}> = ({ children }) => (
  <div className="max-w-6xl mx-auto px-6 mb-24">
    {children}
  </div>
)

export default Layout