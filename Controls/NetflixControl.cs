using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetflixAPISolution.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using NetflixAPISolution.Repository;

namespace NetflixAPISolution.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class DatasetController : ControllerBase
    {
        private readonly NetflixInterface _repository;

        public DatasetController(NetflixInterface repository)
        {
            _repository = repository;
        }

        private readonly List<NetflixData> _stagingDataset = new();

        [HttpPost("load-from-excel")]
        public IActionResult LoadFromExcel([FromQuery] string filePath)
        {
            var loader = new DataLink();
            var data = loader.LoadDataFromExcel(filePath);
            _stagingDataset.AddRange(data);
            return Ok("Data loaded successfully from Excel.");
        }

        /**[HttpPost("load-from-excel")]
        public IActionResult LoadFromExcel([FromQuery] string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                return BadRequest("Invalid file path provided.");
            }

            var loader = new DataLink();
            var data = loader.LoadDataFromExcel(@"C:\Users\zwane\Downloads\netflix.xlsx");
            _stagingDataset.AddRange(data);
            return Ok("Data loaded successfully from Excel.");
        }**/

        [HttpGet("staging-data")]
        public IActionResult GetStagingData()
        {
            return Ok(_stagingDataset);
        }

        //Code to GET or retrieve items from the dataset.
        /**[HttpGet]
        public IActionResult GetData()
        {
            return Ok(_stagingDataset);
        }**/

        //Code to GET or retrieve items from the dataset by ID.
        [HttpGet("{id}")] S
        public IActionResult GetById(int id)
        {
            var item = _stagingDataset.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound($"Item with ID {id} not found.");
            }
            return Ok(item);
        }

        //Code to create new entry in the Netflix dataset.
        [HttpPost]
        public IActionResult CreateItem([FromBody] NetflixData newItem)
        {
            if (_stagingDataset.Any(x => x.Id == newItem.Id))
            {
                return BadRequest("Item with the same ID already exists.");
            }

            _stagingDataset.Add(newItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        //Code to update an existing row in the Netflix dataset.
        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, [FromBody] NetflixData updatedItem)
        {
            var item = _stagingDataset.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound($"Item with ID {id} not found.");
            }

            // Update fields
            item.title = updatedItem.title;
            item.releaseyear = updatedItem.releaseyear;

            return NoContent();
        }

        //Code to remove/delete existing rows in the dataset.
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            var item = _stagingDataset.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound($"Item with ID {id} not found.");
            }

            _stagingDataset.Remove(item);
            return NoContent();
        }

        //Code to retrieve data from the raw excel file and insert into staging dataset.
        [HttpPost("fetch-dataset")]
        public IActionResult FetchDataset([FromQuery] string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
            {
                return BadRequest("Invalid file path provided.");
            }

            var loader = new DataLink();
            var data = loader.LoadDataFromExcel(filePath);
            _stagingDataset.Clear(); // Clear existing data
            _stagingDataset.AddRange(data);
            return Ok("Dataset fetched and loaded successfully.");
        }

        //Code to remove all duplicate records in the dataset.
        [HttpPost("deduplicate")]
        public IActionResult Deduplicate()
        {
            var distinctItems = _stagingDataset
                .GroupBy(x => new { x.title, x.rating, x.ratingLevel, x.ratingDescription, x.releaseyear, x.userratingscore, x.userratingsize })
                .Select(g => g.First())
                .ToList();

            _stagingDataset.Clear();
            _stagingDataset.AddRange(distinctItems);

            return Ok("Duplicates removed successfully.");
        }

    }

}
