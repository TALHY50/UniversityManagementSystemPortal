using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

        public class CsvHelper<T>
        {
            public async Task<List<T>> ReadFromCsvAsync(Stream stream, ClassMap<T> map)
            {
                using var reader = new StreamReader(stream);
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null, // disable header validation
                    MissingFieldFound = null, // disable missing field validation
                    PrepareHeaderForMatch = header => header.Header.ToLower(), // ignore case sensitivity
                };
                using var csv = new CsvReader(reader, csvConfig);
                csv.Context.RegisterClassMap(map); // map the CSV columns to the class properties
                var records = csv.GetRecords<T>().ToList();
                return records;
            }

            public async Task<List<T>> ReadFromCsvAsync(Stream stream)
            {
                using var reader = new StreamReader(stream);
                var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null, // disable header validation
                    MissingFieldFound = null, // disable missing field validation
                    PrepareHeaderForMatch = header => header.Header.ToLower(), // ignore case sensitivity
                };
                using var csv = new CsvReader(reader, csvConfig);
                var records = csv.GetRecords<T>().ToList();
                return records;
            }
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
//    public interface IImportExportService
//{
//    Task<IEnumerable<T>> ReadFromCsvAsync<T>(Stream stream, ClassMap<T> map) where T : class, new();
//    Task<byte[]> WriteToCsvAsync<T>(IEnumerable<T> data, ClassMap<T> map) where T : class;
//}
//    public class ImportExportService<T> : IImportExportService<T>
//    {
//        private readonly CsvConfiguration _csvConfiguration;

//        public ImportExportService(IOptions<CsvConfiguration> csvConfiguration)
//        {
//            _csvConfiguration = csvConfiguration.Value;//        }

//        public async Task<IEnumerable<T>> ReadFromCsvAsync(Stream stream, ClassMap<T> classMap)
//        {
//            using var reader = new StreamReader(stream);
//            using var csv = new CsvReader(reader, _csvConfiguration);
//            csv.Context.RegisterClassMap(classMap);
//            var records = await csv.GetRecordsAsync<T>().ToListAsync();
//            return records;
//        }
//    }


}