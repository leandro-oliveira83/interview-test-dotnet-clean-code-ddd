import React, { useEffect, useState } from 'react';
import { useForm } from 'react-hook-form';
import { Patient } from '../types/Patient';
import api from '../services/api';

interface Props {
  isOpen: boolean;
  onClose: () => void;
  patient: Patient | null;
  onSuccess: () => void;
}

export default function PatientFormModal({ isOpen, onClose, patient, onSuccess }: Props) {
  const { register, handleSubmit, reset, formState: { errors } } = useForm<Patient>();
  const [alertMessage, setAlertMessage] = useState<string | null>(null);

  useEffect(() => {
    if (patient) {
      const formattedDate = patient.dateOfBirth ? new Date(patient.dateOfBirth).toISOString().split('T')[0] : '';
      reset({ ...patient, dateOfBirth: formattedDate });
    } else {
      reset({
        firstName: '', lastName: '', dateOfBirth: '', gender: '', maritalStatus: '', ethnicity: '', race: '',
        socialSecurityNumber: '', email: '', phoneNumber: '', alternatePhoneNumber: '', addressLine1: '', addressLine2: '',
        city: '', state: '', zipCode: '', country: 'USA'
      });
    }
  }, [patient, reset]);

  const onSubmit = async (data: Patient) => {
    try {
      patient?.id
        ? await api.put('/Patient', { ...data, id: patient.id })
        : await api.post('/Patient', data);
      onSuccess();
      onClose();
    } catch (error) {
      setAlertMessage('Error saving patient. Please try again.');
    }
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center z-50">
      <div className="bg-white p-6 rounded-lg w-full max-w-3xl overflow-y-auto max-h-[90vh]">
        <h3 className="text-lg font-semibold mb-4">{patient ? 'Edit Patient' : 'Add Patient'}</h3>

        {alertMessage && (
          <div className="bg-red-100 text-red-700 p-3 rounded mb-4">{alertMessage}</div>
        )}

        <form onSubmit={handleSubmit(onSubmit)} className="grid grid-cols-2 gap-4">
          {[
            { label: 'First Name', name: 'firstName' },
            { label: 'Last Name', name: 'lastName' },
            { label: 'Gender', name: 'gender' },
            { label: 'Marital Status', name: 'maritalStatus' },
            { label: 'Ethnicity', name: 'ethnicity' },
            { label: 'Race', name: 'race' },
            { label: 'Social Security Number', name: 'socialSecurityNumber' },
            { label: 'Email', name: 'email', type: 'email' },
            { label: 'Phone Number', name: 'phoneNumber' },
            { label: 'Alternate Phone Number', name: 'alternatePhoneNumber' },
            { label: 'Address Line 1', name: 'addressLine1' },
            { label: 'Address Line 2', name: 'addressLine2' },
            { label: 'City', name: 'city' },
            { label: 'Zip Code', name: 'zipCode' },
            { label: 'Country', name: 'country' },
          ].map(({ label, name, type = 'text' }) => (
            <div key={name}>
              <label>{label}</label>
              <input 
                type={type} 
                {...register(name as keyof Patient, { required: `${label} is required` })} 
                className="w-full border p-2 rounded" 
              />
              {errors[name as keyof Patient] && (
                <p className="text-red-500 text-sm">{errors[name as keyof Patient]?.message as string}</p>
              )}
            </div>
          ))}

          <div>
            <label>Date of Birth</label>
            <input 
              type="date" 
              {...register('dateOfBirth', { required: 'Date of Birth is required' })} 
              className="w-full border p-2 rounded" 
            />
            {errors.dateOfBirth && <p className="text-red-500 text-sm">{errors.dateOfBirth.message}</p>}
          </div>

          <div>
            <label>State</label>
            <input 
              {...register('state', { 
                required: 'State is required', 
                maxLength: { value: 2, message: 'Max 2 characters' }, 
                minLength: { value: 2, message: 'Must be exactly 2 characters' }
              })} 
              className="w-full border p-2 rounded uppercase" 
            />
            {errors.state && <p className="text-red-500 text-sm">{errors.state.message}</p>}
          </div>

          <div className="col-span-2 flex justify-end space-x-2 mt-4">
            <button type="button" onClick={onClose} className="bg-gray-400 px-4 py-2 rounded">Cancel</button>
            <button type="submit" className="bg-green-500 text-white px-4 py-2 rounded">{patient ? 'Update' : 'Create'}</button>
          </div>
        </form>
      </div>
    </div>
  );
}