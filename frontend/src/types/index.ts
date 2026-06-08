export interface CoinSummary {
  id: string;
  symbol: string;
  name: string;
  image: string;
  current_price: number;
  market_cap: number;
  price_change_percentage_24h: number;
  sparkline_in_7d?: { price: number[] };
}

export interface Portfolio {
  id: string;
  name: string;
  description?: string;
  totalValue?: number;
  assets?: Asset[];
}

export interface Asset {
  id?: string;
  portfolioId?: string;
  symbol: string;
  quantity: number;
  buyPrice: number;
  currentPrice: number;
}

export interface Transaction {
  id?: string;
  portfolioId?: string;
  type: string;
  symbol: string;
  amount: number;
  date: string;
  status?: string;
}

export interface User {
  id?: string;
  username: string;
  email: string;
  role?: string;
}
