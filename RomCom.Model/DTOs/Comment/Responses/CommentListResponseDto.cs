using System.Collections.Generic;

namespace RomCom.Model.DTOs.Comment.Responses
{
    public class CommentListResponseDto
    {
        public required List<CommentDto> Comments { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
