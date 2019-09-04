using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Result
{
    public class ResultViewModel<TViewModel> : IResultFeedbackViewModel, IMultiTenancy, IResultBreadcrumbViewModel
    {
        public TViewModel Data { get; set; }
        public FeedbacksViewModel Feedbacks { get; set; }
        public Guid TenantId { get; set; }
        public IList<BreadcrumbViewModel> Breadcrumbs { get; set; } = new List<BreadcrumbViewModel>();
    }
}
