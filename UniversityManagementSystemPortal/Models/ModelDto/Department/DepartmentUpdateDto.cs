﻿namespace UniversityManagementSystemPortal.Models.ModelDto.Department
{
    public class DepartmentUpdateDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid InstituteId { get; set; }
        public bool IsActive { get; set; }
        public bool IsAcademics { get; set; }
        public bool IsAdministrative { get; set; }
    }
}