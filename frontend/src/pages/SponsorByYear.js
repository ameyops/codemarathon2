import React, { useState } from 'react';
import { useApi } from '../api/api';

const SponsorsByYear = () => {
  const [year, setYear] = useState('');
  const [url, setUrl] = useState(null);

  const { data, loading, error } = useApi(url);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (year) {
      setUrl(`http://localhost:5010/api/Sponsors/ByYear?year=${year}`);
    }
  };

  return (
    <div>
      <h2>Sponsors by Year</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Enter Year:</label>
          <input
            type="number"
            value={year}
            onChange={(e) => setYear(e.target.value)}
            required
          />
        </div>
        <button type="submit">Fetch Sponsors</button>
      </form>

      {loading && <p>Loading...</p>}
      {error && <p>Error: {error}</p>}
      
      {data && data.length > 0 ? (
        <ul>
          {data.map((item, index) => (
            <li key={index}>
              {item.sponsorName} - {item.matchCount}
            </li>
          ))}
        </ul>
      ) : (
        <p>No sponsors found for the given year.</p>
      )}
    </div>
  );
};

export default SponsorsByYear;
