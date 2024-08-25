
import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import SponsorsByYear from './pages/SponsorByYear';
import MatchesWithPayments from './pages/MatchesByPayment';
import SponsorPaymentDetails from './pages/SponsorPaymentDetails';
import Payment from './pages/Payment';

function App() {
  return (
    <div className="App">
      <Router>
      <div>
        
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/sponsors-by-year" element={<SponsorsByYear />} />
          <Route path="/matches-with-payments" element={<MatchesWithPayments />} />
          <Route path="/sponsor-payment-details" element={<SponsorPaymentDetails />} />
          <Route path="/payment-form" element={<Payment />} /> 
        </Routes>
      </div>
    </Router>
    </div>
  );
}

export default App;
