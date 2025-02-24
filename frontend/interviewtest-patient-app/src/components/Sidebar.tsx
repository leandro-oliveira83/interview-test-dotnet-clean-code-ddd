import React from 'react';
import { Link } from 'react-router-dom';

export default function Sidebar() {
  return (
    <>
    <aside className="w-48 bg-gray-700 text-white h-full">
      <nav className="flex flex-col p-4 space-y-4">
        <Link to="/" className="hover:bg-gray-600 p-2 rounded">Dashboard</Link>
        <Link to="/patients" className="hover:bg-gray-600 p-2 rounded">Patients</Link>
      </nav>
    </aside>
    </>
  );
}