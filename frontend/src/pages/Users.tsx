import { useEffect, useState } from 'react';
import { api } from '../services/api';
import { User } from '../types';
import SkeletonCard from '../components/SkeletonCard';

function Users() {
  const [users, setUsers] = useState<User[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function load() {
      try {
        setLoading(true);
        const response = await api.get<User[]>('/api/user');
        setUsers(response.data || []);
      } finally {
        setLoading(false);
      }
    }
    load();
  }, []);

  return (
    <div className="space-y-6">
      <div className="rounded-[32px] border border-white/10 bg-glass p-6 shadow-glow">
        <h2 className="text-3xl font-semibold">Users</h2>
        <p className="mt-2 text-slate-400">Manage user accounts, roles, and access for your financial platform.</p>
      </div>

      <div className="rounded-[32px] border border-white/10 bg-glass p-6 shadow-glow">
        <div className="overflow-hidden rounded-3xl border border-white/10 bg-slate-950/70">
          <table className="min-w-full divide-y divide-white/10 text-left text-sm text-slate-300">
            <thead className="bg-slate-950/90 text-slate-400">
              <tr>
                <th className="px-4 py-4">User</th>
                <th className="px-4 py-4">Email</th>
                <th className="px-4 py-4">Role</th>
              </tr>
            </thead>
            <tbody className="divide-y divide-white/10">
              {loading
                ? Array.from({ length: 4 }).map((_, idx) => (
                    <tr key={idx} className="h-16">
                      <td colSpan={3} className="px-4 py-4">
                        <div className="h-4 w-full rounded-full bg-slate-800/80" />
                      </td>
                    </tr>
                  ))
                : users.length > 0
                ? users.map((user) => (
                    <tr key={user.id ?? user.email} className="hover:bg-white/5">
                      <td className="px-4 py-4 font-semibold text-white">{user.username}</td>
                      <td className="px-4 py-4">{user.email}</td>
                      <td className="px-4 py-4 text-slate-300">{user.role ?? 'User'}</td>
                    </tr>
                  ))
                : (
                  <tr>
                    <td colSpan={3} className="px-4 py-6 text-center text-slate-400">
                      No users found.
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

export default Users;
