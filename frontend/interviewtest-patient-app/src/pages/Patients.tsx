import React, { useEffect, useState } from 'react';
import api from '../services/api';
import { Patient } from '../types/Patient';
import PatientFormModal from '../components/PatientFormModal';
import { toast } from 'react-toastify';

export default function Patients() {
  const [patients, setPatients] = useState<Patient[]>([]);
  const [selectedPatient, setSelectedPatient] = useState<Patient | null>(null);
  const [isModalOpen, setModalOpen] = useState(false);

  const fetchPatients = async () => {
    try {
      const { data } = await api.get<Patient[]>('/Patient');
      setPatients(data.data);
    } catch (error) {
      toast.error('Failed to fetch patients.');
    }
  };

  useEffect(() => {
    fetchPatients();
  }, []);

  const handleEdit = (patient: Patient) => {
    setSelectedPatient(patient);
    setModalOpen(true);
  };

  const handleDelete = async (id: string | undefined) => {
    if (!id) return;
    try {
      await api.delete(`/Patient/${id}`);
      fetchPatients();
      toast.success('Patient deleted successfully.');
    } catch (error) {
      toast.error('Error deleting patient.');
    }
  };

  return (
    <div>
      <h2 className="text-2xl font-semibold mb-4">Patients</h2>
      <button onClick={() => { setSelectedPatient(null); setModalOpen(true); }} className="bg-blue-600 text-white px-4 py-2 rounded mb-4">Add Patient</button>

      <div className="overflow-x-auto">
        <table className="min-w-full bg-white border rounded shadow">
          <thead className="bg-gray-200 text-gray-700">
            <tr>
              <th className="px-4 py-2">First Name</th>
              <th className="px-4 py-2">Last Name</th>
              <th className="px-4 py-2">Email</th>
              <th className="px-4 py-2">Actions</th>
            </tr>
          </thead>
          <tbody>
            {patients.map((patient) => (
              <tr key={patient.id} className="text-center border-t hover:bg-gray-100">
                <td className="px-4 py-2">{patient.firstName}</td>
                <td className="px-4 py-2">{patient.lastName}</td>
                <td className="px-4 py-2">{patient.email}</td>
                <td className="px-4 py-2 space-x-2">
                  <button onClick={() => handleEdit(patient)} className="bg-yellow-500 text-white px-3 py-1 rounded">Edit</button>
                  <button onClick={() => handleDelete(patient.id)} className="bg-red-500 text-white px-3 py-1 rounded">Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      <PatientFormModal 
        isOpen={isModalOpen} 
        onClose={() => setModalOpen(false)} 
        patient={selectedPatient} 
        onSuccess={() => {
          fetchPatients();
          toast.success(selectedPatient ? 'Patient updated successfully.' : 'Patient created successfully.');
        }} 
      />
    </div>
  );
}