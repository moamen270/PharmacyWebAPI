﻿namespace PharmacyWebAPI.Models
{
    public class Prescription
    {
        public int Id { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.Now;
        public bool Dispensing { get; set; } = false;

        [ForeignKey("Patient")]
        public string PatientId { get; set; }

        public User Patient { get; set; }

        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }

        public User Doctor { get; set; }
    }
}