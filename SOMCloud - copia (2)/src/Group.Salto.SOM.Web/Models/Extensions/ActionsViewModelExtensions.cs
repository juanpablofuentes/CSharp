using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Controls.Table.Models;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Actions;
using Group.Salto.SOM.Web.Models.Actions;
using Group.Salto.SOM.Web.Models.Result;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ActionsViewModelExtensions
    {
        public static ResultViewModel<ActionsViewModel> ToResultViewModel(this ResultDto<IList<ActionDto>> source,
            string title = null, string message = null)
        {
            return source.ToViewModel(c => new ActionsViewModel()
            {
                Actions = new MultiItemViewModel<ActionViewModel, int>(c.MapList(x => x.ToViewModel()))
            }, title, message);
        }
    }
}