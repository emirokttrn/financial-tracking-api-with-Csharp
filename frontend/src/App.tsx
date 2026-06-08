import { Routes, Route } from 'react-router-dom';
import Dashboard from './pages/Dashboard';
import Portfolios from './pages/Portfolios';
import PortfolioDetail from './pages/PortfolioDetail';
import Transactions from './pages/Transactions';
import Users from './pages/Users';
import Sidebar from './components/Sidebar';

function App() {
  return (
    <div className="min-h-screen bg-surface text-slate-100">
      <div className="flex min-h-screen overflow-hidden">
        <Sidebar />
        <main className="flex-1 p-4 md:p-6 lg:p-8">
          <div className="mx-auto max-w-[1600px] space-y-6">
            <Routes>
              <Route path="/" element={<Dashboard />} />
              <Route path="/portfolios" element={<Portfolios />} />
              <Route path="/portfolios/:id" element={<PortfolioDetail />} />
              <Route path="/transactions" element={<Transactions />} />
              <Route path="/users" element={<Users />} />
            </Routes>
          </div>
        </main>
      </div>
    </div>
  );
}

export default App;
