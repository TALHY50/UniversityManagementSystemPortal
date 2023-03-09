using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Reflection;
using CsvHelper;
using LinqToExcel.Attributes;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using UniversityManagementsystem.Models;
namespace UniversityManagementSystemPortal.CsvImport
{
    public class ImportExportService<T> where T : class
    {
        private readonly UmspContext _dbContext;

        public ImportExportService(UmspContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> ReadFromCsvAsync(Stream stream)
        {
            using var reader = new StreamReader(stream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<T>().ToList();
            return records;
        }

        public async Task<byte[]> ExportToCsvAsync(IEnumerable<T> data)
        {
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(data);
            await writer.FlushAsync();
            return stream.ToArray();
        }

        public async Task ImportAsync(IEnumerable<T> data, Func<T, object> keySelector)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                var existingKeys = _dbContext.Set<T>().Select(keySelector).ToList();

                var entitiesToAdd = data.Where(d => !existingKeys.Contains(keySelector(d))).ToList();
                var entitiesToUpdate = data.Where(d => existingKeys.Contains(keySelector(d))).ToList();

                _dbContext.Set<T>().AddRange(entitiesToAdd);
                _dbContext.Set<T>().UpdateRange(entitiesToUpdate);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Handle exception and log rollback issue messages
                throw;
            }
        }
    }

}