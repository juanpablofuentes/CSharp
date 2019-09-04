using System;
using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.AssetStatuses;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Brand;
using Group.Salto.ServiceLibrary.Common.Dtos.ExpenseTicket;
using Group.Salto.ServiceLibrary.Common.Dtos.Families;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClients;
using Group.Salto.ServiceLibrary.Common.Dtos.Models;
using Group.Salto.ServiceLibrary.Common.Dtos.Sites;
using Group.Salto.ServiceLibrary.Common.Dtos.StatesSla;
using Group.Salto.ServiceLibrary.Common.Dtos.SubContract;
using Group.Salto.ServiceLibrary.Common.Dtos.SubFamilies;
using Group.Salto.ServiceLibrary.Common.Dtos.Team;
using Group.Salto.ServiceLibrary.Common.Dtos.Tools;
using Group.Salto.ServiceLibrary.Common.Dtos.ToolsType;
using Group.Salto.SOM.Web.Models.Brands;
using Group.Salto.SOM.Web.Models.MultiCombo;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class MultiComboViewModelExtensions
    {
        public static MultiComboViewModel<int,int> ToMultiComboViewModel(this SubContractKnowledgeDto source)
        {
            MultiComboViewModel<int, int> result = null;
            if (source != null)
            {
                result = new MultiComboViewModel<int, int>()
                {
                    Value = source.Id,
                    ValueSecondary = source.Priority,
                    TextSecondary = source.Priority.ToString("00"),
                    Text = source.Name,
                };
            }
            return result;
        }

        public static IList<MultiComboViewModel<int, int>> ToMultiComboViewModel(this IList<ToolsTypeKnowledgeDto> source)
        {
            return source?.MapList(sCK => sCK.ToMultiComboViewModel());
        }

        public static MultiComboViewModel<int, int> ToMultiComboViewModel(this ToolsTypeKnowledgeDto source)
        {
            MultiComboViewModel<int, int> result = null;
            if (source != null)
            {
                result = new MultiComboViewModel<int, int>()
                {
                    Value = source.Id,
                    Text = source.Name,
                };
            }
            return result;
        }

        public static IList<MultiComboViewModel<int, int>> ToMultiComboViewModel(this IList<SubContractKnowledgeDto> source)
        {
            return source?.MapList(sCK => sCK.ToMultiComboViewModel());
        }

        public static MultiComboViewModel<int, int> ToMultiComboViewModel(this BaseNameIdDto<int> source)
        {
            MultiComboViewModel<int, int> result = null;
            if (source != null)
            {
                result = new MultiComboViewModel<int, int>()
                {
                    Value = source.Id,
                    Text = source.Name,
                };
            }
            return result;
        }

        public static IList<MultiComboViewModel<int, int>> ToMultiComboViewModel(this IList<BaseNameIdDto<int>> source)
        {
            return source?.MapList(sCK => sCK.ToMultiComboViewModel());
        }
        public static MultiComboViewModel<int, int> ToMultiComboViewModelSla(this StatesSlaDto source)
        {
            MultiComboViewModel<int, int> result = null;
            if (source != null)
            {
                result = new MultiComboViewModel<int, int>()
                {
                    Value = source.Id,
                    Text = source.RowColor,
                    ValueSecondary = source.SlaId,
                    TextSecondary = source.MinutesToTheEnd.ToString()
                };
            }
            return result;
        }

        public static IList<MultiComboViewModel<int, int>> ToMultiComboViewModelSla(this IList<StatesSlaDto> source)
        {
            return source?.MapList(sCK => sCK.ToMultiComboViewModelSla());
        }

        public static SubContractKnowledgeDto ToSubContractKnowledgeDto(this MultiComboViewModel<int, int> source)
        {
            SubContractKnowledgeDto result = null;
            if (source != null)
            {
                result = new SubContractKnowledgeDto()
                {
                    Id = source.Value,
                    Name = source.Text,
                    Priority = source.ValueSecondary
                };
            }
            return result;
        }

        public static IList<SubContractKnowledgeDto> ToSubContractKnowledgeDto(this IList<MultiComboViewModel<int, int>> source)
        {
            return source?.MapList(sCK => sCK.ToSubContractKnowledgeDto());
        }

        public static ToolsTypeKnowledgeDto ToToolsTypeKnowledgeDto(this MultiComboViewModel<int, int> source)
        {
            ToolsTypeKnowledgeDto result = null;
            if (source != null)
            {
                result = new ToolsTypeKnowledgeDto()
                {
                    Id = source.Value,
                    Name = source.Text,
                };
            }
            return result;
        }

        public static IList<ToolsTypeKnowledgeDto> ToToolsTypeKnowledgeDto(this IList<MultiComboViewModel<int, int>> source)
        {
            return source?.MapList(sCK => sCK.ToToolsTypeKnowledgeDto());
        }

        public static BaseNameIdDto<int> ToToolsToolTypeDto(this MultiComboViewModel<int, int> source)
        {
            BaseNameIdDto<int> result = null;
            if (source != null)
            {
                result = new BaseNameIdDto<int>()
                {
                    Id = source.Value,
                    Name = source.Text,
                };
            }
            return result;
        }

        public static IList<BaseNameIdDto<int>> ToToolsToolTypeDto(this IList<MultiComboViewModel<int, int>> source)
        {
            return source?.MapList(sCK => sCK.ToToolsToolTypeDto());
        }
        public static StatesSlaDto ToStatesSlaDto(this MultiComboViewModel<int, int> source)
        {
            StatesSlaDto result = null;
            if (source != null)
            {
                result = new StatesSlaDto()
                {
                    SlaId = source.Value,
                    Id = source.ValueSecondary,
                    RowColor = source.Text,
                    MinutesToTheEnd = Int32.Parse(source.TextSecondary)
                };
            }
            return result;
        }

        public static IList<int> ToPeopleExpenseTicketDto(this IList<MultiComboViewModel<int, string>> source)
        {
            return source?.MapList(sCK => sCK.ToPeopleExpenseTicketDto());
        }

        public static int ToPeopleExpenseTicketDto(this MultiComboViewModel<int, string> source)
        {
            int result = 0;
            if (source != null)
            {
                result = source.Value;
            }
            return result;
        }

        public static IList<Guid?> ToStatesExpenseTicketDto(this IList<MultiComboViewModel<Guid?, string>> source)
        {
            return source?.MapList(sCK => sCK.ToStatesExpenseTicketDto());
        }

        public static Guid? ToStatesExpenseTicketDto(this MultiComboViewModel<Guid?, string> source)
        {
            Guid? result = new Guid();
            if (source != null && source.Value!=null)
            {
                result = source.Value;
            }
            return result;
        }

        public static IList<StatesSlaDto> ToStatesSlaDto(this IList<MultiComboViewModel<int, int>> source)
        {
            return source?.MapList(sCK => sCK.ToStatesSlaDto());
        }

        public static AssetStatusesDto ToAssetStatusesDto(this MultiComboViewModel<int, int> source)
        {
            AssetStatusesDto result = null;
            if (source != null)
            {
                result = new AssetStatusesDto()
                {
                    Id = source.Value,
                    Name = source.Text,
                };
            }
            return result;
        }

        public static IList<AssetStatusesDto> ToAssetStatusesDto(this IList<MultiComboViewModel<int, int>> source)
        {
            return source?.MapList(sCK => sCK.ToAssetStatusesDto());
        }

        public static ModelsDto ToModelsDto(this MultiComboViewModel<int, int> source)
        {
            ModelsDto result = null;
            if (source != null)
            {
                result = new ModelsDto()
                {
                    Id = source.Value,
                    Name = source.Text,
                };
            }
            return result;
        }

        public static IList<ModelsDto> ToModelsDto(this IList<MultiComboViewModel<int, int>> source)
        {
            return source?.MapList(sCK => sCK.ToModelsDto());
        }

        public static BrandsDto ToBrandsDto(this MultiComboViewModel<int, int> source)
        {
            BrandsDto result = null;
            if (source != null)
            {
                result = new BrandsDto()
                {
                    Id = source.Value,
                    Name = source.Text,
                };
            }
            return result;
        }

        public static IList<BrandsDto> ToBrandsDto(this IList<MultiComboViewModel<int, int>> source)
        {
            return source?.MapList(sCK => sCK.ToBrandsDto());
        }

        public static FamiliesDto ToFamiliesDto(this MultiComboViewModel<int, int> source)
        {
            FamiliesDto result = null;
            if (source != null)
            {
                result = new FamiliesDto()
                {
                    Id = source.Value,
                    Name = source.Text,
                };
            }
            return result;
        }

        public static IList<FamiliesDto> ToFamiliesDto(this IList<MultiComboViewModel<int, int>> source)
        {
            return source?.MapList(sCK => sCK.ToFamiliesDto());
        }

        public static SubFamiliesDto ToSubFamiliesDto(this MultiComboViewModel<int, int> source)
        {
            SubFamiliesDto result = null;
            if (source != null)
            {
                result = new SubFamiliesDto()
                {
                    Id = source.Value,
                    Nom = source.Text,
                };
            }
            return result;
        }

        public static IList<SubFamiliesDto> ToSubFamiliesDto(this IList<MultiComboViewModel<int, int>> source)
        {
            return source?.MapList(sCK => sCK.ToSubFamiliesDto());
        }

        public static SitesDto ToSitesDto(this MultiComboViewModel<int, int> source)
        {
            SitesDto result = null;
            if (source != null)
            {
                result = new SitesDto()
                {
                    Id = source.Value,
                    Name = source.Text,
                };
            }
            return result;
        }

        public static IList<SitesDto> ToSitesDto(this IList<MultiComboViewModel<int, int>> source)
        {
            return source?.MapList(sCK => sCK.ToSitesDto());
        }

        public static FinalClientsDto ToFinalClientsDto(this MultiComboViewModel<int, int> source)
        {
            FinalClientsDto result = null;
            if (source != null)
            {
                result = new FinalClientsDto()
                {
                    Id = source.Value,
                    Name = source.Text,
                };
            }
            return result;
        }

        public static IList<FinalClientsDto> ToFinalClientsDto(this IList<MultiComboViewModel<int, int>> source)
        {
            return source?.MapList(sCK => sCK.ToFinalClientsDto());
        }
    }
}