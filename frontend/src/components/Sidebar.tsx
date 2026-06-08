import { NavLink } from 'react-router-dom';

const navItems = [
  { label: 'Dashboard', path: '/', icon: 'M10 2a8 8 0 100 16 8 8 0 000-16z' },
  { label: 'Portfolios', path: '/portfolios', icon: 'M4 6h16M4 10h16M4 14h16M4 18h16' },
  { label: 'Transactions', path: '/transactions', icon: 'M6 12l4 4 8-8' },
  { label: 'Users', path: '/users', icon: 'M12 12c2.76 0 5-2.24 5-5S14.76 2 12 2 7 4.24 7 7s2.24 5 5 5zm0 2c-3.33 0-10 1.67-10 5v1h20v-1c0-3.33-6.67-5-10-5z' },
];

function Sidebar() {
  return (
    <aside className="hidden w-[280px] flex-col border-r border-glass bg-glass p-6 text-slate-100 md:flex">
      <div className="mb-8 flex items-center gap-3">
        <div className="flex h-12 w-12 items-center justify-center rounded-2xl bg-gradient-to-br from-cyan-400 to-sky-800 text-black shadow-glow">
          F
        </div>
        <div>
          <p className="text-sm uppercase tracking-[0.32em] text-slate-400">FinTrack</p>
          <h1 className="text-xl font-semibold">Analytics</h1>
        </div>
      </div>

      <nav className="space-y-2">
        {navItems.map((item) => (
          <NavLink
            key={item.path}
            to={item.path}
            className={({ isActive }) =>
              `flex items-center gap-3 rounded-3xl px-4 py-3 text-sm font-medium transition ${
                isActive
                  ? 'bg-cyan-500/15 text-cyan-200 ring-1 ring-cyan-500/40'
                  : 'text-slate-300 hover:bg-white/5 hover:text-cyan-100'
              }`
            }
          >
            <svg viewBox="0 0 24 24" className="h-5 w-5" fill="none" stroke="currentColor" strokeWidth="1.8">
              <path d={item.icon} />
            </svg>
            {item.label}
          </NavLink>
        ))}
      </nav>

      <div className="mt-auto rounded-3xl border border-white/5 bg-slate-950/70 p-4 text-sm text-slate-300">
        <p className="font-semibold text-slate-100">Insights</p>
        <p className="mt-2 text-slate-400">Connected to a local API at <span className="text-cyan-300">localhost:5062</span>.</p>
      </div>
    </aside>
  );
}

export default Sidebar;
