import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { api } from '../services/api';
import { Portfolio } from '../types';
import SkeletonCard from '../components/SkeletonCard';

function Portfolios() {
  const [portfolios, setPortfolios] = useState<Portfolio[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function load() {
      try {
        setLoading(true);
        const response = await api.get<Portfolio[]>('/api/portfolio');
        setPortfolios(response.data || []);
      } finally {
        setLoading(false);
      }
    }
    load();
  }, []);

  return (
    <div className="space-y-6">
      <div className="rounded-[32px] border border-white/10 bg-glass p-6 shadow-glow">
        <h2 className="text-3xl font-semibold">Portfolios</h2>
        <p className="mt-2 text-slate-400">Track every portfolio with aggregated performance and distribution.</p>
      </div>

      <div className="grid gap-6 lg:grid-cols-2">
        {loading
          ? Array.from({ length: 4 }).map((_, idx) => <SkeletonCard key={idx} className="h-40" />)
          : portfolios.map((portfolio) => (
              <Link
                key={portfolio.id}
                to={`/portfolios/${portfolio.id}`}
                className="group rounded-[32px] border border-white/10 bg-glass p-6 transition hover:border-cyan-300/20 hover:shadow-glow"
              >
                <div className="flex items-center justify-between gap-4">
                  <div>
                    <p className="text-sm uppercase tracking-[0.3em] text-slate-400">Portfolio</p>
                    <h3 className="mt-3 text-2xl font-semibold text-white">{portfolio.name || 'Untitled portfolio'}</h3>
                  </div>
                  <span className="rounded-3xl bg-slate-900/70 px-4 py-2 text-sm text-slate-300">View</span>
                </div>
                <div className="mt-4 flex flex-wrap gap-3 text-sm text-slate-300">
                  <span className="rounded-3xl bg-slate-950/70 px-3 py-2">Assets {portfolio.assets?.length ?? '–'}</span>
                  <span className="rounded-3xl bg-slate-950/70 px-3 py-2">Value {portfolio.totalValue ? `$${portfolio.totalValue.toLocaleString()}` : 'N/A'}</span>
                </div>
              </Link>
            ))}
      </div>
    </div>
  );
}

export default Portfolios;
