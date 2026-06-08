import { FormEvent, useEffect, useMemo, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { api } from '../services/api';
import { Asset, Portfolio } from '../types';
import { PieChart, Pie, Cell, ResponsiveContainer } from 'recharts';

const pieColors = ['#00d4ff', '#14ff8f', '#7c3aed', '#ff4444', '#38bdf8'];

function formatCurrency(value: number) {
  return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(value);
}

function PortfolioDetail() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [portfolio, setPortfolio] = useState<Portfolio | null>(null);
  const [assets, setAssets] = useState<Asset[]>([]);
  const [loading, setLoading] = useState(true);
  const [formState, setFormState] = useState({ symbol: '', quantity: 0, buyPrice: 0, currentPrice: 0 });
  const [fetchingPrice, setFetchingPrice] = useState(false);

  useEffect(() => {
    if (!id) return;

    async function load() {
      try {
        setLoading(true);
        const [portfolioResponse, assetsResponse] = await Promise.all([
          api.get<Portfolio>(`/api/portfolio/${id}`),
          api.get<Asset[]>('/api/asset'),
        ]);
        setPortfolio(portfolioResponse.data);
        const portfolioAssets = assetsResponse.data.filter((asset) => asset.portfolioId === id);
        setAssets(portfolioAssets);
      } catch {
        setPortfolio(null);
      } finally {
        setLoading(false);
      }
    }

    load();
  }, [id]);

  useEffect(() => {
    async function fetchPrice() {
      if (!formState.symbol.trim()) {
        setFormState((prev) => ({ ...prev, currentPrice: 0 }));
        return;
      }
      try {
        setFetchingPrice(true);
        const response = await api.get(`/api/coingecko/symbol/${formState.symbol.trim().toUpperCase()}`);
        const currentPrice = response.data?.current_price ?? response.data?.price ?? 0;
        setFormState((prev) => ({ ...prev, currentPrice: Number(currentPrice) || 0 }));
      } catch {
        setFormState((prev) => ({ ...prev, currentPrice: 0 }));
      } finally {
        setFetchingPrice(false);
      }
    }

    const delay = window.setTimeout(fetchPrice, 500);
    return () => window.clearTimeout(delay);
  }, [formState.symbol]);

  const distribution = useMemo(
    () => assets.map((asset) => ({ name: asset.symbol.toUpperCase(), value: asset.currentPrice * asset.quantity })),
    [assets],
  );

  const totalValue = useMemo(
    () => assets.reduce((sum, asset) => sum + asset.currentPrice * asset.quantity, 0),
    [assets],
  );

  async function addAsset(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();
    if (!portfolio || !id) return;
    const payload: Asset = {
      portfolioId: id,
      symbol: formState.symbol.toUpperCase(),
      quantity: formState.quantity,
      buyPrice: formState.buyPrice,
      currentPrice: formState.currentPrice,
    };

    await api.post('/api/asset', payload);
    setAssets((prev) => [...prev, payload]);
    setFormState({ symbol: '', quantity: 0, buyPrice: 0, currentPrice: 0 });
  }

  if (!id) {
    return <div className="rounded-[32px] border border-white/10 bg-glass p-8">Invalid portfolio selected.</div>;
  }

  return (
    <div className="space-y-6">
      <button
        type="button"
        onClick={() => navigate(-1)}
        className="inline-flex items-center gap-2 rounded-full bg-slate-950/70 px-4 py-2 text-sm text-slate-300 transition hover:bg-slate-900"
      >
        ← Back to portfolios
      </button>

      <div className="rounded-[32px] border border-white/10 bg-glass p-6 shadow-glow">
        <div className="flex flex-col gap-3 md:flex-row md:items-end md:justify-between">
          <div>
            <p className="text-sm uppercase tracking-[0.3em] text-slate-400">Portfolio detail</p>
            <h2 className="mt-2 text-3xl font-semibold text-white">{portfolio?.name ?? 'Portfolio'}</h2>
          </div>
          <div className="rounded-3xl bg-slate-900/70 px-4 py-3 text-slate-300">
            Estimated value {formatCurrency(totalValue)}
          </div>
        </div>
      </div>

      <div className="grid gap-6 xl:grid-cols-[1.3fr_0.7fr]">
        <section className="space-y-6 rounded-[32px] border border-white/10 bg-glass p-6 shadow-glow">
          <div className="flex flex-col gap-3 sm:flex-row sm:items-center sm:justify-between">
            <div>
              <p className="text-sm uppercase tracking-[0.3em] text-slate-400">Assets</p>
              <h3 className="mt-2 text-2xl font-semibold text-white">Asset breakdown</h3>
            </div>
            <span className="rounded-full bg-cyan-500/10 px-4 py-2 text-sm text-cyan-200">{assets.length} assets</span>
          </div>

          <div className="overflow-hidden rounded-3xl border border-white/10 bg-slate-950/70">
            <table className="min-w-full divide-y divide-white/10 text-left text-sm text-slate-300">
              <thead className="bg-slate-950/90 text-slate-400">
                <tr>
                  <th className="px-4 py-4">Symbol</th>
                  <th className="px-4 py-4">Quantity</th>
                  <th className="px-4 py-4">Buy price</th>
                  <th className="px-4 py-4">Current price</th>
                  <th className="px-4 py-4">P&L</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-white/10">
                {loading ? (
                  Array.from({ length: 3 }).map((_, idx) => (
                    <tr key={idx} className="h-16">
                      <td colSpan={5} className="px-4 py-4">
                        <div className="h-4 w-full rounded-full bg-slate-800/80" />
                      </td>
                    </tr>
                  ))
                ) : assets.length ? (
                  assets.map((asset, index) => {
                    const delta = asset.currentPrice - asset.buyPrice;
                    const positive = delta >= 0;
                    return (
                      <tr key={`${asset.symbol}-${index}`}>
                        <td className="px-4 py-4 text-white">{asset.symbol.toUpperCase()}</td>
                        <td className="px-4 py-4">{asset.quantity}</td>
                        <td className="px-4 py-4">{formatCurrency(asset.buyPrice)}</td>
                        <td className="px-4 py-4">{formatCurrency(asset.currentPrice)}</td>
                        <td className={`px-4 py-4 font-semibold ${positive ? 'text-emerald-300' : 'text-rose-300'}`}>
                          {positive ? '+' : ''}{formatCurrency(delta)}
                        </td>
                      </tr>
                    );
                  })
                ) : (
                  <tr>
                    <td colSpan={5} className="px-4 py-6 text-center text-slate-400">
                      No assets added yet.
                    </td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
        </section>

        <section className="space-y-6 rounded-[32px] border border-white/10 bg-glass p-6 shadow-glow">
          <div>
            <p className="text-sm uppercase tracking-[0.3em] text-slate-400">Add asset</p>
            <h3 className="mt-2 text-2xl font-semibold text-white">Quick add</h3>
          </div>
          <form onSubmit={addAsset} className="space-y-4">
            <div className="space-y-2">
              <label className="text-sm text-slate-300">Symbol</label>
              <input
                value={formState.symbol}
                onChange={(event) => setFormState({ ...formState, symbol: event.target.value })}
                className="w-full rounded-3xl border border-white/10 bg-slate-950/70 px-4 py-3 text-white outline-none transition focus:border-cyan-400/60"
                placeholder="BTC"
              />
            </div>
            <div className="grid gap-4 sm:grid-cols-2">
              <div className="space-y-2">
                <label className="text-sm text-slate-300">Quantity</label>
                <input
                  type="number"
                  value={formState.quantity || ''}
                  onChange={(event) => setFormState({ ...formState, quantity: Number(event.target.value) })}
                  className="w-full rounded-3xl border border-white/10 bg-slate-950/70 px-4 py-3 text-white outline-none focus:border-cyan-400/60"
                  placeholder="1.25"
                />
              </div>
              <div className="space-y-2">
                <label className="text-sm text-slate-300">Buy price</label>
                <input
                  type="number"
                  value={formState.buyPrice || ''}
                  onChange={(event) => setFormState({ ...formState, buyPrice: Number(event.target.value) })}
                  className="w-full rounded-3xl border border-white/10 bg-slate-950/70 px-4 py-3 text-white outline-none focus:border-cyan-400/60"
                  placeholder="42000"
                />
              </div>
            </div>
            <div className="space-y-2">
              <label className="text-sm text-slate-300">Current price</label>
              <div className="flex items-center gap-3 rounded-3xl border border-white/10 bg-slate-950/70 px-4 py-3 text-white">
                <span>{fetchingPrice ? 'Fetching…' : formatCurrency(formState.currentPrice)}</span>
              </div>
            </div>
            <button
              type="submit"
              className="w-full rounded-3xl bg-cyan-400 px-5 py-3 text-sm font-semibold text-slate-950 transition hover:bg-cyan-300"
            >
              Add asset
            </button>
          </form>

          <div className="rounded-[28px] border border-white/10 bg-slate-950/70 p-4">
            <p className="text-sm uppercase tracking-[0.3em] text-slate-400">Distribution</p>
            <div className="h-[280px]">
              <ResponsiveContainer width="100%" height="100%">
                <PieChart>
                  <Pie data={distribution} dataKey="value" nameKey="name" innerRadius={60} outerRadius={100} paddingAngle={4}>
                    {distribution.map((entry, index) => (
                      <Cell key={entry.name} fill={pieColors[index % pieColors.length]} />
                    ))}
                  </Pie>
                </PieChart>
              </ResponsiveContainer>
            </div>
            <div className="mt-4 grid gap-2 text-sm text-slate-300">
              {distribution.map((item, idx) => (
                <div key={item.name} className="flex items-center justify-between rounded-3xl bg-slate-900/70 px-4 py-2">
                  <span className="flex items-center gap-2">
                    <span className="inline-block h-2.5 w-2.5 rounded-full" style={{ backgroundColor: pieColors[idx % pieColors.length] }} />
                    {item.name}
                  </span>
                  <span>{((item.value / (totalValue || 1)) * 100).toFixed(1)}%</span>
                </div>
              ))}
            </div>
          </div>
        </section>
      </div>
    </div>
  );
}

export default PortfolioDetail;
