import React, { useState } from 'react';
import axios from 'axios';

const Payment = () => {
  const [contractID, setContractID] = useState('');
  const [paymentDate, setPaymentDate] = useState('');
  const [amountPaid, setAmountPaid] = useState('');
  const [paymentStatus, setPaymentStatus] = useState('');
  const [message, setMessage] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await axios.post('http://localhost:5010/api/Payments', {
        ContractID: contractID,
        PaymentDate: paymentDate,
        AmountPaid: amountPaid,
        PaymentStatus: paymentStatus
      });
      setMessage('Payment done');
    } catch (error) {
      setMessage('Error : ' + error.message);
    }
  };

  return (
    <div>
      <h2>Add Payment</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Contract ID:</label>
          <input
            type="number"
            value={contractID}
            onChange={(e) => setContractID(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Payment Date:</label>
          <input
            type="date"
            value={paymentDate}
            onChange={(e) => setPaymentDate(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Amount Paid:</label>
          <input
            type="number"
            step="0.01"
            value={amountPaid}
            onChange={(e) => setAmountPaid(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Payment Status:</label>
          <input
            type="text"
            value={paymentStatus}
            onChange={(e) => setPaymentStatus(e.target.value)}
            required
          />
        </div>
        <button type="submit">Submit</button>
      </form>
      {message && <p>{message}</p>}
    </div>
  );
};

export default Payment;
