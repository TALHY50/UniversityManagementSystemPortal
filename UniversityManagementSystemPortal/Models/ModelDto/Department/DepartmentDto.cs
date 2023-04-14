﻿namespace UniversityManagementSystemPortal.Models.ModelDto.Department
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid InstituteId { get; set; }
        public bool IsActive { get; set; }
        public bool IsAcademics { get; set; }
        public bool IsAdministrative { get; set; }
        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}