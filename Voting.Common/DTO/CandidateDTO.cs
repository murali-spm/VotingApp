﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voting.Common.DTO
{
    public class CandidateDTO
    {
        [Required]
        public string Name { get; set; }
        public int TotalVotes { get; set; }        
    }
}
