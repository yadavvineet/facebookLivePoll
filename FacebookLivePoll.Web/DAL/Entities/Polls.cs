using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FacebookLivePoll.Web.DAL.Entities
{
    public class Polls
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(30)]
        [DataType(DataType.Text)]
        public string PollName { get; set; }

    }
}