import React from 'react';
import { useApi } from '../api/api';

const MatchesWithPayments = () => {
  
  const { data, loading, error } = useApi('http://localhost:5010/api/Matches/PaymentDetails');
  console.log(data);

  if (loading) {
    return <p>Loading...</p>;
  }

  if (error) {
    return <p>Error: {error}</p>;
  }


  return (
    <div>
      <h2>Matches with Payments</h2>
      <ul>
        {data.map((match, index) => (
          <ul key={index}>
            {match.matchName} - {match.sum} - {match.matchDate} - {match.location}
          </ul>
        ))}
      </ul>
    </div>
  );
};

export default MatchesWithPayments;
