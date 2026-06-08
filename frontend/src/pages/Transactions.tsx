import { useEffect, useMemo, useState } from 'react';
import { api } from '../services/api';
import { Transaction } from '../types';
import SkeletonCard from '../components/SkeletonCard';

function formatCurrency(value: number) {
  return new Intl.NumberFormat('en-US', { style: 'currency', currency: 'USD' }).format(value);
}

function Transactions() {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [filter, setFilter] = useState('All');
  const [query, setQuery] = useState('');
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function load() {
      try {
        setLoading(true);
        const response = await api.get<Transaction[]>('/api/transaction');
        setTransactions(response.data || []);
      } finally {
        setLoading(false);
      }
    }
    load();
  }, []);

  const filteredTransactions = useMemo(() => {
    return transactions.filter((transaction) => {
      const matchesType = filter === 'All' || transaction.type === filter;
      const matchesText = [transaction.symbol, transaction.status, transaction.id]
        .filter(Boolean)
        .join(' ')
        .toLowerCase()
        .includes(query.toLowerCase());
      return matchesType && matchesText;
    });
  }, [transactions, filter, query]);

  return (
    <div className="space-y-6">
      <div className="rounded-[32px] border border-white/10 bg-glass p-6 shadow-glow">
        <h2 className="text-3xl font-semibold">Transactions</h2>
        <p className="mt-2 text-slate-400">Review the activity log across assets, purchases, and transfers.</p>
      </div>

      <div className="rounded-[32px] border border-white/10 bg-glass p-6 shadow-glow">
        <div className="flex flex-col gap-4 md:flex-row md:items-center md:justify-between">
          <div className="flex flex-wrap gap-3">
            {['All', 'Buy', 'Sell', 'Transfer'].map((option) => (
              <button
                key={option}
                type="button"
                onClick={() => setFilter(option)}
                className={`rounded-3xl px-4 py-2 text-sm transition ${
                  filter === option ? 'bg-cyan-500/15 text-cyan-200 ring-1 ring-cyan-500/25' : 'bg-slate-950/70 text-slate-300 hover:bg-slate-900'
                }`}
              >
                {option}
              </button>
            ))}
          </div>
          <input
            type="search"
            value={query}
            onChange={(event) => setQuery(event.target.value)}
            className="max-w-md rounded-3xl border border-white/10 bg-slate-950/70 px-4 py-3 text-white outline-none focus:border-cyan-400/60"
            placeholder="Search symbol, status, ID"
          />
        </div>

        <div className="mt-6 overflow-hidden rounded-3xl border border-white/10 bg-slate-950/70">
          <table className="min-w-full divide-y divide-white/10 text-left text-sm text-slate-300">
            <thead className="bg-slate-950/90 text-slate-400">
              <tr>
                <th className="px-4 py-4">Date</th>
                <th className="px-4 py-4">Symbol</th>
                <th className="px-4 py-4">Type</th>
                <th className="px-4 py-4">Amount</th>
                <th className="px-4 py-4">Status</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-white/10">
              {loading
                ? Array.from({ length: 4 }).map((_, idx) => (
                    <tr key={idx} className="h-16">
                      <td colSpan={5} className="px-4 py-4">
                        <div className="h-4 w-full rounded-full bg-slate-800/80" />
                      </td>
                    </tr>
                  ))
                : filteredTransactions.length > 0
                ? filteredTransactions.map((transaction) => (
                    <tr key={transaction.id ?? `${transaction.symbol}-${transaction.date}`} className="hover:bg-white/5">
                      <td className="px-4 py-4">{new Date(transaction.date).toLocaleDateString()}</td>
                      <td className="px-4 py-4 uppercase">{transaction.symbol}</td>
                      <td className="px-4 py-4">{transaction.type}</td>
                      <td className="px-4 py-4">{formatCurrency(transaction.amount)}</td>
                      <td className="px-4 py-4 text-slate-300">{transaction.status || 'Confirmed'}</td>
                    </tr>
                  ))
                : (
                  <tr>
                    <td colSpan={5} className="px-4 py-6 text-center text-slate-400">
                      No transactions match the current filter.
                    </td>
                  </tr>
                )}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}

export default Transactions;
