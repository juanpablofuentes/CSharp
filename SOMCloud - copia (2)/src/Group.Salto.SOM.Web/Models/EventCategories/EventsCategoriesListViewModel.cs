using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.EventCategories
{
    public class EventsCategoriesListViewModel
    {
        public MultiItemViewModel<EventCategoriesListViewModel, int> EventCategories { get; set; }

        public EventCategoriesFilterViewModel EventCategoriesFilter { get; set; }
    }
}
