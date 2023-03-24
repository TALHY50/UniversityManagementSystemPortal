﻿using MediatR;
using UniversityManagementSystemPortal.ModelDto.Student;

namespace UniversityManagementSystemPortal.Application.Command.Student
{
    public class ImportStudentsCommand : IRequest<List<string>>
    {
        public List<StudentReadModel> StudentsData { get; }

        public ImportStudentsCommand(List<StudentReadModel> studentsData)
        {
            StudentsData = studentsData;
        }
    }
}