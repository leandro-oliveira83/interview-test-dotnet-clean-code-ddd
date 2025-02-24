import React, { useEffect, useState } from 'react';
import api from '../services/api';
import { Patient } from '../types/Patient';
import PatientFormModal from '../components/PatientFormModal';

export default function Patients() {
  const [patients, setPatients] = useState<Patient[]>([]);
  const [selectedPatient, setSelectedPatient] = useState<Patient | null>(null);
  const [isModalOpen, setModalOpen] = useState(false);

  const fetchPatients = async () => {
    const { data } = await api.get<Patient[]>('/Patient');
    setPatients(data.data.data);
  };

  useEffect(() => {
    fetchPatients();
  }, []);

  const handleEdit = (patient: Patient) => {
    console.log(patient)
    setSelectedPatient(patient);
    setModalOpen(true);
  };

  const handleDelete = async (id: string | undefined) => {
    if (!id) return;
    await api.delete(`/Patient/${id}`);
    fetchPatients();
  };

  return (
    <div>
      <h2 className="text-2xl mb-4">Patients</h2>
      <button onClick={() => { setSelectedPatient(null); setModalOpen(true); }} className="bg-blue-500 text-white px-4 py-2 rounded">Add Patient</button>

      <ul className="mt-6 space-y-2">
        {patients.map((p) => (
          <li key={p.id} className="border p-4 rounded flex justify-between items-center">
            <span>{p.firstName} {p.lastName} - {p.email}</span>
            <div className="space-x-2">
              <button onClick={() => handleEdit(p)} className="bg-yellow-500 px-3 py-1 text-white rounded">Edit</button>
              <button onClick={() => handleDelete(p.id)} className="bg-red-500 px-3 py-1 text-white rounded">Delete</button>
            </div>
          </li>
        ))}
      </ul>

      <PatientFormModal 
        isOpen={isModalOpen} 
        onClose={() => setModalOpen(false)} 
        patient={selectedPatient} 
        onSuccess={fetchPatients} 
      />
    </div>
  );
}