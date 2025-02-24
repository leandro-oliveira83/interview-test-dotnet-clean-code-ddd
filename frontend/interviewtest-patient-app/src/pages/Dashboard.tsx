import React from 'react';
import { Link } from 'react-router-dom';

export default function Dashboard() {
  return (
    <div className="flex justify-center items-center h-full">
      <Link to="/patients" className="bg-blue-500 text-white px-6 py-3 rounded shadow hover:bg-blue-600">
        Go to Patients
      </Link>
    </div>
  );
}