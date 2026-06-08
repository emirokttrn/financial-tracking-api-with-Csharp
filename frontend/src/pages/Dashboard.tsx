import { useEffect, useMemo, useState } from 'react';
import { LineChart, Line, ResponsiveContainer } from 'recharts';
import { api } from '../services/api';
import { CoinSummary, Portfolio } from '../types';
import SkeletonCard from '../components/SkeletonCard';

function formatCurrency(value: number) {
  return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(value);
}

function Dashboard() {
  const [coins, setCoins] = useState<CoinSummary[]>([]);
  const [portfolios, setPortfolios] = useState<Portfolio[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let mounted = true;

    async function load() {
      try {
        setLoading(true);
        const [coinsResponse, portfolioResponse] = await Promise.all([
          api.get<CoinSummary[]>('/api/coingecko/top?count=10'),
          api.get<Portfolio[]>('/api/portfolio'),
        ]);
        if (!mounted) return;
        setCoins(coinsResponse.data || []);
        setPortfolios(portfolioResponse.data || []);
      } finally {
        if (mounted) setLoading(false);
      }
    }

    load();
    const interval = window.setInterval(load, 30000);
    return () => {
      mounted = false;
      window.clearInterval(interval);
    };
  }, []);

  const totalValue = useMemo(
    () =>
      portfolios.reduce((sum, portfolio) => sum + (portfolio.totalValue ?? 0), 0),
    [portfolios],
  );

  return (
    <div className="space-y-6">
      <header className="rounded-[32px] border border-white/10 bg-glass p-6 shadow-glow">
        <div className="flex flex-col gap-4 md:flex-row md:items-end md:justify-between">
          <div>
            <p className="text-sm uppercase tracking-[0.3em] text-slate-400">Overview</p>
            <h2 className="mt-2 text-3xl font-semibold">Portfolio intelligence</h2>
          </div>
          <div className="rounded-3xl bg-slate-950/70 px-4 py-3 text-slate-300 ring-1 ring-white/10">
            Live sync with CoinGecko data every 30 seconds
          </div>
        </div>
      </header>

      <div className="grid gap-6 xl:grid-cols-[1.8fr_1fr]">
        <div className="grid gap-6">
          <div className="rounded-[32px] border border-white/10 bg-glass p-6 shadow-glow">
            <div className="flex items-center justify-between gap-4">
              <div>
                <p className="text-sm uppercase tracking-[0.3em] text-slate-400">Total portfolio value</p>
                <h3 className="mt-3 text-4xl font-semibold text-white">{loading ? '--' : formatCurrency(totalValue || 0)}</h3>
              </div>
              <div className="rounded-3xl bg-cyan-500/10 px-4 py-3 text-cyan-200 ring-1 ring-cyan-500/15">
                {portfolios.length} portfolios
              </div>
            </div>
            <div className="mt-6 grid grid-cols-1 gap-3 sm:grid-cols-3">
              {[
                { title: 'Active coins', value: coins.length, color: 'text-cyan-300' },
                { title: '24h average change', value: `${coins.length ? coins.reduce((sum, c) => sum + (c.price_change_percentage_24h ?? 0), 0) / coins.length : 0}%`, color: 'text-emerald-300' },
                { title: 'Live refresh', value: '30s interval', color: 'text-slate-300' },
              ].map((stat) => (
                <div key={stat.title} className="rounded-3xl bg-slate-950/50 p-4">
                  <p className="text-xs uppercase tracking-[0.3em] text-slate-400">{stat.title}</p>
                  <p className={`mt-3 text-xl font-semibold ${stat.color}`}>{stat.value}</p>
                </div>
              ))}
            </div>
          </div>

          <section className="grid gap-6 md:grid-cols-2">
            {loading ? (
              Array.from({ length: 2 }).map((_, index) => <SkeletonCard key={index} className="h-44" />)
            ) : (
              coins.slice(0, 2).map((coin) => {
                const positive = (coin.price_change_percentage_24h ?? 0) >= 0;
                return (
                  <div key={coin.id} className="rounded-[32px] border border-white/10 bg-glass p-5 shadow-glow">
                    <div className="flex items-center justify-between gap-4">
                      <div>
                        <p className="text-sm uppercase tracking-[0.3em] text-slate-400">{coin.name}</p>
                        <h3 className="mt-3 text-3xl font-semibold text-white">{formatCurrency(coin.current_price)}</h3>
                      </div>
                      <div className={`rounded-3xl px-3 py-2 text-sm font-semibold ${positive ? 'bg-emerald-400/10 text-emerald-300' : 'bg-rose-400/10 text-rose-300'}`}>
                        {positive ? '+' : ''}{coin.price_change_percentage_24h?.toFixed(2)}%
                      </div>
                    </div>
                    <div className="mt-5 h-28">
                      <ResponsiveContainer width="100%" height="100%">
                        <LineChart data={(coin.sparkline_in_7d?.price ?? []).map((value, idx) => ({ idx, value }))}>
                          <Line type="monotone" dataKey="value" stroke="#00d4ff" strokeWidth={2} dot={false} />
                        </LineChart>
                      </ResponsiveContainer>
                    </div>
                  </div>
                );
              })
            )}
          </section>
        </div>

        <div className="rounded-[32px] border border-white/10 bg-glass p-6 shadow-glow">
          <div className="flex items-center justify-between gap-3">
            <div>
              <p className="text-sm uppercase tracking-[0.3em] text-slate-400">Market pulse</p>
              <h3 className="mt-2 text-2xl font-semibold text-white">Top 10 crypto movers</h3>
            </div>
            <span className="rounded-full bg-slate-900/70 px-3 py-2 text-xs uppercase tracking-[0.3em] text-slate-300">Live</span>
          </div>

          <div className="mt-6 space-y-4">
            {loading
              ? Array.from({ length: 5 }).map((_, index) => <SkeletonCard key={index} className="h-24" />)
              : coins.map((coin) => {
                  const positive = (coin.price_change_percentage_24h ?? 0) >= 0;
                  return (
                    <div key={coin.id} className="rounded-3xl border border-white/5 bg-slate-950/70 p-4 transition hover:border-cyan-300/20">
                      <div className="flex items-center justify-between gap-4">
                        <div className="flex items-center gap-3">
                          <img src={coin.image} alt={coin.name} className="h-9 w-9 rounded-full" />
                          <div>
                            <p className="text-sm font-semibold text-white">{coin.symbol.toUpperCase()}</p>
                            <p className="text-xs text-slate-500">{coin.name}</p>
                          </div>
                        </div>
                        <div className="text-right">
                          <p className="text-base font-semibold text-white">{formatCurrency(coin.current_price)}</p>
                          <p className={`text-sm ${positive ? 'text-emerald-300' : 'text-rose-300'}`}>
                            {positive ? '+' : ''}{coin.price_change_percentage_24h?.toFixed(2)}%
                          </p>
                        </div>
                      </div>
                    </div>
                  );
                })}
          </div>
        </div>
      </div>
    </div>
  );
}

export default Dashboard;
