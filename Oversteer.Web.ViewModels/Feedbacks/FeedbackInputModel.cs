namespace Oversteer.Web.ViewModels.Feedbacks
{
    using System.ComponentModel.DataAnnotations;

    public class FeedbackInputModel
    {
        private const int minimumLenghtComment = 10;
        private const int maximumLenghtComment = 500;
        private const string minimumLenghtError = "Your comment must be minimum {1} symbols long.";
        private const string maximumLenghtError = "Your comment must be maximum {1} symbols long.";

        [Required]
        [MinLength(minimumLenghtComment, ErrorMessage = minimumLenghtError)]
        [MaxLength(maximumLenghtComment, ErrorMessage = maximumLenghtError)]
        public string Comment { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public string RentId { get; set; }
    }
}
