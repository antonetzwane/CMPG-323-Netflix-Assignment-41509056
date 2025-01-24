using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using NetflixAPISolution.Models;
using static NetflixAPISolution.Repository.NetflixRepository;


namespace NetflixAPISolution.Repository
{
    public class NetflixRepository
    {

        private readonly List<NetflixData> _stagingDataset = new();

        public List<NetflixData> GetAll()
        {
            return _stagingDataset;
        }

        public NetflixData GetById(int id)
        {
            return _stagingDataset.FirstOrDefault(x => x.Id == id);
        }

        public void Add(NetflixData netflixData)
        {
            netflixData.Id = _stagingDataset.Count > 0 ? _stagingDataset.Max(x => x.Id) + 1 : 1;
            _stagingDataset.Add(netflixData);
        }

        public void Update(int id, NetflixData updatedNetflixData)
        {
            var existingData = GetById(id);
            if (existingData != null)
            {
                existingData.title = updatedNetflixData.title;
                existingData.rating = updatedNetflixData.rating;
                existingData.ratingLevel = updatedNetflixData.ratingLevel;
                existingData.ratingDescription = updatedNetflixData.ratingDescription;
                existingData.releaseyear = updatedNetflixData.releaseyear;
                existingData.userratingscore = updatedNetflixData.userratingscore;
                existingData.userratingsize = updatedNetflixData.userratingsize;
            }
        }

        public void Delete(int id)
        {
            var data = GetById(id);
            if (data != null)
            {
                _stagingDataset.Remove(data);
            }
        }

        public void FetchFromExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (!System.IO.File.Exists(filePath))
                throw new FileNotFoundException($"The file at path {filePath} was not found.");

            using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var netflixData = new NetflixData
                    {
                        title = worksheet.Cells[row, 1].Text,
                        rating = worksheet.Cells[row, 2].Text,
                        ratingLevel = worksheet.Cells[row, 3].Text,
                        ratingDescription = int.TryParse(worksheet.Cells[row, 4].Text, out int desc) ? desc : 0,
                        releaseyear = int.TryParse(worksheet.Cells[row, 5].Text, out int year) ? year : 0,
                        userratingscore = int.TryParse(worksheet.Cells[row, 6].Text, out int score) ? score : 0,
                        userratingsize = int.TryParse(worksheet.Cells[row, 7].Text, out int size) ? size : 0
                    };
                    _stagingDataset.Add(netflixData);
                }
            }
        }

        /**public void Deduplicate()
        {
            _stagingDataset = _stagingDataset
                .GroupBy(x => new { x.title, x.releaseyear }) // Group by Title and Release Year to find duplicates
                .Select(group => group.First()) // Keep only the first entry in each group
                .ToList();
        }**/


    }
}

