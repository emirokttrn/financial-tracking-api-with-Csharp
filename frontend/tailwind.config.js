export default {
  content: ['./index.html', './src/**/*.{ts,tsx}'],
  theme: {
    extend: {
      boxShadow: {
        glow: '0 15px 50px rgba(0, 212, 255, 0.12)',
      },
      colors: {
        surface: '#0a0a0f',
        panel: 'rgba(10, 10, 15, 0.72)',
        border: 'rgba(255,255,255,0.08)',
      },
      fontFamily: {
        sans: ['Inter', 'ui-sans-serif', 'system-ui', 'sans-serif'],
      },
    },
  },
  plugins: [],
};
