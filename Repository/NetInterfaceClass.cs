using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;
using NetflixAPISolution.Models;

namespace NetflixAPISolution.Repository
{
    public interface NetflixInterface
    {
        List<NetflixData> GetAll(); //Retrieves all items.
        NetflixData GetById(int id); // GET by ID.
        void Add(NetflixData netflixData); // Using POST to create a new entry in the database.
        void Update(int id, NetflixData updatedNetflixData); // PUT/PATCH methods to update an existing entry in the database.
        void Delete(int id); // DELETE to r emove an existing entry.
        void FetchFromExcel(string filePath); // FETCH dataset from Excel.
        void Deduplicate(); // Remove duplicates from the dataset.
    }
}
