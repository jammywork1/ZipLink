using System;
using ServiceStack.DataAnnotations;

namespace ZipLink.DataAccessLayer.Entities
{
    public class ZippedLinkEntity
    {
        [PrimaryKey]
        [Required]
        [StringLength(255)]
        public string Hash {get;set;}
        
        [Required]
        public string OriginalLink {get;set;}
        
        [Required]
        public DateTime Created {get;set;}
        
        [Required]
        public int Followed {get;set;}
    }
}