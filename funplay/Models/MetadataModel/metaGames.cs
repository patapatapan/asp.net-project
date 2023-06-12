using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace funplay.Models
{
    [MetadataType(typeof(z_metaGames))]
    public partial class Games
    {
        [NotMapped]
        [Display(Name = "圖片名稱")]
        public string ImageName { get; set; }
    }

    public abstract class z_metaGames
    {

    }
}