import React from 'react';
import { useApi } from '../api/api';

const SponsorPaymentDetails = () => {
  const { data, loading, error } = useApi('http://localhost:5010/api/Sponsors/PaymentDetails');
  console.log(data);

  if (loading) {
    return <p>Loading...</p>;
  }

  if (error) {
    return <p>Error: {error}</p>;
  }

  

  return (
    <div>
      <h2>Sponsor Payment Details</h2>
      {data.length > 0 ? (
        <ul>
          {data.map((detail, index) => (
            <ul key={index}>
              {detail.sponsorName }  --         
              {detail.paymentCount } --
              {detail.totalPayments  } --
              {detail.latestPaymentDate  }
            </ul>
          ))}
        </ul>
      ) : (
        <p>No payment details available.</p>
      )}
    </div>
  );
};

export default SponsorPaymentDetails;
