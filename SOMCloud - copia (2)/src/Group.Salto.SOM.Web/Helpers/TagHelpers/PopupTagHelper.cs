using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Group.Salto.SOM.Web.Helpers.TagHelpers
{
    [HtmlTargetElement("Popup")]
    public class PopupTagHelper : TagHelper
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string CancelButtonText { get; set; }
        public string SubmitButtonText { get; set; }
        public bool ShowCancelButton { get; set; } = true;
        public bool IsImportantMessage { get; set; } = false;
        public string Title { get; set; } = "";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var visibleClass = ShowCancelButton ? string.Empty : " d-none";
            var isImportantClass = IsImportantMessage ? "modal-danger" : "";
            var titleContent = !string.IsNullOrEmpty(Title) ? 
                "<div class='modal-header'>" +
                    "<h4 class='modal-title'>"+Title+"</h4></div>" : "";
            output.TagName = "div";
            output.Content.SetHtmlContent(
                $@"<div class='modal fade {isImportantClass}' id='{Id}' tabindex='-1' style='display: none;'
                        role='dialog' aria-labelledby='myModalLabel' aria-hidden='true'>
                        <div class='modal-dialog' role='document'>
                            <div class='modal-content '>
                                    {titleContent}
                                <div id='textModalDialog' class='modal-body text-center'>
                                    <p>{Text}</p>
                                </div>
                                <div class='modal-footer'>
                                    <button id='{Id}ConfirmCancel' class='btn btn-outline-danger {visibleClass}' type='button' 
                                            data-dismiss='modal' >{CancelButtonText}</button>
                                    <button id='{Id}ConfirmSave' class='btn btn-primary' type='button'>{SubmitButtonText}</button>
                                </div>
                            </div>
                    </div>                  
                </div>");
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}