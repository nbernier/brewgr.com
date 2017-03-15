using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brewgr.Web.Core.Model
{
    public class UserSuggestion
    {
        public int UserSuggestionId { get; set; }

        public int UserId { get; set; }

        [MaxLength(500)]
        public string SuggestionText { get; set; }

        [MaxLength(50)]
        public string UserHostAddress { get; set; }

        public DateTime DateCreated { get; set; }
 }
}
