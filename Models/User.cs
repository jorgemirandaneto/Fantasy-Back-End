﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FantasyServer.Models
{
    [Table("acessuser", Schema = "develop")]
    public class User
    {

        public int id { get; set; }

        public string nome { get; set; }

        public string senha { get; set; }

        public int fkparticipante { get; set; }
    }
}
