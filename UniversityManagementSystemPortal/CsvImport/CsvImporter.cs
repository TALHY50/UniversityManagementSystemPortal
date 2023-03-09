using ClosedXML.Excel;
using CsvHelper.Configuration;
using ExcelDataReader;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace UniversityManagementSystemPortal.CsvImport
{
    public static class ExcelHelper
    {
        public static List<T> ImportCsv<T>(string filePath) where T : new()
        {
            var list = new List<T>();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet(1);
                var rows = worksheet.RowsUsed().Skip(1);
                foreach (var row in rows)
                {
                    var obj = new T();
                    var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var property in properties)
                    {
                        var cellValue = row.Cell(property.Name).Value;
                        if (!string.IsNullOrEmpty(cellValue.ToString()))
                        {
                            var propertyType = property.PropertyType;
                            var convertedValue = Convert.ChangeType(cellValue, propertyType);
                            property.SetValue(obj, convertedValue);
                        }
                    }
                    list.Add(obj);
                }
            }
            return list;
        }
    }
}