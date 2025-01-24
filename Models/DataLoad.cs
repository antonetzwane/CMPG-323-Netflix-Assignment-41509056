using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Office.Interop.Excel;

namespace NetflixAPISolution.Models
{
    public class DataLink
    {
        public List<NetflixData> LoadDataFromExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var netflixItems = new List<NetflixData>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var xlWorkbook = new ExcelPackage(new FileInfo(@"C:\Users\zwane\Downloads\netflix.xlsx"));

                ExcelWorksheet workSheet = xlWorkbook.Workbook.Worksheets[1];
                var worksheet = package.Workbook.Worksheets[""]; // First sheet
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // Start from row 2 to skip headers
                {
                    netflixItems.Add(new NetflixData
                    {
                        /**title = worksheet.Cells[row, 1].Text,
                        rating = worksheet.Cells[row, 2].Text,
                        ratingLevel = worksheet.Cells[row, 3].Text,
                        ratingDescription = int.Parse(worksheet.Cells[row, 4].Text),
                        releaseyear = int.Parse(worksheet.Cells[row, 5].Text),
                        userratingscore = int.Parse(worksheet.Cells[row, 6].Text),
                        userratingsize = int.Parse(worksheet.Cells[row, 7].Text)**/

                        title = worksheet.Cells[row, 1].Text,
                        rating = worksheet.Cells[row, 2].Text,
                        ratingLevel = worksheet.Cells[row, 3].Text,
                        ratingDescription = int.TryParse(worksheet.Cells[row, 4].Text, out var ratingDescription) ? ratingDescription : 0,
                        releaseyear = int.TryParse(worksheet.Cells[row, 5].Text, out var releaseYear) ? releaseYear : 0,
                        userratingscore = int.TryParse(worksheet.Cells[row, 6].Text, out var userRatingScore) ? userRatingScore : 0,
                        userratingsize = int.TryParse(worksheet.Cells[row, 7].Text, out var userRatingSize) ? userRatingSize : 0
                    });
                }
            }

            return netflixItems;
            //push to excel...
        }

    }
}



