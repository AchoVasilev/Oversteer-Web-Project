namespace Oversteer.Web.ViewModels.Companies
{
    using System.Collections.Generic;

    using Oversteer.Web.ViewModels.Cars;
    using Oversteer.Web.ViewModels.Common;

    public class CompanyCar : PagingViewModel
    {
        public int Id { get; set; }

        public IEnumerable<ListCarFormModel> Cars { get; init; }

        public int CompanyId { get; init; }
    }
}
