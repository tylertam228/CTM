namespace CTM.Models
{
    public class School
    {
        // Id: Key
        public int Id { get; set; }
        
        // SchoolNo
        public string SchoolNo { get; set; } = string.Empty;
        
        // SchoolNameEn
        public string SchoolNameEn { get; set; } = string.Empty;
        
        // SchoolName in Chinese
        public string SchoolName { get; set; } = string.Empty;
        
        // DistrictEn
        // Used for filtering searches
        public string DistrictEn { get; set; } = string.Empty;
        
        // District in chinese
        // Used for filtering searches
        public string District { get; set; } = string.Empty;
        
        // SchoolLevelEn
        // Used for filtering searches
        public string SchoolLevelEn { get; set; } = string.Empty;
        
        // SchoolLevel in Chinese
        // Used for filtering searches
        public string SchoolLevel { get; set; } = string.Empty;
        
        // DateCreated 
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}