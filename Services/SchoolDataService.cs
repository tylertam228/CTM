using CTM.Data;
using CTM.Models;
using CTM.Models.DTOs;
using System.Text.Json;

namespace CTM.Services
{
    // This Service is used to fetch school data from EDB and seed into the database
    public class SchoolDataService
    {
        // _context: Tool for database operations
        private readonly ApplicationDbContext _context;
        
        // _httpClient: Tool for sending HTTP requests (fetching API data)
        private readonly HttpClient _httpClient;
        
        // _logger: Tool for logging (e.g., success, failure, error messages)
        private readonly ILogger<SchoolDataService> _logger;
        
        // EDB_JSON_URL
        private const string EDB_JSON_URL = "http://www.edb.gov.hk/attachment/en/student-parents/sch-info/sch-search/sch-location-info/SCH_LOC_EDB.json";

        public SchoolDataService(
            ApplicationDbContext context,
            HttpClient httpClient,
            ILogger<SchoolDataService> logger)
        {
            _context = context;
            _httpClient = httpClient;
            _logger = logger;
        }

        // FetchAndSeedSchoolsAsync: Main method responsible for fetching and seeding school data
        public async Task FetchAndSeedSchoolsAsync()
        {
            // Check if there are already school records in the database
            // If yes, skip importing to avoid wasting time and resources
            if (_context.Schools.Any())
            {
                _logger.LogInformation("Schools already exist, skipping import.");
                return;
            }

            try
            {
                _logger.LogInformation("Fetching school data from EDB...");
                
                // Use HttpClient to send a GET request to the EDB JSON URL
                var response = await _httpClient.GetStringAsync(EDB_JSON_URL);
                
                // Deserialize the JSON string into C# objects (List<EdbSchoolDto>)
                var schools = JsonSerializer.Deserialize<List<EdbSchoolDto>>(response);

                // Check if deserialization was successful and if there is any data
                if (schools == null || !schools.Any())
                {
                    _logger.LogWarning("No schools found in EDB data.");
                    return;
                }

                // Show how many school records were found
                _logger.LogInformation($"Found {schools.Count} schools, preparing to import...");

                // DTO to Entity Mapping
                var schoolEntities = schools
                    .Select(dto => new School
                    {
                        SchoolNo = dto.SchoolNo.ToString(),
                        
                        SchoolName = dto.Name,
                        
                        SchoolNameEn = dto.NameEn,
                        
                        District = dto.District,
                        
                        DistrictEn = dto.DistrictEn,
                        
                        SchoolLevel = dto.SchoolLevelZh,
                        
                        SchoolLevelEn = dto.SchoolLevel
                    })
                    .ToList(); // Turn into a List<School>

                // Add all school entities to the database (but not yet saved)
                _context.Schools.AddRange(schoolEntities);
                
                // Actually save the data to the database
                await _context.SaveChangesAsync();

                _logger.LogInformation("Successfully imported {Count} schools.", schoolEntities.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching school data from EDB");
                throw;
            }
        }
    }
}