namespace PetSanctuary.Web.ViewModels.Api.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class StatisticsResponseModel
    {
        public int TotalDogs { get; set; }

        public int TotalCats { get; set; }

        public int TotalOthers { get; set; }
    }
}
