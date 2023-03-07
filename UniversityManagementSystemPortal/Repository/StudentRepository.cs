﻿using Microsoft.EntityFrameworkCore;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.CsvImport;
using UniversityManagementSystemPortal.CsvImport.dtomodels;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.PictureManager;

namespace UniversityManagementSystemPortal.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UmspContext _dbContext;
        private readonly IPictureManager _pictureManager;

        public StudentRepository(UmspContext dbContext, IPictureManager pictureManager)
        {
            _dbContext = dbContext;
            _pictureManager = pictureManager;
        }

        public async Task<List<Student>> Get()
        {
            var students = await _dbContext.Students
                .Include(s => s.Institute)
                .Include(s => s.StudentPrograms)
                    .ThenInclude(sp => sp.Program)
                        .ThenInclude(p => p.Department)
                .ToListAsync();

            return students;
        }

        public async Task<Student> GetById(Guid id)
        {
            var student = await _dbContext.Students
                .Include(s => s.Institute)
                .Include(s => s.StudentPrograms)
                    .ThenInclude(sp => sp.Program)
                        .ThenInclude(p => p.Department)
                .FirstOrDefaultAsync(s => s.Id == id);

            student.ProfilePath = GetProfilePicturePath(student.ProfilePath);

            return student;
        }

        public async Task<Student> Add(Student student)
        {
            student.Id = Guid.NewGuid();
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();
            return student;
        }

        public async Task<Student> Update(Student student, IFormFile picture)
        {
            if (picture != null)
            {
                student.ProfilePath = await _pictureManager.Update(student.Id, picture, student.ProfilePath);
            }
            student.Id = Guid.NewGuid();

            _dbContext.Students.Update(student);
            await _dbContext.SaveChangesAsync();

            student.ProfilePath = GetProfilePicturePath(student.ProfilePath);

            return student;
        }

        public async Task Delete(Guid id)
        {
            var student = await GetById(id);

            if (!string.IsNullOrEmpty(student.ProfilePath))
            {
                _pictureManager.Delete(student.ProfilePath);
            }

            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
        }

        private string GetProfilePicturePath(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                return Path.Combine("uploads", filePath);
            }

            return null;
        }
        public async Task<Student> GetByAdmissionNo(string admissionNo)
        {
            return await _dbContext.Students.FirstOrDefaultAsync(s => s.AdmissionNo == admissionNo);
        }
        //public void ImportStudentsFromCsv(string filePath)
        //{
        //    var studentDtos = ExcelHelper.ImportCsv<StudentDto>(filePath);
        //    var students = _dbContext.Students
        //        .Include(s => s.Institute)
        //        .Include(s => s.StudentPrograms)
        //            .ThenInclude(sp => sp.Program)
        //                .ThenInclude(p => p.Department)

        //         .Select(s => new CsvStudentDto
        //         {
        //             AdmissionNo = s.AdmissionNo,
        //             FirstName = s.User.FirstName ?? string.Empty,
        //             MiddleName = s.User.MiddleName,
        //             LastName = s.User.LastName,
        //             MobileNo = s.User.MobileNo,
        //             DateOfBirth = s.User.DateOfBirth,
        //             Gender = s.User.Gender ?? Gender.Male,
        //             Email = s.User.Email ?? string.Empty,
        //             Username = s.User.Username ?? string.Empty,
        //             Category = s.Category,
        //             Address = s.Address,
        //             DepartmentCode = sp.de
        //             DepartmentName = .Name ?? string.Empty,
        //             InstituteName = s.Institute.Name,
        //             InstituteType = (int)s.Institute.Type,
        //             InstitutePhone = s.Institute.Phone,
        //             InstituteEmail = s.Institute.Email,
        //             InstituteAddress = s.Institute.Address
        //         })
        //         .ToList();
        //}

    }
}